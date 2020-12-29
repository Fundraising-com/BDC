USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_gross_overview_without_partner]    Script Date: 02/14/2014 13:08:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- sp_ code
CREATE   PROCEDURE [dbo].[sp_gross_overview_without_partner] (@date_from VARCHAR(20) = '', @date_to VARCHAR(20) = '') AS

DECLARE @Partner_ID INT,
        @Promotion_ID INT,
        @Description VARCHAR(50),
        @count_leads INT,
        @count_sales INT,
        @brochure DECIMAL(38,4),
        @candies DECIMAL(38,4),
        @scratchcard DECIMAL(38,4)

CREATE TABLE #tbout_union(Partner_ID INT,
        Promotion_ID INT,
        Description VARCHAR(50),
        CountOfLeadID INT DEFAULT 0,
        CountOfSalesID INT DEFAULT 0,
        brochure DECIMAL(38,4) DEFAULT 0,
        candies DECIMAL(38,4) DEFAULT 0,
        scratchcard DECIMAL(38,4) DEFAULT 0);
        

-- obtension des ventes net pour les leads du mois
SELECT Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS brochure,
       SUM(CASE WHEN Product_Class.Description LIKE 'WFC%' OR Product_Class.Description LIKE 'HERSHEY%' THEN Sales_Item.Sales_Amount ELSE 0 END) AS candies,
       SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS scratchcard
  INTO #tb1
  FROM Promotion
       JOIN Lead ON Lead.Promotion_ID = Promotion.Promotion_ID
       LEFT JOIN Client ON Client.Lead_ID = Lead.Lead_ID
       JOIN Sale ON Sale.Client_ID = Client.Client_ID and Sale.Client_Sequence_Code = Client.Client_Sequence_Code
       JOIN Sales_Item ON Sales_Item.Sales_ID = Sale.Sales_ID
       JOIN Scratch_Book ON Scratch_Book.Scratch_Book_ID = Sales_Item.Scratch_Book_ID
       JOIN Product_Class ON Product_Class.Product_Class_ID = Scratch_Book.Product_Class_ID
 WHERE Sale.Actual_ship_date IS NOT NULL
   AND Lead.Lead_entry_Date BETWEEN @date_from AND @date_to
 GROUP BY Promotion.Partner_ID,
          Promotion.Promotion_ID,
          Promotion.Description

-- Insersion des net sales dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Partner_id, Promotion_id, Description, 
	brochure, candies, scratchcard
	FROM #tb1)
OPEN c1

FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @brochure, @candies, @scratchcard
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_union(Partner_ID, Promotion_ID, Description, brochure, candies, scratchcard) 
	VALUES(@Partner_ID, @Promotion_ID, @Description, @brochure, @candies, @scratchcard)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @brochure, @candies, @scratchcard
END
CLOSE c1
DEALLOCATE c1

-- Obtention du nombre de leads selon leur date d'entrer
SELECT Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       lead.Lead_id
      INTO #tb_leads
      FROM Lead, Promotion
      WHERE Lead.Promotion_ID = Promotion.Promotion_ID
        AND Lead.Lead_Entry_Date BETWEEN @date_from AND @date_to 

-- count de sales selon le lead entry date
SELECT  Promotion.Partner_ID,
        Promotion.Promotion_ID,
        Promotion.Description,
	Sale.Sales_id
      INTO #tb_sales
      FROM Sale, Client, Lead, Promotion
      WHERE Sale.Client_ID = Client.Client_ID
       AND Sale.Client_Sequence_Code = Client.Client_Sequence_Code
       AND Client.Lead_ID = Lead.Lead_ID
       AND Lead.Promotion_ID = Promotion.Promotion_ID
       AND Lead.Lead_Entry_Date BETWEEN @date_from AND @date_to
       AND Sale.Actual_ship_date IS NOT NULL
   

SELECT Partner_id, Promotion_id, Description, 
	count(lead_id) as count_leads
	into #tb_leads2
	FROM #tb_leads GROUP BY Partner_id, Promotion_id, Description;

-- Insersion des shipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Partner_id, Promotion_id, Description, count_leads
	FROM #tb_leads2)
OPEN c1

FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @count_leads
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_union(Partner_ID, Promotion_ID, Description, CountOfLeadID) 
	VALUES(@Partner_ID, @Promotion_ID, @Description, @count_leads)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @count_leads
END
CLOSE c1
DEALLOCATE c1

SELECT Partner_id, Promotion_id, Description, 
	count(sales_id) as count_sales
	INTO #tb_sales2
	FROM #tb_sales  GROUP BY Partner_id, Promotion_id, Description;

-- Insersion des shipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Partner_id, Promotion_id, Description, 
	count_sales FROM #tb_sales2)
OPEN c1

FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @count_sales
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_union(Partner_ID, Promotion_ID, Description, CountOfSalesID) 
	VALUES(@Partner_ID, @Promotion_ID, @Description, @count_sales)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @count_sales
END
CLOSE c1
DEALLOCATE c1

SELECT partner_id, promotion_id, description, sum(countofleadid) as countofleadid, 
	sum(countofsalesid) as countofsalesid,  sum(brochure) as brochure, 
	sum(candies) as candies, sum(scratchcard) as scratchcard
	INTO #tbout_leads
	FROM #tbout_union 
	GROUP BY partner_id, promotion_id, description;

SELECT * FROM #tbout_leads
GO

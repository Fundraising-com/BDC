USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_net_overview_with_partner]    Script Date: 02/14/2014 13:09:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_net_overview_with_partner] (@date_from VARCHAR(20) = '', @date_to VARCHAR(20) = '', @Partner_ID INT = 0) AS

CREATE TABLE #tbout(Partner_ID INT,
        Promotion_ID INT,
        Description VARCHAR(50),
        CountOfLeadID INT,
        CountOfSalesID INT,
        net_brochure DECIMAL(38,4),
        net_candies DECIMAL(38,4),
        net_scratchcard DECIMAL(38,4));

CREATE TABLE #tbout_net_union(Partner_ID INT,
        Promotion_ID INT,
        Description VARCHAR(50),
        CountOfLeadID INT DEFAULT 0,
        CountOfSalesID INT DEFAULT 0,
        shipped_brochure DECIMAL(38,4) DEFAULT 0,
        returned_brochure DECIMAL(38,4) DEFAULT 0,
        reshipped_brochure DECIMAL(38,4) DEFAULT 0,
        shipped_candies DECIMAL(38,4) DEFAULT 0,
        returned_candies DECIMAL(38,4) DEFAULT 0,
        reshipped_candies DECIMAL(38,4) DEFAULT 0,
        shipped_scratchcard DECIMAL(38,4) DEFAULT 0,
        returned_scratchcard DECIMAL(38,4) DEFAULT 0,
        reshipped_scratchcard DECIMAL(38,4) DEFAULT 0);

--DECLARE @Partner_ID INT,
DECLARE @Promotion_ID INT,
        @Description VARCHAR(50),
        @count_leads INT,
        @count_sales INT,
        @net_brochure DECIMAL(38,4),
        @net_candies DECIMAL(38,4),
        @net_scratchcard DECIMAL(38,4),
        @shipped_brochure DECIMAL(38,4),
        @shipped_candies DECIMAL(38,4),
        @shipped_scratchcard DECIMAL(38,4),
        @returned_brochure DECIMAL(38,4),
        @returned_candies DECIMAL(38,4),
        @returned_scratchcard DECIMAL(38,4),
        @reshipped_brochure DECIMAL(38,4),
        @reshipped_candies DECIMAL(38,4),
        @reshipped_scratchcard DECIMAL(38,4);

-- sales shipped
SELECT Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS shipped_brochure,
       SUM(CASE WHEN Product_Class.Description LIKE 'WFC%' OR Product_Class.Description LIKE 'HERSHEY%' THEN Sales_Item.Sales_Amount ELSE 0 END) AS shipped_candies,
       SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS shipped_scratchcard
  INTO #tb1_shipped
  FROM Promotion
       JOIN Lead ON Lead.Promotion_ID = Promotion.Promotion_ID
       LEFT JOIN Client ON Client.Lead_ID = Lead.Lead_ID
       JOIN Sale ON Sale.Client_ID = Client.Client_ID and Sale.Client_Sequence_Code = Client.Client_Sequence_Code
       JOIN Sales_Item ON Sales_Item.Sales_ID = Sale.Sales_ID
       JOIN Scratch_Book ON Scratch_Book.Scratch_Book_ID = Sales_Item.Scratch_Book_ID
       JOIN Product_Class ON Product_Class.Product_Class_ID = Scratch_Book.Product_Class_ID
 WHERE Promotion.Partner_ID = @Partner_ID
   AND Sale.Actual_Ship_Date BETWEEN @date_from AND @date_to
 GROUP BY Promotion.Partner_ID,
          Promotion.Promotion_ID,
          Promotion.Description

-- sales box retunred
SELECT Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS returned_brochure,
       SUM(CASE WHEN Product_Class.Description LIKE 'WFC%' OR Product_Class.Description LIKE 'HERSHEY%' THEN Sales_Item.Sales_Amount ELSE 0 END) AS returned_candies,
       SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS returned_scratchcard
  INTO #tb1_returned
  FROM Promotion
       JOIN Lead ON Lead.Promotion_ID = Promotion.Promotion_ID
       LEFT JOIN Client ON Client.Lead_ID = Lead.Lead_ID
       JOIN Sale ON Sale.Client_ID = Client.Client_ID and Sale.Client_Sequence_Code = Client.Client_Sequence_Code
       JOIN Sales_Item ON Sales_Item.Sales_ID = Sale.Sales_ID
       JOIN Scratch_Book ON Scratch_Book.Scratch_Book_ID = Sales_Item.Scratch_Book_ID
       JOIN Product_Class ON Product_Class.Product_Class_ID = Scratch_Book.Product_Class_ID
 WHERE Promotion.Partner_ID = @Partner_ID
   AND Sale.Box_Return_Date BETWEEN @date_from and @date_to
 GROUP BY Promotion.Partner_ID,
          Promotion.Promotion_ID,
          Promotion.Description

-- sales box reshipped
SELECT Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS reshipped_brochure,
       SUM(CASE WHEN Product_Class.Description LIKE 'WFC%' OR Product_Class.Description LIKE 'HERSHEY%' THEN Sales_Item.Sales_Amount ELSE 0 END) AS reshipped_candies,
       SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS reshipped_scratchcard
  INTO #tb1_reshipped
  FROM Promotion
       JOIN Lead ON Lead.Promotion_ID = Promotion.Promotion_ID
       LEFT JOIN Client ON Client.Lead_ID = Lead.Lead_ID
       JOIN Sale ON Sale.Client_ID = Client.Client_ID and Sale.Client_Sequence_Code = Client.Client_Sequence_Code
       JOIN Sales_Item ON Sales_Item.Sales_ID = Sale.Sales_ID
       JOIN Scratch_Book ON Scratch_Book.Scratch_Book_ID = Sales_Item.Scratch_Book_ID
       JOIN Product_Class ON Product_Class.Product_Class_ID = Scratch_Book.Product_Class_ID
 WHERE Promotion.Partner_ID = @Partner_ID
   AND Sale.Reship_Date BETWEEN @date_from and @date_to
 GROUP BY Promotion.Partner_ID,
          Promotion.Promotion_ID,
          Promotion.Description


-- Insersion des shipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Partner_id, Promotion_id, Description, 
	shipped_brochure, shipped_candies, shipped_scratchcard
	FROM #tb1_shipped)
OPEN c1

FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @shipped_brochure, @shipped_candies, @shipped_scratchcard
WHILE @@FETCH_STATUS = 0
BEGIN

    INSERT INTO #tbout_net_union(Partner_ID, Promotion_ID, Description, shipped_brochure, shipped_candies, shipped_scratchcard) 
	VALUES(@Partner_ID, @Promotion_ID, @Description, @shipped_brochure, @shipped_candies, @shipped_scratchcard)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @shipped_brochure, @shipped_candies, @shipped_scratchcard
END
CLOSE c1
DEALLOCATE c1

-- Insersion des returned dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Partner_id, Promotion_id, Description, 
	returned_brochure, returned_candies, returned_scratchcard
	FROM #tb1_returned)
OPEN c1

FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @returned_brochure, @returned_candies, @returned_scratchcard
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_net_union(Partner_ID, Promotion_ID, Description, returned_brochure, returned_candies, returned_scratchcard) 
	VALUES(@Partner_ID, @Promotion_ID, @Description, @returned_brochure, @returned_candies, @returned_scratchcard)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @returned_brochure, @returned_candies, @returned_scratchcard
END
CLOSE c1
DEALLOCATE c1

-- Insersion des reshipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Partner_id, Promotion_id, Description, 
	reshipped_brochure, reshipped_candies, reshipped_scratchcard
	FROM #tb1_reshipped)
OPEN c1

FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @reshipped_brochure, @reshipped_candies, @reshipped_scratchcard
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_net_union(Partner_ID, Promotion_ID, Description, reshipped_brochure, reshipped_candies, reshipped_scratchcard) 
	VALUES(@partner_id, @Promotion_ID, @Description, @reshipped_brochure, @reshipped_candies, @reshipped_scratchcard)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @reshipped_brochure, @reshipped_candies, @reshipped_scratchcard
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
        AND Promotion.Partner_ID = @Partner_ID

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
       AND Sale.Actual_Ship_Date BETWEEN @date_from AND @date_to
       AND Promotion.Partner_ID = @Partner_ID
       AND Sale.Actual_Ship_Date IS NOT NULL
       AND ((Sale.Reship_Date IS NULL AND Sale.Box_Return_Date IS NULL)
           OR
           (Sale.Reship_Date IS NOT NULL AND Sale.Box_Return_Date IS NOT NULL))

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
	
    INSERT INTO #tbout_net_union(Partner_ID, Promotion_ID, Description, CountOfLeadID) 
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
	
    INSERT INTO #tbout_net_union(Partner_ID, Promotion_ID, Description, CountOfSalesID) 
	VALUES(@Partner_ID, @Promotion_ID, @Description, @count_sales)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @count_sales
END
CLOSE c1
DEALLOCATE c1


-- regroupement des amounts par promotions
SELECT Partner_id, Promotion_id, Description, 
	Sum(countofleadid) as countofleadid, Sum(countofsalesid) as countofsalesid,
	Sum(shipped_brochure) as shipped_brochure, Sum(returned_brochure) as returned_brochure, Sum(reshipped_brochure) as reshipped_brochure,
	Sum(shipped_candies) as shipped_candies, Sum(returned_candies) as returned_candies, Sum(reshipped_candies) as reshipped_candies, 
	sum(shipped_scratchcard) as shipped_scratchcard, Sum(returned_scratchcard) as returned_scratchcard, Sum(reshipped_scratchcard) as reshipped_scratchcard 
	INTO #tb2
	FROM #tbout_net_union 
	GROUP BY partner_id, promotion_id, Description;

-- calculs des net amounts
SELECT partner_id, promotion_id, description, 
	countofleadid, countofsalesid,
	(shipped_brochure - returned_brochure + reshipped_brochure) as net_brochure, 
	(shipped_candies - returned_candies + reshipped_brochure) as net_candies, 
	(shipped_scratchcard - returned_scratchcard + reshipped_scratchcard) as net_scratchcard
	INTO #tb1
	FROM #tb2;


-- valeur de retour
SELECT * FROM #tb1
GO

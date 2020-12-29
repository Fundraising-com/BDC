USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_net_overview_by_consultants]    Script Date: 02/14/2014 13:09:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE          PROCEDURE [dbo].[sp_net_overview_by_consultants] (@date_from VARCHAR(20) = '', @date_to VARCHAR(20) = '') AS

--DECLARE @date_from VARCHAR(20);
--DECLARE @date_to VARCHAR(20);

--SET @date_from = '07/01/2002'; 
--SET @date_to = '06/30/2003 23:59:59';

CREATE TABLE #tbout(
	Consultant_id INT, 
	Partner_ID INT,
        Promotion_ID INT,
        Description VARCHAR(50),
        CountOfLeadID INT,
        CountOfSalesID INT,
        net_brochure DECIMAL(38,4),
        net_candies DECIMAL(38,4),
        net_scratchcard DECIMAL(38,4));

CREATE TABLE #tbout_net_union(
	Consultant_id INT, 
	Partner_ID INT,
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

DECLARE 
	@Consultant_id INT, 
	@Partner_ID INT,
	@Promotion_ID INT,
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
SELECT 
       Sale.Consultant_ID,
       Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS shipped_brochure,
       SUM(CASE WHEN Product_Class.Description LIKE 'WFC%' OR Product_Class.Description LIKE 'HERSHEY%' THEN Sales_Item.Sales_Amount ELSE 0 END) AS shipped_candies,
       SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS shipped_scratchcard
  	INTO #tb1_shipped
	FROM         dbo.Promotion INNER JOIN
                      dbo.Lead ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID LEFT OUTER JOIN
                      dbo.Client ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Sale ON dbo.Sale.Client_ID = dbo.Client.Client_ID AND dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code INNER JOIN
                      dbo.Sales_Item ON dbo.Sales_Item.Sales_ID = dbo.Sale.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Scratch_Book.Scratch_Book_ID = dbo.Sales_Item.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Product_Class.Product_Class_ID = dbo.Scratch_Book.Product_Class_ID INNER JOIN
			dbo.Consultant ON Sale.Consultant_ID = Consultant.Consultant_ID
 	WHERE Sale.Actual_Ship_Date BETWEEN @date_from AND @date_to
		AND Consultant.Is_Active <> 0
		AND Consultant.Department_ID = 7
		AND Consultant.Is_Agent = 0
		AND Consultant.Is_FM = 0
 	GROUP BY Promotion.Partner_ID,
		Sale.Consultant_ID, Promotion.Promotion_ID, Promotion.Description

PRINT 'Shipped sales inserted'

-- sales box retunred
SELECT 
       Sale.Consultant_ID,
       Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS returned_brochure,
       SUM(CASE WHEN Product_Class.Description LIKE 'WFC%' OR Product_Class.Description LIKE 'HERSHEY%' THEN Sales_Item.Sales_Amount ELSE 0 END) AS returned_candies,
       SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS returned_scratchcard
  	INTO #tb1_returned
	FROM         dbo.Promotion INNER JOIN
                      dbo.Lead ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID LEFT OUTER JOIN
                      dbo.Client ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Sale ON dbo.Sale.Client_ID = dbo.Client.Client_ID AND dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code INNER JOIN
                      dbo.Sales_Item ON dbo.Sales_Item.Sales_ID = dbo.Sale.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Scratch_Book.Scratch_Book_ID = dbo.Sales_Item.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Product_Class.Product_Class_ID = dbo.Scratch_Book.Product_Class_ID INNER JOIN
			dbo.Consultant ON Sale.Consultant_ID = Consultant.Consultant_ID
	WHERE Sale.Box_Return_Date BETWEEN @date_from and @date_to
		AND Consultant.Is_Active <> 0
		AND Consultant.Department_ID = 7
		AND Consultant.Is_Agent = 0
		AND Consultant.Is_FM = 0
 	GROUP BY Promotion.Partner_ID,
	       Sale.Consultant_ID, Promotion.Promotion_ID, Promotion.Description


PRINT 'Sales returned table filled'

-- sales box reshipped
SELECT 
       Sale.Consultant_ID,
       Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS reshipped_brochure,
       SUM(CASE WHEN Product_Class.Description LIKE 'WFC%' OR Product_Class.Description LIKE 'HERSHEY%' THEN Sales_Item.Sales_Amount ELSE 0 END) AS reshipped_candies,
       SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS reshipped_scratchcard
  	INTO #tb1_reshipped
	FROM         dbo.Promotion INNER JOIN
                      dbo.Lead ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID LEFT OUTER JOIN
                      dbo.Client ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Sale ON dbo.Sale.Client_ID = dbo.Client.Client_ID AND dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code INNER JOIN
                      dbo.Sales_Item ON dbo.Sales_Item.Sales_ID = dbo.Sale.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Scratch_Book.Scratch_Book_ID = dbo.Sales_Item.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Product_Class.Product_Class_ID = dbo.Scratch_Book.Product_Class_ID 
			INNER JOIN dbo.Consultant ON Consultant.Consultant_ID = Sale.Consultant_ID
 	WHERE Sale.Reship_Date BETWEEN @date_from and @date_to
		AND Consultant.IS_Active <> 0
		AND Consultant.Department_ID = 7
		AND Consultant.Is_Agent = 0
		AND Consultant.Is_FM = 0
 	GROUP BY Promotion.Partner_ID,
	       Sale.Consultant_ID, Promotion.Promotion_ID, Promotion.Description


PRINT 'Sales reshipped table filled'


-- Insersion des shipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Consultant_ID, Partner_id, Promotion_id, Description, 
	shipped_brochure, shipped_candies, shipped_scratchcard
	FROM #tb1_shipped)
OPEN c1

FETCH NEXT FROM c1 INTO @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @shipped_brochure, @shipped_candies, @shipped_scratchcard
WHILE @@FETCH_STATUS = 0
BEGIN
	SET NOCOUNT ON
    INSERT INTO #tbout_net_union( Consultant_ID, Partner_ID, Promotion_ID, Description, shipped_brochure, shipped_candies, shipped_scratchcard) 
	VALUES(@Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @shipped_brochure, @shipped_candies, @shipped_scratchcard)

    FETCH NEXT FROM c1 INTO @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @shipped_brochure, @shipped_candies, @shipped_scratchcard
END
CLOSE c1
DEALLOCATE c1


PRINT 'Shipped sales inserted'

-- Insersion des returned dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Consultant_ID, Partner_id, Promotion_id, Description, 
	returned_brochure, returned_candies, returned_scratchcard
	FROM #tb1_returned)
OPEN c1

FETCH NEXT FROM c1 INTO @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @returned_brochure, @returned_candies, @returned_scratchcard
WHILE @@FETCH_STATUS = 0
BEGIN
	SET NOCOUNT ON
    INSERT INTO #tbout_net_union( Consultant_ID, Partner_ID, Promotion_ID, Description, returned_brochure, returned_candies, returned_scratchcard) 
	VALUES( @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @returned_brochure, @returned_candies, @returned_scratchcard)

    FETCH NEXT FROM c1 INTO  @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @returned_brochure, @returned_candies, @returned_scratchcard
END
CLOSE c1
DEALLOCATE c1


PRINT 'Returned sales inserted'

-- Insersion des reshipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Consultant_ID, Partner_id, Promotion_id, Description, 
	reshipped_brochure, reshipped_candies, reshipped_scratchcard
	FROM #tb1_reshipped)
OPEN c1

FETCH NEXT FROM c1 INTO  @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @reshipped_brochure, @reshipped_candies, @reshipped_scratchcard
WHILE @@FETCH_STATUS = 0
BEGIN
	SET NOCOUNT ON
    INSERT INTO #tbout_net_union( Consultant_ID, Partner_ID, Promotion_ID, Description, reshipped_brochure, reshipped_candies, reshipped_scratchcard) 
	VALUES( @Consultant_ID, @partner_id, @Promotion_ID, @Description, @reshipped_brochure, @reshipped_candies, @reshipped_scratchcard)

    FETCH NEXT FROM c1 INTO @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @reshipped_brochure, @reshipped_candies, @reshipped_scratchcard
END
CLOSE c1
DEALLOCATE c1

PRINT 'Reshipped sales inserted'

-- Obtention du nombre de leads selon leur date d'entrer

SELECT     dbo.Lead.Consultant_ID, dbo.Promotion.Partner_ID, dbo.Promotion.Promotion_ID, dbo.Promotion.Description, dbo.Lead.Lead_ID
INTO #tb_leads
FROM         dbo.Lead INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID
	INNER JOIN dbo.Consultant ON Consultant.Consultant_ID = Lead.Consultant_ID
WHERE     (dbo.Lead.Lead_Entry_Date BETWEEN @date_from AND @date_to)
	AND Consultant.Is_Active <> 0
	AND Consultant.Department_ID = 7
	AND Consultant.Is_Agent = 0
	AND Consultant.Is_FM = 0

	



/*SELECT 
       Lead.Consultant_ID,
       Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       lead.Lead_id
      INTO #tb_leads
      FROM Lead, Promotion, Consultant
      WHERE 
	Lead.Promotion_ID = Promotion.Promotion_ID AND
        Lead.Lead_Entry_Date BETWEEN @date_from AND @date_to */

PRINT 'Lead numbers entered'
  

-- count de sales selon le lead entry date
/*SELECT  
        Sale.Consultant_ID,
        Promotion.Partner_ID,
        Promotion.Promotion_ID,
        Promotion.Description,
	Sale.Sales_id
      INTO #tb_sales
      FROM Sale, Client, Lead, Promotion, Consultant
      WHERE 
       Sale.Client_ID = Client.Client_ID
       AND Sale.Client_Sequence_Code = Client.Client_Sequence_Code
       AND Client.Lead_ID = Lead.Lead_ID
       AND Lead.Promotion_ID = Promotion.Promotion_ID
       AND Sale.Actual_Ship_Date BETWEEN @date_from AND @date_to
       AND Sale.Actual_Ship_Date IS NOT NULL
       AND ((Sale.Reship_Date IS NULL AND Sale.Box_Return_Date IS NULL)
           OR
           (Sale.Reship_Date IS NOT NULL AND Sale.Box_Return_Date IS NOT NULL))*/

SELECT     dbo.Promotion.Partner_ID, dbo.Promotion.Promotion_ID, dbo.Promotion.Description, dbo.Sale.Sales_ID, dbo.Sale.Consultant_ID
INTO #tb_sales
FROM         dbo.Sale INNER JOIN
                      dbo.Client ON dbo.Sale.Client_ID = dbo.Client.Client_ID AND dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code INNER JOIN
                      dbo.Lead ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID
			INNER JOIN dbo.Consultant ON Consultant.Consultant_ID = Sale.Consultant_ID
WHERE     (dbo.Sale.Actual_Ship_Date BETWEEN @date_from AND @date_to) AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND 
                      (dbo.Sale.Reship_Date IS NULL) AND (dbo.Sale.Box_Return_Date IS NULL) OR
                      (dbo.Sale.Actual_Ship_Date BETWEEN @date_from AND @date_to) AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND 
                      (dbo.Sale.Reship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NOT NULL)
			AND (Consultant.Is_Active <> 0)
			AND (Consultant.Department_ID = 7)
			AND (Consultant.Is_Agent = 0)
			AND (Consultant.Is_FM = 0)

PRINT 'Sales numbers entered'

SELECT Consultant_ID, Partner_id, Promotion_id, Description, 
	count(lead_id) as count_leads
	into #tb_leads2
	FROM #tb_leads 
	GROUP BY Consultant_ID, Partner_id, Promotion_id, Description;


PRINT '#tb_leads2 filled'

-- Insersion des shipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Consultant_ID, Partner_id, Promotion_id, Description, count_leads
	FROM #tb_leads2)
OPEN c1

FETCH NEXT FROM c1 INTO @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @count_leads
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_net_union(Consultant_ID, Partner_ID, Promotion_ID, Description, CountOfLeadID) 
	VALUES(@Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @count_leads)

    FETCH NEXT FROM c1 INTO @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @count_leads
END
CLOSE c1
DEALLOCATE c1



PRINT '#tbout_net_union filled with shipped sales'

SELECT Consultant_ID, Partner_id, Promotion_id, Description, 
	count(sales_id) as count_sales
	INTO #tb_sales2
	FROM #tb_sales  GROUP BY Consultant_ID, Partner_id, Promotion_id, Description;

PRINT '#tb_leads2 filled'


-- Insersion des shipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Consultant_ID, Partner_id, Promotion_id, Description, 
	count_sales FROM #tb_sales2)
OPEN c1

FETCH NEXT FROM c1 INTO @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @count_sales
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_net_union(Consultant_ID, Partner_ID, Promotion_ID, Description, CountOfSalesID) 
	VALUES(@Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @count_sales)

    FETCH NEXT FROM c1 INTO @Consultant_ID, @Partner_ID, @Promotion_ID, @Description, @count_sales
END
CLOSE c1
DEALLOCATE c1



-- regroupement des amounts par promotions
SELECT Consultant_ID, Partner_id, Promotion_id, Description, 
	Sum(countofleadid) as countofleadid, Sum(countofsalesid) as countofsalesid,
	Sum(shipped_brochure) as shipped_brochure, Sum(returned_brochure) as returned_brochure, Sum(reshipped_brochure) as reshipped_brochure,
	Sum(shipped_candies) as shipped_candies, Sum(returned_candies) as returned_candies, Sum(reshipped_candies) as reshipped_candies, 
	sum(shipped_scratchcard) as shipped_scratchcard, Sum(returned_scratchcard) as returned_scratchcard, Sum(reshipped_scratchcard) as reshipped_scratchcard 
	INTO #tb2
	FROM #tbout_net_union 
	GROUP BY Consultant_ID, partner_id, promotion_id, Description;


--SELECT  * FROM #tb2
-- calculs des net amounts
SELECT dbo.Consultant.Consultant_ID, Tabletemp.partner_id, promotion_id, description, 
	countofleadid, countofsalesid,
	(shipped_brochure - returned_brochure + reshipped_brochure) as net_brochure, 
	(shipped_candies - returned_candies + reshipped_brochure) as net_candies, 
	(shipped_scratchcard - returned_scratchcard + reshipped_scratchcard) as net_scratchcard,
	Consultant.Name,
	Partner.Partner_Name
	INTO #tb1
	FROM #tb2 AS Tabletemp INNER JOIN dbo.consultant ON Tabletemp.Consultant_ID = dbo.Consultant.Consultant_ID
	INNER JOIN dbo.partner ON Tabletemp.Partner_ID = partner.partner_ID
	WHERE (Consultant.Is_Active <> 0)
			AND (Consultant.Department_ID = 7)
			AND (Consultant.Is_Agent = 0)
			AND (Consultant.Is_FM = 0);



-- valeur de retour
SELECT  * FROM #tb1
GO

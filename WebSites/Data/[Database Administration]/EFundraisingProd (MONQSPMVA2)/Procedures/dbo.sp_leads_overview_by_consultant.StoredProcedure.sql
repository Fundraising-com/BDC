USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_leads_overview_by_consultant]    Script Date: 02/14/2014 13:09:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- sp_ code
CREATE PROCEDURE [dbo].[sp_leads_overview_by_consultant] (@date_from VARCHAR(20) = '', @date_to VARCHAR(20) = '') AS

DECLARE @Partner_ID INT,
        @Promotion_ID INT,
        @Description VARCHAR(50),
        @count_leads INT,
        @count_sales INT,
        @brochure DECIMAL(38,4),
        @candies DECIMAL(38,4),
        @scratchcard DECIMAL(38,4),
	@consultantID INT,
	@consultantName VARCHAR(50)

CREATE TABLE #tbout_union(Partner_ID INT,
        Promotion_ID INT,
        Description VARCHAR(50),
        CountOfLeadID INT DEFAULT 0,
        CountOfSalesID INT DEFAULT 0,
        brochure DECIMAL(38,4) DEFAULT 0,
        candies DECIMAL(38,4) DEFAULT 0,
        scratchcard DECIMAL(38,4) DEFAULT 0,
	ConsultantID INT,
	ConsultantName VARCHAR(50));
        

-- obtension des ventes net pour les leads du mois
SELECT     
	Promotion.Partner_ID, 
	Promotion.Promotion_ID, 
	Promotion.Description, 
        SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS brochure, 
        SUM(CASE WHEN Product_Class.Description LIKE 'WFC%' OR Product_Class.Description LIKE 'HERSHEY%' THEN Sales_Item.Sales_Amount ELSE 0 END) AS candies, 
        SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS scratchcard, 
	Consultant.Consultant_ID, 
        Consultant.Name
INTO #tb1
FROM         
	Promotion INNER JOIN Lead 
		ON Lead.Promotion_ID = Promotion.Promotion_ID 
	LEFT JOIN Client 
		ON Client.Lead_ID = Lead.Lead_ID 
	INNER JOIN Sale
		ON Sale.Client_ID = Client.Client_ID 
		AND Sale.Client_Sequence_Code = Client.Client_Sequence_Code 
	INNER JOIN Sales_Item 
		ON Sales_Item.Sales_ID = Sale.Sales_ID 
	INNER JOIN Scratch_Book 
		ON Scratch_Book.Scratch_Book_ID = Sales_Item.Scratch_Book_ID 
	INNER JOIN Product_Class 
		ON Product_Class.Product_Class_ID = Scratch_Book.Product_Class_ID 
	INNER JOIN dbo.Consultant 
		ON Sale.Consultant_ID = Consultant.Consultant_ID
WHERE     
	Sale.Actual_Ship_Date IS NOT NULL 
	AND 
	(
		(
			Sale.Reship_Date IS NULL AND Sale.Box_Return_Date IS NULL
		) 
		OR
                (
			Sale.Reship_Date IS NOT NULL AND Sale.Box_Return_Date IS NOT NULL
		)
	)
	AND Lead.Lead_Entry_Date BETWEEN @date_from AND @date_to 
	AND Consultant.Department_ID = 7 
	AND Consultant.IS_Active <> 0 
	AND Consultant.Is_FM = 0 
	AND Consultant.Is_Agent = 0
GROUP BY 
	Promotion.Partner_ID, 
	Promotion.Promotion_ID, 
	Promotion.Description, 
	Consultant.Consultant_ID, 
	Consultant.Name

-- Insersion des net sales dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Partner_id, Promotion_id, Description, 
	brochure, candies, scratchcard, consultant_id, name
	FROM #tb1)
OPEN c1

FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @brochure, @candies, @scratchcard, @consultantID, @consultantName
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_union(Partner_ID, Promotion_ID, Description, brochure, candies, scratchcard, consultantID, consultantName) 
	VALUES(@Partner_ID, @Promotion_ID, @Description, @brochure, @candies, @scratchcard, @consultantID, @consultantName)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @brochure, @candies, @scratchcard, @consultantID, @consultantName
END
CLOSE c1
DEALLOCATE c1


-- Obtention du nombre de leads selon leur date d'entrer
SELECT     
	dbo.Promotion.Partner_ID, 
	dbo.Promotion.Promotion_ID, 
	dbo.Promotion.Description, 
	dbo.Lead.Lead_ID, 
	dbo.Consultant.Consultant_ID, 
        dbo.Consultant.Name
INTO #tb_leads
FROM         
	dbo.Lead INNER JOIN dbo.Promotion 
		ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID 
	INNER JOIN dbo.Consultant 
		ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID
WHERE     
	(dbo.Lead.Lead_Entry_Date BETWEEN @date_From AND @date_to) 
	AND (dbo.Consultant.Is_Agent = 0) 
	AND (dbo.Consultant.Is_Active <> 0) 
	AND (dbo.Consultant.Is_Fm = 0) 
	AND (dbo.Consultant.Department_ID = 7)



-- count de sales selon le lead entry date
SELECT     
	dbo.Promotion.Partner_ID, 
	dbo.Promotion.Promotion_ID, 
	dbo.Promotion.Description, 
	dbo.Sale.Sales_ID, 
	dbo.Consultant.Consultant_ID, 
        dbo.Consultant.Name
INTO #tb_sales
FROM         
	dbo.Sale INNER JOIN dbo.Client 
		ON dbo.Sale.Client_ID = dbo.Client.Client_ID 
		AND dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code 
	INNER JOIN dbo.Lead 
		ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID 
	INNER JOIN dbo.Promotion 
		ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID 
	INNER JOIN dbo.Consultant 	
		ON dbo.Sale.Consultant_ID = dbo.Consultant.Consultant_ID
WHERE     
	(dbo.Lead.Lead_Entry_Date BETWEEN @date_from AND @date_to) 
	AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) 
	AND (dbo.Sale.Reship_Date IS NULL) 
	AND (dbo.Sale.Box_Return_Date IS NULL) 
	AND (dbo.Consultant.Is_Agent = 0) 
	AND (dbo.Consultant.Is_Active <> 0) 
	AND (dbo.Consultant.Is_Fm = 0) 
	AND (dbo.Consultant.Department_ID = 7) 
	OR (dbo.Lead.Lead_Entry_Date BETWEEN @date_from AND @date_to) 
	AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) 
	AND (dbo.Sale.Reship_Date IS NOT NULL) 
	AND (dbo.Sale.Box_Return_Date IS NOT NULL) 
	AND (dbo.Consultant.Is_Agent = 0) 
	AND (dbo.Consultant.Is_Active <> 0) 
	AND (dbo.Consultant.Is_Fm = 0) 
	AND (dbo.Consultant.Department_ID = 7)


SELECT Partner_id, Promotion_id, Description, 
	count(lead_id) as count_leads, Consultant_ID, Name
	into #tb_leads2
	FROM #tb_leads GROUP BY Partner_id, Promotion_id, Description, Consultant_ID, Name;



-- Insersion des shipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Partner_id, Promotion_id, Description, count_leads, Consultant_ID, Name
	FROM #tb_leads2)
OPEN c1

FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @count_leads, @ConsultantID, @ConsultantName
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_union(Partner_ID, Promotion_ID, Description, CountOfLeadID, ConsultantID, ConsultantName) 
	VALUES(@Partner_ID, @Promotion_ID, @Description, @count_leads, @ConsultantID, @ConsultantName)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @count_leads, @ConsultantID, @ConsultantName
END
CLOSE c1
DEALLOCATE c1



SELECT Partner_id, Promotion_id, Description, 
	count(sales_id) as count_sales, Consultant_ID, Name
	INTO #tb_sales2
	FROM #tb_sales  GROUP BY Partner_id, Promotion_id, Description, Consultant_ID, Name;



-- Insersion des shipped dans la table temporaire
DECLARE c1 CURSOR FOR (SELECT Partner_id, Promotion_id, Description, 
	count_sales, Consultant_ID, Name FROM #tb_sales2)
OPEN c1

FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @count_sales, @ConsultantID, @ConsultantName
WHILE @@FETCH_STATUS = 0
BEGIN
	
    INSERT INTO #tbout_union(Partner_ID, Promotion_ID, Description, CountOfSalesID, ConsultantID, ConsultantName) 
	VALUES(@Partner_ID, @Promotion_ID, @Description, @count_sales, @ConsultantID, @ConsultantName)

    FETCH NEXT FROM c1 INTO @Partner_ID, @Promotion_ID, @Description, @count_sales, @ConsultantID, @ConsultantName
END
CLOSE c1
DEALLOCATE c1




SELECT TableTemp.partner_id, TableTemp.promotion_id, TableTemp.description, sum(countofleadid) as countofleadid, 
	sum(countofsalesid) as countofsalesid,  sum(brochure) as brochure, 
	sum(candies) as candies, sum(scratchcard) as scratchcard, ConsultantName, Partner.Partner_Name
	INTO #tbout_leads
	FROM #tbout_union AS TableTemp
		INNER JOIN dbo.Partner ON TableTemp.Partner_id = Partner.Partner_ID
	GROUP BY TableTemp.partner_id, TableTemp.promotion_id, TableTemp.description, ConsultantName,Partner.Partner_Name;



SELECT * FROM #tbout_leads
GO

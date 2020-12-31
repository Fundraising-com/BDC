USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterBatch_SelectAll]    Script Date: 06/07/2017 09:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'SwitchLetterBatch'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_SwitchLetterBatch_SelectAll]

AS
SET NOCOUNT ON

-- Get latest products in case a title has changed
SELECT	p.Product_Code,
		MAX(p.Product_Year) AS Product_Year,
		CONVERT(varchar(1), '') AS Product_Season
INTO		#ProductsYear
FROM		QSPCanadaProduct..Product p
WHERE	p.Type = 46001
GROUP BY	p.Product_Code


SELECT	#ProductsYear.Product_Code,
		#ProductsYear.Product_Year,
		MAX(p.Product_Season) AS Product_Season
INTO		#Products
FROM		#ProductsYear,
		QSPCanadaProduct..Product p
WHERE	p.Product_Code = #ProductsYear.Product_Code
AND		p.Product_Year = #ProductsYear.Product_Year
GROUP BY	#ProductsYear.Product_Code,
		#ProductsYear.Product_Year


-- SELECT all rows from the table.
SELECT	slb.[Instance],
		slb.[ProductCode],
		slb.[DateCreated],
		slb.[UserID] as UserName,
		(select count(*) from SwitchLetterBatchCustomerOrderDetail sblcod where slb.Instance =  sblcod.SwitchLetterBatchInstance) as Quantity,
	 	pr.product_sort_name as MagazineTitle,
		CASE slb.IsPrinted when 1 then 'Yes' when 0 then 'NO' else 'NO' end as IsPrinted,
		slb.DatePrinted,
		CASE slb.IsLocked when 1 then 'Closed' when 0 then 'Open' else 'Open' end as IsLocked

FROM		[dbo].[SwitchLetterBatch] slb,
		QSPCanadaProduct..Product pr,
		#Products p
WHERE	slb.ProductCode = convert(nvarchar,pr.product_code)
AND		p.Product_Code = pr.Product_Code
AND		p.Product_Year = pr.Product_Year
AND		p.Product_Season = pr.Product_Season

ORDER BY 
	slb.[Instance] DESC

DROP TABLE	#ProductsYear
DROP TABLE	#Products
GO

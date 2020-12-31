USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_BasePrice_RemitRate]    Script Date: 06/07/2017 09:20:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  procedure [dbo].[pr_RemitTest_BasePrice_RemitRate]

@iRunID		int = 0

AS

/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

SELECT		codrh.TitleCode,
		codrh.BasePrice,
		codrh.NumberOfIssues,
		codrh.RemitRate,
		COUNT(codrh.TitleCode) AS TitleCount
INTO		#base
FROM		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb,
		CustomerOrderDetail cod,
		QSPCanadaProduct..Program_Master pm, 	-- Catalog
		QSPCanadaProduct..ProgramSection ps, 	-- Catalog Section
		QSPCanadaProduct..Pricing_Details pd, 	-- Offer
		QSPCanadaProduct..Product p		-- Product
WHERE		codrh.RemitBatchID = rb.ID
AND		cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		cod.TransID = codrh.TransID
AND		pd.MagPrice_Instance = cod.PricingDetailsID
AND		p.Product_Instance = pd.Product_Instance
AND		codrh.Status IN (42000, 42001)
AND		p.Product_Year = @ProductYear
AND		p.Product_Season = @ProductSeason
AND		pd.ProgramSectionID = ps.ID
AND 		pm.Code = ps.CatalogCode
AND		pm.SubType <> 30305	-- not $20 Gift Catalog
AND		rb.RunID = @iRunID
GROUP BY	codrh.Titlecode,
		codrh.BasePrice,
		codrh.NumberOfIssues,
		codrh.RemitRate
ORDER BY	codrh.TitleCode,
		codrh.BasePrice,
		codrh.NumberOfIssues  

IF EXISTS
(
	SELECT		1
	FROM		#base
	WHERE		TitleCode IN
			(SELECT		TitleCode
			FROM		#base
			GROUP BY	TitleCode,
					NumberOfIssues  
			HAVING		COUNT(BasePrice) > 1 
			OR		COUNT(RemitRate) > 1)
)
	SELECT 1
ELSE
	SELECT 0

DROP TABLE	#base
GO

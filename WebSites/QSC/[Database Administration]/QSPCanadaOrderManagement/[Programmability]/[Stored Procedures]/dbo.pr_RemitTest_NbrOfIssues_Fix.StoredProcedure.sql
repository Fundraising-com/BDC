USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_NbrOfIssues_Fix]    Script Date: 06/07/2017 09:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_NbrOfIssues_Fix]

@iRunID		int = 0

AS

/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

UPDATE		cod
SET		cod.Quantity = pd.Nbr_Of_Issues
FROM		CustomerOrderDetail cod,
		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb,
		QSPCanadaProduct..Pricing_Details pd
WHERE		cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		cod.TransID = codrh.TransID
AND		codrh.RemitBatchID = rb.ID
AND		codrh.Status IN (42000, 42001)
AND		pd.MagPrice_Instance = cod.PricingDetailsID
AND		pd.Pricing_Year = @ProductYear
AND		pd.Pricing_Season = @ProductSeason
AND		cod.Quantity <> pd.Nbr_Of_Issues
AND		rb.RunID = @iRunID


UPDATE		codrh
SET		codrh.Quantity = pd.Nbr_Of_Issues,
		codrh.NumberOfIssues = pd.Nbr_Of_Issues
FROM		CustomerOrderDetail cod,
		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb,
		QSPCanadaProduct..Pricing_Details pd
WHERE		cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		cod.TransID = codrh.TransID
AND		codrh.RemitBatchID = rb.ID
AND		codrh.Status IN (42000, 42001)
AND		pd.MagPrice_Instance = cod.PricingDetailsID
AND		pd.Pricing_Year = @ProductYear
AND		pd.Pricing_Season = @ProductSeason
AND		(codrh.NumberOfIssues <> pd.Nbr_Of_Issues
OR		codrh.Quantity <> pd.Nbr_Of_Issues)
AND		rb.RunID = @iRunID
GO

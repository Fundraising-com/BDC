USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_CheckTitleAndFH]    Script Date: 06/07/2017 09:20:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_CheckTitleAndFH]

@iRunID 	int = 0

AS

/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

SELECT		CONVERT(bit, COUNT(*))
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
				AND	rb.RunID = @iRunID
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
				--AND	p.Product_Year = @ProductYear
				--AND	p.Product_Season = @ProductSeason
WHERE		rb.FulfillmentHouseNbr <> p.Fulfill_House_Nbr
GO

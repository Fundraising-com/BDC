USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_BasePrice_RemitRate_Fix]    Script Date: 06/07/2017 09:20:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_RemitTest_BasePrice_RemitRate_Fix]

@iRunID		int = 0

AS

UPDATE		codrh
SET		codrh.BasePrice = pd.Basic_Price_Yr,
		codrh.RemitRate = pd.Remit_Rate
FROM		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb,
		CustomerOrderDetail cod,
		QSPCanadaProduct..Pricing_Details pd
WHERE		rb.ID = codrh.RemitBatchID
AND		cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		cod.TransID = codrh.TransID
AND		pd.MagPrice_Instance = cod.PricingDetailsID
AND		codrh.Status IN (42000, 42001)
AND		(codrh.BasePrice <> pd.Basic_Price_Yr
OR		codrh.RemitRate <> pd.Remit_Rate)

AND		rb.RunID = @iRunID
GO

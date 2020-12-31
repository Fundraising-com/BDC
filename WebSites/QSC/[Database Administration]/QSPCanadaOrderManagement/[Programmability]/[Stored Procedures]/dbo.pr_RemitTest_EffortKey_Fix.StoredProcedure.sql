USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_EffortKey_Fix]    Script Date: 06/07/2017 09:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_EffortKey_Fix]

@iRunID		int = 0

AS

/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

UPDATE		codrh
SET		codrh.EffortKey = pd.Effort_Key
FROM		CustomerOrderDetail cod,
		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb,
		QSPCanadaProduct..Pricing_Details pd
WHERE		codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
AND		codrh.TransID = cod.TransID
AND		rb.ID = codrh.RemitBatchID
AND		pd.MagPrice_Instance = cod.PricingDetailsID
AND		codrh.Status IN (42000, 42001) -- "Needs to be sent" or "Sent"
AND		pd.Pricing_Year = @ProductYear
AND		pd.Pricing_Season = @ProductSeason
AND		codrh.EffortKey COLLATE Latin1_General_CS_AS  <> pd.Effort_Key COLLATE Latin1_General_CS_AS 
AND		rb.RunID = @iRunID
GO

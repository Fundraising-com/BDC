USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Premium]    Script Date: 06/07/2017 09:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_Premium]

@iRunID		int = 0

AS
IF EXISTS
(
	SELECT		pd.ABCCode,
			pd.prdPremiumInd,
			pd.prdPremiumCode,
			pd.prdPremiumCopy,
			codrh.*
	FROM		CustomerOrderDetailRemitHistory codrh,
			RemitBatch rb,
			CustomerOrderDetail cod,
			QSPCanadaProduct..Pricing_Details pd
	WHERE		rb.ID = codrh.RemitBatchID
	AND		cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
	AND		cod.TransID = codrh.TransID
	AND		pd.MagPrice_Instance = cod.PricingDetailsID
	AND		(COALESCE(codrh.ABCCode, '') <> COALESCE(pd.ABCCode, '')
	OR		COALESCE(CONVERT(varchar(1), codrh.PremiumIndicator), '') <> COALESCE(pd.prdPremiumInd, '')
	OR		COALESCE(codrh.PremiumCode, '') <> COALESCE(pd.prdPremiumCode, '')
	OR		COALESCE(codrh.PremiumDescription, '') <> COALESCE(pd.prdPremiumCopy, ''))
	AND		rb.RunID = @iRunID 
)
	SELECT 1
ELSE
	SELECT 0
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Premium_Fix]    Script Date: 06/07/2017 09:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_Premium_Fix]

@iRunID		int = 0

AS
UPDATE		codrh
SET		codrh.ABCCode = pd.ABCCode,
		codrh.PremiumIndicator = CASE pd.prdPremiumInd WHEN '' THEN NULL ELSE pd.prdPremiumInd END,
		codrh.PremiumCode = COALESCE(pd.prdPremiumCode, ''),
		codrh.PremiumDescription = COALESCE(pd.prdPremiumCopy, '')
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
and pd.prdPremiumInd <> 'P'
GO

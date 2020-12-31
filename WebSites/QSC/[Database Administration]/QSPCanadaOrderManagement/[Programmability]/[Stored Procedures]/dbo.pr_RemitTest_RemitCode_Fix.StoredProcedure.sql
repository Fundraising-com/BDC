USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_RemitCode_Fix]    Script Date: 06/07/2017 09:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_RemitCode_Fix]

@iRunID		int = 0

AS
UPDATE		codrh
SET		codrh.RemitCode = CASE SUBSTRING(p.RemitCode, 1, 1) WHEN 'X' THEN SUBSTRING(p.RemitCode, 2, LEN(p.RemitCode)-1) ELSE p.RemitCode END
FROM		CustomerOrderDetailRemitHistory codrh,
		CustomerOrderDetail cod,
		RemitBatch rb,
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaProduct..Product p
WHERE		cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		cod.TransID = codrh.TransID
AND		rb.ID = codrh.RemitBatchID
AND		pd.MagPrice_Instance = cod.PricingDetailsID
AND		p.Product_Instance = pd.Product_Instance
AND		COALESCE(codrh.RemitCode, '') <> CASE SUBSTRING(p.RemitCode, 1, 1) WHEN 'X' THEN SUBSTRING(p.RemitCode, 2, LEN(p.RemitCode)-1) ELSE p.RemitCode END
AND		rb.RunID = @iRunID
GO

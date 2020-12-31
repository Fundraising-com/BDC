USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Cancelled_Fix]    Script Date: 06/07/2017 09:20:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[pr_RemitTest_Cancelled_Fix]

@iRunID		int = 0

AS

UPDATE		codrh
SET			codrh.Status = 42010 -- Magazine Inactive
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
WHERE		p.Status = 30601
AND			codrh.Status <> 42010
AND			rb.RunID = @iRunID
GO

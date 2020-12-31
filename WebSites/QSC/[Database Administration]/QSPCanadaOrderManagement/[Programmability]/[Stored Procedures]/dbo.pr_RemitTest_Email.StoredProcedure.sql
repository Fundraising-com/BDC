USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Email]    Script Date: 06/07/2017 09:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_Email]

@iRunID		int = 0

AS

IF EXISTS
(
	SELECT		*
	FROM		CustomerOrderDetail cod
	JOIN		CustomerOrderHeader coh
					ON	coh.Instance = cod.CustomerOrderHeaderInstance
	JOIN		Customer cust
					ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
											WHEN 0 THEN coh.CustomerBillToInstance
											ELSE		cod.CustomerShipToInstance
										END
	JOIN		CustomerOrderDetailRemitHistory codrh
					ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
					AND	codrh.TransID = cod.TransID
	JOIN		RemitBatch rb
					ON	rb.ID = codrh.RemitBatchID
	WHERE		cod.ProductCode LIKE 'D%'
	AND			ISNULL(cust.Email, '') = ''
	AND			ISNULL(cod.DelFlag, 0) = 0
	AND			ISNULL(codrh.Status, 0) IN (42000)
	AND			rb.RunID = @iRunID
)
	SELECT 1
ELSE
	SELECT 0
GO

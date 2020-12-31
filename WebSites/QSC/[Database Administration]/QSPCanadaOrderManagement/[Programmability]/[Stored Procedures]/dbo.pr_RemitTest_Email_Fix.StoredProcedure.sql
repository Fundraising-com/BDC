USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Email_Fix]    Script Date: 06/07/2017 09:20:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_Email_Fix]

@iRunID		int = 0

AS

CREATE TABLE	[#UnremittableSubs](
				[CustomerOrderHeaderInstance] [int] NOT NULL,
                [TransID] [int] NOT NULL,
				[NewRemitBatchID] [int] NOT NULL)

INSERT INTO	[#UnremittableSubs](
			[CustomerOrderHeaderInstance],
			[TransID],
			[NewRemitBatchID])
SELECT		codrh.CustomerOrderHeaderInstance,
			codrh.TransID,
			rbNext.ID AS NewRemitBatchID
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
JOIN		RemitBatch rbNext
				ON	rbNext.FulfillmentHouseNbr = rb.FulfillmentHouseNbr
				AND	rbNext.Status = 42000
				AND	rbNext.RunID is null
WHERE		cod.ProductCode LIKE 'D%'
AND			ISNULL(cust.Email, '') = ''
AND			ISNULL(cod.DelFlag, 0) = 0
AND			ISNULL(codrh.Status, 0) IN (42000)
AND			rb.RunID = @iRunID

UPDATE		crh
SET			crh.RemitBatchID = us.NewRemitBatchID
FROM		CustomerRemitHistory crh
JOIN		CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN		#UnremittableSubs us
				ON	us.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	us.TransID = codrh.TransID

UPDATE		crha
SET			crha.RemitBatchID = us.NewRemitBatchID
FROM		CustomerRemitHistoryAudit crha
JOIN		CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerRemitHistoryInstance = crha.Instance
JOIN		#UnremittableSubs us
				ON	us.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	us.TransID = codrh.TransID
WHERE		crha.AuditDate =
			(SELECT		TOP 1
						crha2.AuditDate
			FROM		CustomerRemitHistoryAudit crha2
			WHERE		crha2.Instance = crha.Instance
			ORDER BY	crha2.AuditDate DESC)

UPDATE		codrh
SET			codrh.RemitBatchID = us.NewRemitBatchID
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		#UnremittableSubs us
				ON	us.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	us.TransID = codrh.TransID

UPDATE		codrha
SET			codrha.RemitBatchID = us.NewRemitBatchID
FROM		CustomerOrderDetailRemitHistoryAudit codrha
JOIN		#UnremittableSubs us
				ON	us.CustomerOrderHeaderInstance = codrha.CustomerOrderHeaderInstance
				AND	us.TransID = codrha.TransID
WHERE		codrha.AuditDate =
			(SELECT		TOP 1
						codrha2.AuditDate
			FROM		CustomerOrderDetailRemitHistoryAudit codrha2
			WHERE		codrha2.CustomerRemitHistoryInstance = codrha.CustomerRemitHistoryInstance
			ORDER BY	codrha2.AuditDate DESC)

DROP TABLE	#UnremittableSubs
GO

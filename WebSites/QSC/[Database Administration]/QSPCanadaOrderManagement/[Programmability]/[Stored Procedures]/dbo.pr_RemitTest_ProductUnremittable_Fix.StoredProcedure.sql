USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_ProductUnremittable_Fix]    Script Date: 06/07/2017 09:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pr_RemitTest_ProductUnremittable_Fix]

@iRunID		int = 0

AS

/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

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
FROM		CustomerOrderDetailRemitHistory codrh,
			QSPCanadaProduct..Product p,
			RemitBatch rb,
			RemitBatch rbNext
WHERE		p.Product_Code = codrh.TitleCode
AND			p.Product_Year = @ProductYear
AND			p.Product_Season = @ProductSeason
AND			rb.ID = codrh.RemitBatchID
AND			p.Status = 30603 --Magazine Unremittable
AND			rb.RunID = @iRunID
AND			rbNext.FulfillmentHouseNbr = p.Fulfill_House_Nbr
AND			rbNext.Status = 42000
AND			rbNext.RunID is null

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

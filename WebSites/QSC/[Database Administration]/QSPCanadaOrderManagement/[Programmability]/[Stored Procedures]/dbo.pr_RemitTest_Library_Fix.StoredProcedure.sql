USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Library_Fix]    Script Date: 06/07/2017 09:20:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_Library_Fix]

@iRunID		int = 0

AS

SELECT	codrh.CustomerOrderHeaderInstance,
		codrh.TransID,
		b.OrderID,
		crh.Instance AS CustomerRemitHistoryInstance
INTO	#Subs
FROM	CustomerOrderDetailRemitHistory codrh
JOIN	CustomerRemitHistory crh
			ON	crh.Instance = codrh.CustomerRemitHistoryInstance
JOIN	RemitBatch rb
			ON	rb.ID = crh.RemitBatchID
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = codrh.CustomerOrderHeaderInstance
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
WHERE	((crh.FirstName = 'J'
AND		crh.LastName 	= 'SMITH')
OR		(crh.FirstName 	LIKE 'MEDIA%'
AND		crh.LastName 	LIKE 'CENTER%')
OR		(crh.FirstName 	LIKE '%LIBRAR%'
OR		crh.FirstName 	LIKE '%BIBLIOT%'
OR		crh.LastName 	LIKE '%LIBRAR%'
OR		crh.LastName 	LIKE '%BIBLIOT%'))
AND		rb.RunID 	= @iRunID

UPDATE	crh
SET		crh.FirstName = 'CURRENT',
		crh.LastName = 'OCCUPANT'
FROM	CustomerRemitHistory crh
JOIN	#Subs s
			ON	s.CustomerRemitHistoryInstance = crh.Instance
			
UPDATE	cod
SET		Recipient = 'CURRENT OCCUPANT'
FROM	CustomerOrderDetail cod
JOIN	#Subs s
			ON	s.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
			AND	s.TransID = cod.TransID

Update	ReportRequestBatch_OrderEntryFollowupReport
Set		createdate = getdate(), QUEUEDATE = NULL,RUNDATESTART= NULL,FILENAME =NULL 
where	reportrequestbatchid  IN (select id from ReportRequestBatch
								where batchorderid IN (SELECT OrderID FROM #Subs))
GO

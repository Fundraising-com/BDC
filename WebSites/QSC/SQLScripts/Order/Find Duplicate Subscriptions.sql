--This gets all Subscriptions that have duplicate subs within the same order, checking
--by duplicate Order Header, Recipients name, Remit Code, Order Type
DROP TABLE	#GroupedSubs
SELECT		coh.Instance,
			b.OrderID,
			crh.LastName,
			crh.FirstName,
			codrh.RemitCode,
			cd.Description
INTO		#GroupedSubs
FROM		CustomerOrderDetailRemitHistory codrh, 
			CustomerRemitHistory crh,
			RemitBatch rb,
			Batch b,
			CustomerOrderHeader coh,
			QSPCanadaCommon..CodeDetail cd
WHERE		codrh.Status IN (42000, 42001)
AND			codrh.RemitBatchID = rb.ID
AND			codrh.CustomerRemitHistoryInstance = crh.Instance
AND			coh.OrderBatchDate = b.[Date]
AND			coh.OrderBatchID = b.ID
AND			b.OrderQualifierID = cd.Instance
AND			codrh.CustomerOrderHeaderInstance = coh.Instance
AND			rb.RunID IS NULL
AND			rb.Status = 42000
--AND			b.OrderQualifierID IN (39001, 39002, 39003) --Resolve Orders
GROUP BY	coh.Instance,
			b.OrderID,
			crh.LastName,
			crh.FirstName,
			codrh.RemitCode, 
			cd.Description
HAVING		COUNT(*) > 1


--List all duplicated subscriptions, not Grouping them
DROP TABLE	#ExplodedSubs
SELECT		coh.Instance,
			codrh.TransID,
			b.OrderID,
			crh.LastName,
			crh.FirstName,
			codrh.RemitCode,
			cd.Description
INTO		#ExplodedSubs
FROM		CustomerOrderHeader coh
JOIN		CustomerOrderDetailRemitHistory codrh
				ON codrh.CustomerOrderHeaderInstance = coh.Instance
JOIN		CustomerRemitHistory crh
				ON codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN		#GroupedSubs gs
				ON gs.Instance = coh.Instance
				AND	gs.LastName = crh.LastName
				AND gs.FirstName = crh.FirstName
				AND	gs.RemitCode = codrh.RemitCode,
			RemitBatch rb,
			Batch b,
			QSPCanadaCommon..CodeDetail cd,
			CustomerOrderDetail cod
WHERE		codrh.Status IN (42000, 42001)
AND			codrh.RemitBatchID = rb.ID
AND			coh.OrderBatchDate = b.[Date]
AND			coh.OrderBatchID = b.ID
AND			b.OrderQualifierID = cd.Instance
AND			rb.RunID IS NULL
AND			rb.Status = 42000
--AND			b.OrderQualifierID IN (39001, 39002, 39003)
GROUP BY	coh.Instance,
			b.OrderID,
			crh.LastName,
			crh.FirstName,
			codrh.RemitCode, 
			cd.Description,
			codrh.TransID


--List duplicate subs that have the minimum transID from each group
drop table	#SubsToKeep
SELECT		Min(codrh.TransID) AS MinTransID,
			coh.Instance,
			b.OrderID,
			crh.LastName,
			crh.FirstName,
			codrh.RemitCode,
			cd.Description,
			COUNT(*) AS NumberOfCopies
INTO		#SubsToKeep
FROM		CustomerOrderHeader coh
JOIN		CustomerOrderDetailRemitHistory codrh
				ON codrh.CustomerOrderHeaderInstance = coh.Instance
JOIN		CustomerRemitHistory crh
				ON codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN		#ExplodedSubs es
				ON es.Instance = coh.Instance
				AND	es.LastName = crh.LastName
				AND es.FirstName = crh.FirstName
				AND	es.RemitCode = codrh.RemitCode,
			RemitBatch rb,
			Batch b,
			QSPCanadaCommon..CodeDetail cd
WHERE		codrh.Status IN (42000, 42001)
AND			codrh.RemitBatchID = rb.ID
AND			coh.OrderBatchDate = b.[Date]
AND			coh.OrderBatchID = b.ID
AND			b.OrderQualifierID = cd.Instance
AND			rb.RunID IS NULL
AND			rb.Status = 42000
--AND			b.OrderQualifierID IN (39001, 39002, 39003)
GROUP BY	coh.Instance,
			b.OrderID,
			crh.LastName,
			crh.FirstName,
			codrh.RemitCode, 
			cd.Description
ORDER BY	coh.Instance


--List all duplicate subs except for the ones with the minimum transID from each group,
--since we will be keeping the duplicates with the minimum transID, grouped by Order
drop table	#SubsToCancel
SELECT		es.Instance,
			es.TransID,
			es.OrderID,
			es.LastName,
			es.FirstName,
			es.RemitCode,
			es.Description
INTO		#SubsToCancel
FROM		#ExplodedSubs es
			LEFT JOIN #substokeep stk
				ON stk.Instance = es.Instance
				AND	stk.LastName = es.LastName
				AND stk.FirstName = es.FirstName
				AND	stk.RemitCode = es.RemitCode
				AND stk.MinTransID = es.TransID
WHERE			stk.Instance is null
GROUP BY	es.Instance,
			es.OrderID,
			es.LastName,
			es.FirstName,
			es.RemitCode, 
			es.Description,
			es.TransID
ORDER BY	es.Instance, TransID


--Delete CustomerRemitHistory record for duplicates to be removed

DELETE		crh
FROM		CustomerRemitHistory crh
JOIN		CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN		#SubsToCancel stc
				ON	stc.Instance = codrh.CustomerOrderHeaderInstance
				AND	stc.TransID = codrh.TransID


--Delete CustomerOrderDetailRemitHistory record for duplicates to be removed
DELETE		codrh
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		#SubsToCancel stc
				ON	stc.Instance = codrh.CustomerOrderHeaderInstance
				AND	stc.TransID = codrh.TransID


--Set CustomerOrderDetail.StatusInstance to 506 (Order Detail Voided Due To Error)
--for the duplicates that are to be removed
UPDATE		cod
SET			StatusInstance = 506
FROM		CustomerOrderDetail cod,
			#SubsToCancel stc
WHERE		cod.CustomerOrderHeaderInstance = stc.Instance
AND			cod.TransID = stc.TransID
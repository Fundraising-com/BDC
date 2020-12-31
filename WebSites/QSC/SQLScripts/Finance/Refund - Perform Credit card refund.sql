--subs grouped
drop table #groupedsubs
SELECT	cph.TotalAmount,
		coh.Instance,
		--codrh.TransID,
		b.OrderID,
		crh.LastName,
		crh.FirstName,
		codrh.RemitCode,
		cd.Description,
		COUNT(*) AS NumberOfCopies,
		coh.PaymentMethodInstance,
		ccp.CreditCardNumber,
		ccp.ExpirationDate
into #groupedsubs
FROM	CustomerOrderDetailRemitHistory codrh, 
		CustomerRemitHistory crh,
		RemitBatch rb,
		Batch b,
		CustomerOrderHeader coh,
		QSPCanadaCommon..CodeDetail cd,
		CustomerPaymentHeader cph,
		CreditCardPayment ccp
WHERE	codrh.Status IN (42000, 42001)
AND		codrh.RemitBatchID = rb.ID
AND		codrh.CustomerRemitHistoryInstance = crh.Instance
AND		coh.OrderBatchDate = b.Date
AND		coh.OrderBatchID = b.ID
AND		b.OrderQualifierID = cd.Instance
AND		codrh.CustomerOrderHeaderInstance = coh.Instance
AND		rb.RunID IS NULL
AND		rb.Status = 42000
AND		b.OrderQualifierID IN (39001, 39002, 39003)
AND		coh.PaymentMethodInstance IN (50003, 50004)
AND		cph.CustomerOrderHeaderInstance = coh.Instance
AND		ccp.CustomerPaymentHeaderInstance = cph.Instance
GROUP BY	cph.TotalAmount,
		coh.Instance,
		b.OrderID,
		crh.LastName,
		crh.FirstName,
		codrh.RemitCode, 
		cd.Description,
		ccp.CreditCardNumber,
		ccp.ExpirationDate,
		--codrh.TransID,
		coh.PaymentMethodInstance
HAVING	COUNT(*) > 1
ORDER BY coh.Instance


--select * from #groupedsubs
--SELECT * FROM #explodedsubs
--explode all duped subs
drop table #explodedsubs
SELECT	cod.Price,--cph.TotalAmount,
		coh.Instance,
		codrh.TransID,
		b.OrderID,
		crh.LastName,
		crh.FirstName,
		codrh.RemitCode,
		cd.Description,
		COUNT(*) AS NumberOfCopies,
		coh.PaymentMethodInstance,
		ccp.CreditCardNumber,
		ccp.ExpirationDate
into #explodedsubs
FROM	CustomerOrderHeader coh
JOIN	CustomerOrderDetailRemitHistory codrh
			ON codrh.CustomerOrderHeaderInstance = coh.Instance
JOIN	CustomerRemitHistory crh
			ON codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN	#groupedsubs gs
			ON gs.Instance = coh.Instance
			AND	gs.LastName = crh.LastName
			AND gs.FirstName = crh.FirstName
			AND	gs.RemitCode = codrh.RemitCode,
		RemitBatch rb,
		Batch b,
		QSPCanadaCommon..CodeDetail cd,
		CustomerPaymentHeader cph,
		CreditCardPayment ccp,
		CustomerOrderDetail cod
WHERE	codrh.Status IN (42000, 42001)
AND		codrh.RemitBatchID = rb.ID
AND		coh.OrderBatchDate = b.Date
AND		coh.OrderBatchID = b.ID
AND		b.OrderQualifierID = cd.Instance
AND		rb.RunID IS NULL
AND		rb.Status = 42000
AND		b.OrderQualifierID IN (39001, 39002, 39003)
AND		coh.PaymentMethodInstance IN (50003, 50004)
AND		cph.CustomerOrderHeaderInstance = coh.Instance
AND		ccp.CustomerPaymentHeaderInstance = cph.Instance
AND		cod.CustomerOrderHeaderInstance = coh.Instance
AND		cod.TransID = codrh.TransID
GROUP BY	--cph.TotalAmount,
		coh.Instance,
		b.OrderID,
		crh.LastName,
		crh.FirstName,
		codrh.RemitCode, 
		cd.Description,
		ccp.CreditCardNumber,
		ccp.ExpirationDate,
		codrh.TransID,
		coh.PaymentMethodInstance,
		cod.Price
--HAVING	COUNT(*) > 1
ORDER BY coh.Instance



--SELECT * FROM #explodedsubs
--get the minimum transID from each group
drop table #substokeep
SELECT	Min(codrh.TransID) AS MinTransID,
		cod.Price,
		coh.Instance,
		b.OrderID,
		crh.LastName,
		crh.FirstName,
		codrh.RemitCode,
		cd.Description,
		COUNT(*) AS NumberOfCopies,
		coh.PaymentMethodInstance,
		ccp.CreditCardNumber,
		ccp.ExpirationDate
into #substokeep
FROM	CustomerOrderHeader coh
JOIN	CustomerOrderDetailRemitHistory codrh
			ON codrh.CustomerOrderHeaderInstance = coh.Instance
JOIN	CustomerRemitHistory crh
			ON codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN	#explodedsubs es
			ON es.Instance = coh.Instance
			AND	es.LastName = crh.LastName
			AND es.FirstName = crh.FirstName
			AND	es.RemitCode = codrh.RemitCode,
		RemitBatch rb,
		Batch b,
		QSPCanadaCommon..CodeDetail cd,
		CustomerPaymentHeader cph,
		CreditCardPayment ccp,
		CustomerOrderDetail cod
WHERE	codrh.Status IN (42000, 42001)
AND		codrh.RemitBatchID = rb.ID
AND		coh.OrderBatchDate = b.Date
AND		coh.OrderBatchID = b.ID
AND		b.OrderQualifierID = cd.Instance
AND		rb.RunID IS NULL
AND		rb.Status = 42000
AND		b.OrderQualifierID IN (39001, 39002, 39003)
AND		coh.PaymentMethodInstance IN (50003, 50004)
AND		cph.CustomerOrderHeaderInstance = coh.Instance
AND		ccp.CustomerPaymentHeaderInstance = cph.Instance
AND		cod.CustomerOrderHeaderInstance = coh.Instance
AND		cod.TransID = codrh.TransID
GROUP BY	coh.Instance,
		b.OrderID,
		crh.LastName,
		crh.FirstName,
		codrh.RemitCode, 
		cd.Description,
		ccp.CreditCardNumber,
		ccp.ExpirationDate,
		coh.PaymentMethodInstance,
		cod.Price
--HAVING	COUNT(*) > 1
ORDER BY coh.Instance


--select * from #explodedsubs
--SELECT * FROM #substokeep
--get all but the minimum transID from each group
drop table #cancelsubs
SELECT	codrh.TransID,
		cod.Price,
		coh.Instance,
		b.OrderID,
		crh.LastName,
		crh.FirstName,
		codrh.RemitCode,
		cd.Description,
		COUNT(*) AS NumberOfCopies,
		coh.PaymentMethodInstance,
		ccp.CreditCardNumber,
		ccp.ExpirationDate
into #cancelsubs
FROM	CustomerOrderHeader coh
JOIN	CustomerOrderDetailRemitHistory codrh
			ON codrh.CustomerOrderHeaderInstance = coh.Instance
JOIN	CustomerRemitHistory crh
			ON codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN	#explodedsubs es
			ON es.Instance = coh.Instance
			AND	es.LastName = crh.LastName
			AND es.FirstName = crh.FirstName
			AND	es.RemitCode = codrh.RemitCode
LEFT JOIN	#substokeep stk
				ON stk.Instance = coh.Instance
				AND	stk.LastName = crh.LastName
				AND stk.FirstName = crh.FirstName
				AND	stk.RemitCode = codrh.RemitCode
				AND stk.MinTransID = codrh.TransID,
		RemitBatch rb,
		Batch b,
		QSPCanadaCommon..CodeDetail cd,
		CustomerPaymentHeader cph,
		CreditCardPayment ccp,
		CustomerOrderDetail cod
WHERE	codrh.Status IN (42000, 42001)
AND		codrh.RemitBatchID = rb.ID
AND		coh.OrderBatchDate = b.Date
AND		coh.OrderBatchID = b.ID
AND		b.OrderQualifierID = cd.Instance
AND		rb.RunID IS NULL
AND		rb.Status = 42000
AND		b.OrderQualifierID IN (39001, 39002, 39003)
AND		coh.PaymentMethodInstance IN (50003, 50004)
AND		cph.CustomerOrderHeaderInstance = coh.Instance
AND		ccp.CustomerPaymentHeaderInstance = cph.Instance
AND		stk.Instance is null
AND		cod.CustomerOrderHeaderInstance = coh.Instance
AND		cod.TransID = codrh.TransID
GROUP BY	cod.Price,
		coh.Instance,
		b.OrderID,
		crh.LastName,
		crh.FirstName,
		codrh.RemitCode, 
		cd.Description,
		ccp.CreditCardNumber,
		ccp.ExpirationDate,
		codrh.TransID,
		coh.PaymentMethodInstance
--HAVING	COUNT(*) > 1
ORDER BY coh.Instance

select * from qspcanadacommon..codedetail where codeheaderinstance = 500
--select * from resolvecreditcardrefund where refundstatus = 0
--select top 2 * from resolvecreditcardrefunddetails

--select * from #cancelsubs
--select * from customerorderdetail where customerorderheaderinstance = 8143430
--select * from #groupedsubs
--select * from customerorderdetail where customerorderheaderinstance = 8086982

--insert cancelled subs into credit refund tables
insert into resolvecreditcardrefund
SELECT	Sum(Price) AS Price,
		CreditCardNumber,
		SUBSTRING(ExpirationDate,1,2) AS ExpirationMonth,
		SUBSTRING(ExpirationDate,3,2) AS ExpirationYear,
		PaymentMethodInstance,
		FirstName,
		LastName,
		Count(*) AS SubsCount,
		0 AS RefundStatus
--into	#insertedrefunds
FROM	#cancelsubs
GROUP BY CreditCardNumber,
		ExpirationDate,
		PaymentMethodInstance,
		FirstName,
		LastName

insert into resolvecreditcardrefunddetails
SELECT	Instance,
		TransID,
		CreditCardNumber
FROM	#cancelsubs

--report of success/failure of refunding
--select creditcardnumber,count(*) from resolvecreditcardrefund group by creditcardnumber
--select * from #insertedrefunds
SELECT		rccr.*
FROM		resolvecreditcardrefund rccr
LEFT JOIN	#insertedrefunds ir
				ON ir.CreditCardNumber = rccr.CreditCardNumber
				AND	ir.Price = rccr.Price
				AND	ir.FirstName = rccr.FirstName
				AND ir.LastName = rccr.LastName
WHERE		ir.CreditCardNumber is not null


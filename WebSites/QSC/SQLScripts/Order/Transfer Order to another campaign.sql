USE QSPCanadaOrderManagement
GO

/*
SELECT	b.CampaignID, b.OrderQualifierID, b.statusinstance, b.OrderID, b.Date, cod.isshippedtoaccount, cod.producttype, cod.invoicenumber, *
FROM	CustomerOrderHeader coh
join	CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
join	Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
where	b.campaignid in (106572, 106558)
and		b.orderqualifierid not in (39007)
order by b.campaignid, coh.instance
*/

SELECT	coh.Instance AS CustomerOrderHeaderInstance,
		CASE b.CampaignID WHEN 106572 THEN 106558 END CampaignID,
		CASE b.AccountID WHEN 10598 THEN 9908  END AccountID,
		b.CampaignID OldCampaignID
INTO	#OrdersToTransfer
FROM	CustomerOrderHeader coh
join	Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
where	b.orderid = 12731531
and		b.orderqualifierid not in (39007)

SELECT	*
FROM	#OrdersToTransfer
order by campaignid

--Only can do if Invoices/Statement have not been printed
SELECT		inv.Is_Printed, *
FROM		QSPCanadaFinance..Invoice inv
JOIN		Batch b
				ON	b.OrderID = inv.Order_ID
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.[Date]
JOIN		#OrdersToTransfer o
				ON	o.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	QSPCanadaFinance..StatementInvoice si
				ON	si.InvoiceID = inv.Invoice_ID
LEFT JOIN	QSPCanadaFinance..StatementInvoiceOnline sio
				ON	sio.InvoiceID = inv.Invoice_ID
LEFT JOIN	QSPCanadaFinance..StatementInvoiceCustSvc sic
				ON	sic.InvoiceID = inv.Invoice_ID

SELECT			*
FROM			QSPCanadaFinance..Statement s
LEFT JOIN		QSPCanadaFinance..StatementPrintRequest spr ON spr.StatementID = s.StatementID
JOIN			#OrdersToTransfer o ON (o.CampaignID = s.CampaignID OR o.OldCampaignID = s.CampaignID)
ORDER BY		s.StatementID DESC

BEGIN TRAN

UPDATE	B
SET		CampaignID = o.CampaignID,
		AccountID = o.AccountID,
		ShipToAccountID = o.AccountID
FROM	CustomerOrderHeader coh
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o
			ON	o.CustomerOrderHeaderInstance = coh.Instance

UPDATE	coh
SET		CampaignID = o.CampaignID,
		AccountID = o.AccountID
FROM	CustomerOrderHeader coh
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance

SELECT	*
FROM	QSPCanadaFinance..Adjustment adj
JOIN	Batch b
			ON	b.OrderID = adj.Order_ID
JOIN	CustomerOrderHeader coh
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance

UPDATE	adj
SET		Campaign_ID = o.CampaignID
FROM	QSPCanadaFinance..Adjustment adj
JOIN	Batch b
			ON	b.OrderID = adj.Order_ID
JOIN	CustomerOrderHeader coh
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance

SELECT	*
FROM	QSPCanadaFinance..Payment pmt
JOIN	Batch b
			ON	b.OrderID = pmt.Order_ID
JOIN	CustomerOrderHeader coh
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance

UPDATE	pmt
SET		Campaign_ID = o.CampaignID,
		ACCOUNT_ID = o.AccountID
FROM	QSPCanadaFinance..Payment pmt
JOIN	Batch b
			ON	b.OrderID = pmt.Order_ID
JOIN	CustomerOrderHeader coh
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance

SELECT	*
FROM	Teacher t
JOIN	Student s
			ON	s.TeacherInstance = t.Instance
JOIN	CustomerOrderHeader coh
			ON	coh.StudentInstance = s.Instance
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance

UPDATE	t
SET		AccountID = o.AccountID
FROM	Teacher t
JOIN	Student s
			ON	s.TeacherInstance = t.Instance
JOIN	CustomerOrderHeader coh
			ON	coh.StudentInstance = s.Instance
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance

UPDATE	inv
SET		Account_ID = o.AccountID
FROM	QSPCanadaFinance..INVOICE inv
JOIN	Batch b
			ON	b.OrderID = inv.Order_ID
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance
	
COMMIT

/* If moving online orders from a CA that had a landed order. Need to rerun landed order reports if it isn't cancelled.
begin tran
delete onlineordermappingtable
where landedorderid = 12504971
commit tran
*/

/* If moving a landed order
begin tran
delete onlineordermappingtable
where landedorderid = 12731531

exec pr_MergeOnline 12731531

--If original campaign running Cumulative
update	cod
set		delflag = 1
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	b.OrderID = 12731531
AND		cod.ProductType IN (46013, 46014)

--If new campaign running cumulative
declare @OrderID int, @id int, @date datetime, @supplemental int
set @OrderID = 12731531
select @id = id, @date = date, @supplemental = case orderqualifierid when 39001 then 0 else 1 end from Batch where OrderID = @OrderID

declare @SuccessTF int
exec GenerateIncentiveOrders_NonEnvelope_V2 @id,@Date, @supplemental,@SuccessTF OUTPUT, '1'
print @SuccessTF

update QSPCanadaOrderManagement..CustomerOrderDetail set StatusInstance=510
	from QSPCanadaOrderManagement..CustomerOrderDetail ,
		QSPCanadaOrderManagement..CustomerOrderHeader,
		QSPCanadaOrderManagement..Batch
	   where  QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=
			QSPCanadaOrderManagement..CustomerOrderHeader.Instance
		and OrderBatchDate=Date
		and OrderBatchID=id
		and OrderID = @orderid
		and (producttype in (46013, 46014))
		and CustomerOrderDetail.StatusInstance  not in ( 513, 501, 508)
		and (CustomerOrderDetail.StatusInstance  not in (500) OR ProductType IN (46004, 46013, 46014))
		and CustomerOrderDetail.DelFlag = 0

exec pr_ProcessReserveQuantities_ByOrderId_V2 @OrderID

--Regenerate Reports

--If original CA had a landed order come in after it as supplementary due to the order being in the wrong CA
begin tran
update batch
set orderqualifierid = 39001
where orderid = 12755611

--Repeat everything in the Landed order fixup for this Order, but this should be done prior to moving the original order since that moves Teacher records, even if tied to online orders of the original CA

commit tran
*/




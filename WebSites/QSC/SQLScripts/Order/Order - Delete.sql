/************************************
------------------------------------
	Canada Side
------------------------------------
*************************************/

USE [QSPCanadaOrderManagement]
GO

BEGIN TRAN

CREATE TABLE #BatchOrderIDsToDelete ( ID int)

/*UPDATE ID's THAT NEED TO BE MODIFIED*/
INSERT INTO #BatchOrderIDsToDelete
select	orderid
from	Batch
where	OrderID in (
8692583
)

-------------------------------------------------------------------
-- start clearing out stuff

print 'Deleteing OrderInEnvelopeMap'
delete from OrderInEnvelopeMap where EnvelopeID in
(
	select instance from envelope, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)

print 'Deleteing envelope'
delete from envelope where instance in
(
	select instance from envelope, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)

print 'Deleting CustomerRemitHistory'
delete from CustomerRemitHistory where instance in 
(
	select CustomerRemitHistoryInstance
	from customerorderdetailremithistory where customerorderheaderinstance in
	(
		select instance from customerorderheader, batch
			where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
	)
)

print 'Deleteing customerorderdetailremithistory'
delete from customerorderdetailremithistory where customerorderheaderinstance in
(
	select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)


print 'Deleteing customerorderdetail'
delete from customerorderdetail where customerorderheaderinstance in
(
	select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)

print 'Deleting CreditCardBatch'
delete from CreditCardBatch where ID IN
(
	select batchID
	from creditcardpayment where customerpaymentheaderinstance in
	(
		select instance from customerpaymentheader where customerorderheaderinstance in
		(
			select instance from customerorderheader, batch
			where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
		)
	)
)

print 'Deleting creditcardpayment'
delete from creditcardpayment where customerpaymentheaderinstance in
(
	select instance from customerpaymentheader where customerorderheaderinstance in
	(
		select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
	)
)

print 'Deleting CustomerPaymentDetail'
delete from CustomerPaymentDetail where CustomerPaymentHeaderInstance in
(
	select instance from customerpaymentheader where customerorderheaderinstance in
	(
		select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
	)
)

print 'Deleting customerpaymentheader'
delete from customerpaymentheader where customerorderheaderinstance in
(
	select instance from customerorderheader, batch
	where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)

print 'Deleting InternetOrderID'
delete from InternetOrderID where CustomerOrderHeaderInstance in
(
	select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)

print 'Deleting LandedOrder'
delete from LandedOrder where CustomerOrderHeaderInstance in
(
	select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)

print 'Deleting Teacher'

delete from Teacher where instance in
(
	select TeacherInstance from Student where instance in
	(
		select StudentInstance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
	)
)
and DateCreated >= '2014-10-18'

print 'Deleting Student'

delete from Student where instance in
(
	select StudentInstance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)
and DateCreated >= '2014-10-18'

print 'Deleting Customer'

delete from Customer where instance in
(
	select CustomerBillToInstance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)
and ChangeDate >= '2014-10-18'

print 'Deleteing ordermappingtable'

delete from OnlineOrderMappingTable where LandedOrderID in (select id from #BatchOrderIDsToDelete)

print 'Deleteing BatchDistributionCenter'

delete from bdc
from BatchDistributionCenter bdc
join Batch b on b.ID = bdc.BatchID and b.Date = bdc.BatchDate
where b.OrderID in (select id from #BatchOrderIDsToDelete)

print 'Deleteing customerorderheader'

delete from customerorderheader where instance in
(
	select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid in (select id from #BatchOrderIDsToDelete)
)

print 'deleting batch'
delete from batch where orderid in (select id from #BatchOrderIDsToDelete)

EXEC pr_cleanprintqueue 	8692583

ROLLBACK
--COMMIT


/************************************************
------------------------------------------------
	GA Side
------------------------------------------------
************************************************/

USE GA
GO
BEGIN TRAN

Create table #CustomerOrderIDsToUpdate ( ID int)
Create table #SQLIDsToUpdate ( ID int)

/*UPDATE ID's THAT NEED TO BE MODIFIED*/
INSERT INTO #CustomerOrderIDsToUpdate
select	co.CustomerOrderID
from	core.CustomerOrder co
join	core.Tote t on t.ToteID = co.ToteIDContract
join	core.supertotetote stt on stt.toteid = t.toteid
join	core.supertote st on st.supertoteid = stt.supertoteid
join	core.sapsqlid s on s.sourceid = st.supertoteid and s.SQLIDTypeID = 6
where	s.sqlid in (
8692583
)

INSERT INTO #SQLIDsToUpdate 
select	sqlid
from	sapsqlid
where	SQLIDTypeID = 6
and		sqlid in (
8692583
)

-------------------------------------------------------------------
--Set SAPTransmittedDate to null
/*UPDATE so
SET SAPTransmittedDate = NULL
FROM Core.SalesOrder so with(nolock)
INNER JOIN Core.SAPSQLID s with(nolock) ON s.SourceID = so.SalesOrderID AND s.SQLIDTypeID = 4 /* SalesOrder */
INNER JOIN Core.SalesOrderDetail sod with(nolock) ON sod.SalesOrderID = so.SalesOrderID AND so.SAPTransmittedDate IS NOT NULL AND so.ArrivalTypeID in (93, 94, 97, 70, 71, 72, 73)
INNER JOIN Core.CustomerOrderDetail cod with(nolock) ON cod.SalesOrderDetailID = sod.SalesOrderDetailID
INNER JOIN Core.CustomerSubOrder  cso with(nolock) ON cso.CustomerSubOrderID = cod.CustomerSubOrderID
INNER JOIN Core.CustomerOrder co with(nolock) ON co.CustomerOrderID = cso.CustomerOrderID   /* ReadyToExportFFS */
where s.SQLID  IN
(
	SELECT ID FROM #SQLIDsToUpdate
)*/
UPDATE st
set SAPTransmittedDate = null
from core.SuperTote st with(nolock)
join core.SAPSQLID s with(nolock) on s.SourceID = st.SuperToteID
where s.sqlid In
(
	SELECT ID FROM #SQLIDsToUpdate
)

--set the customerorderStateID to 38.  Need to queury on CustomerOrderID and not SQLID because a customer order may be tied to two SQLID's
update core.CustomerOrder
set CustomerOrderStateID = 38
where CustomerOrderStateID <> 38
AND CustomerOrderID IN
(
	SELECT ID FROM #CustomerOrderIDsToUpdate
) 


ROLLBACK
--COMMIT
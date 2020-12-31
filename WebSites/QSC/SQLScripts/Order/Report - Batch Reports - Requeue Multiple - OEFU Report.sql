USE [QSPCanadaOrderManagement]

begin tran

DECLARE @OrderID INT

DECLARE c cursor for 
	SELECT distinct	b.OrderID
	FROM	QSPCanadaOrderManagement..batch b
	where b.orderid in ()
	/*JOIN	QSPCanadaOrderManagement..customerorderheader	coh  ON coh.orderbatchid = b.id and coh.orderbatchdate = b.date
	JOIN	QSPCanadaOrderManagement..CustomerOrderDetail	cod  ON cod.customerorderheaderinstance = coh.instance
	join Incident i on i.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and i.TransID = cod.TransID
	join IncidentAction ia on ia.IncidentInstance = i.IncidentInstance
	where b.Date >= '2014-07-01'
	and ia.ActionInstance in (150, 151, 22)
	and b.OrderQualifierID in (39001, 39002)
	ORDER BY b.OrderID*/
	
Open c
fetch NEXT from c into @OrderID;

while @@fetch_status = 0
BEGIN

		 Update  QspCanadaOrderManagement.dbo.[ReportRequestBatch_OrderEntryFollowupReport] 
		 Set createdate = getdate(), QUEUEDATE = NULL,RUNDATESTART= NULL,FILENAME =NULL 
		 where reportrequestbatchid  IN  ( select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch]
		 where batchorderid  = @OrderID     )  

	fetch NEXT from c into @OrderID
END

CLOSE c
DEALLOCATE c





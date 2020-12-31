USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ClearOutTestBatch]    Script Date: 06/07/2017 09:19:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[ClearOutTestBatch]
	@orderid int
as

-- start clearing out stuff
delete from customer where instance in
(
	select customerbilltoinstance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid=@orderid
)
print 'Deleteing customer'

delete from OrderInEnvelopeMap where EnvelopeID in
(
	select instance from envelope, batch
		where orderbatchdate=date and orderbatchid=id and orderid=@orderid
)

print 'Deleteing envelope'
delete from envelope where instance in
(
	select instance from envelope, batch
		where orderbatchdate=date and orderbatchid=id and orderid=@orderid
)

print 'Deleteing customerorderdetailremithistory'

delete from customerorderdetailremithistory where customerorderheaderinstance in
(
	select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid=@orderid
)

print 'Deleteing customerorderdetail'

delete from customerorderdetail where customerorderheaderinstance in
(
	select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid=@orderid
)


delete from creditcardpayment where customerpaymentheaderinstance in
(
	select instance from customerpaymentheader where customerorderheaderinstance in
	(
		select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid=@orderid
	)

)


delete from customerpaymentheader where customerorderheaderinstance in
(
	select instance from customerorderheader, batch
	where orderbatchdate=date and orderbatchid=id and orderid=@orderid
)
print 'Deleteing customerorderheader'

delete from customerorderheader where instance in
(
	select instance from customerorderheader, batch
		where orderbatchdate=date and orderbatchid=id and orderid=@orderid
)
/*
delete from customerremithistory where instance not in
(
select customerremithistoryinstance from customerorderdetailremithistory
)
*/
print 'deleting batch'
delete from batch where orderid = @orderid
GO

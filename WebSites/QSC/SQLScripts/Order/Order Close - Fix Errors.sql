select *
from QSPCanadaOrderManagement.dbo.OrderClosingLog
where orderid = 908952

select b.IsStaffOrder, camp.IsStaffOrder, ioi.internetorderid
from customerorderdetail cod
join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join batch b on id = orderbatchid and date = orderbatchdate
join customerpaymentheader cph on cph.customerorderheaderinstance = coh.instance
join creditcardpayment ccp on CCP.CustomerPaymentHeaderInstance = cph.Instance
join qspcanadacommon..campaign camp on camp.id = coh.campaignid
join internetorderid ioi on ioi.customerorderheaderinstance = coh.instance
where b.orderid = 10023616

update batch
set IsStaffOrder = 1
where orderid = 10023616

select *
from customerorderdetail cod
join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join batch b on id = orderbatchid and date = orderbatchdate
join customerpaymentheader cph on cph.customerorderheaderinstance = coh.instance
join creditcardpayment ccp on CCP.CustomerPaymentHeaderInstance = cph.Instance
where b.orderid in (10031397,10031114,10029026,10029015)
and ccp.statusinstance = 19001

select *
from customerorderdetail cod
join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join batch b on id = orderbatchid and date = orderbatchdate
where b.orderid in (10027757)

update cod
set magazinetitle = 'Readers Digest'
from customerorderdetailremithistory cod
where customerorderheaderinstance = 11464533 and transid = 1


USE [QSPCanadaOrderManagement]
GO

DECLARE	@return_value int,
		@RetVal int

EXEC	@return_value = [dbo].[spPostCloseVerification]
		@OrderId = 10023464,
		@RetVal = @RetVal OUTPUT

SELECT	@RetVal as N'@RetVal'

SELECT	'Return Value' = @return_value

GO


USE [QSPCanadaOrderManagement]
GO

DECLARE	@return_value int,
		@RetVal int

EXEC	@return_value = [dbo].[spPreCloseVerification]
		@OrderId = 10023464,
		@RetVal = @RetVal OUTPUT

SELECT	@RetVal as N'@RetVal'

SELECT	'Return Value' = @return_value

GO

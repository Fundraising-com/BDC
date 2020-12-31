USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetAllStaffOrderWithTimeTitles]    Script Date: 06/07/2017 09:19:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAllStaffOrderWithTimeTitles] @Fromdate DateTime, 
							  @Todate DateTime,
							  @AccountId Int =0, 
							  @CampaignId Int =0, 
							  @SortBy Varchar(10) = 'FM'
AS

Declare 	@SelectFrom	Varchar(8000),
		@GroupBy	Varchar(1000),
		@OrderBy          Varchar(1000)
		

Set @SelectFrom='
Select 	
	b.OrderId ,
	Convert(Varchar(10),b.DateReceived,101) DateReceived,
	b.OrderQualifierId,
	cd.Description,
	b.CampaignId,
	c.IsstaffOrder,
	a.id,
	a.Name,
	h.PaymentMethodInstance,
	cdpay.Description PaidBy,
	od.productname,
	od.productcode,
	od.CatalogPrice,
	od.Recipient,
	cust.FirstName+'' ''+cust.LastName CustomerName,
	f.FMID,f.FirstName+'' ''+f.LastName FMName, 
	cont.FirstName + '' '' + Cont.Lastname  ContactName ,
	Max(Cont.Id) ContactId 
From 	QSPCanadaOrderManagement..customerorderdetail od,
	QSPCanadaOrderManagement..batch b, 
	QSPCanadaOrderManagement..customerorderheader h Left Join 
	QSPCanadaOrderManagement..Customer Cust ON h.CustomerBillToInstance = cust.Instance,
	QSPcanadaCommon..codedetail cd,
	QSPcanadaCommon..codedetail cdPay,
	QSPCanadaCommon..fieldmanager f,
	QSPCanadaCommon..Caccount a,
	QSPCanadaCommon..campaign c Left Join
	QSPCanadaCommon.dbo.Contact cont ON c.ShipToCampaignContactID = cont.ID
Where b.id=OrderBatchId
And b.date=orderBatchdate
And b.campaignid=c.id
And h.Instance=od.CustomerOrderHeaderInstance
And c.FMID=f.FMID
And a.Id = B.AccountID
And od.delflag=0
And b.statusInstance <> 40005
And c.IsstaffOrder=1
And od.statusInstance =515 
And b.OrderQualifierId=CD.Instance
And h.PaymentMethodInstance=CDPay.Instance
And b.date between '''+CONVERT(nvarchar, @Fromdate,101) +'''  AND '''+ CONVERT(nvarchar, @Todate,101) +''' '


IF(IsNull(@CampaignId,0) <> 0)
Begin
	Set @SelectFrom = @SelectFrom + ' And  c.ID = ' + Cast(@CampaignID as varchar(7)) + ' '
End

IF(isNull(@AccountId,0)<> 0)
Begin
	Set @SelectFrom = @SelectFrom + ' And  b.AccountID = ' + Cast(@AccountId as varchar(7)) + ' '
End


Set @GroupBy='
Group By b.OrderId,
	b.DateReceived,
	b.OrderQualifierId,
	cd.Description,
	CDpay.Description,
	b.CampaignId,
	c.IsstaffOrder,
	a.id,
	a.Name,
	od.productname,
	od.productcode,
	od.CatalogPrice,
	od.Recipient,
	f.FMID,
	f.FirstName,
	f.LastName, 
	cont.FirstName ,
	cont.Lastname,
	h.PaymentMethodInstance,
	cust.FirstName,
	cust.LastName'


Set @SelectFrom= @SelectFrom+' '+@GroupBy

If @SortBy = 'ACCOUNT'
Begin
     Set @OrderBy = ' Order by b.AccountId,b.CampaignId,b.OrderId '
End

If @SortBy = 'ACCOUNTNAME'
Begin
      Set @OrderBy = ' Order by a.Name,b.CampaignId,b.OrderId '
End

If @SortBy = 'FM'
Begin
     Set @OrderBy = ' Order by f.FMID,a.Name,b.CampaignId,b.OrderId '
End
   
Set @SelectFrom = @SelectFrom + ' '+ @OrderBy

Exec (@SelectFrom)
GO

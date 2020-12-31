USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[DeadOrderReport]    Script Date: 06/07/2017 09:19:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE   [dbo].[DeadOrderReport]	  ( @StartDate                 DateTime,
						    @EndDate                  DateTime,
						    @OrderId		Int,
						    @AccountId		Int,
						    @CampaignId		Int,
						    @ErrorType		Varchar(20)
						 )
As
If  Upper(@ErrorType) in ( 'ALL' , 'ADDRESS', 'CREDIT CARD')
Begin
		
		Declare @TabErrorOrderItem Table(
		     Tindex		Int Identity,
		      ErrorType		Varchar(30),
		      CA			Int, 
		      Account 		Int,
		      OrderId		Int, 
		      TitleCode		Varchar(30), 
		      TitleName		Varchar(50), 
		      CatalogPrice		Numeric(10,2) , 
		      ErrorDesc		Varchar(30)
		    
		    )	

Insert into @TabErrorOrderItem
Select 
	'ADDRESS' Type   , 
	batch.campaignid,
	batch.accountid,
	batch.orderid,
       	cod.ProductCode as TitleCode,
	cod.ProductName as MagazineTitle,
	cod.Catalogprice,
	Case  	when cod.ProductCode = '9999'       then 'Could not read Catalogue Code - Illegible Item'
	          	when Isnull(cod.Recipient,'')= ''         then 'Missing Recipient Name'
		when (IsNull(CustBill.Address1,'') = '' and
			Isnull(CustBill.City,'') = '' )  or  
		         	(IsNull(CustBill.State,'') = '' and
		   	Isnull(CustBill.Zip,'') = '')  or  
	           		(IsNull(CustBill.Address1,'') = '' and
		   	Isnull(CustBill.State,'') = '')  or  
	           		(IsNull(CustBill.City,'') = '' and
		   	Isnull(CustBill.Zip,'') = '')     then 'Address Missing or Incomplete'
	      	when IsNull(CustBill.Address1,'') = ''  then 'Missing Street Address'
	      	when Isnull(CustBill.City,'') = ''      then 'Missing City'
	      	when Isnull(CustBill.State,'')= ''      then 'Missing Province'
              	when Isnull(CustBill.Zip,'')= ''        then 'Missing Postal Code'
	      else 'Customer Status Error'
	      end  as Description
 From 	QSPCanadaOrderManagement..CustomerOrderHeader 	as coh,
	QSPCanadaCommon..Campaign 				as ca,
	QSPCanadaOrderManagement..CustomerOrderDetail 	as cod ,
	QSPCanadaOrderManagement..Customer 		as CustBill,
	QSPCanadaOrderManagement..Batch 			as batch
 Where coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.CampaignID 		= ca.ID
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= batch.Date
   and cod.ProductType in  (46001,46006)
   and ( cod.Recipient is null 		or cod.Recipient = '' or
	 CustBill.Address1 is null 	or CustBill.Address1 = '' or
	 CustBill.City is null 		or CustBill.City = '' or
	 CustBill.State is null 	or CustBill.State   = '' or
	 CustBill.Zip is null 		or CustBill.Zip = '' or
	 CustBill.StatusInstance  = 301
	 OR cod.ProductCode = '9999' --illegible items
          ) 
  and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
  and batch.OrderID 		= isnull(@OrderID,batch.OrderID)
 and batch.AccountId		=IsNull(@AccountId,Batch.AccountId)
 and batch.CampaignId		=IsNull(@CampaignId,Batch.CampaignId)
 and cast(convert(varchar(10),batch.date,101)as datetime) between @StartDate  and @EndDate


Insert into @TabErrorOrderItem
Select 
	'CREDIT CARD' Type   ,    
	batch.campaignid,batch.accountid,batch.orderid,
	cod.ProductCode 	as TitleCode,
	cod.productName 	as MagazineTitle,  
	cod.price 		as Catalogprice,
	CD.Description
 From 	QSPCanadaOrderManagement..CreditCardPayment 	as cp,
      	QSPCanadaOrderManagement..CustomerPaymentHeader  as ph,
	QSPCanadaOrderManagement..CustomerOrderHeader 	as coh,
	QSPCanadaCommon..Campaign 				as ca,
	QSPCanadaOrderManagement..CustomerOrderDetail 	as cod ,
	QSPCanadaOrderManagement..Customer 		as CustBill,
	QSPCanadaOrderManagement..Batch 			as batch,
	QSPCanadaCommon..CodeDetail CD
 Where cp.CustomerPaymentHeaderInstance = ph.Instance
   and ph.CustomerOrderHeaderInstance  = coh.Instance	
   and coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.CampaignID 		= ca.ID
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= batch.Date 
   and cp.StatusInstance        = CD.Instance
   and coh.PaymentMethodInstance in (50003,50004) -- credit card payments
   and cp.StatusInstance in ( 19001,19002,19005) -- CC payment fail
   and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
  and batch.OrderID 		= isnull(@OrderID,batch.OrderID)
  and batch.AccountId		=IsNull(@AccountId,Batch.AccountId)
  and batch.CampaignId		=IsNull(@CampaignId,Batch.CampaignId)
  and cast(convert(varchar(10),batch.date,101)as datetime) between @StartDate  and @EndDate

If  Upper(@ErrorType) = 'ALL'
Begin
	Select * from @TabErrorOrderItem
	order by ErrorType, CA, Account,OrderId
End
Else
	Select * from @TabErrorOrderItem
	Where ErrorType =  Upper(@ErrorType )
	order by ErrorType, CA, Account,OrderId			


End
GO

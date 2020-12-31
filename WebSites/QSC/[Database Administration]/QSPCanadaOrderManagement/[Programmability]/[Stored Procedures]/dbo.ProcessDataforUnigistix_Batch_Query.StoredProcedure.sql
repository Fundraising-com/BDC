USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ProcessDataforUnigistix_Batch_Query]    Script Date: 06/07/2017 09:20:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[ProcessDataforUnigistix_Batch_Query] @BatchID int, @BatchDate datetime    AS

Select 	Convert(varchar(10),Batch.Date,101) as BatchDate,
	Batch.ID as BatchID,
        Batch.OrderID,
	Batch.OrderTypeCode,
	Convert(varchar(10),Batch.DateReceived,101) as DateOrderReceived,
	Convert(varchar(10),Batch.DateCreated,101) as DateOrderCreated,
	Batch.ContactFirstName,
	Batch.ContactLastName,
	Batch.ContactPhone,
	Batch.Comment,
	Campaign.ID as CampaignID,
	Campaign.Lang,
	FM.FMID,
	FM.FirstName,
	FM.LastName,
	FM.Email,
	Account.id as AccoundID,
	Account.name as AccountName,
	Account.Address1 as AccountAddress1,
	Account.Address2 as AccountAddress2,
	Account.City  as AccountCity,
	Account.State as AccounState,
	Account.ZIP   as AccounZIP,
	Envelope.Instance as EnvelopeID,
	Teacher.LastName as TeacherLastName,
	Teacher.MiddleInitial as TeacherMiddleName,
	Teacher.Classroom,
	Participant.FirstName  	  as ParticipantFirstName,
	Participant.LastName 	  as ParticipantLastName,
	CustomerInfo.FirstName    as CustomerFirstName,
	CustomerInfo.LastName 	  as CustomerLastName,
	CustomerInfo.Address1,
	CustomerInfo.Address2,
	CustomerInfo.City,
	CustomerInfo.State,
	CustomerInfo.Zip,
	CustomerInfo.Phone,
	CustomerInfo.StatusInstance as CustomerStatus,
	OrderDetail.CustomerOrderHeaderInstance as OrderHeaderInstance,
	OrderDetail.TransID,
	OrderDetail.Recipient,
	OrderDetail.ProductCode,
	PricingDetail.OracleCode,
	OrderDetail.ProductName,
	OrderDetail.Quantity,
	Subscription.NumberofIssues,
	OrderDetail.Price,
	OrderDetail.CatalogPrice,
	OrderDetail.Renewal,
	OrderDetail.StatusInstance as OrderDetailStatus,
	OrderDetail.PriceOverrideID,
	OrderDetail.ProductType
 from 	QSPCanadaOrderManagement..Batch as Batch,
	QSPCanadaOrderManagement..Account as Account,
	QSPCanadaCommon..Campaign as Campaign,
	QSPCanadaCommon..FieldManager as FM,
	QSPCanadaOrderManagement..Envelope As Envelope,
	QSPCanadaOrderManagement..Teacher As Teacher,
	QSPCanadaOrderManagement..Student as Participant,
	QSPCanadaOrderManagement..customerorderheader as OrderHeader, 
	QSPCanadaOrderManagement..customerorderdetail as OrderDetail
	Left outer Join QSPCanadaOrderManagement..customerorderdetailRemitHistory as Subscription
	ON OrderDetail.CustomerOrderHeaderInstance = Subscription.CustomerOrderHeaderInstance
	and OrderDetail.TransID  = Subscription.TransID, 
	QSPCanadaOrderManagement..OrderInEnvelopeMap as Orders,
	QSPCanadaOrderManagement..Customer as CustomerInfo,
	QSPCanadaProduct..Pricing_Details as PricingDetail
Where Campaign.id   = batch.campaignid 
and   Campaign.FMID = FM.FMID  
and account.id = batch.accountid  
and envelope.orderbatchid=batch.id  
and envelope.orderbatchdate=date  
and envelope.instance = envelopeid      
and OrderHeader.instance = Orders.customerorderheaderinstance  
and envelope.teacherinstance=teacher.instance 
and StudentInstance=Participant.Instance 
and Participant.Teacherinstance=teacher.instance 
and OrderDetail.customerorderheaderinstance = OrderHeader.instance 
and CustomerInfo.Instance = OrderHeader.customerbilltoinstance
and OrderDetail.PricingDetailsID  = PricingDetail.MagPrice_Instance
--and batch.id 	= @BatchID
--and batch.date 	= @BatchDate
and orderid=2828 
and orderheader.instance=16 
Order by Batch.OrderID, Envelope.Instance, Participant.Lastname, ProductType 
FOR XML AUTO,ELEMENTS
GO

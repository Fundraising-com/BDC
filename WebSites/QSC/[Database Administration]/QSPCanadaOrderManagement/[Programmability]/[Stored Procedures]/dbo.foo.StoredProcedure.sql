USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[foo]    Script Date: 06/07/2017 09:19:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[foo]
as
select 
	Date as BatchDate,
	Batch.ID as BatchID,
	batch.CampaignID,
	batch.accountid,
--	account.name,
	Envelope.ReportedEnvelopeAmount,
	Teacher.LastName,
	MiddleInitial,
	Classroom,
	Participant.lastname,
	participant.firstname,
	MailingAddress.Address1,
	MailingAddress.Address2,
	MailingAddress.City,
	MailingAddress.State,
	MailingAddress.Zip,
	MailingAddress.Phone,
	MailingAddress.StatusInstance,
	MagazineOrders.Envelopeid,
	MagOrder.Instance,
	MagOrderDetail.Recipient,
	MagOrderDetail.ProductCode,
	MagOrderDetail.ProductName,
	MagOrderDetail.Quantity,
	MagOrderDetail.Price,
	MagOrderDetail.Renewal,
	MagOrderDetail.StatusInstance,
	MagOrderDetail.PriceOverrideID
--	Teacher.LastName as lastName
 from batch,account,Envelope,Teacher,
Student as Participant,
customerorderheader MagOrder, 
customerorderdetail as MagOrderDetail,
OrderInEnvelopeMap as MagazineOrders, 
customer as MailingAddress
--teacher 
where orderid=2905 
and account.id = batch.accountid
and envelope.orderbatchid=batch.id
--and coh.orderbatchdate=date and coh.orderbatchid=batch.id
and envelope.orderbatchdate=date
and envelope.instance = envelopeid
and MagOrder.instance = MagazineOrders.customerorderheaderinstance
and envelope.teacherinstance=teacher.instance
and StudentInstance=Participant.Instance
and Participant.Teacherinstance=teacher.instance
and MailingAddress.instance = MagOrder.customerbilltoinstance
and MagOrderDetail.customerorderheaderinstance = MagOrder.instance

order by Envelope.Instance, Participant.Lastname

--and teacher.instance=envelope.teacherinstance
For xml auto,elements
GO

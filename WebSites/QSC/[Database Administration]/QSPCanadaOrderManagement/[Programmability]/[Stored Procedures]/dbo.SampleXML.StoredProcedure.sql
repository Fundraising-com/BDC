USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[SampleXML]    Script Date: 06/07/2017 09:20:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  procedure [dbo].[SampleXML]
as
select
	Date as BatchDate,
	Batch.ID as BatchID,
	batch.CampaignID,
	batch.accountid,
	account.name,
	--campaign.id,
	campaign.lang,
	cp.isprecollect,
	program.name,
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
	Orders.Envelopeid,
	OrderHeader.Instance,
	OrderDetail.TransID,
	OrderDetail.Recipient,
	OrderDetail.ProductCode,
	OrderDetail.ProductName,
	OrderDetail.Quantity,
	OrderDetail.Price,
	OrderDetail.Renewal,
	OrderDetail.StatusInstance,
	OrderDetail.PriceOverrideID,
	OrderDetail.ProductType
--	Teacher.LastName as lastName
 from batch,account,
qspcanadacommon..campaign as Campaign,
qspcanadacommon..campaignprogram as CP,
qspcanadacommon..program as Program,
Envelope,Teacher,
Student as Participant,
customerorderheader as OrderHeader, 
customerorderdetail as OrderDetail,
OrderInEnvelopeMap as Orders, 
customer as MailingAddress
--teacher 
where orderid=2905 
and orderheader.instance=2398
and campaign.id=batch.campaignid
and cp.campaignid=campaign.id
and Program.ID= cp.programid
and account.id = batch.accountid
and envelope.orderbatchid=batch.id
--and coh.orderbatchdate=date and coh.orderbatchid=batch.id
and envelope.orderbatchdate=date
and envelope.instance = envelopeid
and OrderHeader.instance = Orders.customerorderheaderinstance
and envelope.teacherinstance=teacher.instance
and StudentInstance=Participant.Instance
and Participant.Teacherinstance=teacher.instance
and MailingAddress.instance = OrderHeader.customerbilltoinstance
and OrderDetail.customerorderheaderinstance = OrderHeader.instance

order by Envelope.Instance, Participant.Lastname, ProductType

--and teacher.instance=envelope.teacherinstance
For xml auto,elements
GO

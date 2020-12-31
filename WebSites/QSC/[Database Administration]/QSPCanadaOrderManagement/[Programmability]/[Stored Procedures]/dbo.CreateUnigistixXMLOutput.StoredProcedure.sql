USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateUnigistixXMLOutput]    Script Date: 06/07/2017 09:19:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE                    procedure [dbo].[CreateUnigistixXMLOutput]
	@orderid int

as
select 
Convert(varchar(10),BatchDate,101) as BatchDate,
	BatchID,
        Batch.OrderID,
	Batch.OrderTypeCode,
	Convert(varchar(10),DateOrderReceived,101) as DateOrderReceived,
	Convert(varchar(10), DateOrderCreated,101) as DateOrderCreated,
	Isnull(Batch.BilltoContactFirstName,'') as BilltoContactFirstName,
	Isnull(Batch.BilltoContactLastName,'') as BilltoContactLastName,
	Isnull(Batch.BilltoContactPhone,'') as BilltoContactPhone,
	isnull(Batch.Comment,'') as Comment,
	Batch.CampaignID,
	Batch.Lang,
	Batch.FMID,
	Batch.FMFirstName,
	Batch.FMLastName,
	Batch.Email,
	BillToAccountID,
	BillToAccountName,
	BillToAccountAddress1,
	isnull(BillToAccountAddress2,'') as BillToAccountAddress2,
	BillToAccountCity,
	BillToAccountState,
	BillToAccountZip,
--	IsNULL(BillToAccountPhone,'') as BillToAccountPhone,
	isnull(ShipToContactFirstName,'') as ShipToContactFirstName,
	IsNull(ShipToContactLastName,'') as ShipToContactLastName,
	Isnull(ShipToContactPhone,'') as ShipToContactPhone,
	ShipToAccountID,
	ShipToAccountName,
	ShipToAccountAddress1,
	isnull(ShipToAccountAddress2,'') as ShipToAccountAddress2,
	ShipToAccountCity,
	ShipToAccountState,
	ShipToAccountZip,
--	isnull(ShipToAccountPhone,'') as ShipToAccountPhone,
	Teacher.TeacherInstance  as TeacherInstance,
	Teacher.TeacherLastname,
	Teacher.TeacherMiddle as TeacherMiddleName,
	Teacher.Classroom,
	Teacher.EnvelopeID as EnvelopeID,
	Participant.StudentInstance as ParticipantInstance,
	Participant.StudentFirstName ParticipantFirstName,
	Participant.StudentLastName as ParticipantLastName,
	isnull(Participant.ReadersPremiums,0) as ReadersPremiums,
	isnull(Participant.OtherPremiums,0) as OtherPremiums,
	CustomerInfo.Instance as CustomerInstance,
	Isnull(CustomerInfo.FirstName, '') as CustomerFirstName,
	Isnull(CustomerInfo.LastName,'') as CustomerLastName,
	Isnull(CustomerInfo.Address1,'') as Address1,
	Isnull(CustomerInfo.Address2,'') as Address2,
	Isnull(CustomerInfo.City,'') as City,
	Isnull(CustomerInfo.State,'') as State,
	Isnull(CustomerInfo.Zip,'') as Zip,
	CustomerInfo.StatusInstance as CustomerStatus,
	OrderDetail.customerorderheaderinstance As OrderHeaderInstance,
	OrderDetail.TransID,
	ISNULL(OrderDetail.PaymentStatusInstance,0) as PaymentStatusInstance,
	OrderDetail.Recipient,
	case OrderDetail.Type when 46002 then 'Q'+OrderDetail.ProductCode
		when 46003 then 'Q'+OrderDetail.CatalogProductCode
		Else OrderDetail.ProductCode
	End as ProductCode,
	OrderDetail.ProductName,
	OrderDetail.QuantityOrdered as QuantityOrdered,
	OrderDetail.Renewal,
	OrderDetail.Price,
	Isnull(OrderDetail.SupporterName, '') as SupporterName,
	OrderDetail.PriceOverrideID,
	OrderDetail.CatalogPrice,
	OrderDetail.OracleCode,
	case OrderDetail.Type when 46002 then 'Q'+OrderDetail.CatalogProductCode
		when 46003 then 'Q'+OrderDetail.CatalogProductCode
		Else OrderDetail.CatalogProductCode
	End as CatalogProductCode,
	OrderDetail.Type,
	OrderDetail.LevelCode,
	OrderDetail.StatusInstance,
	OrderDetail.ReplacedItemCode

 	from UnigistixBatch as Batch, 
		UnigistixEnvelopeStaging Teacher,
		UnigistixStudentStaging Participant,
		Student s, UnigistixOrderStaging OrderDetail,
		Customer CustomerInfo,
		CustomerOrderHeader  coh
	where 
		 batch.orderid=OrderDetail.orderid
		and Teacher.envelopeid = OrderDetail.EnvelopeID 
		and Participant.studentinstance=s.instance
		and s.teacherinstance = Teacher.teacherinstance
		and OrderDetail.StudentInstance = Participant.StudentInstance
		and coh.OrderBatchDate = BatchDate
		and Coh.OrderBatchID = BatchID
		and CustomerInfo.Instance = OrderDetail.CustomerInstance
		and coh.Instance = OrderDetail.CustomerOrderHeaderInstance
		and Batch.orderid=@orderid

--and Classroom <> '245'
	order by Teacher.EnvelopeID, Teacher.Classroom, Participant.StudentLastName,
		Participant.StudentFirstName

FOR XML AUTO,ELEMENTS
GO

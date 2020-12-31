USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateXMLOutput2]    Script Date: 06/07/2017 09:19:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_GenerateXMLOutput2]

	@OrderID int
 AS

	

	select Convert(varchar(10),Batch.BatchDate,101) as BatchDate,
		BatchID,
		Batch.OrderID as OrderID,
		isnull(OrderTypeCode,'') as OrderTypeCode,
		Convert(varchar(10),DateOrderReceived,101) as DateOrderReceived,
		Convert(varchar(10),DateOrderCreated,101) as DateOrderCreated,
		isnull(BillToContactFirstName,'') as BilltoContactFirstName,
		isnull(BillToContactLastName,'') as BilltoContactLastName,
		isnull(BillToContactPhone,'') as BillToContactPhone,
		isnull(Comment,'') as Comment,
		isnull(Batch.CampaignID,'') as CampaignID,
		isnull(Batch.Lang,'') as Lang,
		isnull(FMID,'') as FMID,
		isnull(FMFirstName,'') as FMFirstName,
		isnull(FMLastName,'') as FMLastName,
		isnull(Batch.Email,'') as FMEMail,
		isnull(BillToAccountID,'') as BillToAccountID,
		isnull(BillToAccountName,'') as BillToAccountName,
		isnull(BilltoAccountAddress1,'') as BillToAccountAddress1,
		isnull(BilltoAccountAddress2,'') as BillToAccountAddress2,
		isnull(BilltoAccountCity,'') as BillToAccountCity,
		isnull(BilltoAccountState,'') as BillToAccountState,
		isnull(BilltoAccountZip,'') as BillToAccountZip,
		isnull(ShipToContactFirstName,'') as ShipToContactFirstName,
		isnull(ShipToContactLastName,'') as ShipToContactLastName,
		isnull(BillToContactPhone,'')  as ShipToContactPhone,
		isnull(BilltoAccountID,'') as ShipToAccountID,
		isnull(ShipToAccountName,'') as ShipToAccountName,
		isnull(ShipToAccountAddress1,'') as ShipToAccountAddress1,
		coalesce(ShipToAccountAddress2,'') as ShipToAccountAddress2,
		isnull(ShipToAccountCity,'') as ShipToAccountCity,
		isnull(ShipToAccountState,'') as ShipToAccountState,
		isnull(ShipToAccountZip,'') as ShipToAccountZip,
		cast(@orderid as varchar) + '.pdf' as PDFFilename,
		Teacher.Instance as QSPTeacherInstance,
		cast(Teacher.Instance as varchar) + cast(@orderid as varchar) as TeacherInstance,
		'' as TeacherMiddleName,
		isnull(TeacherLastName,'') as TeacherLastName, 
		isnull(Teacher.Classroom,'') as Classroom,
		ParticipantInstance as QSPParticipantInstance,
		cast(ParticipantInstance as varchar) + cast(@orderid as varchar)  as ParticipantInstance,
		isnull(ParticipantFirstName,'') as ParticipantFirstName,
		isnull(ParticipantLastname,'') as ParticipantLastname,
		CustomerInfo.Instance as QSPCustomerInstance,
		cast(CustomerInfo.Instance as varchar) + cast(@orderid as varchar) as CustomerInstance,
		isnull(CustomerInfo.FirstName,'') as CustomerFirstName,
		isnull(CustomerInfo.LastName,'') as CustomerLastName,
		IsNull(CustomerInfo.Address1,'') As Address1,
		IsNull(CustomerInfo.Address2,'') As Address2,
		IsNull(CustomerInfo.City,'') as City,
		IsNull(CustomerInfo.State,'') as State,
		IsNull(CustomerInfo.Zip,'') as Zip,
		CustomerOrderHeaderInstance as OrderHeaderInstance,
		TransID,
		isnull(Recipient,'') as Recipient,
		isnull(ProductCode,'') as ProductCode,
		isnull(ProductName,'') as ProductName,  -- product name fill in w/productdescription
		isnull(QuantityOrdered,0) as QuantityOrdered,
		isnull(Price,0) as Price,
		isnull(CatalogPrice,0) as CatalogPrice,	
		isnull(OracleCode,'') as OracleCode,  -- Per unigistix put product code into oracle code
		isnull(CatalogProductCode,'') as CatalogProductCode,
		isnull(OrderDetail.Type,'') as Type,
		isnull(OrderDetail.QtyShipped,0) as QtyShipped, -- shipped
		coalesce(OrderDetail.ReplacedItemCode,'') as ReplacedItemCode,
		isnull(OrderDetail.ReplacedItemQty,0) as ReplacedItemQty,
		isnull(LevelCode,'') as LevelCode
/*
	 from vw_GetUnigixtisBatchHeader  As Batch,
		vw_GetUnigistixTeacher as Teacher ,
		Student as Participant,
		Customer as CustomerInfo ,
		vw_GetUnigistixOrderDetail as OrderDetail
*/

	from 	##UnigistixOrderStaging  OrderDetail inner join Customer CustomerInfo  on  OrderDetail.CustomerInstance = CustomerInfo.Instance
			inner join vw_UnigistixStudentStaging  Participant on OrderDetail.StudentInstance = Participant.ParticipantInstance
			inner join vw_UnigistixEnvelopeStaging  Teacher on  Participant.TeacherInstance = Teacher.Instance
			inner join vw_UnigistixBatch  Batch on Teacher.orderid =  batch.orderid
			

		--vw_UnigistixBatch as Batch, --join
		--vw_UnigistixEnvelopeStaging as Teacher, 
		--vw_UnigistixStudentStaging as Participant,
		
		--Customer CustomerInfo 
	where 
		/*Batch.orderid=@orderid
		and Teacher.orderid =  batch.orderid
		and OrderDetail.StudentInstance = Participant.ParticipantInstance
		and batch.orderid=OrderDetail.orderid
		and Participant.orderid =  batch.orderid
		and Participant.teacherinstance = Teacher.teacherinstance
		--and coh.OrderBatchDate = BatchDate
		--and Coh.OrderBatchID = BatchID
		and CustomerInfo.Instance = OrderDetail.CustomerInstance
		--and coh.Instance = OrderDetail.CustomerOrderHeaderInstance*/
		Batch.orderid=@OrderID 
	--and Teacher.orderid=Batch.orderid
	----and Teacher.Instance=Participant.TeacherInstance
	--and Participant.ParticipantInstance = OrderDetail.StudentInstance
	--and customerInfo.instance= OrderDetail.CustomerInstance
	--and OrderDetail.OrderID=@OrderID
	order by Teacher.TeacherLastName, Teacher.Classroom, ParticipantLastName,
		ParticipantFirstName
	FOR XML AUTO, ELEMENTS
GO

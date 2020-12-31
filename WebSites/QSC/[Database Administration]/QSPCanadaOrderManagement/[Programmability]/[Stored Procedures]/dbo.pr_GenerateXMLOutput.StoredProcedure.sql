USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateXMLOutput]    Script Date: 06/07/2017 09:19:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_GenerateXMLOutput]

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
		Case isNull(IsQspPrint,1) when 0 then 
			cast(@orderid as varchar) + '.pdf' 
			else
				''  End As PDFFilename,
		Teacher.Instance as QSPTeacherInstance,
		cast(Teacher.Instance as varchar) + cast(@orderid as varchar) as TeacherInstance,
		'' as TeacherMiddleName,
		isnull(TeacherLastName,'') as TeacherLastName, 
		Left(isnull(Teacher.Classroom,''), 10) as Classroom,
		ParticipantInstance as QSPParticipantInstance,
		cast(ParticipantInstance as varchar) + cast(@orderid as varchar)  as ParticipantInstance,
		isnull(ParticipantFirstName,'') as ParticipantFirstName,
		isnull(ParticipantLastname,'') as ParticipantLastName,
--		cast(CustomerInfo.Instance as varchar) + cast(ParticipantInstance as varchar) + cast(@orderid as varchar) as QSPCustomerInstance,
		CustomerInfo.Instance as QSPCustomerInstance,
		cast(CustomerInfo.Instance as varchar) + cast(ParticipantInstance as varchar) + cast(@orderid as varchar) as CustomerInstance,
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
		--coalesce(OrderDetail.QtyShipped,0) as QtyShipped, -- shipped
		coalesce(OrderDetail.ReplacedItemCode,'') as ReplacedItemCode,
		isnull(OrderDetail.ReplacedItemQty,0) as ReplacedItemQty,
		isnull(LevelCode,'') as LevelCode

	/* Disabled MS Jan03 2006 Issue# 23.
	from 	##UnigistixOrderStaging  OrderDetail inner join Customer CustomerInfo  on  OrderDetail.CustomerInstance = CustomerInfo.Instance
			inner join vw_UnigistixStudentStaging  Participant on OrderDetail.StudentInstance = Participant.ParticipantInstance
			inner join vw_UnigistixEnvelopeStaging  Teacher on  Participant.TeacherInstance = Teacher.Instance
			inner join vw_UnigistixBatch  Batch on Teacher.orderid =  batch.orderid
			Left Join ReportRequestBatch on batchorderid=batch.orderid
	where 
		Batch.orderid=@OrderID */

	From 	##UnigistixOrderStaging  OrderDetail ,
		Customer CustomerInfo,
		vw_UnigistixStudentStaging  Participant,
		vw_UnigistixEnvelopeStaging  Teacher,
		vw_UnigistixBatch  Batch,
		ReportRequestBatch
	Where 	Batch.orderid=@OrderID 
	And OrderDetail.CustomerInstance = CustomerInfo.Instance	
	And OrderDetail.StudentInstance = Participant.ParticipantInstance
	And OrderDetail.OrderId=Participant.OrderId
	And Participant.TeacherInstance = Teacher.Instance
	And Teacher.orderid =  batch.orderid
	And batchorderid=batch.orderid

	Order By Teacher.Classroom, Teacher.TeacherLastName, ParticipantLastName, ParticipantFirstName, Recipient,
	CASE OrderDetail.Type
		WHEN 46001 Then (CASE Batch.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	 
		WHEN 46002 Then (CASE Batch.Lang
					WHEN 'EN' Then 'Gift'  	   --Gift
					WHEN 'FR' Then 'Cadeau'   --French
					ELSE 'Gift'			
				   END) 
		WHEN 46003 Then (CASE Batch.Lang
					WHEN 'EN' Then 'WFC'  	   --WFC
					WHEN 'FR' Then 'Chocolat Le Meilleur au Monde'   --French
					ELSE 'WFC'			
				   END) 
		WHEN 46005 Then (CASE Batch.Lang
					WHEN 'EN' Then 'Food'  	   --Food
					WHEN 'FR' Then 'Produit alimentaire'   --French
					ELSE 'Food'			
				   END) 
		WHEN 46006 Then (CASE Batch.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END)  --'Book'
		WHEN 46007 Then (CASE Batch.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END)  --'Music'
		WHEN 46010  Then (CASE Batch.Lang
					WHEN 'EN' Then 'Magazine'   --MMB
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	 --MMB
		WHEN 46011 Then (CASE Batch.Lang
					WHEN 'EN' Then 'National'   --National
					WHEN 'FR' Then 'National'   --French
					ELSE 'National'			
				   END)	--National
		WHEN 46012 Then (CASE Batch.Lang
					WHEN 'EN' Then 'Video'   --Video
					WHEN 'FR' Then 'Video'   --French
					ELSE 'Video'			
				   END)	--Video
		ELSE ''			END
,ProductName
		

	--ORDER BY Classroom,TeacherName, ParticipentName , ProductTypeName, ProductName

	FOR XML AUTO, ELEMENTS

/*

CASE COD.ProductType
		WHEN 46001 Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	 
		WHEN 46002 Then (CASE C.Lang
					WHEN 'EN' Then 'Gift'  	   --Gift
					WHEN 'FR' Then 'Cadeau'   --French
					ELSE 'Gift'			
				   END) 
		WHEN 46003 Then (CASE C.Lang
					WHEN 'EN' Then 'WFC'  	   --WFC
					WHEN 'FR' Then 'Chocolat Le Meilleur au Monde'   --French
					ELSE 'WFC'			
				   END) 
		WHEN 46005 Then (CASE C.Lang
					WHEN 'EN' Then 'Food'  	   --Food
					WHEN 'FR' Then 'Produit alimentaire'   --French
					ELSE 'Food'			
				   END) 
		WHEN 46006 Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END)  --'Book'
		WHEN 46007 Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END)  --'Music'
		WHEN 46010  Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --MMB
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	 --MMB
		WHEN 46011 Then (CASE C.Lang
					WHEN 'EN' Then 'National'   --National
					WHEN 'FR' Then 'National'   --French
					ELSE 'National'			
				   END)	--National
		WHEN 46012 Then (CASE C.Lang
					WHEN 'EN' Then 'Video'   --Video
					WHEN 'FR' Then 'Video'   --French
					ELSE 'Video'			
				   END)	--Video
		ELSE ''				
	END as ProductTypeName,

*/
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[PrepStagingTablesForUnigistix]    Script Date: 06/07/2017 09:20:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       procedure [dbo].[PrepStagingTablesForUnigistix]
	@orderid int
as
set nocount on
declare @campaign int
declare @lang varchar(2)
select @campaign=CampaignID
	from QSPCanadaOrderManagement..Batch where orderid=@orderid
select @lang=Lang from  QSPCanadaCommon..Campaign as Campaign where ID = @campaign

--print @campaign
--print @lang
--sp_columns 'UnigistixBatch'
--select * from unigistixbatch
--delete from unigistixbatch
	insert UnigistixBatch 
	       (BatchDate,
		BatchID,
		OrderID,
		OrderTypeCode,
		DateOrderReceived,
		DateOrderCreated,
	        CampaignID,
		Comment,
		Lang,
		FMID,
		FMFirstName,
		FMLastname,
		EMail,
		BillToAccountID,
		BillToAccountName,
		BillToAccountAddress1,
		BillToAccountAddress2,
		BillToAccountCity,
		BillToAccountState,
		BillToAccountZip,
		BillToAccountPhone,
		ShipToAccountID,
		ShipToAccountName,
		ShipToAccountAddress1,
		ShipToAccountAddress2,
		ShipToAccountCity,
		ShipToAccountState,
		ShipToAccountZip,
		ShipToAccountPhone

		)
	select Date,
		batch.ID,
		OrderID,
		OrderTypeCode,
		DateReceived,
		DateCreated,
		Campaign.ID,
		Batch.Comment,
		Campaign.Lang,
		FM.FMID,
		FM.FirstName,
		FM.LastName,
		FM.Email, 
		A.ID,
		A.Name,
		AdBill.Street1 		as BillingAddress,
		AdBill.Street2 		as BillingAddress2,
		AdBill.City      		as BillingCity,
		AdBill.StateProvince	as BillingState,
		AdBill.Postal_Code      	as BillingZip,
		P.PhoneNumber,
		A.ID,
		A.Name,
		AdShip.Street1 		as BillingAddress,
		AdShip.Street2 		as BillingAddress2,
		AdShip.City      		as BillingCity,
		AdShip.StateProvince	as BillingState,
		AdShip.Postal_Code      	as BillingZip,
		P.PhoneNumber
		from 
			QSPCanadaOrderManagement..Batch as Batch,
			QSPCanadaCommon..Campaign as Campaign,
			QSPCanadaCommon..CAccount A 
			LEFT JOIN QSPCanadaCommon..AddressList AL on A.AddressListID = AL.ID
			LEFT JOIN QSPCanadaCommon..Address AdShip on AL.ID = AdShip.AddressListID AND AdShip.Address_Type = 54001 --Ship To
			LEFT JOIN QSPCanadaCommon..Address AdBill   on AL.ID = AdBill.AddressListID   AND AdBill.Address_Type = 54002 --Use ship to for both.  Bill To=2
			Left JOIN QSPCanadaCommon..PhoneList PL on A.PhoneListID = PL.ID
			Left JOIN QSPCanadaCommon..Phone P on P.PhoneListID = PL.ID and Type=30501,
			QSPCanadaCommon..FieldManager as FM
		where orderid=@orderid
			and Batch.CampaignID = Campaign.ID
			and FM.FMID = Campaign.FMID
			and Campaign.BilltoAccountid = a.id

	insert UnigistixEnvelopeStaging
	(       EnvelopeID,
		TeacherInstance,
		TeacherMiddle,
		TeacherLastName,
		Classroom	
	)
	select isnull(Envelope.Instance,0),
	       isnull(Teacher.Instance,0),
		isnull(Teacher.MiddleInitial,'N/A'),
		isnull(Teacher.LastName,'N/A'),
		isnull(Classroom,'N/A')
		from 			
		QSPCanadaOrderManagement..Batch As Batch
			left join QSPCanadaOrderManagement..Envelope As Envelope on
				envelope.orderbatchid=batch.id  
				and envelope.orderbatchdate=date  
			left join QSPCanadaOrderManagement..Teacher As Teacher on
					envelope.teacherinstance=teacher.instance 
			where  	 orderid=  @orderid
			   and Envelope.Instance not in  (select EnvelopeID from UnigistixEnvelopeStaging)

	-- Unigistix wants premiums at participant level
	exec PrepStudentStagingForUnigistix @orderid

	exec PrepOrderDetailForUnigistix @orderid, @lang
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_UnigistixBatch]    Script Date: 06/07/2017 09:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_UnigistixBatch] AS

	

	SELECT 	Date AS BatchDate,
		batch.ID AS BatchID,
		OrderID,
		OrderTypeCode,
		DateReceived AS DateOrderReceived,
		DateCreated AS DateOrderCreated,
		Campaign.ID AS CampaignID,
		Campaign.ShiptoCampaignContactID,
		Campaign.BilltoCampaignContactID,
		BillContact.FirstName as BillToContactFirstName,
		BillContact.LastName as BillToContactLastName,
		p2.phonenumber as BillToContactPhone,
		ShipContact.FirstName as ShipToContactFirstName,
		ShipContact.LastName as ShipToContactLastName,
		Batch.Comment,
		Campaign.Lang,
		FM.FMID,
		FM.FirstName AS FMFirstName,
		FM.LastName AS FMLastName,
		FM.Email, 
		A.ID AS BillToAccountID,
		A.Name AS BillToAccountName,
		AdBill.Street1 		as BillToAccountAddress1,
		AdBill.Street2 		as BillToAccountAddress2,
		AdBill.City      		as BillToAccountCity,
		AdBill.StateProvince	as BillToAccountState,
		AdBill.Postal_Code      	as BillToAccountZip,
		P.PhoneNumber BillToAccountPhone,
		A.ID AS ShipToAccountID,
		A.Name ShipToAccountName,
		AdShip.Street1 		as ShipToAccountAddress1,
		AdShip.Street2 		as ShipToAccountAddress2,
		AdShip.City      		as ShipToAccountCity,
		AdShip.StateProvince	as ShipToAccountState,
		AdShip.Postal_Code      	as ShipToAccountZip,
		P.PhoneNumber AS ShipToAccountPhone
	FROM	QSPCanadaOrderManagement..Batch as Batch join 
		QSPCanadaCommon..Campaign as Campaign on Batch.CampaignID = Campaign.ID join 
		QSPCanadaCommon..FieldManager as FM on FM.FMID = Campaign.FMID join
		QSPCanadaCommon..CAccount A on Campaign.BilltoAccountid = a.id
			LEFT JOIN QSPCanadaCommon..AddressList AL on A.AddressListID = AL.ID
			LEFT JOIN QSPCanadaCommon..Address AdShip on AL.ID = AdShip.AddressListID AND AdShip.Address_Type = 54001 --Ship To
			LEFT JOIN QSPCanadaCommon..Address AdBill   on AL.ID = AdBill.AddressListID   AND AdBill.Address_Type = 54002 --Use ship to for both.  Bill To=2
			Left JOIN QSPCanadaCommon..PhoneList PL on A.PhoneListID = PL.ID
			Left JOIN QSPCanadaCommon..Phone P on P.PhoneListID = PL.ID and Type=30501
 			left join QSPCanadaCommon..Contact BillContact on Campaign.BilltoCampaignContactID = BillContact.ID
			left join QSPCanadaCommon..Contact ShipContact  on Campaign.ShiptoCampaignContactID = ShipContact.ID
			left join QSPCanadaCommon..PhoneList pl2 on BillContact.PhoneListID = pl2.ID
			left join QSPCanadaCommon..Phone p2 on p2.phonelistid = pl2.id and p2.type=30501
		
/*
	SELECT 	Date AS BatchDate,
		batch.ID AS BatchID,
		OrderID,
		OrderTypeCode,
		DateReceived AS DateOrderReceived,
		DateCreated AS DateOrderCreated,
		Campaign.ID AS CampaignID,
		Campaign.ShiptoCampaignContactID,
		Campaign.BilltoCampaignContactID,
		Batch.Comment,
		Campaign.Lang,
		FM.FMID,
		FM.FirstName AS FMFirstName,
		FM.LastName AS FMLastName,
		FM.Email, 
		A.ID AS BillToAccountID,
		A.Name AS BillToAccountName,
		AdBill.Street1 		as BillToAccountAddress1,
		AdBill.Street2 		as BillToAccountAddress2,
		AdBill.City      		as BillToAccountCity,
		AdBill.StateProvince	as BillToAccountState,
		AdBill.Postal_Code      	as BillToAccountZip,
		P.PhoneNumber BillToAccountPhone,
		A.ID AS ShipToAccountID,
		A.Name ShipToAccountName,
		AdShip.Street1 		as ShipToAccountAddress1,
		AdShip.Street2 		as ShipToAccountAddress2,
		AdShip.City      		as ShipToAccountCity,
		AdShip.StateProvince	as ShipToAccountState,
		AdShip.Postal_Code      	as ShipToAccountZip,
		P.PhoneNumber AS ShipToAccountPhone
	FROM	QSPCanadaOrderManagement..Batch as Batch,
		QSPCanadaCommon..Campaign as Campaign,
		QSPCanadaCommon..CAccount A 
			LEFT JOIN QSPCanadaCommon..AddressList AL on A.AddressListID = AL.ID
			LEFT JOIN QSPCanadaCommon..Address AdShip on AL.ID = AdShip.AddressListID AND AdShip.Address_Type = 54001 --Ship To
			LEFT JOIN QSPCanadaCommon..Address AdBill   on AL.ID = AdBill.AddressListID   AND AdBill.Address_Type = 54002 --Use ship to for both.  Bill To=2
			Left JOIN QSPCanadaCommon..PhoneList PL on A.PhoneListID = PL.ID
			Left JOIN QSPCanadaCommon..Phone P on P.PhoneListID = PL.ID and Type=30501,
		QSPCanadaCommon..FieldManager as FM
	WHERE  	Batch.CampaignID = Campaign.ID
		and FM.FMID = Campaign.FMID
		and Campaign.BilltoAccountid = a.id




*/
GO

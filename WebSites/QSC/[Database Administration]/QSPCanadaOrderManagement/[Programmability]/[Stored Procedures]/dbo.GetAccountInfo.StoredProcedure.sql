USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetAccountInfo]    Script Date: 06/07/2017 09:19:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[GetAccountInfo] ( @CampaignId Int) AS
Begin
SELECT DISTINCT  
	  b.campaignid
	, a.Id AS BillToAccountId
	, a.Name AS BillToAccount
	, Addr.street1 AS AccountAddress1
	, Addr.street2 AS AccountAddress2
	, Addr.city AS AccountCity
	, Addr.stateProvince AS AccountProvince
	, Addr.postal_code AS AccountPCode
	, shipa.Id AS ShipAccountId
	, shipa.Name AS ShiptoAccount
	, ShipAdr.street1 AS ShipAccountAddress1
	, ShipAdr.street2 AS ShipAccountAddress2
	, ShipAdr.city AS ShipAccountCity
	, ShipAdr.stateProvince AS ShipAccountProvince
	, ShipAdr.postal_code AS ShipAccountPcode
	, b.ContactFirstName AS AccountContactFname
	, b.ContactLastName AS AccountContactLname
	, b.ContactPhone AS AccountContactPhone
	, b.BillToFMID
	, b.ShipToFMID
	, bShip.ContactFirstName AS ShipContactFname
	, bShip.ContactLastName AS ShipContactLname
	, bShip.ContactPhone AS ShipContactPhone
	, bShip.ContactEmail ShipcontactEmail
	, b.ContactEmail contactEmail
FROM 
	QSPCanadaCommon.dbo.Address ShipAdr 
	INNER JOIN QSPCanadaCommon.dbo.CAccount shipa ON ShipAdr.AddressListID = shipa.AddressListID AND ShipAdr.address_type = 54001 
	RIGHT OUTER JOIN dbo.Batch bShip 
	RIGHT OUTER JOIN dbo.Batch b ON bShip.ID = b.ID AND bShip.[Date] = b.[Date] AND bShip.ShipToAccountID = b.ShipToAccountID ON shipa.Id = b.ShipToAccountID 
	LEFT OUTER JOIN QSPCanadaCommon.dbo.Address Addr 
	INNER JOIN QSPCanadaCommon.dbo.CAccount a ON Addr.AddressListID = a.AddressListID AND Addr.address_type = 54002 ON b.AccountID = a.Id
where 
	b.campaignid=@CampaignId
End
GO

USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Account_4new_Campaign]    Script Date: 06/07/2017 09:33:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_get_Account_4new_Campaign]
  @AccountID integer
AS

--- 
--- Present an Account in order to create a campaign on it 
--- JLC 26-AUG-2004 its update time, remove un-needed stuff
--- JLC 10-MAY-2004 Initial Version
---

SELECT
	CAccount.[Id]				AS AccountId,
	CAccount.[Name]			AS AccountName,
	isnull(ShipAddr.address_id,-1)		AS ShipToAddrId,
	isnull(ShipAddr.street1, '')			AS ShipToAddress1,
	isnull(ShipAddr.street2, '')			AS ShipToAddress2,
	isnull(ShipAddr.city,'')			AS ShipToCity,
	isnull(ShipAddr.stateProvince,'')		AS ShipToState,
	isnull(ShipAddr.postal_code,'')		AS ShipToZip,
	isnull(ShipAddr.zip4,'')			AS ShipToZip4,
	isnull(ShipAddr.country,'')			AS ShipToCountry,
	isnull(BillAddr.address_id	,-1)		AS BillToAddrId,
	isnull(BillAddr.street1,'')			AS BillToAddress1,
	isnull(BillAddr.street2,'')			AS BillToAddress2,
	isnull(BillAddr.city,'')			AS BillToCity,
	isnull(BillAddr.stateProvince,'')		AS BillToState,
	isnull(BillAddr.postal_code,'')		AS BillToZip,
	isnull(BillAddr.zip4,'')			AS BillToZip4,
	isnull(BillAddr.country,'')			AS BillToCountry,
	CAccount.CAccountCodeClass		AS AccountClass,
--	'SOME CLASS'				AS AccountClassOther,
	CAccount.CAccountCodeGroup		AS AccountCode,
--	'SOME CODE'				AS AccountCodeOther,
	ActPhone.PhoneNumber			AS AccountPhone,
	FaxPhone.PhoneNumber			AS AccountFax,
	CAccount.EMail				AS AccountEmail,
	CAccount.Sponsor 			AS ContactName,
	CAccount.Lang				AS Lang
FROM 
	CAccount 
	LEFT JOIN Address ShipAddr		ON CAccount.AddressListID	= ShipAddr.AddressListID AND ShipAddr.address_type = 1
	LEFT JOIN Address BillAddr		ON CAccount.AddressListID	= BillAddr.AddressListID	AND BillAddr.address_type = 2
	LEFT JOIN Phone ActPhone		ON CAccount.PhoneListID	= ActPhone.PhoneListID	AND ActPhone.Type = 1
	LEFT JOIN Phone FaxPhone		ON CAccount.PhoneListID	= FaxPhone.PhoneListID	AND FaxPhone.Type = 2
WHERE
	CAccount.[ID] = @AccountID
GO

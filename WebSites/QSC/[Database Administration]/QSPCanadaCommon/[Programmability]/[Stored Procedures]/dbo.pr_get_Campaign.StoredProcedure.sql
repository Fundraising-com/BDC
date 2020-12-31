USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Campaign]    Script Date: 06/07/2017 09:33:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_get_Campaign]
  @CampaignID integer
AS

--- 
--- Present a Campaign for reporting purposes
--- JLC 26-AUG-2004 third version, now unneeded items commented out
--- JLC 06-MAY-2004 Second  Version
--- JLC 08-APR-2004 Initial Version
---

SELECT
	Campaign.[ID]						AS CampaignID,
	Campaign.Status,
	Campaign.Country,
	Campaign.FMID,
--	Campaign.DateChanged,
	Campaign.Lang,
	Campaign.StartDate,
	Campaign.EndDate,
	isnull(Campaign.IncentivesBillToID, -1)			AS IncentivesBillToID,
	isnull(Campaign.IncentivesDistributionID, -1)		AS IncentivesDistributionID,
	isnull(Campaign.ShipToCampaignContactID, -1) 		AS ShipToCampaignContactID,
	Campaign.ShipToAccountID,
	Campaign.BillToAccountID,
	Campaign.EstimatedGross,
	Campaign.NumberOfParticipants,
	Campaign.NumberOfClassroooms,
	Campaign.NumberOfStaff,
	isnull(Campaign.BillToCampaignContactID, -1) 		AS BillToCampaignContactID,
	isnull(Campaign.SuppliesCampaignContactID, -1) 		AS SuppliesCampaignContactID,
	isnull(Campaign.SuppliesShipToCampaignContactID, -1) 	AS SuppliesShipToCampaignContactID ,
	ISNULL(Campaign.SuppliesDeliveryDate, '1995-01-01 00:00:00.000') AS SuppliesDeliveryDate,
	Campaign.SpecialInstructions,
	Campaign.IsStaffOrder,
	ISNULL(Campaign.StaffOrderDiscount, 0)			AS StaffOrderDiscount,
	Campaign.IsTestCampaign,
	Campaign.IsPayLater,
	Campaign.FSOrderRecCreated,
	ISNULL(Campaign.ApprovedStatusDate, '1995-01-01 00:00:00.000') AS ApprovedStatusDate,
	ISNULL(Campaign.MagnetStatementDate, '1995-01-01 00:00:00.000') AS MagnetStatementDate,
	--CAccount_Ship.[Id]					AS AccountId,
	CAccount_Ship.[Name]					AS AccountName,
	isnull(ShipAddr.address_id, -1)					AS ShipToAddrId,
	ShipAddr.street1						AS ShipToAddress1,
	ShipAddr.street2						AS ShipToAddress2,
	ShipAddr.city						AS ShipToCity,
	ShipAddr.stateProvince					AS ShipToState,
	ShipAddr.postal_code					AS ShipToZip,
	ShipAddr.zip4						AS ShipToZip4,
	ShipAddr.country					AS ShipToCountry,
	isnull(BillAddr.address_id, -1)					AS BillToAddrId,
	BillAddr.street1						AS BillToAddress1,
	BillAddr.street2						AS BillToAddress2,
	BillAddr.city						AS BillToCity,
	BillAddr.stateProvince					AS BillToState,
	BillAddr.postal_code					AS BillToZip,
	BillAddr.zip4						AS BillToZip4,
	BillAddr.country						AS BillToCountry,
	CAccount_Ship.CAccountCodeClass			AS AccountClass,
--	'SOME CLASS'						AS AccountClassOther,
	CAccount_Ship.CAccountCodeGroup			AS AccountCode,
--	'SOME CODE'						AS AccountCodeOther,
	ActPhone.PhoneNumber					AS AccountPhone,
	FaxPhone.PhoneNumber					AS AccountFax,
	CAccount_Ship.EMail					AS AccountEmail,
	--CAccount_Ship.Sponsor 				AS ContactName,
	Campaign.ContactName					AS ContactName,
	ContactPhone.PhoneNumber				AS ContactPhone,
	--ISNULL(RewardsProgramCumulative,0)			AS RewardsProgramCumulative,
	--ISNULL(RewardsProgramChart,0)				AS RewardsProgramChart,
	--ISNULL(RewardsProgramDraw,0)				AS RewardsProgramDraw,

/*
     	CASE	WHEN C15.ProgramID IS null THEN CAST(0 AS bit)
		ELSE CAST(1 AS bit)
	END							AS RewardsProgramCumulative,
     	CASE	WHEN C16.ProgramID IS null THEN CAST(0 AS bit)
		ELSE CAST(1 AS bit)
	END							AS RewardsProgramChart,
     	CASE	WHEN C9.ProgramID IS null THEN CAST(0 AS bit)
		ELSE CAST(1 AS bit)
	END							AS RewardsProgramDraw,

	CASE   	WHEN C1.ProgramID IS null THEN CAST(0 AS bit)
		ELSE CAST(1 AS bit)
	END							AS ProgramMagReg,
     	CASE   	WHEN C2.ProgramID IS null THEN CAST(0 AS bit)
		ELSE CAST(1 AS bit)
	END							AS ProgramMagExp,
     	CASE	WHEN C3.ProgramID IS null THEN CAST(0 AS bit)
		ELSE CAST(1 AS bit)
	END							AS ProgramMagCombo,
     	CASE	WHEN C13.ProgramID IS null THEN CAST(0 AS bit)
		ELSE CAST(1 AS bit)
	END							AS ProgramMagnet,
     	CASE	WHEN C14.ProgramID IS null THEN CAST(0 AS bit)
		ELSE CAST(1 AS bit)
	END							AS ProgramMagStaff,
*/
	Campaign.DateModified,
	Campaign.UserIDModified	
FROM 
	Campaign 
	LEFT JOIN CAccount CAccount_Ship	ON Campaign.ShipToAccountID		= CAccount_Ship.[Id]
	LEFT JOIN Address ShipAddr		ON CAccount_Ship.AddressListID	= ShipAddr.AddressListID 	AND ShipAddr.address_type = 1
	LEFT JOIN CAccount CAccount_Bill	ON Campaign.BillToAccountID		= CAccount_Bill.[Id]
	LEFT JOIN Address BillAddr		ON CAccount_Bill.AddressListID		= BillAddr.AddressListID		AND BillAddr.address_type = 1
	LEFT JOIN Phone ActPhone		ON CAccount_Ship.PhoneListID		= ActPhone.PhoneListID		AND ActPhone.Type = 1
	LEFT JOIN Phone FaxPhone		ON CAccount_Ship.PhoneListID		= FaxPhone.PhoneListID		AND FaxPhone.Type = 2
	LEFT JOIN Phone ContactPhone		ON Campaign.PhoneListID 		= ContactPhone.PhoneListID	AND ContactPhone.Type = 1
--	LEFT JOIN CampaignProgram   C1 on ((Campaign.ID =   C1.CampaignID) AND    (C1.ProgramID =  1))
--	LEFT JOIN CampaignProgram   C2 on ((Campaign.ID =   C2.CampaignID) AND    (C2.ProgramID =  2))
--	LEFT JOIN CampaignProgram   C3 on ((Campaign.ID =   C3.CampaignID) AND    (C3.ProgramID =  3))
--	LEFT JOIN CampaignProgram   C9 on ((Campaign.ID =   C9.CampaignID) AND    (C9.ProgramID =  9))
--	LEFT JOIN CampaignProgram C13 on ((Campaign.ID = C13.CampaignID) AND (C13.ProgramID = 13))
--	LEFT JOIN CampaignProgram C14 on ((Campaign.ID = C14.CampaignID) AND (C14.ProgramID = 14))
--	LEFT JOIN CampaignProgram C15 on ((Campaign.ID = C15.CampaignID) AND (C15.ProgramID = 15))
--	LEFT JOIN CampaignProgram C16 on ((Campaign.ID = C16.CampaignID) AND (C16.ProgramID = 16))

WHERE
	Campaign.[ID] = @CampaignID
GO

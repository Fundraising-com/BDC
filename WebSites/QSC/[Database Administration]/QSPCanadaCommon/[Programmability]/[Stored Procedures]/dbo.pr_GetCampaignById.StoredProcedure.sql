USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetCampaignById]    Script Date: 06/07/2017 09:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GetCampaignById]
  @CampaignID integer
AS

--- 
--- Present a Campaign for reporting purposes
--- JLC 08-APR-2004 Initial Version
---

  
SELECT
	Campaign.[ID],
	Campaign.Status,
	Campaign.Country,
	Campaign.FMID,
	Campaign.DateChanged,
	Campaign.Lang,
	Campaign.EndDate,
	Campaign.StartDate,
	Campaign.IncentivesBillToID,
	Campaign.BillToAccountID,
	Campaign.ShipToCampaignContactID,
	Campaign.ShipToAccountID,
	Campaign.EstimatedGross,
	Campaign.NumberOfParticipants,
	Campaign.NumberOfClassroooms,
	Campaign.NumberOfStaff,
	Campaign.BillToCampaignContactID,
	Campaign.SuppliesCampaignContactID,
	Campaign.SuppliesShipToCampaignContactID,
	Campaign.SuppliesDeliveryDate,
	Campaign.SpecialInstructions,
	Campaign.IsStaffOrder,
	Campaign.StaffOrderDiscount,
	Campaign.IsTestCampaign,
	Campaign.DateModified,
	Campaign.UserIDModified,
	Campaign.IsPayLater,
	Campaign.IncentivesDistributionID,
	Campaign.FSOrderRecCreated,
	Campaign.ApprovedStatusDate,
	Campaign.MagnetStatementDate,
	CAccount_Ship.[Id]			AS AccountId,
	CAccount_Ship.[Name]			AS AccountName,
	CAccount_Ship.ShipToAddress1,
	CAccount_Ship.ShipToAddress2,
	CAccount_Ship.ShipToCity,
	CAccount_Ship.ShipToState,
	CAccount_Ship.ShipToZip,
	CAccount_Ship.ShipToZip4,
	CAccount_Bill.Address1			AS BillToAddress1,
	CAccount_Bill.Address2			AS BillToAddress2,
	CAccount_Bill.City			AS BillToCity,
	CAccount_Bill.State			AS BillToState,
	CAccount_Bill.Zip			AS BillToZip,
	CAccount_Bill.Zip4			AS BillToZip4,
	CAccount_Ship.CAccountCodeClass	AS GroupClass,
	'SOME CLASS'				AS GroupClassOther,
	CAccount_Ship.CAccountCodeGroup	AS GroupCode,
	'SOME CODE'				AS GroupCodeOther,
	'ABC QSP ELEM'			AS GroupName,
	Phone1.PhoneNumber			AS AccountPhone,
	Phone2.PhoneNumber			AS AccountFax,
	CAccount_Ship.EMail			AS AccountEmail,
	'Josh Caesar'				AS ContactName,
	'914-244-5785'				AS ContactPhone,
	CAST(1 as bit)				AS ShipFieldSuppliesYN,
	CAST(1 as bit)				AS RewardDistParticipantBag,
	CAST(1 as bit)				AS RewardDistClassBox,
	CAST(1 as bit)				AS RewardsProgramCumulative,
	CAST(1 as bit)				AS RewardsProgramChart,
	CAST(1 as bit)				AS RewardsProgramDraw,
	CAST(1 as bit)				AS ProgramMagReg,
	CAST(1 as bit)				AS ProgramMagExp,
	CAST(1 as bit)				AS ProgramMagCombo,
	CAST(1 as bit)				AS ProgramMagnet,
	CAST(1 as bit)				AS ProgramMagStaff	
FROM 
	Campaign LEFT JOIN CAccount CAccount_Ship ON Campaign.ShipToAccountID = CAccount_Ship.[Id]
	LEFT JOIN CAccount CAccount_Bill ON Campaign.BillToAccountID = CAccount_Bill.[Id]
	LEFT JOIN Phone Phone1 ON CAccount_Ship.PhoneListID = Phone1.PhoneListID AND Phone1.Type = 1
	LEFT JOIN Phone Phone2 ON CAccount_Ship.PhoneListID = Phone2.PhoneListID AND Phone2.Type = 2
WHERE
	Campaign.[ID] = @CampaignID
GO

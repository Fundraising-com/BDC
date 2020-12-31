USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateCampaignListForResolve]    Script Date: 06/07/2017 09:33:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_GenerateCampaignListForResolve]
as
select 
	Camp.ID,
	FMID,
	Camp.Lang,
	StartDate,
	EndDate,
	BillToAccountID       = BillAcct.[Id],
	ShipToAccountID    = ShipAcct.[Id],
	Camp.[EstimatedGross],
	NumberOfParticipants,
	NumberOfClassroooms,
	NumberOfStaff,
	Camp.IsStaffOrder,
	ContactFirstName    = Cont.[FirstName],
	ContactLastName    = Cont.[LastName],
	ContactEmail            = Cont.[Email],
	ContactPhone          = PH.[PhoneNumber],
	ContactFax               = PHF.[PhoneNumber]
 from 
	QSPCanadaCommon.dbo.Campaign Camp

	LEFT JOIN QSPCanadaCommon.dbo.CAccount     BillAcct
		ON Camp.[BillToAccountID] = BillAcct.[Id]

	
	LEFT JOIN QSPCanadaCommon.dbo.Contact      Cont
		ON Camp.[ShipToCampaignContactID] = Cont.[Id]

	LEFT JOIN QSPCanadaCommon.dbo.Phone        PH
		ON Cont.[PhoneListID] = PH.[PhoneListID] 
		AND PH.[Type] <> 30503 --not fax
		and PH.Type is not null

	LEFT JOIN QSPCanadaCommon.dbo.PhoneType        PHtype
		ON PH.[Type] = PHtype.[PhoneTypeID]
	
	LEFT JOIN QSPCanadaCommon.dbo.Phone        PHF
		ON Cont.[PhoneListID] = PHF.[PhoneListID] 
		AND PHF.[Type] = 30503 --fax

	LEFT JOIN QSPCanadaCommon.dbo.CAccount     ShipAcct
		ON Camp.[ShipToAccountID] = ShipAcct.[Id]

where startdate >='7/1/04'
GO

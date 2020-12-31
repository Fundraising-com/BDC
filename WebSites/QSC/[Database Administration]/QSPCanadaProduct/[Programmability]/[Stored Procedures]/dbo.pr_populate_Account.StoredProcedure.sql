USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_populate_Account]    Script Date: 06/07/2017 09:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_populate_Account]
 @MinAccountID int
,@MaxAccountID int
AS

INSERT INTO QSPCanadaOrderManagement.dbo.Account(
	  [ID]
	, [Name]
	, [Address1]
	, [Address2]
	, [City]
	, [State]
	, [Zip]
	, [ZipPlusFour]
	, [AttnLine]
	, [FieldManagerNo]
	, [FieldManagerRegion]
	, [County]
	, [CountyCode]
	, [SchoolType]
	, [IsNational]
	, [PublicCatholic]
	, [TaxExemptNumber]
	, [CampaignStart]
	, [CampaignEnd]
	, [UnitType]
	, [Commission]
	, [NationalDistrict]
	, [NationalFieldManager]
	, [SchoolDistrictName]
	, [NumberOfClassrooms]
	, [NumberOfStudents]
	, [ShipToAcctOrFM]
	, [AMFMInd]
) SELECT	
	  dbo.CAccount.[Id] 				as [ID]
	, dbo.CAccount.[Name] 				as [Name]
	, dbo.Address.street1 				as [Address1]
	, dbo.Address.street2 				as [Address2]
	, dbo.Address.City 				as [City]
	, dbo.Address.stateProvince 			as [State]
	, Replace(dbo.Address.postal_code, ' ', '') 	as [Zip]
	, dbo.Address.Zip4 				as [ZipPlusFour]
	--, dbo.Address.address_type
	, null as [AttnLine]
	--, dbo.Campaign.[FMID] 				as [FieldManagerNo]
	, null as [FieldManagerNo]
	--select distinct region from cuserprofile : (0, null, 99)
	--no region on fieldmanager
	--select distinct SalesRegionID from CAccount : (2,3,4,38001,38002,38003,38004, NULL)
	--select distinct FieldManagerRegion from QSPCanadaOrderManagement..Account --(' ', NULL)
	, null as [FieldManagerRegion] -- which one do I use 
	, null as [County]           --select distinct County           from QSPCanadaOrderManagement..Account --(' ', NULL)
	, null as [CountyCode]       --select distinct CountyCode       from QSPCanadaOrderManagement..Account --(' ', NULL)
	, null as [SchoolType]       --select distinct SchoolType       from QSPCanadaOrderManagement..Account --(' ', NULL)
	, 0 as [IsNational]          --select distinct IsNational       from QSPCanadaOrderManagement..Account --(0)
	, null as [PublicCatholic]   --select distinct PublicCatholic   from QSPCanadaOrderManagement..Account --(' ', NULL)
	, null as [TaxExemptNumber]  --select distinct TaxExemptNumber  from QSPCanadaOrderManagement..Account --(' ', NULL)
	--, dbo.Campaign.StartDate 			as [CampaignStart] --distinct value('1995-01-01 00:00:00.000')
	--, dbo.Campaign.EndDate 				as [CampaignEnd]   --distinct value('1995-01-01 00:00:00.000')
	, '01/01/1995' as [CampaignStart]
	, '01/01/1995' as [CampaignEnd]
	, null as [UnitType]         --select distinct UnitType         from QSPCanadaOrderManagement..Account --(' ', NULL)
	, 0.0 as [Commission]        --select distinct Commission       from QSPCanadaOrderManagement..Account --(0.0)
	, null as [NationalDistrict] --select distinct NationalDistrict from QSPCanadaOrderManagement..Account --(NULL)
	, null as [NationalFieldManager] --distinct values (NULL)
	, null as [SchoolDistrictName]   --distinct values (NULL)
	, 0 as [NumberOfClassrooms]      --distinct values (0)
	, 0 as [NumberOfStudents]        --distinct values (0)
	, null as [ShipToAcctOrFM]       --distinct values (NULL)
	, null as [AMFMInd]              --distinct values (NULL)
FROM 
	dbo.CAccount left join dbo.Address 
	ON dbo.CAccount.AddressListID = dbo.Address.AddressListID AND dbo.Address.address_type = 54001
	--left join dbo.Campaign
	--on dbo.CAccount.[Id] = dbo.Campaign.[ShipToAccountID]
where
	dbo.CAccount.[Id] BETWEEN @MinAccountID and @MaxAccountID
	AND dbo.Address.address_type = 54001
ORDER BY 
	dbo.CAccount.[Id] ASC
GO

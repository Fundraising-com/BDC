USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_populate_Account]    Script Date: 06/07/2017 09:20:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_populate_Account]
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
	  CACCOUNT.[Id] 				as [ID]
	, CACCOUNT.[Name] 				as [Name]
	, ADDRESS.street1 				as [Address1]
	, ADDRESS.street2 				as [Address2]
	, ADDRESS.City 					as [City]
	, ADDRESS.stateProvince 			as [State]
	, Replace(ADDRESS.postal_code, ' ', '') 	as [Zip]
	, ADDRESS.Zip4 					as [ZipPlusFour]
	--, ADDRESS.address_type
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
	QSPCanadaCommon.dbo.CAccount CACCOUNT
	left join QSPCanadaCommon.dbo.Address ADDRESS
	ON (CACCOUNT.AddressListID = ADDRESS.AddressListID AND ADDRESS.address_type = 54001)
	--left join dbo.Campaign
	--on CACCOUNT.[Id] = dbo.Campaign.[ShipToAccountID]
where
	CACCOUNT.[Id] BETWEEN @MinAccountID and @MaxAccountID
	AND ADDRESS.address_type = 54001
ORDER BY 
	CACCOUNT.[Id] ASC
GO

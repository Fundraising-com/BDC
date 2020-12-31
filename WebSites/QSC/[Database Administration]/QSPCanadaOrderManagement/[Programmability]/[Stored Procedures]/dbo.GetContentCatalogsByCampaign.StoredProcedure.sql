USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetContentCatalogsByCampaign]    Script Date: 06/07/2017 09:19:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
--	GetContentCatalogsByCampaign - based on GetContentCatalogsByProgram by Charles J Scally III  6/10/2004
--	JLC 6/11/2004
--	
--
--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

CREATE PROCEDURE [dbo].[GetContentCatalogsByCampaign]

@CampaignId int

AS


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will populate the @Season and @Year variables which will be used to limit the items picked.
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Declare @Season char(1)
Declare @Year int
Declare @Lang varchar(10), @FMId varchar(4)
Declare @ShipToAccountId int, @BillToAccountId int, @ShipToType Int, @TaxRegionId Int, @NumberOfStudents int, @NumberOfStaff int, @NumberOfClasses int

SELECT 

	@Year = CASE
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) > 6 THEN YEAR(CONVERT(smalldatetime,StartDate))+ 1
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) <= 6 THEN YEAR(CONVERT(smalldatetime,StartDate))
		ELSE
			0
		END
	,@Season = CASE
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) > 6 THEN 'F'
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) <= 6 THEN 'S'
		ELSE ''
		END
	,@Lang = Lang
	,@ShipToAccountId = ShipToAccountId
	,@ShipToType = SuppliesShipToCampaignContactId
	,@BillToAccountId = BillToAccountId
	,@NumberOfStudents = NumberOfParticipants
	,@NumberOfStaff = NumberOfStaff
	,@NumberOfClasses = NumberOfClassroooms
	,@FMId = FMId
	
FROM
	QSPCanadaCommon..Campaign
WHERE
	Id = @CampaignId

PRINT '@Season=' + Convert(varchar, @Season)
PRINT '@Year=' + Convert(varchar, @Year)
PRINT '@Lang=' + Convert(varchar, @Lang)

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will select all the applicable content catalog codes and their respective description
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO [QSPCanadaCommon].[dbo].[test_jlc2]([CampaignID], [ProgramID], [Content_Catalog_Code], [Description])
SELECT
	A.CampaignId			AS CampaignID,
	E.FSProgram_Id			AS ProgramID,
	E.FSContent_Catalog_Code	AS Content_Catalog_Code,
	F.Program_Type			AS 'Description'
FROM
	QSPCanadaCommon..CampaignToContentCatalog A
	INNER JOIN QSPCanadaCommon..Program B ON A.ProgramId = B.Id
	INNER JOIN QSPCanadaCommon..Campaign C ON C.Id = A.CampaignId
	INNER JOIN QSPCanadaProduct..Pricing_Details E ON E.FSContent_Catalog_Code = A.Content_Catalog_Code
	INNER JOIN QSPCanadaProduct..ProgramSection D ON D.Id = E.ProgramSectionId
	INNER JOIN QSPCanadaProduct..Program_Master F ON F.Code LIKE E.FSContent_Catalog_Code
	INNER JOIN QSPCanadaProduct..Product G ON (G.Product_Code = E.Product_Code AND G.Product_Year = E.Pricing_Year AND G.Product_Season = E.Pricing_Season AND G.Lang = E.Language_Code AND G.OracleCode LIKE E.OracleCode)
WHERE
	A.CampaignId = @CampaignId
	AND E.Pricing_Year = @Year 
	AND E.Pricing_Season = @Season
	AND F.Status in (30403,30404 )  
	--AND F.FSIsBrochure = 1 
	AND E.Language_Code = @Lang

GROUP BY
	A.CampaignId,
	E.FSProgram_Id,
	E.FSContent_Catalog_Code,
	F.Program_Type
GO

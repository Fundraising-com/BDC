USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spGetProductCodeFromRemitCodeAndTerm]    Script Date: 06/07/2017 09:20:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetProductCodeFromRemitCodeAndTerm]
	
	@zRemitCode		varchar(20),
	@iTerm			int,
	@iCampaignID	int

 AS

/*********************** get current season ******************************/
DECLARE 	@zProductSeason 	char(1)
DECLARE		@iProductYear		int

EXEC		pr_RemitTest_GetCurrentSeason @zProductSeason output, @iProductYear output
/*************************************************************************/

SET NOCOUNT ON

DECLARE @zProductCode					varchar(20),
		@iMainENProgramSectionID		int,
		@iMainFRProgramSectionID		int,
		@iStaffProgramSectionID			int,
		@bIsStaffCampaign				bit,
		@iAccountID						int

-- Get catalog sections
SELECT	@iMainENProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps
JOIN	QSPCanadaProduct..Program_Master pm on pm.Program_ID = ps.Program_ID
JOIN	QSPCanadaCommon..Season s on s.ID = pm.Season
WHERE	pm.Code LIKE 'MAG%'
AND		pm.Lang = 'EN'
AND		s.FiscalYear = @iProductYear
AND		s.Season = @zProductSeason

/*SELECT	@iMainENProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaProduct..Pricing_Details pdFS,
		QSPCanadaProduct..ProgramSection psFS,
		QSPCanadaProduct..Program_Master pmFS,
		QSPCanadaProduct..ProgFSSectionMap pfssmFS
WHERE	pm.Program_ID = ps.Program_ID
AND		pdFS.FSContent_Catalog_Code = pm.Code
AND		psFS.ID = pdFS.ProgramSectionID
AND		pmFS.Program_ID = psFS.Program_ID
AND		pfssmFS.Catalog_Section_ID = psFS.ID
AND		psFS.Type = 3
AND		pfssmFS.Program_ID = 1
AND		pdFS.FSIsBrochure = 1
AND		pdFS.TaxRegionID = 1
AND		COALESCE(pdFS.FSProvinceCode, '') = ''
AND		pm.SubType <> 30305
AND		COALESCE(ps.Name, '') NOT LIKE 'Book%'
AND		pdFS.Language_Code = 'EN'
AND		pdFS.Product_Code LIKE '%GST%'
AND		pm.Program_Type NOT LIKE 'Staff%'
AND		pdFS.Pricing_Year = @iProductYear
AND		pdFS.Pricing_Season = @zProductSeason*/

SELECT	@iMainFRProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps
JOIN	QSPCanadaProduct..Program_Master pm on pm.Program_ID = ps.Program_ID
JOIN	QSPCanadaCommon..Season s on s.ID = pm.Season
WHERE	pm.Code LIKE 'MAG%'
AND		pm.Lang = 'FR'
AND		s.FiscalYear = @iProductYear
AND		s.Season = @zProductSeason

/*SELECT	@iMainFRProgramSectionID =  ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaProduct..Pricing_Details pdFS,
		QSPCanadaProduct..ProgramSection psFS,
		QSPCanadaProduct..Program_Master pmFS,
		QSPCanadaProduct..ProgFSSectionMap pfssmFS
WHERE	pm.Program_ID = ps.Program_ID
AND		pdFS.FSContent_Catalog_Code = pm.Code
AND		psFS.ID = pdFS.ProgramSectionID
AND		pmFS.Program_ID = psFS.Program_ID
AND		pfssmFS.Catalog_Section_ID = psFS.ID
AND		psFS.Type = 3
AND		pfssmFS.Program_ID = 1
AND		pdFS.FSIsBrochure = 1
AND		pdFS.TaxRegionID = 1
AND		COALESCE(pdFS.FSProvinceCode, '') = ''
AND		pm.SubType <> 30305
AND		COALESCE(ps.Name, '') NOT LIKE 'Book%'
AND		pdFS.Language_Code = 'FR'
AND		pdFS.Product_Code LIKE '%GST%'
AND		pm.Program_Type NOT LIKE 'Staff%'
AND		pdFS.Pricing_Year = @iProductYear
AND		pdFS.Pricing_Season = @zProductSeason*/

SELECT	@iStaffProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaCommon..Season s
WHERE	pm.Program_ID = ps.Program_ID
AND		s.ID = pm.Season
AND		pm.Program_Type like 'Staff%'
AND		s.FiscalYear = @iProductYear
AND		s.Season = @zProductSeason

SELECT	@bIsStaffCampaign = IsStaffOrder,
		@iAccountID = ShipToAccountID
FROM	QSPCanadaCommon..Campaign
WHERE	ID = @iCampaignID

--If Staff campaign, get Staff product code
IF (@bIsStaffCampaign = 1)
BEGIN
	SELECT DISTINCT	@zProductCode = ISNULL(p.Product_code,'')
	FROM			QSPCanadaProduct..Product p
	LEFT JOIN		QSPCanadaproduct..Pricing_Details pd
						ON	pd.Product_Instance = p.Product_Instance
	LEFT JOIN		QSPCanadaproduct..ProgramSection ps
						ON	ps.ID = pd.ProgramSectionID
	LEFT JOIN		QSPCanadaproduct..Program_Master pm
						ON	pm.Program_ID = ps.Program_ID
	WHERE			p.RemitCode = @zRemitCode
	AND				pd.Nbr_of_Issues = @iTerm
	AND				p.Status IN (30600, 30603) --Active or Unremittable
	AND				ps.ID IN (@iStaffProgramSectionID)

	-- if none found then disregard terms chosen
	IF (@zProductCode IS NULL)
	BEGIN	
		SELECT DISTINCT TOP 1	@zProductCode = ISNULL(p.Product_code,'')
		FROM					QSPCanadaProduct..Product p
		LEFT JOIN				QSPCanadaproduct..Pricing_Details pd
									ON	pd.Product_Instance = p.Product_Instance
		LEFT JOIN				QSPCanadaproduct..ProgramSection ps
									ON	ps.ID = pd.ProgramSectionID
		LEFT JOIN				QSPCanadaproduct..Program_Master pm
									ON	pm.Program_ID = ps.Program_ID
		WHERE					p.Product_Year = @iProductYear
		AND						p.Product_Season = @zProductSeason
		AND						p.RemitCode = @zRemitCode
		AND						p.Status IN (30600, 30603) --Active or Unremittable
		AND						ps.ID IN (@iStaffProgramSectionID)
	END

	-- if none found then assume the remit code matches the product code (necessary for Books Online)
	IF (@zProductCode IS NULL)
	BEGIN	
		SELECT	@zProductCode = ISNULL(@zRemitCode, '')
	END
END
ELSE
BEGIN
	SELECT DISTINCT	@zProductCode = ISNULL(p.Product_code,'')
	FROM			QSPCanadaProduct..Product p
	LEFT JOIN		QSPCanadaproduct..Pricing_Details pd
						ON	pd.Product_Instance = p.Product_Instance
	LEFT JOIN		QSPCanadaproduct..ProgramSection ps
						ON	ps.ID = pd.ProgramSectionID
	LEFT JOIN		QSPCanadaproduct..Program_Master pm
						ON	pm.Program_ID = ps.Program_ID
	WHERE			p.RemitCode = @zRemitCode
	AND				pd.Nbr_of_Issues = @iTerm
	AND				p.Status IN (30600, 30603) --Active or Unremittable
	AND				ps.ID IN (@iMainENProgramSectionID, @iMainFRProgramSectionID)
	AND				p.Product_Code <> '8212' --Sports Illustrated Kids has 2 products for 1 remit code
	AND				LEFT(p.Product_code, 1) <> 'K'
	AND				((LEFT(p.Product_code, 1) = 'G' AND RIGHT(p.Product_code, 1) = 'G' AND @iAccountID = 30045)
	OR				(LEFT(p.Product_code, 1) <> 'G' AND @iAccountID <> 30045))


	-- if none found then disregard terms chosen
	IF (@zProductCode IS NULL)
	BEGIN	
		SELECT DISTINCT TOP 1	@zProductCode = ISNULL(p.Product_code,'')
		FROM					QSPCanadaProduct..Product p
		LEFT JOIN				QSPCanadaproduct..Pricing_Details pd
									ON	pd.Product_Instance = p.Product_Instance
		LEFT JOIN				QSPCanadaproduct..ProgramSection ps
									ON	ps.ID = pd.ProgramSectionID
		LEFT JOIN				QSPCanadaproduct..Program_Master pm
									ON	pm.Program_ID = ps.Program_ID
		WHERE					p.Product_Year = @iProductYear
		AND						p.Product_Season = @zProductSeason
		AND						p.RemitCode = @zRemitCode
		AND						p.Status IN (30600, 30603) --Active or Unremittable
		AND						ps.ID IN (@iMainENProgramSectionID, @iMainFRProgramSectionID)
		AND						p.Product_Code <> '8212' --Sports Illustrated Kids has 2 products for 1 remit code
		AND						LEFT(p.Product_code, 1) <> 'K'
		AND						((LEFT(p.Product_code, 1) = 'G' AND RIGHT(p.Product_code, 1) = 'G' AND @iAccountID = 30045)
		OR						(LEFT(p.Product_code, 1) <> 'G' AND @iAccountID <> 30045))	
	END

	-- if none found then assume the remit code matches the product code (necessary for Books Online)
	IF (@zProductCode IS NULL)
	BEGIN	
		SELECT	@zProductCode = ISNULL(@zRemitCode, '')
	END
END

SELECT @zProductCode
GO

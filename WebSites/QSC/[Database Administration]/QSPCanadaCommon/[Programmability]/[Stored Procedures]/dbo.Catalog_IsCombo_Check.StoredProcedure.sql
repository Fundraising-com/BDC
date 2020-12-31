USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[Catalog_IsCombo_Check]    Script Date: 06/07/2017 09:33:08 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Catalog_IsCombo_Check]

@CampaignId int,
@IsCombo int output

AS
SET NOCOUNT ON
SET @IsCombo = 0

--- First Test For More Than 1 Program
SELECT * FROM QSPCanadaCommon..CampaignProgram WHERE CampaignId = @CampaignId AND DeletedTF = 0

If @@RowCount > 0
	BEGIN
	
	
		DECLARE @SEASON int
		DECLARE @FISCAL int
		DECLARE @STARTDATE smalldatetime
		DECLARE @LANG varchar(3)
		
		SELECT @StartDate = StartDate, @Lang = Lang FROM QSPCanadaCommon..Campaign A WHERE Id = @CampaignId
		SELECT @Season = Id, @Fiscal = FiscalYear FROM QSPCanadaCommon..Season B WHERE StartDate <= @StartDate and EndDate >= @StartDate AND Season <> 'Y'
		SELECT @FISCAL = Id FROM QSPCanadaCommon..Season B WHERE FiscalYear = @FISCAL AND Season LIKE 'Y'
		
		SELECT @Season
		
		SELECT
			A.*, B.*
		FROM
			QSPCanadaProduct..Program_Master A
			INNER JOIN QSPCanadaProduct..ProgramSection B ON A.Code = B.CatalogCode
			INNER JOIN QSPCanadaProduct..Pricing_Details C ON C.ProgramSectionId = B.Id
		WHERE
			A.Lang = @Lang
			AND (A.Season = @Season OR A.Season = @Fiscal)
			AND B.Type = 3 
			AND C.FSIsBrochure = 1
			-- AND A.Status in (30403,30404 )  
			AND B.Id IN (SELECT DISTINCT ProgramSectionId FROM QSPCanadaCommon..Brochure WHERE CampaignId = @CampaignId AND DeletedTF <> 1)

		IF @@RowCount > 0 
			BEGIN
				SET @IsCombo = 1
			END
		
	END
SET NOCOUNT OFF
SELECT @IsCombo As 'IsCombo'
GO

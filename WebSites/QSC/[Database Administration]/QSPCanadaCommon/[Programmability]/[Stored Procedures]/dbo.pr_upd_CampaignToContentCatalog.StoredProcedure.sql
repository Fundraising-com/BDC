USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_CampaignToContentCatalog]    Script Date: 06/07/2017 09:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE           PROCEDURE [dbo].[pr_upd_CampaignToContentCatalog]
  @CampaignID int,
  @ProgramID int,
  @Content_Catalog_Code varchar(20),
  @Choice bit,
  @IsCombo int,
  @UserIDModified UserID_UDDT
AS

set nocount on
Declare @now datetime
Declare @Year int
--Declare @Season Varchar(1)
Declare @lang varchar(2)
Declare @IsStaffOrder int


--Get actual season/year
--SET @now = getDate()
SELECT @now = StartDate,@lang=lang,@IsStaffOrder=IsStaffOrder From QSPCanadaCommon..Campaign where id=@CampaignID

SELECT 

	@Year = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 6 THEN YEAR(CONVERT(smalldatetime,@now)) + 1
		WHEN MONTH(CONVERT(smalldatetime,@now)) <= 6 THEN YEAR(CONVERT(smalldatetime,@now))
		ELSE
			0
		END
	/*,@Season = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 6 THEN  'F'
		WHEN MONTH(CONVERT(smalldatetime,@now)) <= 6 THEN 'S'
		ELSE ''
		END*/

DECLARE @CatalogCount int
SELECT 
	@CatalogCount = Count(Content_Catalog_Code)  
  FROM 
	CampaignToContentCatalog 
 WHERE 
	CampaignID = @CampaignID 
	AND ProgramId = @ProgramID
	AND Content_Catalog_Code = @Content_Catalog_Code
	AND DeletedTF = 0

Declare @ProgramType int

--Sept 1 2006 ; MS if CA is running only Gift program we need to treat it as combo
IF IsNull(@IsCombo,0)=0
Begin
 	SELECT  *  FROM QSPCanadaCommon..CampaignProgram 
	WHERE CampaignId = @CampaignID 
	AND DeletedTF = 0
	AND ProgramId in (27,4) --Gift

	If @@Rowcount > 0
	Begin
	   SET @isCombo=1
	End

End

Select @ProgramType= ProgramTypeID from Program where Id = @ProgramID
/*
print 'combo'
print @IsCombo
print 'Program Type'
print @ProgramType
print 'Program'
print @ProgramID
print 'Staff'
print @IsStaffOrder
*/
IF (@CatalogCount = 0 AND @Choice = 1 )
begin
	--No Campaign/Program/Catalog marriage yet, insert it
	
	--but was there a logical delete previously ??
	DECLARE @CatalogCountLD int --Logical Delete	
	SELECT 
		@CatalogCountLD = Count(Content_Catalog_Code)   
	FROM 
		CampaignToContentCatalog 
	WHERE 
		CampaignID = @CampaignID 
		AND ProgramId = @ProgramID
		AND Content_Catalog_Code = @Content_Catalog_Code
		AND DeletedTF = 1
		
	IF @CatalogCountLD > 0 
	BEGIN
		--logically UN-DELETE this item
		UPDATE 
			CampaignToContentCatalog
		SET
			DeletedTF = 0,
			ModifyDate = getdate(),
			ModifiedBy = @UserIDModified
		WHERE
			CampaignID = @CampaignID 
			AND ProgramId = @ProgramID
			AND Content_Catalog_Code = @Content_Catalog_Code ;
	END
	ELSE
	BEGIN

		Declare @Content varchar(20)				

		if(@ProgramType = 36001)  -- non incentive
		begin
			if(@IsStaffOrder = 0)
			begin

				/***** Disabled MS Aug 16, 2007 Issue# 3163 multiple record selected because of in-adequate filter
				select @Content= FSContent_Catalog_Code from 
					qspcanadaproduct..ProgFSSectionMap map,  QSPCanadaProduct..Pricing_Details PD,
					QSPCanadaCommon..TaxRegionProvince TRP
					where ProgramSectionID = CATALOG_SECTION_ID
					and map.program_id = @ProgramID
					and PD.TaxRegionID= TRP.TaxRegionID
					and Trp.Province = QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince( @CampaignID )
					and Pricing_Year = @Year
					and Pricing_Season = @Season
					and (PD.Language_Code = @lang or PD.Language_code='EN/FR')
					and  FSApplicabilityId <> 43104
				****************************************************************************************************/
				Select Distinct @Content= FSContent_Catalog_Code 
				From 
					qspcanadaproduct..ProgFSSectionMap map,  QSPCanadaProduct..Pricing_Details PD,
					QSPCanadaCommon..TaxRegionProvince TRP,
					QSPCanadaProduct..Program_Master pm,
					QSPCanadaProduct..ProgramSection ps
					where ProgramSectionID = CATALOG_SECTION_ID
					and map.program_id = @ProgramID
					and (PD.TaxRegionID = TRP.TaxRegionID OR PD.TaxRegionID = 0)
					and Trp.Province = QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince( @CampaignID )
					and Pricing_Year = @Year
					--and Pricing_Season = @Season
					and (PD.Language_Code = @lang or PD.Language_code='EN/FR')
					and  FSApplicabilityId <> 43104
					and ps.id=pd.ProgramSectionID
					and pm.Program_ID=ps.Program_ID
					and pm.Lang=@lang
					and pd.FSContent_Catalog_Code <> ''

								
			end
			else
			begin
				/********* Disabled MS Aug 16, 2007 Issue# 3163 multiple record selected because of in-adequate filter
					select @Content= FSContent_Catalog_Code from 
					qspcanadaproduct..ProgFSSectionMap map,  QSPCanadaProduct..Pricing_Details PD,
					QSPCanadaCommon..TaxRegionProvince TRP
					where ProgramSectionID = CATALOG_SECTION_ID
					and map.program_id = @ProgramID
					and PD.TaxRegionID= TRP.TaxRegionID
					and Trp.Province = QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince( @CampaignID )
					and Pricing_Year = @Year
					and Pricing_Season = @Season
					and (PD.Language_Code = @lang or PD.Language_code='EN/FR')
					and FSApplicabilityId = 43104
				***********************************************************************************************/
				Select Distinct @Content= FSContent_Catalog_Code 
				From 
					qspcanadaproduct..ProgFSSectionMap map,  QSPCanadaProduct..Pricing_Details PD,
					QSPCanadaCommon..TaxRegionProvince TRP,
					QSPCanadaProduct..Program_Master pm,
					QSPCanadaProduct..ProgramSection ps
					where ProgramSectionID = CATALOG_SECTION_ID
					and map.program_id = @ProgramID
					and (PD.TaxRegionID = TRP.TaxRegionID OR PD.TaxRegionID = 0)
					and Trp.Province = QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince( @CampaignID )
					and Pricing_Year = @Year
					--and Pricing_Season = @Season
					and (PD.Language_Code = @lang or PD.Language_code='EN/FR')
					and  FSApplicabilityId = 43104
					and ps.id=pd.ProgramSectionID
					and pm.Program_ID=ps.Program_ID
					and pm.Lang=@lang
					and pd.FSContent_Catalog_Code <> ''

print 'con'
print @lang
print @programID
print @year
--print @season
print @content
			end
		end
		else
		begin

			if(@IsCombo=1)
			begin
				select @Content= isnull(FSContent_Catalog_Code,'') from 
					qspcanadaproduct..ProgFSSectionMap map,  QSPCanadaProduct..Pricing_Details PD,
					QSPCanadaCommon..TaxRegionProvince TRP
					where ProgramSectionID = CATALOG_SECTION_ID
					and map.program_id = @ProgramID
					and (PD.TaxRegionID = TRP.TaxRegionID OR PD.TaxRegionID = 0)
					and Trp.Province = QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince( @CampaignID )
					and Pricing_Year = @Year
					--and Pricing_Season = @Season
					and FSApplicabilityId IN (43101, 43102) --43101:Any Campaign, 43102:Combo
					and (PD.Language_Code = @lang or PD.Language_code='EN/FR')
					and pd.FSContent_Catalog_Code <> ''

			end
			else
			begin
				select @Content= isnull(FSContent_Catalog_Code,'') from 
					qspcanadaproduct..ProgFSSectionMap map,  QSPCanadaProduct..Pricing_Details PD,
					QSPCanadaCommon..TaxRegionProvince TRP
					where ProgramSectionID = CATALOG_SECTION_ID
					and map.program_id = @ProgramID
					and (PD.TaxRegionID = TRP.TaxRegionID OR PD.TaxRegionID = 0)
					and Trp.Province = QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince( @CampaignID )
					and Pricing_Year = @Year
					--and Pricing_Season = @Season
					and FSApplicabilityId in (43101, 43103)
					and (PD.Language_Code = @lang or PD.Language_code='EN/FR')
					and pd.FSContent_Catalog_Code <> ''


			end
		end
/*
select * from 
			qspcanadaproduct..ProgFSSectionMap map,  QSPCanadaProduct..Pricing_Details PD,
			QSPCanadaCommon..TaxRegionProvince TRP
			where ProgramSectionID = CATALOG_SECTION_ID
			and map.program_id = @ProgramID
			and PD.TaxRegionID= TRP.TaxRegionID
			and Trp.Province = QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince( @CampaignID )
			and Pricing_Year = @Year
			and Pricing_Season = @Season
*/
		if(@Content<>'')
		begin
			--do a true physical insert
			INSERT INTO CampaignToContentCatalog(
	 			CampaignId, 
	 			Content_Catalog_Code,
	 			ProgramId,
				ModifyDate,
				ModifiedBy,
				DeletedTF,
				ProgramContentCatalogCodeLookupID
			)VALUES(
	 			@CampaignID,
				@Content,
	 			@ProgramID,
				getdate(),
				@UserIDModified,
				0,
				0
			);
		End
	END	

--	return(0);
end
ELSE IF (@CatalogCount = 0 AND @Choice = 0)
begin
	 --DO NOTHING (no Campaign/Program/Catalog marriage, none wanted)
	return(0);
end
ELSE  IF (@CatalogCount = 1 AND @Choice = 1)
begin
	 --DO NOTHING (user is happy with the choice of current Campaign/Program/Catalog)
	return(0);
end
ELSE  IF (@CatalogCount = 1 AND @Choice = 0) 
begin
	--user wants to delete an existing Campaign/Program/Catalog
	
	--check to see if it can be deleted
	DECLARE @AllowDeletion int
	--etc find out this rule
	SELECT @AllowDeletion = 0
	if (@AllowDeletion > 0)
	begin
		--this deletion is not allowed
		return (-5);
	end
	else
	begin
		--yeah, do the deletion
		UPDATE 
			CampaignToContentCatalog
		SET
			DeletedTF = 1,
			ModifyDate = getdate(),
			ModifiedBy = @UserIDModified
		WHERE
			CampaignID = @CampaignID 
			AND ProgramId = @ProgramID
			AND Content_Catalog_Code = @Content_Catalog_Code

		return(0);
	end
end


--print @@fetch_status
GO

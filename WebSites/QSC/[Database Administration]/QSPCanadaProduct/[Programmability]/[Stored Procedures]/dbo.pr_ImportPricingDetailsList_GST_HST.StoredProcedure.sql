USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ImportPricingDetailsList_GST_HST]    Script Date: 06/07/2017 09:17:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
-- 28/05/2006 - Ben : SHOULD NOT BE USED ANYMORE - The process is managed in the .NET code
CREATE PROCEDURE [dbo].[pr_ImportPricingDetailsList_GST_HST]

	@iNewCatalogSectionID	int,
	@iProductInstance	int = 0,
	@zProductCode		varchar(20) = '',
	@zRemitCode		varchar(20) = '',
	@zProductName	varchar(55) = '',
	@iYearSearch		int = 0,
	@zSeasonSearch	varchar(1) = '',
	@iProductStatus	int = 0,
	@iProductType		int = 0,
	@iNumberOfIssues	int = 0,
	@zOracleCode		varchar(30) = '',
	@zCatalogCode		varchar(10) = '',
	@iPublisherID		int = 0,
	@iFulfillmentHouseID	int = 0,
	@zImportForSeason	varchar(1) = 'F',
	@zUserID		varchar(30)

AS

	DECLARE	@iDuplicateCount			int,
			@iYear					int,
			@zSeason				varchar(1),	
			@dStartDate				datetime,
			@dEndDate				datetime,
			@iOfferCode				int,
			@iProductCount				int


	DECLARE	@iMagPrice_InstanceGSTCopy		int,
			@iMagPrice_InstanceHSTCopy		int,
			@zProduct_CodeCopy			varchar(20),
			@zRemitCodeCopy			varchar(20),
			@iProgram_IDCopy			int,
			@zProgram_TypeCopy			varchar(25),
			@iYearCopy				int,
			@zSeasonCopy				varchar(1),
			@zProduct_Sort_NameCopy		varchar(55),
			@iOffer_CodeCopy			int,
			@iStatusInstanceCopy			int,
			@zStatusCopy				varchar(64),
			@fRemit_RateCopy			numeric(10, 4),
			@iNbr_Of_IssuesCopy			int,
			@iDurationCopy				int,
			@zDuration_MeasureCopy		varchar(10),
			@fNewsStand_Price_YrCopy		numeric(10, 2),
			@fBasic_Price_YrCopy			numeric(10, 2),
			@fGSTPriceCopy			numeric(10, 2),
			@fHSTPriceCopy			numeric(10, 2),
			@bEffortKeyRequiredCopy		bit,
			@zEffort_KeyCopy			varchar(40),
			@fNewsStandPriceOriginalCurrencyCopy	numeric(10, 2),
			@fConversionRateCopy			numeric(10, 2),
			@zCommentCopy			varchar(200),
			@fBasePriceOriginalCurrencyCopy	numeric(10, 2),
			@fDefaultGrossValueCopy		numeric(10, 2),
			@iFSExtra_Limit_RateCopy		int,
			@iFSIsBrochureCopy			int,
			@iFSApplicabilityIdCopy			int,
			@zFSApplicabilityCopy			varchar(64),
			@iFSDistributionLevelIdCopy		int,
			@zFSDistributionLevelCopy		varchar(64),
			@zLanguageCopy			varchar(10),
			@zFSCatalog_Product_CodeCopy	varchar(50),
			@zFSContent_Catalog_CodeCopy	varchar(50),
			@iFSProgram_IdCopy			int,
			@zOracleCodeCopy			varchar(50),
			@bInternetApprovalCopy			bit,
			@zABCCodeCopy			varchar(20),
			@bAdInCatalogCopy			bit,
			@iAdPageSizeIDCopy			int,
			@fAdCostCopy				numeric(10, 2),
			@iAdCostCurrencyCopy			int,
			@iListingLevelIDCopy			int,
			@zListingCopyTextCopy			varchar(500),
			@zPremiumIndCopy			varchar(1),
			@zPremiumCodeCopy			varchar(50),
			@zPremiumCopyCopy			varchar(500),
			@zFSProvinceCodeCopy		varchar(10),
			@iProductTypeInstanceCopy		int,
			@zProductTypeCopy			varchar(64),
			@zCatalogCodeCopy			varchar(10),
			@zCatalogNameCopy			varchar(50),
			@iPublisherIDCopy			int,
			@iFulfillmentHouseIDCopy		int



	-- Get informations for the season of the new contract
	SELECT	@iYear = s.FiscalYear,
			@zSeason = CASE s.Season WHEN 'Y' THEN CASE @zImportForSeason WHEN '' THEN 'F' ELSE @zImportForSeason END ELSE s.Season END,
			@dStartDate = s.StartDate,
			@dEndDate = s.EndDate
	FROM		ProgramSection ps,
			PROGRAM_MASTER pm,
			QSPCanadaCommon..Season s
	WHERE	ps.ID = @iNewCatalogSectionID
	AND		pm.Program_ID = ps.Program_ID
	AND		s.ID = pm.Season

	CREATE TABLE #temp
			(MagPrice_InstanceGST		int,
			MagPrice_InstanceHST		int,
			Product_Code			varchar(20),
			RemitCode			varchar(20),
			Program_ID			int,
			Program_Type			varchar(25),
			Year				int,
			Season				varchar(1),
			Product_Sort_Name		varchar(55),
			Offer_Code			int,
			StatusInstance			int,
			Status				varchar(64),
			Remit_Rate			numeric(10, 4),
			Nbr_Of_Issues			int,
			Duration			int,
			Duration_Measure		varchar(10),
			NewsStand_Price_Yr		numeric(10, 2),
			Basic_Price_Yr			numeric(10, 2),
			GSTPrice			numeric(10, 2),
			HSTPrice			numeric(10, 2),
			EffortKeyRequired		bit,
			Effort_Key			varchar(40),
			NewsStandPriceOriginalCurrency	numeric(10, 2),
			ConversionRate			numeric(10, 2),
			Comment			varchar(200),
			BasePriceOriginalCurrency	numeric(10, 2),
			DefaultGrossValue		numeric(10, 2),
			FSExtra_Limit_Rate		int,
			FSIsBrochure			int,
			FSApplicabilityId			int,
			FSApplicability			varchar(64),
			FSDistributionLevelId		int,
			FSDistributionLevel		varchar(64),
			Language			varchar(10),
			FSCatalog_Product_Code	varchar(50),
			FSContent_Catalog_Code	varchar(50),
			FSProgram_Id			int,
			OracleCode			varchar(50),
			InternetApproval			bit,
			ABCCode			varchar(20),
			AdInCatalog			bit,
			AdPageSizeID			int,
			AdCost				numeric(10, 2),
			AdCostCurrency			int,
			ListingLevelID			int,
			ListingCopyText			varchar(500),
			PremiumInd			varchar(1),
			PremiumCode			varchar(50),
			PremiumCopy			varchar(500),
			FSProvinceCode		varchar(10),
			ProductTypeInstance		int,
			ProductType			varchar(64),
			CatalogCode			varchar(10),
			CatalogName			varchar(50),
			PublisherID			int,
			FulfillmentHouseID		int)

	INSERT INTO #temp EXEC pr_SelectAllPricingDetails_GST_HST @iProductInstance, @zProductCode, @zRemitCode, @zProductName, @iYearSearch, @zSeasonSearch, @iProductStatus, @iProductType, @iNumberOfIssues, @zOracleCode, @zCatalogCode, @iPublisherID, @iFulfillmentHouseID, 0, 1

	DECLARE @temp int
	SELECT @temp = COUNT(*) from #temp

	PRINT @temp

	DECLARE c1 CURSOR FOR
		SELECT	MagPrice_InstanceGST,
				MagPrice_InstanceHST,
				Product_Code,
				RemitCode,
				Program_ID,
				Program_Type,
				Year,
				Season,
				Product_Sort_Name,
				Offer_Code,
				StatusInstance,
				Status,
				Remit_Rate,
				Nbr_Of_Issues,
				Duration,
				Duration_Measure,
				NewsStand_Price_Yr,
				Basic_Price_Yr,
				GSTPrice,
				HSTPrice,
				EffortKeyRequired,
				Effort_Key,
				NewsStandPriceOriginalCurrency,
				ConversionRate,
				Comment,
				BasePriceOriginalCurrency,
				DefaultGrossValue,
				FSExtra_Limit_Rate,
				FSIsBrochure,
				FSApplicabilityId,
				FSApplicability,
				FSDistributionLevelId,
				FSDistributionLevel,
				Language,
				FSCatalog_Product_Code,
				FSContent_Catalog_Code,
				FSProgram_Id,
				OracleCode,
				InternetApproval	,
				ABCCode,
				AdInCatalog,
				AdPageSizeID,
				AdCost,
				AdCostCurrency,
				ListingLevelID,
				ListingCopyText,
				PremiumInd,
				PremiumCode,
				PremiumInd,
				FSProvinceCode,
				ProductTypeInstance,
				ProductType,
				CatalogCode,
				CatalogName,
				PublisherID,
				FulfillmentHouseID
		FROM #temp

	CREATE TABLE #temp2
	(
		 NextInstance int
	)

	OPEN c1
	FETCH NEXT FROM c1 INTO @iMagPrice_InstanceGSTCopy, @iMagPrice_InstanceHSTCopy, @zProduct_CodeCopy, @zRemitCodeCopy, @iProgram_IDCopy, @zProgram_TypeCopy, @iYearCopy, @zSeasonCopy, @zProduct_Sort_NameCopy, @iOffer_CodeCopy, @iStatusInstanceCopy, @zStatusCopy, @fRemit_RateCopy, @iNbr_Of_IssuesCopy, @iDurationCopy, @zDuration_MeasureCopy, @fNewsStand_Price_YrCopy, @fBasic_Price_YrCopy, @fGSTPriceCopy, @fHSTPriceCopy, @bEffortKeyRequiredCopy, @zEffort_KeyCopy, @fNewsStandPriceOriginalCurrencyCopy, @fConversionRateCopy, @zCommentCopy, @fBasePriceOriginalCurrencyCopy, @fDefaultGrossValueCopy, @iFSExtra_Limit_RateCopy, @iFSIsBrochureCopy, @iFSApplicabilityIdCopy, @zFSApplicabilityCopy, @iFSDistributionLevelIdCopy, @zFSDistributionLevelCopy, @zLanguageCopy, @zFSCatalog_Product_CodeCopy, @zFSContent_Catalog_CodeCopy, @iFSProgram_IdCopy, @zOracleCodeCopy, @bInternetApprovalCopy, @zABCCodeCopy, @bAdInCatalogCopy, @iAdPageSizeIDCopy, @fAdCostCopy, @iAdCostCurrencyCopy, @iListingLevelIDCopy, @zListingCopyTextCopy, @zPremiumIndCopy, @zPremiumCodeCopy, @zPremiumCopyCopy, @zFSProvinceCodeCopy, @iProductTypeInstanceCopy, @zProductTypeCopy, @zCatalogCodeCopy, @zCatalogNameCopy, @iPublisherIDCopy, @iFulfillmentHouseIDCopy
	WHILE @@fetch_status = 0
	BEGIN
		PRINT 'OK'

		SELECT	@iDuplicateCount = COUNT(pd.MagPrice_Instance)
		FROM		Product p,
				Pricing_Details pd
		WHERE	p.Product_Code = @zProduct_CodeCopy
		AND		p.Product_Year = @iYear
		AND		p.Product_Season = @zSeason
		AND		pd.Product_Instance = p.Product_Instance
		AND		pd.Nbr_of_Issues = @iNbr_of_IssuesCopy
		AND		pd.ProgramSectionID = @iNewCatalogSectionID

		IF(@iDuplicateCount = 0)
		BEGIN			
			SELECT	@iProductCount = count(p.Product_Code)
			FROM		Product p
			WHERE	p.Product_Code = @zProduct_CodeCopy
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
	
			if(coalesce(@iProductCount, 0) = 0)
			BEGIN
				-- Create a new product
				INSERT INTO	Product
						(Product_Code,
						RemitCode,
						Product_Year,
						Product_Season,
						Alpha_Code,
						Product_Name,
						Product_Sort_Name,
						Pub_Nbr,
						Ages,
						Internet,
						Issue_Rcvd_Dt,
						CoverReceived,
						HighlightCover,
						Featuring,
						Status,
						Comment,
						CommentDate,
						Category_Code,
						Fulfill_House_Nbr,
						Mail_Dt,
						Auth_Form_Rtrn_Dt,
						IssueDateUsed,
						Logged_By,
						Log_Dt,
						Lang,
						ProductLine,
						DaysLeadTime,
						VendorNumber,
						VendorSiteName,
						PayGroupLookUpCode,
						TermsName,
						Currency,
						CountryCode,
						Type,
						UnitOfMeasure,
						UOMConvFactor,
						UnitWeight,
						UnitCost,
						OracleCode,
						Prize_Level,
						Prize_Level_Qty_Required,
						Nbr_Of_Issues_Per_Year,
						RemitCode,
						IsQSPExclusive)
				SELECT	Product_Code,
						RemitCode,
						@iYear,
						@zSeason,
						Alpha_Code,
						Product_Name,
						Product_Sort_Name,
						Pub_Nbr,
						Ages,
						Internet,
						Issue_Rcvd_Dt,
						CoverReceived,
						HighlightCover,
						Featuring,
						Status,
						Comment,
						CommentDate,
						Category_Code,
						Fulfill_House_Nbr,
						Mail_Dt,
						Auth_Form_Rtrn_Dt,
						IssueDateUsed,
						substring(@zUserID, 1, 15),
						getdate(),
						Lang,
						ProductLine,
						DaysLeadTime,
						VendorNumber,
						VendorSiteName,
						PayGroupLookUpCode,
						TermsName,
						Currency,
						CountryCode,
						Type,
						UnitOfMeasure,
						UOMConvFactor,
						UnitWeight,
						UnitCost,
						OracleCode,
						Prize_Level,
						Prize_Level_Qty_Required,
						Nbr_Of_Issues_Per_Year,
						RemitCode,
						IsQSPExclusive
				FROM		Product
				WHERE	Product_Code = @zProduct_CodeCopy
				AND		Product_Year = @iYearCopy
				AND		Product_Season = @zSeasonCopy
			END
			
			IF(@iProductTypeInstanceCopy = 46001)
			BEGIN
				SELECT	DISTINCT
						@iOfferCode = pd.Offer_Code
				FROM		Product p,
						Pricing_Details pd
				WHERE	p.Product_Code = @zProduct_CodeCopy
				AND		p.Product_Year = @iYear
				AND		p.Product_Season = @zSeason
				AND		pd.Product_Instance = p.Product_Instance
				AND		pd.Nbr_of_Issues = @iNbr_of_IssuesCopy
		
				IF(coalesce(@iOfferCode, 0) = 0)
				BEGIN
					-- Create a new offer code
					DELETE FROM #temp2
				
					insert into #temp2 exec qspcanadaordermanagement..InsertNextInstance 19 -- OfferCodeNext
					select @iOfferCode = nextinstance from #temp2
					truncate table #temp2
				END
			END
			ELSE
			BEGIN
				SET @iOfferCode = @iOffer_CodeCopy
			END
	
	
			INSERT INTO	Pricing_Details
					(Product_Instance,
					Pricing_Year,
					Pricing_Season,
					Product_Code,
					Program_ID,
					Program_Type,
					ProgramSectionID,
					Offer_Code,
					Status,
					Remit_Rate,
					Nbr_of_Issues,
					Duration,
					Duration_Measure,
					NewsStand_Price_Yr,
					Basic_Price_Yr,
					QSP_Price,
					EffortKeyRequired,
					Effort_Key,
					Logged_By,
					Log_Dt,
					EffectiveDate,
					EndDate,
					NewsStandPriceOriginalCurrency,
					ConversionRate,
					Comment,
					DateSubmitted,
					BasePriceOriginalCurrency,
					TaxRegionID,
					DefaultGrossValue,
					FSExtra_Limit_Rate,
					FSIsBrochure,
					FSApplicabilityId,
					FSDistributionLevelId,
					Language_Code,
					FSCatalog_Product_Code,
					FSContent_Catalog_Code,
					FSProgram_Id,
					OracleCode,
					InternetApproval,
					ABCCode,
					AdInCatalog,
					AdPageSizeID,
					AdCost,
					AdCostCurrency,
					ListingLevelID,
					ListingCopyText,
					QSPPremiumID,
					prdPremiumInd,
					prdPremiumCode,
					prdPremiumCopy,
					FSProvinceCode)
			SELECT	p.Product_Instance,
					@iYear,
					@zSeason,
					@zProduct_CodeCopy,
					@iProgram_IDCopy,
					@zProgram_TypeCopy,
					@iNewCatalogSectionID,
					@iOfferCode,
					@iStatusInstanceCopy,
					@fRemit_RateCopy,
					@iNbr_of_IssuesCopy,
					@iDurationCopy,
					@zDuration_MeasureCopy,
					@fNewsStand_Price_YrCopy,
					@fBasic_Price_YrCopy,
					CASE	@iProductTypeInstanceCopy
						WHEN	46001 THEN CEILING(ROUND((@fBasic_Price_YrCopy * CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END *
							(SELECT	1 + (ConsolidatedRate / 100)
							FROM		QSPCanadaCommon..TaxRegion
							WHERE	ID = 1
							AND		EffectiveDate =
									(SELECT	max(EffectiveDate)
									FROM 		QSPCanadaCommon..TaxRegion
									WHERE	ID = 1
									AND		EffectiveDate <= getdate()))), 2))
						ELSE	@fGSTPriceCopy
						END,
					@bEffortKeyRequiredCopy,
					@zEffort_KeyCopy,
					@zUserID,
					getdate(),
					@dStartDate,
					@dEndDate,
					CASE	@iProductTypeInstanceCopy
						WHEN	46001 THEN ROUND(@fNewsStand_Price_YrCopy / CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END, 2)
						ELSE	@fNewsStandPriceOriginalCurrencyCopy
					END,
					CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END,
					@zCommentCopy,
					getdate(),
					CASE	@iProductTypeInstanceCopy
						WHEN	46001 THEN ROUND(@fBasic_Price_YrCopy * CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END, 2)
						ELSE	@fBasePriceOriginalCurrencyCopy
					END,
					1,
					@fDefaultGrossValueCopy,
					@iFSExtra_Limit_RateCopy,
					@iFSIsBrochureCopy,
					@iFSApplicabilityIdCopy,
					@iFSDistributionLevelIdCopy,
					@zLanguageCopy,
					@zFSCatalog_Product_CodeCopy,
					@zFSContent_Catalog_CodeCopy,
					@iFSProgram_IdCopy,					@zOracleCodeCopy,
					@bInternetApprovalCopy,
					@zABCCodeCopy,
					@bAdInCatalogCopy,
					@iAdPageSizeIDCopy,
					@fAdCostCopy,
					@iAdCostCurrencyCopy,
					@iListingLevelIDCopy,
					@zListingCopyTextCopy,
					0,
					@zPremiumIndCopy,
					@zPremiumCodeCopy,
					@zPremiumCopyCopy,
					@zFSProvinceCodeCopy
			FROM		Product p,
					QSPCanadaCommon..Season s
			WHERE	p.Product_Code = @zProduct_CodeCopy
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			AND		s.FiscalYear = @iYear
			AND		s.Season = @zSeason
	
			INSERT INTO	Pricing_Details
					(Product_Instance,
					Pricing_Year,
					Pricing_Season,
					Product_Code,
					Program_ID,
					Program_Type,
					ProgramSectionID,
					Offer_Code,
					Status,
					Remit_Rate,
					Nbr_of_Issues,
					Duration,
					Duration_Measure,
					NewsStand_Price_Yr,
					Basic_Price_Yr,
					QSP_Price,
					EffortKeyRequired,
					Effort_Key,
					Logged_By,
					Log_Dt,
					EffectiveDate,
					EndDate,
					NewsStandPriceOriginalCurrency,
					ConversionRate,
					Comment,
					DateSubmitted,
					BasePriceOriginalCurrency,
					TaxRegionID,
					DefaultGrossValue,
					FSExtra_Limit_Rate,
					FSIsBrochure,
					FSApplicabilityId,
					FSDistributionLevelId,
					Language_Code,
					FSCatalog_Product_Code,
					FSContent_Catalog_Code,
					FSProgram_Id,
					OracleCode,
					InternetApproval,
					ABCCode,
					AdInCatalog,
					AdPageSizeID,
					AdCost,
					AdCostCurrency,
					ListingLevelID,
					ListingCopyText,
					QSPPremiumID,
					prdPremiumInd,
					prdPremiumCode,
					prdPremiumCopy,
					FSProvinceCode)
			SELECT	p.Product_Instance,
					@iYear,
					@zSeason,
					@zProduct_CodeCopy,
					@iProgram_IDCopy,
					@zProgram_TypeCopy,
					@iNewCatalogSectionID,
					@iOfferCode,
					@iStatusInstanceCopy,
					@fRemit_RateCopy,
					@iNbr_of_IssuesCopy,
					@iDurationCopy,
					@zDuration_MeasureCopy,
					@fNewsStand_Price_YrCopy,
					@fBasic_Price_YrCopy,
					CASE	@iProductTypeInstanceCopy
						WHEN	46001 THEN CEILING(ROUND((@fBasic_Price_YrCopy * CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END *
							(SELECT	1 + (ConsolidatedRate / 100)
							FROM		QSPCanadaCommon..TaxRegion
							WHERE	ID = 2
							AND		EffectiveDate =
									(SELECT	max(EffectiveDate)
									FROM 		QSPCanadaCommon..TaxRegion
									WHERE	ID = 2
									AND		EffectiveDate <= getdate()))), 2))
						ELSE	@fHSTPriceCopy
						END,
					@bEffortKeyRequiredCopy,
					@zEffort_KeyCopy,
					@zUserID,
					getdate(),
					@dStartDate,
					@dEndDate,
					CASE	@iProductTypeInstanceCopy
						WHEN	46001 THEN ROUND(@fNewsStand_Price_YrCopy / CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END, 2)
						ELSE	@fNewsStandPriceOriginalCurrencyCopy
					END,
					CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END,
					@zCommentCopy,
					getdate(),
					CASE	@iProductTypeInstanceCopy
						WHEN	46001 THEN ROUND(@fBasic_Price_YrCopy * CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END, 2)
						ELSE	@fBasePriceOriginalCurrencyCopy
					END,
					2,
					@fDefaultGrossValueCopy,
					@iFSExtra_Limit_RateCopy,
					@iFSIsBrochureCopy,
					@iFSApplicabilityIdCopy,
					@iFSDistributionLevelIdCopy,
					@zLanguageCopy,
					@zFSCatalog_Product_CodeCopy,
					@zFSContent_Catalog_CodeCopy,
					@iFSProgram_IdCopy,
					@zOracleCodeCopy,
					@bInternetApprovalCopy,
					@zABCCodeCopy,
					@bAdInCatalogCopy,
					@iAdPageSizeIDCopy,
					@fAdCostCopy,
					@iAdCostCurrencyCopy,
					@iListingLevelIDCopy,
					@zListingCopyTextCopy,
					0,
					@zPremiumIndCopy,
					@zPremiumCodeCopy,
					@zPremiumCopyCopy,
					@zFSProvinceCodeCopy
			FROM		Product p,
					QSPCanadaCommon..Season s
			WHERE	p.Product_Code = @zProduct_CodeCopy
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			AND		s.FiscalYear = @iYear
			AND		s.Season = @zSeason
		END
		ELSE IF(@iProductTypeInstanceCopy = 46001)
		BEGIN
			-- If Magazine, insert pending contract that has to be updated
			INSERT INTO	Pricing_Details
					(Product_Instance,
					Pricing_Year,
					Pricing_Season,
					Product_Code,
					Program_ID,
					Program_Type,
					ProgramSectionID,
					Offer_Code,
					Status,
					Remit_Rate,
					Nbr_of_Issues,
					Duration,
					Duration_Measure,
					NewsStand_Price_Yr,
					Basic_Price_Yr,
					QSP_Price,
					EffortKeyRequired,
					Effort_Key,
					Logged_By,
					Log_Dt,
					EffectiveDate,
					EndDate,
					NewsStandPriceOriginalCurrency,
					ConversionRate,
					Comment,
					DateSubmitted,
					BasePriceOriginalCurrency,
					TaxRegionID,
					DefaultGrossValue,
					FSExtra_Limit_Rate,
					FSIsBrochure,
					FSApplicabilityId,
					FSDistributionLevelId,
					Language_Code,
					FSCatalog_Product_Code,
					FSContent_Catalog_Code,
					FSProgram_Id,
					OracleCode,
					InternetApproval,
					ABCCode,
					AdInCatalog,
					AdPageSizeID,
					AdCost,
					AdCostCurrency,
					ListingLevelID,
					ListingCopyText,
					QSPPremiumID,
					prdPremiumInd,
					prdPremiumCode,
					prdPremiumCopy,
					FSProvinceCode)
			SELECT	p.Product_Instance,
					@iYear,
					@zSeason,
					@zProduct_CodeCopy,
					0,
					'',
					@iNewCatalogSectionID,
					0,
					30602,
					@fRemit_RateCopy,
					0,
					@iDurationCopy,
					@zDuration_MeasureCopy,
					0,
					0,
					0,
					@bEffortKeyRequiredCopy,
					'',
					@zUserID,
					getdate(),
					@dStartDate,
					@dEndDate,
					0,
					CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END,
					@zCommentCopy,
					getdate(),
					0,
					1,
					@fDefaultGrossValueCopy,
					@iFSExtra_Limit_RateCopy,
					@iFSIsBrochureCopy,
					@iFSApplicabilityIdCopy,
					@iFSDistributionLevelIdCopy,
					@zLanguageCopy,
					@zFSCatalog_Product_CodeCopy,
					@zFSContent_Catalog_CodeCopy,
					@iFSProgram_IdCopy,
					@zOracleCodeCopy,
					@bInternetApprovalCopy,
					@zABCCodeCopy,
					@bAdInCatalogCopy,
					@iAdPageSizeIDCopy,
					@fAdCostCopy,
					@iAdCostCurrencyCopy,
					@iListingLevelIDCopy,
					@zListingCopyTextCopy,
					0,
					'',
					'',
					'',
					@zFSProvinceCodeCopy
			FROM		Product p,
					QSPCanadaCommon..Season s
			WHERE	p.Product_Code = @zProduct_CodeCopy
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			AND		s.FiscalYear = @iYear
			AND		s.Season = @zSeason
	
			INSERT INTO	Pricing_Details
					(Product_Instance,
					Pricing_Year,
					Pricing_Season,
					Product_Code,
					Program_ID,
					Program_Type,
					ProgramSectionID,
					Offer_Code,
					Status,
					Remit_Rate,
					Nbr_of_Issues,
					Duration,
					Duration_Measure,
					NewsStand_Price_Yr,
					Basic_Price_Yr,
					QSP_Price,
					EffortKeyRequired,
					Effort_Key,
					Logged_By,
					Log_Dt,
					EffectiveDate,
					EndDate,
					NewsStandPriceOriginalCurrency,
					ConversionRate,
					Comment,
					DateSubmitted,
					BasePriceOriginalCurrency,
					TaxRegionID,
					DefaultGrossValue,
					FSExtra_Limit_Rate,
					FSIsBrochure,
					FSApplicabilityId,
					FSDistributionLevelId,
					Language_Code,
					FSCatalog_Product_Code,
					FSContent_Catalog_Code,
					FSProgram_Id,
					OracleCode,
					InternetApproval,
					ABCCode,
					AdInCatalog,
					AdPageSizeID,
					AdCost,
					AdCostCurrency,
					ListingLevelID,
					ListingCopyText,
					QSPPremiumID,
					prdPremiumInd,
					prdPremiumCode,
					prdPremiumCopy,
					FSProvinceCode)
			SELECT	p.Product_Instance,
					@iYear,
					@zSeason,
					@zProduct_CodeCopy,
					0,
					'',
					@iNewCatalogSectionID,
					0,
					30602,
					@fRemit_RateCopy,
					0,
					@iDurationCopy,
					@zDuration_MeasureCopy,
					0,
					0,
					0,
					@bEffortKeyRequiredCopy,
					'',
					@zUserID,
					getdate(),
					@dStartDate,
					@dEndDate,
					0,
					CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE @fConversionRateCopy END,
					@zCommentCopy,
					getdate(),
					0,
					2,
					@fDefaultGrossValueCopy,
					@iFSExtra_Limit_RateCopy,
					@iFSIsBrochureCopy,
					@iFSApplicabilityIdCopy,
					@iFSDistributionLevelIdCopy,
					@zLanguageCopy,
					@zFSCatalog_Product_CodeCopy,
					@zFSContent_Catalog_CodeCopy,
					@iFSProgram_IdCopy,
					@zOracleCodeCopy,
					@bInternetApprovalCopy,
					@zABCCodeCopy,
					@bAdInCatalogCopy,
					@iAdPageSizeIDCopy,
					@fAdCostCopy,
					@iAdCostCurrencyCopy,
					@iListingLevelIDCopy,
					@zListingCopyTextCopy,
					0,
					'',
					'',
					'',
					@zFSProvinceCodeCopy
			FROM		Product p,
					QSPCanadaCommon..Season s
			WHERE	p.Product_Code = @zProduct_CodeCopy
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			AND		s.FiscalYear = @iYear
			AND		s.Season = @zSeason
		END

		FETCH NEXT FROM c1 INTO @iMagPrice_InstanceGSTCopy, @iMagPrice_InstanceHSTCopy, @zProduct_CodeCopy, @zRemitCodeCopy, @iProgram_IDCopy, @zProgram_TypeCopy, @iYearCopy, @zSeasonCopy, @zProduct_Sort_NameCopy, @iOffer_CodeCopy, @iStatusInstanceCopy, @zStatusCopy, @fRemit_RateCopy, @iNbr_Of_IssuesCopy, @iDurationCopy, @zDuration_MeasureCopy, @fNewsStand_Price_YrCopy, @fBasic_Price_YrCopy, @fGSTPriceCopy, @fHSTPriceCopy, @bEffortKeyRequiredCopy, @zEffort_KeyCopy, @fNewsStandPriceOriginalCurrencyCopy, @fConversionRateCopy, @zCommentCopy, @fBasePriceOriginalCurrencyCopy, @fDefaultGrossValueCopy, @iFSExtra_Limit_RateCopy, @iFSIsBrochureCopy, @iFSApplicabilityIdCopy, @zFSApplicabilityCopy, @iFSDistributionLevelIdCopy, @zFSDistributionLevelCopy, @zLanguageCopy, @zFSCatalog_Product_CodeCopy, @zFSContent_Catalog_CodeCopy, @iFSProgram_IdCopy, @zOracleCodeCopy, @bInternetApprovalCopy, @zABCCodeCopy, @bAdInCatalogCopy, @iAdPageSizeIDCopy, @fAdCostCopy, @iAdCostCurrencyCopy, @iListingLevelIDCopy, @zListingCopyTextCopy, @zPremiumIndCopy, @zPremiumCodeCopy, @zPremiumCopyCopy, @zFSProvinceCodeCopy, @iProductTypeInstanceCopy, @zProductTypeCopy, @zCatalogCodeCopy, @zCatalogNameCopy, @iPublisherIDCopy, @iFulfillmentHouseIDCopy
	END

	CLOSE c1
	DEALLOCATE c1

	DROP TABLE 		#temp2
	TRUNCATE TABLE	#temp
	DROP TABLE		#temp
GO

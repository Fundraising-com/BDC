USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ImportPricingDetails_GST_HST]    Script Date: 06/07/2017 09:17:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE[dbo].[pr_ImportPricingDetails_GST_HST]

	@iMagPriceInstanceGST	int,
	@iMagPriceInstanceHST	int,
	@iProductInstance		int,
	@iNewCatalogSectionID	int,
	@zImportForSeason		varchar(1) = 'F',
	@zUserID				varchar(30),
	@zNewProductCode		varchar(20)

AS

	DECLARE @iOfferCode			int

	DECLARE @iDuplicateCount		int
	DECLARE @iYear			int
	DECLARE @zSeason			varchar(1)
	DECLARE @iNumberOfIssues		int
	DECLARE @iProductType		int
	DECLARE @iContractProductInstance	int

	DECLARE @dStartDate			datetime
	DECLARE @dEndDate			datetime

	DECLARE @iProductCount		int

	DECLARE @iNewMagPriceInstanceGST	int
	DECLARE @iNewMagPriceInstanceHST	int

	
	if(@iMagPriceInstanceGST <> 0 AND @iMagPriceInstanceHST <> 0)
	BEGIN
		-- Get informations for the Offer Code and the season of the new contract
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

		-- Get Product Instance and Type
		SELECT	@iContractProductInstance = p.Product_Instance,
				@iProductType = p.Type
		FROM		Product p,
				Pricing_Details pd
		WHERE	pd.Product_Instance = p.Product_Instance
		AND		pd.MagPrice_Instance = @iMagPriceInstanceGST

		-- Check if the product exists for this year
		SELECT	@iProductCount = count(p.Product_Instance)
		FROM		Product p
				--Product pFrom,
				--Pricing_Details pdFrom
		WHERE	p.Product_Code = @zNewProductCode --pFrom.Product_Code
		AND		p.Product_Year = @iYear
		AND		p.Product_Season = @zSeason
		--AND		pFrom.Product_Instance = pdFrom.Product_Instance
		--AND		pdFrom.MagPrice_Instance = @iMagPriceInstanceGST

		if(coalesce(@iProductCount, 0) = 0)
		BEGIN
			-- Create a new product
			INSERT INTO	Product
					(Product_Code,
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
			SELECT	@zNewProductCode, --p.Product_Code,
					@iYear,
					@zSeason,
					p.Alpha_Code,
					p.Product_Name,
					p.Product_Sort_Name,
					p.Pub_Nbr,
					p.Ages,
					p.Internet,
					p.Issue_Rcvd_Dt,
					p.CoverReceived,
					p.HighlightCover,
					p.Featuring,
					p.Status,
					p.Comment,
					p.CommentDate,
					p.Category_Code,
					p.Fulfill_House_Nbr,
					p.Mail_Dt,
					p.Auth_Form_Rtrn_Dt,
					p.IssueDateUsed,
					substring(@zUserID, 1, 15),
					getdate(),
					p.Lang,
					p.ProductLine,
					p.DaysLeadTime,
					p.VendorNumber,
					p.VendorSiteName,
					p.PayGroupLookUpCode,
					p.TermsName,
					p.Currency,
					p.CountryCode,
					p.Type,
					p.UnitOfMeasure,
					p.UOMConvFactor,
					p.UnitWeight,
					p.UnitCost,
					p.OracleCode,
					p.Prize_Level,
					p.Prize_Level_Qty_Required,
					p.Nbr_Of_Issues_Per_Year,
					p.RemitCode,
					p.IsQSPExclusive
			FROM		Product p
			WHERE	p.Product_Instance = @iContractProductInstance

			SET @iDuplicateCount = 0
		END
		ELSE
		BEGIN
			SELECT	@iDuplicateCount = COUNT(pd.MagPrice_Instance)
			FROM		Pricing_Details pd,
					Product p
					--Pricing_Details pdFrom,
					--Product pFrom
			WHERE	--pdFrom.MagPrice_Instance = @iMagPriceInstanceGST
			--AND		pFrom.Product_Instance = pdFrom.Product_Instance
					p.Product_Code = @zNewProductCode --pFrom.Product_Code
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			AND		pd.Product_Instance = p.Product_Instance
			--AND		pd.Nbr_of_Issues = pdFrom.Nbr_of_Issues
			AND		pd.ProgramSectionID = @iNewCatalogSectionID
		END

		IF(@iDuplicateCount = 0)
		BEGIN
			IF(@iProductType = 46001)
			BEGIN
				-- Check if an offer code already exists
				SELECT	@iOfferCode = pd.Offer_Code
				FROM		Pricing_Details pd,
						Product p
						--Pricing_Details pdFrom,
						--Product pFrom
				WHERE	--pdFrom.MagPrice_Instance = @iMagPriceInstanceGST
				--AND		pFrom.Product_Instance = pdFrom.Product_Instance
						p.Product_Code = @zNewProductCode --pFrom.Product_Code
				AND		p.Product_Year = @iYear
				AND		p.Product_Season = @zSeason
				AND		pd.Product_Instance = p.Product_Instance
				--AND		pd.Nbr_of_Issues = pdFrom.Nbr_of_Issues
			
				if(coalesce(@iOfferCode, 0) = 0)
				begin
					-- Create a new offer code
					create table #temp
					(
						 NextInstance int
					)
				
					insert into #temp exec qspcanadaordermanagement..InsertNextInstance 19 -- OfferCodeNext
					select @iOfferCode = nextinstance from #temp
					truncate table #temp
						
					drop table #temp
				end
			END
			ELSE
			BEGIN
				SET @iOfferCode = 0
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
					FSProvinceCode,
					ContractFormReceived,
					MagazineCoverFilename,
					CatalogAdFilename,
					CatalogPageNumber,
					PlacementLevel, 
					ContractComment,
					PrinterComment,
					QSPCAListingCopyText,
					BasePriceSansPostage,
					PostageRemitRate,
					PostageAmount,
					BaseRemitRate,
					ListAgentCode,
					QSPAgencyCode)
			SELECT	p.Product_Instance,
					@iYear,
					@zSeason,
					p.Product_Code,
					pd.Program_ID,
					pd.Program_Type,
					@iNewCatalogSectionID,
					CASE COALESCE(@iOfferCode, 0) WHEN 0 THEN pd.Offer_Code ELSE @iOfferCode END,
					pd.Status,
					pd.Remit_Rate,
					pd.Nbr_of_Issues,
					pd.Duration,
					pd.Duration_Measure,
					pd.NewsStand_Price_Yr,
					pd.Basic_Price_Yr,
					CASE	@iProductType
						WHEN	46001 THEN CEILING(ROUND((pd.Basic_Price_Yr * CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE pd.ConversionRate END *
							(SELECT	1 + (ConsolidatedRate / 100)
							FROM		QSPCanadaCommon..TaxRegion
							WHERE	ID = pd.TaxRegionID
							AND		EffectiveDate =
									(SELECT	max(EffectiveDate)
									FROM 		QSPCanadaCommon..TaxRegion
									WHERE	ID = pd.TaxRegionID
									AND		EffectiveDate <= getdate()))), 2))
						ELSE	pd.QSP_Price
					END,
					pd.EffortKeyRequired,
					pd.Effort_Key,
					@zUserID,
					getdate(),
					@dStartDate,
					@dEndDate,
					CASE	@iProductType
						WHEN	46001 THEN ROUND(pd.NewsStand_Price_Yr / CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE pd.ConversionRate END, 2)
						ELSE	pd.NewsStandPriceOriginalCurrency
					END,
					CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE ConversionRate END,
					pd.Comment,
					getdate(),
					CASE	@iProductType

						WHEN	46001 THEN ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE pd.ConversionRate END, 2)
						ELSE	pd.BasePriceOriginalCurrency
					END,
					pd.TaxRegionID,
					pd.DefaultGrossValue,
					pd.FSExtra_Limit_Rate,
					pd.FSIsBrochure,
					pd.FSApplicabilityId,
					pd.FSDistributionLevelId,
					pd.Language_Code,
					pd.FSCatalog_Product_Code,
					pd.FSContent_Catalog_Code,
					pd.FSProgram_Id,
					pd.OracleCode,
					pd.InternetApproval,
					pd.ABCCode,
					pd.AdInCatalog,
					pd.AdPageSizeID,
					pd.AdCost,
					pd.AdCostCurrency,
					pd.ListingLevelID,
					pd.ListingCopyText,
					0,
					pd.prdPremiumInd,
					pd.prdPremiumCode,
					pd.prdPremiumCopy,
					pd.FSProvinceCode,
					0,--pd.ContractFormReceived,
					pd.MagazineCoverFilename,
					pd.CatalogAdFilename,
					pd.CatalogPageNumber,
					pd.PlacementLevel, 
					pd.ContractComment,
					pd.PrinterComment,
					pd.QSPCAListingCopyText,
					pd.BasePriceSansPostage,
					pd.PostageRemitRate,
					pd.PostageAmount,
					pd.BaseRemitRate,
					pd.ListAgentCode,
					pd.QSPAgencyCode	
			FROM		Pricing_Details pd,
					--Product pFrom,
					Product p,
					QSPCanadaCommon..Season s
			WHERE	--pFrom.Product_Instance = pd.Product_Instance
					p.Product_Code = @zNewProductCode --pFrom.Product_Code
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			AND		pd.MagPrice_Instance IN (@iMagPriceInstanceGST, @iMagPriceInstanceHST)
			AND		s.FiscalYear = @iYear
			AND		s.Season = @zSeason

		END
		ELSE IF(@iProductType = 46001)
		BEGIN
			-- If magazine, insert pending contract that has to be updated
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
					FSProvinceCode,	
					ContractFormReceived,
					MagazineCoverFilename,
					CatalogAdFilename,
					CatalogPageNumber,
					PlacementLevel, 
					ContractComment,
					PrinterComment,
					QSPCAListingCopyText,
					BasePriceSansPostage,
					PostageRemitRate,
					PostageAmount,
					BaseRemitRate,
					ListAgentCode,
					QSPAgencyCode)
			SELECT	p.Product_Instance,
					@iYear,
					@zSeason,
					p.Product_Code,
					0,
					'',
					@iNewCatalogSectionID,
					0,
					30602,
					pd.Remit_Rate,
					0,
					pd.Duration,
					pd.Duration_Measure,
					0,
					0,
					0,
					pd.EffortKeyRequired,
					'',
					@zUserID,
					getdate(),
					@dStartDate,
					@dEndDate,
					0,
					CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE ConversionRate END,
					pd.Comment,
					getdate(),
					0,
					pd.TaxRegionID,
					pd.DefaultGrossValue,
					pd.FSExtra_Limit_Rate,
					pd.FSIsBrochure,
					pd.FSApplicabilityId,
					pd.FSDistributionLevelId,
					pd.Language_Code,
					pd.FSCatalog_Product_Code,
					pd.FSContent_Catalog_Code,
					pd.FSProgram_Id,
					pd.OracleCode,
					pd.InternetApproval,
					pd.ABCCode,
					pd.AdInCatalog,
					pd.AdPageSizeID,
					pd.AdCost,
					pd.AdCostCurrency,
					pd.ListingLevelID,
					pd.ListingCopyText,
					0,
					pd.prdPremiumInd,
					pd.prdPremiumCode,
					pd.prdPremiumCopy,
					pd.FSProvinceCode,
					0,--pd.ContractFormReceived,
					pd.MagazineCoverFilename,
					pd.CatalogAdFilename,
					pd.CatalogPageNumber,
					pd.PlacementLevel, 
					pd.ContractComment,
					pd.PrinterComment,
					pd.QSPCAListingCopyText,
					pd.BasePriceSansPostage,
					pd.PostageRemitRate,
					pd.PostageAmount,
					pd.BaseRemitRate,
					pd.ListAgentCode,
					pd.QSPAgencyCode	
			FROM		Pricing_Details pd,
					--Product pFrom,
					Product p,
					QSPCanadaCommon..Season s
			WHERE	--pFrom.Product_Instance = pd.Product_Instance
					p.Product_Code = @zNewProductCode
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			AND		pd.MagPrice_Instance IN (@iMagPriceInstanceGST, @iMagPriceInstanceHST)
			AND		s.FiscalYear = @iYear
			AND		s.Season = @zSeason
		END

	END
	ELSE
	BEGIN
		-- Get informations for the Offer Code and the the new contract's season
		SELECT	@iYear = s.FiscalYear,
				@zSeason = s.Season,
				@dStartDate = s.StartDate,
				@dEndDate = s.EndDate
		FROM		ProgramSection ps,
				PROGRAM_MASTER pm,
				QSPCanadaCommon..Season s
		WHERE	ps.ID = @iNewCatalogSectionID
		AND		pm.Program_ID = ps.Program_ID
		AND		s.ID = pm.Season

		-- Check if the product exists for this year
		SELECT	@iProductCount = count(p.Product_Instance)
		FROM		Product p
				--Product pFrom
		WHERE	p.Product_Code = @zNewProductCode --pFrom.Product_Code
		AND		p.Product_Year = @iYear
		AND		p.Product_Season = @zSeason
		--AND		pFrom.Product_Instance = @iProductInstance

		if(coalesce(@iProductCount, 0) = 0)
		BEGIN
			-- Create a new product
			INSERT INTO	Product
					(Product_Code,
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
			SELECT	@zNewProductCode, --p.Product_Code,
					@iYear,
					@zSeason,
					p.Alpha_Code,
					p.Product_Name,
					p.Product_Sort_Name,
					p.Pub_Nbr,
					p.Ages,
					p.Internet,
					p.Issue_Rcvd_Dt,
					p.CoverReceived,
					p.HighlightCover,
					p.Featuring,
					p.Status,
					p.Comment,
					p.CommentDate,
					p.Category_Code,
					p.Fulfill_House_Nbr,
					p.Mail_Dt,
					p.Auth_Form_Rtrn_Dt,
					p.IssueDateUsed,
					substring(@zUserID, 1, 15),
					getdate(),
					p.Lang,
					p.ProductLine,
					p.DaysLeadTime,
					p.VendorNumber,
					p.VendorSiteName,
					p.PayGroupLookUpCode,
					p.TermsName,
					p.Currency,
					p.CountryCode,
					p.Type,
					p.UnitOfMeasure,
					p.UOMConvFactor,
					p.UnitWeight,
					p.UnitCost,
					p.OracleCode,
					p.Prize_Level,
					p.Prize_Level_Qty_Required,
					p.Nbr_Of_Issues_Per_Year,
					p.RemitCode,
					p.IsQSPExclusive
			FROM		Product p
			WHERE	p.Product_Instance = @iProductInstance

			SET @iDuplicateCount = 0
		END
		ELSE
		BEGIN
			SELECT	@iDuplicateCount = COUNT(pd.MagPrice_Instance)
			FROM		Pricing_Details pd,
					Product p
					--Product pFrom
			WHERE	p.Product_Code = @zNewProductCode --pFrom.Product_Code
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			--AND		pFrom.Product_Instance = @iProductInstance
			AND		pd.Product_Instance = p.Product_Instance
			AND		pd.Nbr_of_Issues = 0
			AND		pd.ProgramSectionID = @iNewCatalogSectionID
		END

		IF(@iDuplicateCount = 0)
		BEGIN			
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
					FSProvinceCode,
					ContractFormReceived,
					MagazineCoverFilename,
					CatalogAdFilename,
					CatalogPageNumber,
					PlacementLevel, 
					ContractComment,
					PrinterComment,
					QSPCAListingCopyText,
					BasePriceSansPostage,
					PostageRemitRate,
					PostageAmount,
					BaseRemitRate,
					ListAgentCode,
					QSPAgencyCode)
			SELECT	p.Product_Instance,
					p.Product_Year,
					p.Product_Season,
					p.Product_Code,
					0,
					'',
					@iNewCatalogSectionID,
					0,
					p.Status,
					0,
					0,
					0,
					'',
					0,
					0,
					0,
					0,
					'',
					p.Logged_By,
					p.Log_Dt,
					s.StartDate,
					s.EndDate,
					0,
					CASE p.Currency WHEN 801 THEN 1.00 WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE 1.00 END,
					'',
					getdate(),
					0,
					tr.ID,
					null,

					0,
					0,
					0,
					0,
					p.Lang,
					'',
					'',
					0,
					p.OracleCode,
					0,
					null,
					0,
					0,
					0,
					0,
					0,
					'',
					0,
					'',
					'',
					'',
					'',
					0,
					'',
					'',
					0,
					0, 
					'',
					'',
					'',
					0,
					0,
					0,
					0,
					null,
					null	
			FROM		Product p
			LEFT OUTER JOIN	QSPCanadaCommon..Season  s
						ON	s.FiscalYear = p.Product_Year
						AND	s.Season = p.Product_Season,
						(SELECT 1 AS ID
						UNION ALL
						SELECT 2 AS ID) tr
					--Product pFrom
			WHERE	p.Product_Code = @zNewProductCode--pFrom.Product_Code
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			--AND		pFrom.Product_Instance = @iProductInstance
		END
	END
GO

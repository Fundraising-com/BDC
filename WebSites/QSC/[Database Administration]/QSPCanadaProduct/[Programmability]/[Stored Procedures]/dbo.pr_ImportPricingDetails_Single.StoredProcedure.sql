USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ImportPricingDetails_Single]    Script Date: 06/07/2017 09:17:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE[dbo].[pr_ImportPricingDetails_Single]

	@iMagPriceInstance		int,
	@iProductInstance		int,
	@iNewCatalogSectionID	int,
	@zImportForSeason		varchar(1) = 'F',
	@zUserID				varchar(30),
	@zNewProductCode		varchar(20)

AS

	DECLARE @iDuplicateCount		int
	DECLARE @iYear			int
	DECLARE @zSeason			varchar(1)
	DECLARE @iProductType		int
	DECLARE @iContractProductInstance	int

	DECLARE @dStartDate			datetime
	DECLARE @dEndDate			datetime

	DECLARE @iProductCount		int

	
	if(@iMagPriceInstance <> 0)
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
		AND		pd.MagPrice_Instance = @iMagPriceInstance

		-- Check if the product exists for this year
		SELECT	@iProductCount = count(p.Product_Instance)
		FROM		Product p
				--Product pFrom,
				--Pricing_Details pdFrom
		WHERE	p.Product_Code = @zNewProductCode --pFrom.Product_Code
		AND		p.Product_Year = @iYear
		AND		p.Product_Season = @zSeason
		--AND		pFrom.Product_Instance = pdFrom.Product_Instance
		--AND		pdFrom.MagPrice_Instance = @iMagPriceInstance

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
					IsQSPExclusive,
					VendorProductCode)
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
					p.IsQSPExclusive,
					p.VendorProductCode
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
			WHERE	--pdFrom.MagPrice_Instance = @iMagPriceInstance
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
					AddlHandlingFee)
			SELECT	p.Product_Instance,
					@iYear,
					@zSeason,
					p.Product_Code,
					pd.Program_ID,
					pd.Program_Type,
					@iNewCatalogSectionID,
					pd.Offer_Code,
					pd.Status,
					pd.Remit_Rate,
					pd.Nbr_of_Issues,
					pd.Duration,
					pd.Duration_Measure,
					pd.NewsStand_Price_Yr,
					pd.Basic_Price_Yr,
					pd.QSP_Price,
					pd.EffortKeyRequired,
					pd.Effort_Key,
					@zUserID,
					getdate(),
					@dStartDate,
					@dEndDate,
					pd.NewsStandPriceOriginalCurrency,
					pd.ConversionRate,
					pd.Comment,
					getdate(),
					pd.BasePriceOriginalCurrency,
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
					NULL,
					pd.prdPremiumInd,
					pd.prdPremiumCode,
					pd.prdPremiumCopy,
					pd.FSProvinceCode,
					pd.AddlHandlingFee
			FROM		Pricing_Details pd,
					--Product pFrom,
					Product p
			WHERE	--pFrom.Product_Instance = pd.Product_Instance
					p.Product_Code = @zNewProductCode --pFrom.Product_Code
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			AND		pd.MagPrice_Instance = @iMagPriceInstance
		END
	END
	ELSE
	BEGIN
		-- Get informations for the Offer Code and the the new contract's season
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
					FSProvinceCode)
			SELECT	p.Product_Instance,
					p.Product_Year,
					p.Product_Season,
					p.Product_Code,
					0,
					'',
					@iNewCatalogSectionID,
					0,
					p.Status,
					NULL,
					0,
					NULL,
					NULL,
					NULL,
					NULL,
					0,
					0,
					NULL,
					p.Logged_By,
					p.Log_Dt,
					s.StartDate,
					s.EndDate,
					NULL,
					NULL,
					'',
					getdate(),
					NULL,
					0,
					NULL,					CASE p.Type WHEN 46004 THEN 0 ELSE NULL END,
					CASE p.Type WHEN 46004 THEN 0 ELSE NULL END,
					CASE p.Type WHEN 46004 THEN 0 ELSE NULL END,
					CASE p.Type WHEN 46004 THEN 0 ELSE NULL END,
					p.Lang,
					p.Product_Code,
					CASE p.Type WHEN 46004 THEN '' ELSE NULL END,
					CASE p.Type WHEN 46004 THEN 0 ELSE NULL END,
					p.OracleCode,
					0,
					NULL,
					NULL,
					NULL,
					NULL,
					NULL,
					NULL,
					NULL,
					NULL,
					NULL,
					NULL,
					NULL,
					CASE p.Type WHEN 46004 THEN '' ELSE NULL END
			FROM		Product p
			LEFT OUTER JOIN	QSPCanadaCommon..Season  s
						ON	s.FiscalYear = p.Product_Year
						AND	s.Season = p.Product_Season
					--Product pFrom
			WHERE	p.Product_Code = @zNewProductCode --pFrom.Product_Code
			AND		p.Product_Year = @iYear
			AND		p.Product_Season = @zSeason
			--AND		pFrom.Product_Instance = @iProductInstance
		END
	END
GO

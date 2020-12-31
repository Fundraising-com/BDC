USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertProduct]    Script Date: 06/07/2017 09:17:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertProduct]

	@zUMC			varchar(20),
	@iYear				int,
	@zSeason			varchar(1),
	@zProductName		varchar(55),
	@zProductSortName		varchar(55),
	@zLanguage			varchar(10),
	@iCategoryID			int,
	@iStatus			int,
	@iProductType			int,
	@iDaysLeadTime		int,
	@iNbrOfIssuesPerYear		int,
	@iPublisherID			int,
	@iFulfillmentHouseID		int,
	@zComment			varchar(200),
	@zVendorNumber		varchar(30),
	@zVendorSiteName		varchar(15),
	@zPayGroupLookupCode	varchar(25),
	@iCurrency			int,
	@zGSTRegistrationNumber	varchar(20),
	@zHSTRegistrationNumber	varchar(20),
	@zPSTRegistrationNumber	varchar(20),
	@zOracleCode			varchar(50),
	@zPrizeLevel			varchar(10),
	@iPrizeLevelQtyRequired	int,
	@zRemitCode			varchar(20),
	@bIsQSPExclusive		bit,
	@zEnglishDescription		varchar(50),
	@zFrenchDescription		varchar(50),
	@zUserID			varchar(50),
	@zVendorProductCode			varchar(12)

AS

	DECLARE	@iProductInstance	int,
			@iDescriptionCount	int

	IF(@iProductType IN (46001, 46006, 46007, 46012))
	BEGIN
		INSERT INTO	Product
				(Product_Code,
				Product_Season,
				Product_Year,
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
				PayGroupLookupCode,
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
		VALUES
				(@zUMC,
				@zSeason,
				@iYear,
				0,
				@zProductName,
				@zProductSortName,
				@iPublisherID,
				'',
				'',
				'1995-01-01',
				'',
				0,
				0,
				@iStatus,
				@zComment,
				getdate(),
				@iCategoryID,
				@iFulfillmentHouseID,
				'1/1/95',
				'1995-01-01',
				'',
				@zUserID,
				getdate(),
				@zLanguage,
				CASE @iProductType WHEN 46016 THEN 14 ELSE @iProductType - 46000 END,
				@iDaysLeadTime,
				@zVendorNumber,
				@zVendorSiteName,
				@zPayGroupLookupCode,
				'IMMEDIATE',
				@iCurrency,
				'CA',
				@iProductType,
				'',
				0,
				0.00,
				0.00,
				@zOracleCode,
				@zPrizeLevel,
				@iPrizeLevelQtyRequired,
				@iNbrOfIssuesPerYear,
				@zRemitCode,
				@bIsQSPExclusive)
	END
	ELSE
	BEGIN
		INSERT INTO	Product
				(Product_Code,
				Product_Season,
				Product_Year,
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
				PayGroupLookupCode,
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
		VALUES
				(@zUMC,
				@zSeason,
				@iYear,
				NULL,
				@zProductName,
				@zProductSortName,
				@iPublisherID,
				NULL,
				NULL,
				NULL,
				NULL,
				NULL,
				NULL,
				@iStatus,
				@zComment,
				getdate(),
				NULL,
				NULL,
				NULL,
				NULL,
				NULL,
				@zUserID,
				getdate(),
				@zLanguage,
				CASE @iProductType WHEN 46016 THEN 14 ELSE @iProductType - 46000 END,
				NULL,
				NULL,
				NULL,
				NULL,
				NULL,
				@iCurrency,
				'CA',
				@iProductType,
				NULL,
				NULL,
				NULL,
				NULL,
				@zOracleCode,
				@zPrizeLevel,
				@iPrizeLevelQtyRequired,
				NULL,
				NULL,
				NULL,
				@zVendorProductCode)
	END

	SET		@iProductInstance = SCOPE_IDENTITY()

	-- ENGLISH DESCRIPTION
	IF(@zEnglishDescription <> '')
	BEGIN
		SELECT	@iDescriptionCount = COUNT(*)
		FROM		ProductDescription pDescEN
		WHERE	pDescEN.Product_Code = @zOracleCode
		AND		pDescEN.Language_Code = 'EN'

		IF(@iDescriptionCount > 0)
		BEGIN
			SELECT	@iDescriptionCount = COUNT(*)
			FROM		ProductDescription pDescEN
			WHERE	pDescEN.Product_Code = @zOracleCode
			AND		pDescEN.Language_Code = 'EN'
			AND		pDescEN.Product_Description_Alt = @zEnglishDescription

			IF(@iDescriptionCount = 0)
			BEGIN
				UPDATE	ProductDescription
				SET		Product_Description_Alt = @zEnglishDescription,
						CatalogProductCode = @zUMC
				WHERE	Product_Code = @zOracleCode
				AND		Language_Code = 'EN'
			END
		END
		ELSE
		BEGIN
			INSERT INTO	ProductDescription
					(Product_Code,
					Language_Code,
					Product_Description_Alt,
					Country_Code,
					CatalogProductCode)
			VALUES	(@zOracleCode,
					'EN',
					@zEnglishDescription,
					'CA',
					@zUMC)
		END
	END

	-- FRENCH DESCRIPTION
	IF(@zFrenchDescription <> '')
	BEGIN
		SELECT	@iDescriptionCount = COUNT(*)
		FROM		ProductDescription pDescFR
		WHERE	pDescFR.Product_Code = @zOracleCode
		AND		pDescFR.Language_Code = 'FR'

		IF(@iDescriptionCount > 0)
		BEGIN
			SELECT	@iDescriptionCount = COUNT(*)
			FROM		ProductDescription pDescFR
			WHERE	pDescFR.Product_Code = @zOracleCode
			AND		pDescFR.Language_Code = 'FR'
			AND		pDescFR.Product_Description_Alt = @zFrenchDescription

			IF(@iDescriptionCount = 0)
			BEGIN
				UPDATE	ProductDescription
				SET		Product_Description_Alt = @zFrenchDescription,
						CatalogProductCode = @zUMC
				WHERE	Product_Code = @zOracleCode
				AND		Language_Code = 'FR'
			END
		END
		ELSE
		BEGIN
			INSERT INTO	ProductDescription
					(Product_Code,
					Language_Code,
					Product_Description_Alt,
					Country_Code,
					CatalogProductCode)
			VALUES	(@zOracleCode,
					'FR',
					@zFrenchDescription,
					'CA',
					@zUMC)
		END
	END

	-- TAXES
	IF(@iProductType = 46001)
	BEGIN
		EXEC pr_RegisterTaxMag @zRemitCode, @zGSTRegistrationNumber, @zHSTRegistrationNumber, @zPSTRegistrationNumber
	END

	SELECT	@iProductInstance
GO

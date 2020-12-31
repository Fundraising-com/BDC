USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateProduct]    Script Date: 06/07/2017 09:18:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_UpdateProduct]

	@iProductInstance		int,
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
	@zFrenchDescription			varchar(50),
	@zVendorProductCode			varchar(12)

AS

	DECLARE	@zCurrentProductCode		varchar(20),
			@zCurrentLanguageCode	varchar(10),
			@zCurrentOracleCode		varchar(50),
			@iDescriptionCount		int

	SELECT	@zCurrentProductCode = p.Product_Code,
			@zCurrentLanguageCode = p.Lang,
			@zCurrentOracleCode = p.OracleCode
	FROM		Product p
	WHERE	p.Product_Instance = @iProductInstance

	-- 06/09/2006 - Issue #530 - Ben : Update redundant fields in the contracts
	IF(@zUMC <> @zCurrentProductCode OR @zLanguage <> @zCurrentLanguageCode OR @zOracleCode <> @zCurrentOracleCode)
	BEGIN
		UPDATE	Pricing_Details
		SET		Product_Code = @zUMC,
				Language_Code = @zLanguage,
				OracleCode = @zOracleCode
		WHERE	Product_Instance = @iProductInstance
	END


	IF(@iProductType IN (46001, 46006, 46007, 46012))
	BEGIN
		UPDATE	Product
		SET		Product_Code = @zUMC,
				Product_Name = @zProductName,
				Product_Sort_Name = @zProductSortName,
				Lang = @zLanguage,
				Category_Code = @iCategoryID,
				Status = @iStatus,
				Type = @iProductType,
				DaysLeadTime = @iDaysLeadTime,
				Nbr_Of_Issues_Per_Year = @iNbrOfIssuesPerYear,
				Pub_Nbr = @iPublisherID,
				Fulfill_House_Nbr = @iFulfillmentHouseID,
				Comment = @zComment,
				CommentDate = GetDate(),
				VendorNumber = @zVendorNumber,
				VendorSiteName = @zVendorSiteName,
				PayGroupLookUpCode = @zPayGroupLookUpCode,
				Currency = @iCurrency,
				OracleCode = @zOracleCode,
				Prize_Level = @zPrizeLevel,
				Prize_Level_Qty_Required = @iPrizeLevelQtyRequired,
				RemitCode = @zRemitCode,
				IsQSPExclusive = @bIsQSPExclusive
	
		WHERE	Product_Instance = @iProductInstance
	END
	ELSE
	BEGIN
		UPDATE	Product
		SET		Product_Code = @zUMC,
				Product_Name = @zProductName,
				Product_Sort_Name = @zProductSortName,
				Lang = @zLanguage,
				Status = @iStatus,
				Type = @iProductType,
				Comment = @zComment,
				CommentDate = GetDate(),
				Currency = @iCurrency,
				OracleCode = @zOracleCode,
				Prize_Level = @zPrizeLevel,
				Prize_Level_Qty_Required = @iPrizeLevelQtyRequired,
				VendorProductCode = @zVendorProductCode
	
		WHERE	Product_Instance = @iProductInstance
	END


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
	ELSE
	BEGIN
		DELETE FROM	ProductDescription
		WHERE	Product_Code = @zOracleCode
		AND		Language_Code = 'EN'
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
	ELSE
	BEGIN
		DELETE FROM	ProductDescription
		WHERE	Product_Code = @zOracleCode
		AND		Language_Code = 'FR'
	END

	-- TAXES
	IF(@iProductType = 46001)
	BEGIN
		EXEC pr_RegisterTaxMag @zRemitCode, @zGSTRegistrationNumber, @zHSTRegistrationNumber, @zPSTRegistrationNumber
	END

/*
	-- CONTRACT STATUS UPDATE
	IF (@iStatus <> 30600)
	BEGIN
		IF(@iProductType = 46001)
		BEGIN
			UPDATE	p
			SET		p.Status = @iStatus
			FROM		Product p
			WHERE	p.RemitCode = @zRemitCode
			AND		p.Product_Sort_Name = @zProductSortName
	
			UPDATE	pd
			SET		pd.Status = @iStatus
			FROM		Pricing_Details pd,
					Product p
			WHERE	p.Product_Instance = pd.Product_Instance
			AND		p.RemitCode = @zRemitCode
			AND		p.Product_Sort_Name = @zProductSortName
		END
		ELSE
		BEGIN
			UPDATE	p
			SET		p.Status = @iStatus
			FROM		Product p
			WHERE	p.Product_Code = @zUMC
			AND		p.Product_Sort_Name = @zProductSortName
	
			UPDATE	pd
			SET		pd.Status = @iStatus
			FROM		Pricing_Details pd,
					Product p
			WHERE	p.Product_Instance = pd.Product_Instance
			AND		p.Product_Code = @zUMC
			AND		p.Product_Sort_Name = @zProductSortName
		END
	END
*/
GO

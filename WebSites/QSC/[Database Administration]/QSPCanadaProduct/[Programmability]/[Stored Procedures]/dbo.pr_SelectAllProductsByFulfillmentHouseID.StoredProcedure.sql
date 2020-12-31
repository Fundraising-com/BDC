USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllProductsByFulfillmentHouseID]    Script Date: 06/07/2017 09:18:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllProductsByFulfillmentHouseID]

	@iFulfillmentHouseID	int

AS
	
	/*SELECT	p.[Product_Instance],
			p.[Product_Code],
			p.[Product_Year],
			p.[Product_Season],
			p.[Alpha_Code],
			p.[Product_Name],
			p.[Product_Sort_Name],
			COALESCE(p.[Pub_Nbr], 0) AS Pub_Nbr,
			p.[Ages],
			p.[Internet],
			p.[Issue_Rcvd_Dt],
			p.[CoverReceived],
			p.[HighlightCover],
			p.[Featuring],
			COALESCE(p.[Status], 0) AS Status,
			p.[Comment],
			p.[CommentDate],
			COALESCE(p.[Category_Code], 0) AS Category_Code,
			COALESCE(p.[Fulfill_House_Nbr], 0) AS Fulfill_House_Nbr,
			p.[Mail_Dt],
			p.[Auth_Form_Rtrn_Dt],
			p.[IssueDateUsed],
			p.[Logged_By],
			p.[Log_Dt],
			p.[Lang],
			p.[ProductLine],
			COALESCE(p.[DaysLeadTime], 0) AS DaysLeadTime,
			p.[VendorNumber],
			p.[VendorSiteName],
			p.[PayGroupLookUpCode],
			p.[TermsName],
			COALESCE(p.[Currency], 0) AS Currency,
			p.[CountryCode],
			p.[Type],
			p.[UnitOfMeasure],
			p.[UOMConvFactor],
			p.[UnitWeight],
			p.[UnitCost],
			p.[OracleCode],
			COALESCE(p.[Prize_Level], '') AS Prize_Level,
			COALESCE(p.[Prize_Level_Qty_Required], 0) AS Prize_Level_Qty_Required,
			COALESCE(p.[Nbr_Of_Issues_Per_Year], 0) AS Nbr_Of_Issues_Per_Year,
			COALESCE(p.RemitCode, '') AS RemitCode,
			COALESCE(p.IsQSPExclusive, 0) AS IsQSPExclusive,
			COALESCE(TMGGST.TAX_REGISTRATION_NUMBER, '') [GST_Registration_Nbr],
			COALESCE(TMGHST.TAX_REGISTRATION_NUMBER, '') [HST_Registration_Nbr],
			COALESCE(TMGPST.TAX_REGISTRATION_NUMBER, '') [PST_Registration_Nbr],
			COALESCE(pDescEN.Product_Description_Alt, '') AS EnglishDescription,
			COALESCE(pDescFR.Product_Description_Alt, '') AS FrenchDescription
	FROM		QspCanadaProduct..[Product] p
	LEFT JOIN	QSPCanadaCommon..TaxMagRegistration TMGGST
				ON	TMGGST.TITLE_CODE = p.Product_Code
				AND	TMGGST.TAX_ID = 1
	LEFT JOIN	QSPCanadaCommon..TaxMagRegistration TMGHST
				ON	TMGHST.TITLE_CODE = p.Product_Code
				AND	TMGHST.TAX_ID = 2
	LEFT JOIN	QSPCanadaCommon..TaxMagRegistration TMGPST
				ON	TMGPST.TITLE_CODE = p.Product_Code
				AND	TMGPST.TAX_ID = 3
	LEFT JOIN	QSPCanadaProduct..ProductDescription pDescEN
				ON	pDescEN.Product_Code = p.OracleCode
				AND	pDescEN.Language_Code = 'EN'
	LEFT JOIN	QSPCanadaProduct..ProductDescription pDescFR
				ON	pDescFR.Product_Code = p.OracleCode
				AND	pDescFR.Language_Code = 'FR',
			QSPCanadaCommon..Season s
	WHERE	p.[Fulfill_House_Nbr] = @iFulfillmentHouseID
	AND		s.FiscalYear = p.Product_Year
	AND		s.Season = p.Product_Season
	AND		GetDate()  <= s.EndDate*/
GO

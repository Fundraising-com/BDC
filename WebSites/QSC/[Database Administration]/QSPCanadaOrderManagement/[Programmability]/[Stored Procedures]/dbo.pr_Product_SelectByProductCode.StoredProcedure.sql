USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Product_SelectByProductCode]    Script Date: 06/07/2017 09:20:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Product_SelectByProductCode]
	@sProduct_Code nvarchar(50),
	@iProduct_Year int = 0,
	@sProduct_Season varchar(1) = ''
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.

DECLARE @sqlStatement nvarchar(4000)

set @sqlStatement =   'SELECT
	[Product_Instance],
	[Product_Code],
	[Product_Year],
	[Product_Season],
	[Alpha_Code],
	[Product_Name],
	[Product_Sort_Name],
	[Pub_Nbr],
	[Ages],
	[Internet],
	[Issue_Rcvd_Dt],
	[CoverReceived],
	[HighlightCover],
	[Featuring],
	[Status],
	[Comment],
	[CommentDate],
	[Category_Code],
	[Fulfill_House_Nbr],
	[Mail_Dt],
	[Auth_Form_Rtrn_Dt],
	[IssueDateUsed],
	[Logged_By],
	[Log_Dt],
	[Lang],
	[ProductLine],
	[DaysLeadTime],
	[VendorNumber],
	[VendorSiteName],
	[PayGroupLookUpCode],
	[TermsName],
	[Currency],
	[CountryCode],
	[Type],
	[UnitOfMeasure],
	[UOMConvFactor],
	[UnitWeight],
	[UnitCost],
	[OracleCode],
	[Prize_Level],
	[Prize_Level_Qty_Required],
	[Nbr_Of_Issues_Per_Year],
	COALESCE(TMGGST.TAX_REGISTRATION_NUMBER, '''') [GST_Registration_Nbr],
	COALESCE(TMGHST.TAX_REGISTRATION_NUMBER, '''') [HST_Registration_Nbr],
	COALESCE(TMGPST.TAX_REGISTRATION_NUMBER, '''') [PST_Registration_Nbr]
FROM		QspCanadaProduct..[Product]
LEFT JOIN	QSPCanadaCommon..TaxMagRegistration TMGGST
			ON	TMGGST.TITLE_CODE = Product_Code
			AND	TMGGST.TAX_ID = 1
LEFT JOIN	QSPCanadaCommon..TaxMagRegistration TMGHST
			ON	TMGHST.TITLE_CODE = Product_Code
			AND	TMGHST.TAX_ID = 2
LEFT JOIN	QSPCanadaCommon..TaxMagRegistration TMGPST
			ON	TMGPST.TITLE_CODE = Product_Code
			AND	TMGPST.TAX_ID = 3
WHERE
	[Product_Code] = ''' + @sProduct_Code + ''' '

if(@iProduct_Year <> 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' AND [Product_Year] = ' + convert(varchar, @iProduct_Year)
END

if(@sProduct_Season <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' AND [Product_Season] = ''' + @sProduct_Season + ''' '
END


exec (@sqlStatement)
GO

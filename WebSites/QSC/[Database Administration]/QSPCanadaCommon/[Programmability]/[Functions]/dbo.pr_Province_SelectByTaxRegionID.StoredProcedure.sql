USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Province_SelectByTaxRegionID]    Script Date: 06/07/2017 09:33:28 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Province_SelectByTaxRegionID]

	@iTaxRegionID	int

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT	p.[COUNTRY_CODE],
		p.[PROVINCE_CODE],
		p.[PROVINCE_NAME],
		p.[LAPSE_DAYS_DELIVERY],
		p.[TAX_BACKOUT_FUNCTION],
		p.[LAPSE_DAYS_FIELD_SUPPLY_PREP]
FROM		[dbo].[Province] p,
		[dbo].[TaxRegionProvince] trp
WHERE	trp.Province = p.Province_Code
AND		trp.TaxID =
		(SELECT	MAX(trp2.TaxID)
		FROM		TaxRegionProvince trp2
		WHERE	trp2.Province = p.Province_Code)
AND		trp.TaxRegionID = @iTaxRegionID
ORDER BY	p.[COUNTRY_CODE],
		p.[PROVINCE_CODE]
GO

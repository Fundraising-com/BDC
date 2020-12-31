USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Province_SelectByCountryCode]    Script Date: 06/07/2017 09:33:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Province_SelectByCountryCode]
	@sCOUNTRY_CODE varchar(10)
AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[COUNTRY_CODE],
	[PROVINCE_CODE],
	[PROVINCE_NAME],
	[LAPSE_DAYS_DELIVERY],
	[TAX_BACKOUT_FUNCTION],
	[LAPSE_DAYS_FIELD_SUPPLY_PREP]
FROM [dbo].[Province]

WHERE
	[COUNTRY_CODE] = @sCOUNTRY_CODE
	
ORDER BY 
	[COUNTRY_CODE] ASC
	, [PROVINCE_CODE] ASC
GO

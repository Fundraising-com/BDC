USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Province_SelectOne]    Script Date: 06/07/2017 09:33:28 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'Province'
-- based on the Primary Key.
-- Gets: @sCOUNTRY_CODE varchar(10)
-- Gets: @sPROVINCE_CODE varchar(10)
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Province_SelectOne]
	@sPROVINCE_CODE varchar(10)
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[COUNTRY_CODE],
	[PROVINCE_CODE],
	[PROVINCE_NAME],
	[LAPSE_DAYS_DELIVERY],
	[TAX_BACKOUT_FUNCTION],
	[LAPSE_DAYS_FIELD_SUPPLY_PREP]
FROM [dbo].[Province]
WHERE
	[PROVINCE_CODE] = @sPROVINCE_CODE
GO

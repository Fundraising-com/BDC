USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Province_SelectAll]    Script Date: 06/07/2017 09:33:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'Province'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Province_SelectAll]

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
ORDER BY 
	--[COUNTRY_CODE] ASC
	--, [PROVINCE_CODE] ASC
	[PROVINCE_NAME]
GO

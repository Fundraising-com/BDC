USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_culture]    Script Date: 02/14/2014 13:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

*/
CREATE PROCEDURE [dbo].[es_get_culture]
	@culture_code nvarchar(5)
AS
BEGIN
SELECT culture_code
	, country_code
	, language_code
	, culture_name
FROM culture
WHERE culture_code = @culture_code
END
GO

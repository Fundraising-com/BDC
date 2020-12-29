USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_subdivision]    Script Date: 02/14/2014 13:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	
*/
CREATE PROCEDURE [dbo].[es_get_subdivision]
	@country_code nvarchar(2)
	, @language_code nvarchar(2)
AS
BEGIN
	SELECT subdivision_code
		, CASE WHEN @language_code = 'fr' THEN subdivision_name_2
			ELSE subdivision_name_1
		  END as subdivision_name
	FROM subdivision
	WHERE country_code = @country_code
      AND (subdivision_category = 'state' OR subdivision_category = 'district' OR subdivision_category = 'us army')
END
GO

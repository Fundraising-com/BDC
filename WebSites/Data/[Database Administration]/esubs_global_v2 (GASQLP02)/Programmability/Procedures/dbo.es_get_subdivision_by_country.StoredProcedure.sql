USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_subdivision_by_country]    Script Date: 02/14/2014 13:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_subdivision_by_country]
                @country_code nvarchar(2)
AS
BEGIN
	SELECT subdivision_code, subdivision_category
         , subdivision_name_1 as subdivision_name_en
         , CASE WHEN subdivision_name_2 IS NULL THEN subdivision_name_1 ELSE subdivision_name_2 END as subdivision_name_fr
    FROM subdivision
    WHERE country_code = @country_code
	  AND 
	(
		(@country_code = 'us' AND (subdivision_category = 'state' or subdivision_category = 'district' or subdivision_category = 'us army'))
		OR  
		(@country_code = 'ca' AND subdivision_category = 'province')
		OR @country_code <> 'us' OR @country_code = 'ca'
	)

END
GO

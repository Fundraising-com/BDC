USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_attribute]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	
*/
CREATE PROC [dbo].[es_get_partner_attribute]
	@partner_id int
	, @partner_attribute_name varchar(50)
	, @culture_code nvarchar(5)
	, @partner_attribute_value varchar(255) OUTPUT
AS
BEGIN
	SELECT @partner_attribute_value = pav.value
	FROM partner_attribute_value pav
		INNER JOIN partner_attribute pa
			ON pav.partner_attribute_id = pa.partner_attribute_id
	WHERE partner_attribute_name = @partner_attribute_name
	  AND partner_id = @partner_id
	  AND culture_code = @culture_code
END
GO

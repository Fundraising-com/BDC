USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_set_partner_attribute]    Script Date: 02/14/2014 13:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

*/
CREATE PROC [dbo].[es_set_partner_attribute]
	@partner_id int
	,@partner_attribute_name varchar(50)
	,@partner_attribute_value varchar(255)
AS
BEGIN
	UPDATE partner_attribute_value
	SET value = @partner_attribute_value
	FROM partner_attribute pa
	WHERE partner_id = @partner_id
	  AND partner_attribute_value.partner_attribute_id = pa.partner_attribute_id
	  AND pa.partner_attribute_name = @partner_attribute_name
END
GO

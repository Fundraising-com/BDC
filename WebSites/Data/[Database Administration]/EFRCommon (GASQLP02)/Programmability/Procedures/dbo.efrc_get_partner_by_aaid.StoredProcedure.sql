USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_partner_by_aaid]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC [dbo].[efrc_get_partner_by_aaid] 'envision'

CREATE  PROCEDURE [dbo].[efrc_get_partner_by_aaid]
 @a_aid varchar(255)
AS
BEGIN
SELECT     

 p.partner_id
 , p.partner_type_id
 , p.partner_name
 , p.has_collection_site
 , p.guid
 , pt.partner_type_name 

FROM partner p WITH (NOLOCK)
	inner join partner_type pt WITH (NOLOCK) on pt.partner_type_id = p.partner_type_id
	inner join partner_attribute_value pav WITH (NOLOCK) on p.partner_id = pav.partner_id 

where 
 pav.partner_attribute_id= 12 and pav.value = @a_aid


END
GO

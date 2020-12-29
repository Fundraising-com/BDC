USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_default_promotion_by_partnerName]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[efrc_get_default_promotion_by_partnerName]
 @a_aid varchar(255)
AS
BEGIN
SELECT     
	pr.promotion_id
	, pr.promotion_type_code
	, pr.promotion_destination_id
	, pr.promotion_name
	, pr.script_name
	, pr.active
	, pr.create_date
	, pr.cookie_content
	, pr.keyword
	, pr.is_displayable
 

FROM promotion pr
	inner join partner_promotion pp on pr.promotion_id = pp.promotion_id
	inner join partner p on p.partner_id = pp.partner_id 
	inner join partner_attribute_value pav on p.partner_id = pav.partner_id 

where 
pav.value = @a_aid and
pav.partner_attribute_id  = 3 and
(pr.script_name = @a_aid OR pr.promotion_name = @a_aid)


END
GO

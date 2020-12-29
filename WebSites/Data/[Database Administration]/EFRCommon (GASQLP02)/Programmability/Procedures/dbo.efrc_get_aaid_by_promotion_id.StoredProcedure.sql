USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_aaid_by_promotion_id]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
select top 20 * from promotion order by 1 desc
select top 20 * from partner order by 1 desc
exec [efrc_get_aaid_by_promotion_id]  20171
*/
CREATE  PROCEDURE [dbo].[efrc_get_aaid_by_promotion_id]
 @promotion_id int
AS
BEGIN
SELECT     
	pav.value as a_aid
 

FROM promotion pr WITH (NOLOCK)
	inner join partner_promotion pp WITH (NOLOCK) on pr.promotion_id = pp.promotion_id
	inner join partner p WITH (NOLOCK) on p.partner_id = pp.partner_id 
	inner join partner_attribute_value pav WITH (NOLOCK) on p.partner_id = pav.partner_id 

where 
 pav.partner_attribute_id= 12
 and pr.promotion_id = @promotion_id


END
GO

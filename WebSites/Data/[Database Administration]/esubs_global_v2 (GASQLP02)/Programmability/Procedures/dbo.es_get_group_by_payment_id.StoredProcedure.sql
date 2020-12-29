USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_by_payment_id]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_group_by_payment_id]
	@payment_id int
AS
BEGIN
SELECT     
	g.group_id
	, g.parent_group_id
	, g.sponsor_id
	, g.partner_id
	, g.lead_id
	, g.external_group_id
	, g.group_name
	, g.test_group
	, g.expected_membership
	, g.redirect
	, g.group_url
	, g.comments
	, g.create_date
FROM    Payment p INNER JOIN Payment_info pif ON p.payment_info_id =  pif.payment_info_id     
		     INNER JOIN [Group] g  ON pif.group_id = g.group_id
WHERE     
	p.payment_id =@payment_id
END
GO

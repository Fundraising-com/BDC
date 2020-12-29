USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_by_payment_id]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_partner_by_payment_id]
@Payment_id INT
AS
BEGIN
SELECT     
	p.partner_id
	, p.partner_type_id
	, p.partner_name
	, p.has_collection_site
	, p.guid
	, pt.create_date
FROM Payment pt INNER JOIN  Payment_info pti ON pt.payment_info_id = pti.payment_info_id
INNER JOIN [Group] g ON pti.group_id = g.group_id
INNER JOIN Partner p ON g.partner_id = p.partner_id 
WHERE Payment_id = @Payment_id
END
GO

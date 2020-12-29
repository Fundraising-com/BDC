USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_379]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_379]
	@identification int
	,@source_id bigint
AS
BEGIN
	SELECT @identification  as identification
		, @source_id as source_id
		, m.first_name + ' ' + m.last_name as [sponsor_name]
		, 379 as email_template_id
		, m.email_address as email
		, m.[password] as pass
		, e.redirect 
		, pav.value as partner_name
	FROM member m with(nolock)
		INNER JOIN member_hierarchy mh with(nolock)
		ON mh.member_id = m.member_id
		INNER JOIN event_participation ep with(nolock)
		ON ep.member_hierarchy_id = mh.member_hierarchy_id
		INNER JOIN event e with(nolock)
		ON ep.event_id = e.event_id
		INNER JOIN event_group eg with(nolock)
		ON eg.event_id = e.event_id
		INNER JOIN [group] g with(nolock)
		ON g.group_id = eg.group_id	
		INNER JOIN partner_attribute_value pav with(nolock)
		ON pav.partner_id = g.partner_id
		AND pav.culture_code COLLATE Latin1_General_CI_AS = e.culture_code
		AND partner_attribute_id = 3
		INNER JOIN efundweb.dbo.partner p with(nolock)
		ON p.partner_id = g.partner_id
	WHERE ep.event_participation_id = @identification
END
GO

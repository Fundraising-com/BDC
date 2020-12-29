USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_direct_mail_recipients]    Script Date: 02/14/2014 13:05:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_direct_mail_recipients] @direct_mail_info_id int
AS
BEGIN

SELECT     
	mh.member_hierarchy_id
	, mh.parent_member_hierarchy_id
	, mh.member_id
	, mh.creation_channel_id
	, m.culture_code
	, m.opt_status_id
	, m.first_name
	, m.middle_name
	, m.last_name
	, m.gender
	, m.email_address
	, m.password
	, m.bounced
	, m.parent_first_name
	, m.parent_last_name
	, m.external_member_id
   	, m.partner_id
	, m.comments
	, m.lead_id
	, m.facebook_id
	, m.greeting
	, cc.creation_channel_name
	, cc.description
	, cc.active
	, mh.unsubscribe
	, 2 as user_type
	, ep.salutation
    , m.[user_id]
FROM         
	member_hierarchy mh
	INNER JOIN member m
	ON mh.member_id = m.member_id 
	LEFT OUTER JOIN creation_channel cc
	ON mh.creation_channel_id = cc.creation_channel_id
	INNER JOIN event_participation ep
	ON ep.member_hierarchy_id = mh.member_hierarchy_id
	INNER JOIN direct_mail dm 
	ON dm.event_participation_id = ep.event_participation_id and dm.direct_mail_info_id = @direct_mail_info_id
/* REMOVE BY MELISSA COTE CHANGED FOR INNER JOIN
WHERE ep.event_participation_id in 
(
	SELECT dm.event_participation_id 
	FROM direct_mail dm
	WHERE dm.direct_mail_info_id = @direct_mail_info_id
)*/
/*
	INNER JOIN direct_mail dm
	ON dm.direct_mail_info_id = dm
	INNER JOIN direct_mail_info dmi
	ON ep.event_participation_id = dmi.event_participation_id
WHERE 
	dmi.direct_mail_info_id = @direct_mail_info_id;
*/
/*
SELECT     
	mh.member_hierarchy_id
	, mh.parent_member_hierarchy_id
	, mh.member_id
	, mh.creation_channel_id
	, m.culture_code
	, m.opt_status_id
	, m.first_name
	, m.middle_name
	, m.last_name
	, m.gender
	, m.email_address
	, m.password
	, m.bounced
	, m.parent_first_name
	, m.parent_last_name
	, m.external_member_id
   	, m.partner_id
	, m.comments
	, m.lead_id
	, m.facebook_id
	, cc.creation_channel_name
	, cc.description
	, cc.active
	, mh.unsubscribe
	, 2 as user_type
	, ep.salutation
    , m.[user_id]
FROM         
	member_hierarchy mh
	INNER JOIN member m
	ON mh.member_id = m.member_id 
	LEFT OUTER JOIN creation_channel cc
	ON mh.creation_channel_id = cc.creation_channel_id
	INNER JOIN event_participation ep
	ON ep.member_hierarchy_id = mh.member_hierarchy_id
where ep.event_participation_id in (

	select event_participation_id
	from direct_mail
	where direct_mail_info_id in (

		select direct_mail_info_id
		from direct_mail_info
		where event_participation_id = @sender_event_participation_id

	)

)
*/
 
END
GO

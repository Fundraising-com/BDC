USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_sponsor_info]    Script Date: 02/26/2015 14:23:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- exec dbo.cc_get_sponsor_info 1060828

ALTER PROCEDURE [dbo].[cc_get_sponsor_info]
	@event_id int
AS
BEGIN

IF NOT EXISTS(
			SELECT *
			FROM event_participation ep JOIN member_hierarchy mh 
			  ON ep.member_hierarchy_id = mh.member_hierarchy_id JOIN member m
			  ON mh.member_id = m.member_id JOIN users u
			  ON m.user_id = u.user_id
			WHERE ep.event_id = @event_id AND ep.participation_channel_id = 3
		) 
BEGIN
	DECLARE @member_id int, @user_id int, @email_address VARCHAR(100), @password VARCHAR(100), @partner_id int;
	SELECT @member_id = m.member_id, @email_address = m.email_address, @password = m.password, @partner_id = m.partner_id
	FROM event_participation ep JOIN member_hierarchy mh 
	  ON ep.member_hierarchy_id = mh.member_hierarchy_id JOIN member m
	  ON mh.member_id = m.member_id 
	WHERE ep.event_id = @event_id AND ep.participation_channel_id = 3
	
	-- denormalyse member
	exec [es_denormalise_login] @member_id

	-- need to update all match members under the same login
	select @user_id=u.[user_id]
	from  users u where member_id = @member_id 
	
	update member set [user_id] = @user_id 
	from member m
	inner join member_hierarchy mh on m.member_id = mh.member_id
	where
		m.email_address = @email_address
	and 	m.[password] = @password
	and 	m.partner_id = @partner_id
	and 	mh.active =1
END

SELECT
     p.partner_name
	,g.group_name
    ,e.active
    ,e.create_date
    ,e.end_date
    ,e.event_name
	,e.event_id
	,pn.phone_number
	,u.email_address
	,u.[password]
	,u.first_name + ' ' + u.last_name as [name]
	,u.first_name
	,u.last_name
	,ep.event_participation_id
	,(CASE WHEN u.unsubscribe is null
	  THEN CASE WHEN m.unsubscribe = 0 THEN 0 ELSE 1 END
	       ELSE CASE WHEN u.unsubscribe = 0 THEN 0 ELSE 1 END
	  END) as unsubscribed
	--, (CASE WHEN u.opt_status_id IS NULL 
	--        THEN CASE WHEN m.opt_status_id = 1 THEN 0 ELSE 1 END
	--        ELSE CASE WHEN u.opt_status_id = 1 THEN 0 ELSE 1 END
	--    END) as unsubscribed
    ,g.group_id
 	,MAX(CASE WHEN epr.prize_item_id > 0 THEN 'Yes' ELSE 'No' END ) as movie_ticket
 	, '' as account_number
FROM
	partner p with (nolock)
	join [group] g with (nolock)
	on p.partner_id = g.partner_id
	join event_group ge with (nolock)
	on g.group_id = ge.group_id
	join event e with (nolock)
	on e.event_id = ge.event_id
	join event_participation ep with (nolock)
	on ep.event_id = e.event_id and ep.participation_channel_id = 3
	join member_hierarchy mh with (nolock)
	on mh.member_hierarchy_id = ep.member_hierarchy_id and mh.active = 1
	join member m with (nolock)
	on m.member_id = mh.member_id --and m.deleted = 0
	join [users] u  with (nolock)
	on m.user_id = u.user_id
    left join payment_info pi with (nolock)
    on e.event_id = pi.event_id and pi.active = 1
	left join phone_number pn with (nolock)
	on pn.phone_number_id = pi.phone_number_id and pi.active = 1
    left join earned_prize epr with (nolock)
    on ep.event_participation_id = epr.event_participation_id	
WHERE e.event_id = @event_id
GROUP BY
         p.partner_name
		,g.group_name
        ,e.event_name
        ,e.create_date
        ,e.end_date
        ,e.active
		,e.event_id
		,pn.phone_number
		,u.email_address
		,u.[password]
		,u.first_name
	    ,u.last_name
		,ep.event_participation_id
		,(CASE WHEN u.unsubscribe is null
		  THEN CASE WHEN m.unsubscribe = 0 THEN 0 ELSE 1 END
			   ELSE CASE WHEN u.unsubscribe = 0 THEN 0 ELSE 1 END
		  END)
        ,g.group_id 	
end










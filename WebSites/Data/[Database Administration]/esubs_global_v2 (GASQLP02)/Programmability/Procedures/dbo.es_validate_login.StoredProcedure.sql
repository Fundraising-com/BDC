USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_validate_login]    Script Date: 02/14/2014 13:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
  Author:      Melissa Cote
  Create Date: 2011-03-10 11:00
  Description: 
  Results:   
  Examples:
		exec [es_validate_login] 'mcote@efund2.com','qazxsw',0
		exec [es_validate_login] 'mcote@rd.com','qwerty', 0 
  
  Change History:
  Author             Date				Description
  ------             ----				-----------
  Melissa Cote		 2011-03-10 11:00	CREATED
*/

CREATE PROCEDURE [dbo].[es_validate_login]
	@email_address varchar(100)
	,@password varchar(100)
	,@partner_id int = 0
AS

	declare @member_id int 
	declare @user_id int

	-- validate if user exist
	if exists (	
		select u.[user_id]
			,m.member_id
			,mh.member_hierarchy_id
			,ep.event_participation_id
			,e.event_id
			,e.active
			,m.partner_id
			,dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
		from users u with(nolock)
			inner join member m with(nolock) on u.[user_id] = m.[user_id]
			inner join member_hierarchy mh with(nolock) on m.member_id = mh.member_id
			left outer join event_participation ep with(nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
			left outer join [event] e with(nolock) on e.event_id = ep.event_id and e.active =1
		where u.email_address = @email_address
		and u.[password] = @password
		and mh.active =1
		group by 
			u.[user_id]
			, m.member_id
			,mh.member_hierarchy_id
			,ep.event_participation_id
			,e.event_id
			,e.active
			,m.partner_id
	)
	begin 
		-- select user information
		select u.[user_id]
			,m.member_id
			,mh.member_hierarchy_id
			,ep.event_participation_id
			,e.event_id
			, e.active
			,m.partner_id
			,dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
		from users u with(nolock)
			inner join member m with(nolock) on u.[user_id] = m.[user_id]
			inner join member_hierarchy mh with(nolock) on m.member_id = mh.member_id
			left outer join event_participation ep with(nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
			left outer join [event] e with(nolock) on e.event_id = ep.event_id and e.active =1
		where  u.email_address = @email_address
			and	u.[password] = @password
			and	mh.active =1
		group by 
			u.[user_id]
			, m.member_id
			,mh.member_hierarchy_id
			,ep.event_participation_id
			,e.event_id
			,e.active
			,m.partner_id
		order by e.event_id desc
	end
	else 
	begin 
		-- user doesn't exist validate if member exist
		if exists (	
		select m.member_id
			,mh.member_hierarchy_id
			,ep.event_participation_id
			,e.event_id
			, e.active
			,m.partner_id
			,dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
		from member m with(nolock)
			inner join member_hierarchy mh with(nolock) on m.member_id = mh.member_id
			left outer join event_participation ep with(nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
			left outer join [event] e with(nolock) on e.event_id = ep.event_id and e.active =1
		where
			m.email_address = @email_address
		and 	m.[password] = @password
		and 	mh.active =1
		group by 
			m.member_id
			,mh.member_hierarchy_id
			,ep.event_participation_id
			,e.event_id
			,e.active
			,m.partner_id
		)
		begin  
			--get member info
			select @member_id = m.member_id
			from member m with(nolock)
				inner join member_hierarchy mh with(nolock) on m.member_id = mh.member_id
				left outer join event_participation ep with(nolock)  on ep.member_hierarchy_id = mh.member_hierarchy_id
				left outer join [event] e with(nolock)  on e.event_id = ep.event_id and e.active =1
			where m.email_address = @email_address
				and m.[password] = @password
				and mh.active =1
			
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
			--and 	m.partner_id = @partner_id
			and 	mh.active =1

			-- get user info
			select u.[user_id]
				,m.member_id
				,mh.member_hierarchy_id
				,ep.event_participation_id
				,e.event_id
				, e.active
				,m.partner_id
				,dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
			from
				users u  with(nolock) 
				inner join member m  with(nolock) on u.[user_id] = m.[user_id]
				inner join member_hierarchy mh  with(nolock) on m.member_id = mh.member_id
				left outer join event_participation ep  with(nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
				left outer join [event] e  with(nolock) on e.event_id = ep.event_id and e.active =1
			where
				u.[user_id] = @user_id
			group by 
				u.[user_id]
				,m.member_id
				,mh.member_hierarchy_id
				,ep.event_participation_id
				,e.event_id
				,e.active
				,m.partner_id
			order by e.event_id desc	
		end
	end
GO

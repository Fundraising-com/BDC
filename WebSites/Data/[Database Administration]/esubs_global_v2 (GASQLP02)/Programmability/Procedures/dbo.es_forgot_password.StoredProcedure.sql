USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_forgot_password]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_forgot_password]
	@email_address varchar(100)
as

declare @user_type tinyint

if exists (
	select 
		member_hierarchy_id
	from
		member m
		inner join member_hierarchy mh
		on m.member_id = mh.member_id
	where
		m.email_address = @email_address
	and 	len([password]) > 0
)
begin 
	select 
		member_hierarchy_id
	from
		member m
		inner join member_hierarchy mh
		on m.member_id = mh.member_hierarchy_id
	where
		m.email_address = @email_address
	and 	len([password]) > 0
end
else
begin

	update member
	set [password] = LOWER( LEFT( NEWID(), 8 ) )
	from
	member m
	inner join member_hierarchy mh
	on m.member_id = mh.member_id
	where member_hierarchy_id in(
		select 
			member_hierarchy_id 
		from(
			select 
				member_hierarchy_id
				,dbo.es_get_user_type (member_hierarchy_id) as user_type
			from
				member m
				inner join member_hierarchy mh
				on m.member_id = mh.member_id
			where
				m.email_address = @email_address
		) a
		where user_type < 3 --pas les supporters
	)									


	select 
		member_hierarchy_id
	from
		member m
		inner join member_hierarchy mh
		on m.member_id = mh.member_id
	where
		m.email_address = @email_address
	and 	len([password]) > 0
	
	


end
GO

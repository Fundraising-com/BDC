USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_denormalise_login]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
  Author:      Melissa Cote
  Create Date: 2011-03-10 11:00
  Description: replace member dataconversion from a deployment perspective
			and do it on the user visit.
			exec [es_denormalise_login] 265838
			select * from users
  Results:    
  
  Change History:
  Author             Date				Description
  ------             ----				-----------
  Melissa Cote		 2011-03-10 11:00	CREATED
*/
CREATE PROCEDURE [dbo].[es_denormalise_login]
	@member_id int
AS

--declare @member_id int
--Set @member_id = 1167511
--select * from member where first_name like 'Mel%' and last_name like 'Cote'
	
declare @culture_code nvarchar(5)
declare @opt_status_id int
declare @first_name varchar(100)
declare @middle_name varchar(100) 
declare @last_name varchar(100)
declare @gender char(1)
declare @email_address varchar(100)
declare @password varchar(100) 
declare @bounced bit
declare @external_member_id varchar(20) 
declare @comments varchar(1024) 
declare @create_date datetime 
declare @parent_first_name varchar(100) 
declare @parent_last_name varchar(100) 
declare @partner_id int 
declare @lead_id int 
declare @unsubscribe bit
declare @unsubscribe_date datetime 
declare @facebook_id int
declare @user_id int 
declare @deleted bit

--declare @user_id int

declare @coppa_month int
declare @coppa_year int
declare @agree_term_services bit

declare @member_hierarchy_id int 
declare @parent_member_hierarchy_id int
declare @creation_channel_id int 
declare @mh_create_date datetime
declare @active bit

declare @new_member_id int

-- get member information 
select 
	@culture_code=m.culture_code
	, @opt_status_id=m.opt_status_id
	, @first_name=m.first_name
	, @middle_name=m.middle_name
	, @last_name=m.last_name
	, @gender=m.gender
	, @email_address=m.email_address
	, @password=m.password
	, @bounced=m.bounced
	, @external_member_id=m.external_member_id
	, @comments=m.comments
	, @create_date=m.create_date
	, @parent_first_name=m.parent_first_name
	, @parent_last_name=m.parent_last_name
	, @partner_id=m.partner_id
	, @lead_id=m.lead_id
	, @unsubscribe=m.unsubscribe
	, @unsubscribe_date=m.unsubscribe_date
	, @facebook_id=m.facebook_id
	, @deleted=m.deleted
	, @coppa_month=ep.coppa_month
	, @coppa_year=ep.coppa_year
	, @agree_term_services=ep.agree_term_services
	--, @user_id=user_id
from member m with(nolock)
inner join member_hierarchy mh with(nolock) on mh.member_id = m.member_id  
left outer join event_participation ep with(nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
where m.member_id = @member_id

--create user 
insert into users (culture_code, first_name, last_name, email_address, username, password, partner_id, create_date, member_id, coppa_month, coppa_year, agree_term_services, unsubscribe, unsubscribe_date, opt_status_id)
values ( @culture_code, @first_name, @last_name, @email_address, @email_address, @password, @partner_id, @create_date, @member_id, @coppa_month, @coppa_year, @agree_term_services, @unsubscribe, @unsubscribe_date, @opt_status_id)

select @user_id = @@identity

update member set [user_id] = @user_id where member_id = @member_id

--select @user_id = user_id from users where member_id = @member_id 

--select * from member_hierarchy where member_id = 1167511

DECLARE member_hierarchy_cursor CURSOR FOR 
select member_hierarchy_id, parent_member_hierarchy_id, creation_channel_id, create_date, active
from member_hierarchy with(nolock)
where member_id = @member_id


OPEN member_hierarchy_cursor;

FETCH NEXT FROM member_hierarchy_cursor 
INTO @member_hierarchy_id, @parent_member_hierarchy_id, @creation_channel_id, @mh_create_date, @active;
-- skip first
FETCH NEXT FROM member_hierarchy_cursor 
INTO @member_hierarchy_id, @parent_member_hierarchy_id, @creation_channel_id, @mh_create_date, @active;

WHILE @@FETCH_STATUS = 0
BEGIN
	insert into member (culture_code, opt_status_id, first_name, middle_name, last_name, gender, email_address, password, bounced, external_member_id, comments, create_date, parent_first_name, parent_last_name, partner_id, lead_id, unsubscribe, unsubscribe_date, facebook_id, deleted, user_id)
	values (@culture_code, @opt_status_id, @first_name, @middle_name, @last_name, @gender, @email_address, @password, @bounced, @external_member_id, @comments, @mh_create_date, @parent_first_name, @parent_last_name, @partner_id, @lead_id, @unsubscribe, @unsubscribe_date, @facebook_id, @deleted, @user_id)

	select @new_member_id = @@Identity

	update member_hierarchy set member_id = @new_member_id where member_hierarchy_id=@member_hierarchy_id

    FETCH NEXT FROM member_hierarchy_cursor 
	INTO @member_hierarchy_id, @parent_member_hierarchy_id, @creation_channel_id, @mh_create_date, @active;

END
CLOSE member_hierarchy_cursor;
DEALLOCATE member_hierarchy_cursor;
GO

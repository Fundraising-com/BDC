USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business193]    Script Date: 02/14/2014 13:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*******************************************************
CREATE BY: Jiro Hidaka
CREATE DATE: 2011-08-08

DESCRIPTION
	SPONSOR - TO SUPPORTERS LABOR DAY
	Email template 477, es_get_param_email_477
	Business Rule 193, esubs_global_v2.dbo.es_touch_business193
    [es_touch_generate_business193]
*******************************************************/
CREATE PROCEDURE [dbo].[es_touch_generate_business193]
	@launch_date datetime = NULL
AS
BEGIN
	SET NOCOUNT ON;
    
    declare @touch_info_id int
    declare @subject varchar(100)
    declare @body_txt varchar(8000)
    declare @body_html varchar(8000)
    
    if @launch_date is null 
      set @launch_date = getdate()

    begin transaction

    select @subject = subject, @body_txt = body_text, @body_html = body_html
    from business_rule br with(nolock) inner join email_template_culture etc with (nolock)
      on br.email_template_id = etc.email_template_id	
    where br.business_rule_id = 193

    exec [dbo].[es_create_touch_info]
		 @subject = @subject,
		 @body_txt = @body_txt,
		 @body_html = @body_html,
		 @launch_date = @launch_date,
		 @business_rule_id = 193,
		 @touch_info_id = @touch_info_id OUTPUT

    if @@error <> 0
    begin
	    rollback transaction
    end
    else
    begin
    	
	    insert into touch(event_participation_id,touch_info_id,processed,create_date)
    	
	    select distinct
		    ep.event_participation_id
		    ,@touch_info_id as touch_info_id
		    ,0 as processed
		    ,getdate() as create_date -- to change for the launch for -- dateadd(dd, 2, getdate()) as create_date 
		--select *     
		from 
		    member_hierarchy mh  with(nolock)
		    inner join event_participation ep with(nolock)
		    on ep.member_hierarchy_id = mh.member_hierarchy_id
		    left outer join (
			    select
				    t.touch_id
				    ,t.event_participation_id
			    from 
				    touch t		 with(nolock)
			    inner join touch_info ti with(nolock)
			    on ti.touch_info_id = t.touch_info_id
			    and (
					business_rule_id in (193, 194, 195, 196)
					and
					datepart(year,t.create_date)=datepart(year,GETDATE())
				)
		    ) t
		    on t.event_participation_id = ep.event_participation_id
		    inner join event e with(nolock)
		    on e.event_id = ep.event_id
		    inner join creation_channel cc with(nolock)
		    on cc.creation_channel_id = mh.creation_channel_id
			-- Parent
			inner join member_hierarchy mhp  with(nolock)
				on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
			inner join member mp with (nolock)
                on mhp.member_id = mp.member_id
			inner join creation_channel ccp with(nolock)
				on ccp.creation_channel_id = mhp.creation_channel_id
            inner join member m with (nolock)
                on mh.member_id = m.member_id
	    where ep.holiday_reminders = 1
--	    and 	mh.creation_channel_id not in(2,30)
	    and 	mh.parent_member_hierarchy_id is not null
	    and		t.touch_id is null
	   -- and 	e.event_type_id = 1
	    and 	cc.member_type_id = 3
	    and 	ccp.member_type_id = 1
		and		e.active = 1	
		and		mh.unsubscribe = 0
		and		m.partner_id not in (135, 143, 593, 719, 721, 131)
		and		mh.active = 1
		and		m.deleted = 0
		and		mhp.active = 1
		and		mp.deleted = 0

	    IF @@ROWCOUNT = 0 -- no rows inserted
	    begin
		    rollback transaction
	    end
	    else
	    begin
		    commit transaction
	    end
    end	

END
GO

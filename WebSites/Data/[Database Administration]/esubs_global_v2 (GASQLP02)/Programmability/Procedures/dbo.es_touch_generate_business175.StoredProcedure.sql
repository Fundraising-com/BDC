USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business175]    Script Date: 02/14/2014 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create by : Melissa Cote
-- Create date: 2010.09.02
--
-- SPONSOR - REGISTRATION CONFIRMATION w/ PROMPT TO PERSONALIZE
-- 459 / Subject: Your online fundraising campaign has been created
-- Rules: if user click skip to the personalization this email will be sent 
-- 	From System - Sponsor - Registration confirmation w/ prompt Personalize
-- 	Email template 459, es_get_param_email_459
-- 	Business Rule 175, esubs_global_v2.dbo.es_touch_business175
--  [es_touch_generate_business175]
--
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business175]
AS
BEGIN
	SET NOCOUNT ON;
    
    declare @touch_info_id int

    begin transaction

    insert into touch_info(
	    business_rule_id
	    ,launch_date
	    ,create_date
    )values(
	    175
	    ,getdate()
	    ,getdate()
    )

    if @@error <> 0
    begin
	    rollback transaction
    end
    else
    begin
    	
	    set @touch_info_id = @@identity

	    insert into touch(ep.event_participation_id,touch_info_id,processed,create_date)
    	
	    select
		    ep.event_participation_id
		    ,@touch_info_id as touch_info_id
		    ,0 as processed
		    ,getdate() as create_date
	    from 
		    member_hierarchy mh  with(nolock)
		    inner join member m with (nolock)
		    on mh.member_id = m.member_id
		    inner join [group] g with(nolock)
		    on mh.member_hierarchy_id = g.sponsor_id
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
			    and business_rule_id =175
		    ) t
		    on t.event_participation_id = ep.event_participation_id
		    inner join event e with(nolock)
		    on e.event_id = ep.event_id
			inner join dbo.personalization p with(nolock)
			on ep.event_participation_id = p.event_participation_id
		    inner join creation_channel cc with(nolock)
		    on cc.creation_channel_id = mh.creation_channel_id
	    where 
				mh.create_date > '2012-12-15 23:59:59' -- la date du lancement
	    and 	mh.creation_channel_id not in(2,30)
	    and 	mh.parent_member_hierarchy_id is null
	    and		t.touch_id is null
	    and 	e.event_type_id = 1
	    and 	cc.member_type_id = 1
		and		e.active = 1	
		and		p.skip = 1
        and		g.partner_id not in (719, 816, 832)
        and		mh.active = 1
        and		m.deleted = 0

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

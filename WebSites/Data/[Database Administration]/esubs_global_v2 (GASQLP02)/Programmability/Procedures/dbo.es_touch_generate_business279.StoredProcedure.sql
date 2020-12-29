USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business279]    Script Date: 02/14/2014 13:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*******************************************************
CREATE BY: Jiro Hidaka
CREATE DATE: 2013-11-22

DESCRIPTION
	From System - General Newsletter Email for all Sponsors excluding Great American (857)
	Email template 508, es_get_param_email_508
	Business Rule 279, esubs_global_v2.dbo.es_touch_business279
	
	Update November 29, 2013: Make emails distinct between all newsletter (279,280,281,282)
	
    [es_touch_generate_business279]
    exec [dbo].[es_touch_generate_business279]
*******************************************************/
CREATE PROCEDURE [dbo].[es_touch_generate_business279]
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
    where br.business_rule_id = 279

    exec [dbo].[es_create_touch_info]
		 @subject = @subject,
		 @body_txt = @body_txt,
		 @body_html = @body_html,
		 @launch_date = @launch_date,
		 @business_rule_id = 279,
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
		    ,getdate() as create_date
		--select *     
		from event_participation ep with (nolock)
				join member_hierarchy mh with (nolock)
				on mh.member_hierarchy_id = ep.member_hierarchy_id
				join member m with (nolock)
				on m.member_id = mh.member_id
				join event e with (nolock)
				on e.event_id = ep.event_id
				join event_group eg with (nolock)
				on e.event_id = eg.event_id
				join [group] g with (nolock)
				on g.group_id = eg.group_id	
				left join users u with (nolock)
				on m.[user_id] = u.[user_id]
				left join personalization p with (nolock)
				on ep.event_participation_id = p.event_participation_id
				join partner_product_offer ppo with (nolock)
				on ppo.partner_id = g.partner_id
				left outer join (
					select
						t.touch_id
						,t.event_participation_id
						,m.email_address
					from 
						touch t	with(nolock)
					join touch_info ti with(nolock)
					on ti.touch_info_id = t.touch_info_id
					join event_participation ep with (nolock)
					on t.event_participation_id = ep.event_participation_id
					join member_hierarchy mh with (nolock)
					on ep.member_hierarchy_id = mh.member_hierarchy_id
					join member m with (nolock)
					on mh.member_id = m.member_id
					and (
						business_rule_id in (279, 280, 281, 282)
						and
						DATEPART(YEAR,ti.launch_date)=DATEPART(YEAR,@launch_date) and
						DATEPART(MONTH,ti.launch_date)=DATEPART(MONTH,@launch_date) and
						DATEPART(DAY,ti.launch_date)=DATEPART(DAY,@launch_date)
					)
				) t
				on t.email_address = m.email_address
		where t.touch_id is null
		    and mh.unsubscribe = 0
			and	m.unsubscribe = 0
			and (u.unsubscribe is null or u.unsubscribe = 0)
			and m.bounced = 0 
			and g.partner_id not in (719, 131, 782, 143, 807, 816, 70, 126, 668, 857, 832)
			and e.active = 1
			and mh.active = 1
			and m.deleted = 0
			and e.culture_code = 'en-US'
			and ep.participation_channel_id=3
			and ppo.product_offer_id not in (5)
			and DATEPART(YEAR,e.create_date)=2013 and DATEPART(MONTH,e.create_date) BETWEEN 4 AND 12

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

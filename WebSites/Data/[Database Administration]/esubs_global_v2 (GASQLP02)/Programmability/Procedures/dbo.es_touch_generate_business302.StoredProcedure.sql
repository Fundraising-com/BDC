USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business302]    Script Date: 03/20/2015 13:51:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[es_touch_generate_business302]
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
    from business_rule br (nolock) join email_template_culture etc (nolock)
      on br.email_template_id = etc.email_template_id	
    where br.business_rule_id = 302

    exec [dbo].[es_create_touch_info]
		 @subject = @subject,
		 @body_txt = @body_txt,
		 @body_html = @body_html,
		 @launch_date = @launch_date,
		 @business_rule_id = 302,
		 @touch_info_id = @touch_info_id OUTPUT

    if @@error <> 0
    begin
	    rollback transaction
    end
    else
    begin
    	
	    insert into touch(event_participation_id,touch_info_id,processed,create_date)
    	
	    select
		    MAX(ep.event_participation_id)
		    ,@touch_info_id as touch_info_id
		    ,0 as processed
		    ,getdate() as create_date -- to change for the launch for -- dateadd(dd, 2, getdate()) as create_date 
		--select distinct,ep.event_participation_id,0 as processed,getdate()      
		from 
		    member_hierarchy mh  (nolock)
		    join event_participation ep (nolock)
		    on ep.member_hierarchy_id = mh.member_hierarchy_id
		    left outer join (
			    select
				    t.touch_id
				    ,t.event_participation_id
			    from 
				    touch t		 (nolock)
			    join touch_info ti (nolock)
			    on ti.touch_info_id = t.touch_info_id
			    and (
					business_rule_id in (302)
					and
					datepart(month,t.create_date)=datepart(month,GETDATE())
				)
		    ) t
		    on t.event_participation_id = ep.event_participation_id
		    join event e (nolock)
		    on e.event_id = ep.event_id
		    join creation_channel cc (nolock)
		    on cc.creation_channel_id = mh.creation_channel_id
            join member m (nolock)
            on mh.member_id = m.member_id
            join partner_product_offer ppo (nolock)
            on m.partner_id = ppo.partner_id
            join efrcommon.dbo.partner_profit pp (nolock)
            on m.partner_id = pp.partner_id and pp.end_date IS NULL
	    where t.touch_id is null
	    and		ppo.product_offer_id<>5
	    and		pp.profit_group_id=2
	    and 	cc.member_type_id = 1
		and		e.active = 1	
		and		mh.unsubscribe = 0
		and		m.partner_id not in (143,771,807,719,816,605,607,614,672,738,743,811,812,861,863,5649,5650,839)
		and		mh.active = 1
		and		m.deleted = 0
		and		m.bounced = 0
		and		e.culture_code = 'en-US'
		and DATEPART(YEAR, e.create_date)>2013
		group by m.email_address

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

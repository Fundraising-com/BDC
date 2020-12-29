USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business78]    Script Date: 02/14/2014 13:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2006/03/22
-- Description:	kickoff not activated
--	            less than 12 emails sent
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business78]
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
	    78
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

	    insert into touch(event_participation_id,touch_info_id,processed,create_date)
	    select 
		    event_participation_id
		    ,touch_info_id
		    ,processed
		    ,create_date
	    from (
		    select 
			    ep.event_participation_id
			    ,@touch_info_id as touch_info_id
			    ,0 as processed
			    ,dbo.es_count_event_participant(ep.event_id)  as nb_participants
			    ,dbo.es_subs_by_event(ep.event_id) as nb_subs
			    ,getdate() as create_date
		    from 
			    event e with(nolock)
			    inner join event_group eg with(nolock)
			    on eg.event_id = e.event_id
			    inner join [group] g with(nolock)
			    on g.group_id = eg.group_id
			    inner join event_participation ep with(nolock)
			    on e.event_id = ep.event_id
			    and ep.participation_channel_id = 3
			    left outer join (
				    select
					    t.touch_id
					    ,t.event_participation_id
				    from 
					    touch t		 with(nolock)
				    inner join touch_info ti with(nolock)
				    on ti.touch_info_id = t.touch_info_id
				    and business_rule_id in(78,79)
			    ) t
			    on t.event_participation_id = ep.event_participation_id
    			
		    where 
			    e.create_date > '2005-09-15 21:30:00' -- la date du lancement
		    and	t.touch_id is null
		    and 	e.create_date + 4 < getdate() -- 4 jours après le lancement
		    and 	g.partner_id <> 143
			and e.active = 1
    		
	    ) a
	    where 
		    nb_participants <=12
		    and nb_subs =0

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

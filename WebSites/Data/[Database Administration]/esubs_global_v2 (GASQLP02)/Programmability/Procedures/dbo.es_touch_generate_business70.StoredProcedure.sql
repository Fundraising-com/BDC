USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business70]    Script Date: 02/14/2014 13:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2006/03/22
-- Description:	Generates first email to sponsor
--              business rule 70
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business70]
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
	    70
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
			    -- Modification sscherr begin (Modif)
				--,dbo.es_count_event_participant(ep.event_id)  as nb_participants
			    , ecp.nb_participants
			    -- Modification sscherr end (Modif)
			    ,getdate() as create_date
		    from 
			    event e with (nolock)
			    inner join event_participation ep with (nolock)
			    on e.event_id = ep.event_id
			    and ep.participation_channel_id = 3
				inner join member_hierarchy mh with (nolock)
			    on mh.member_hierarchy_id = ep.member_hierarchy_id
			    inner join member m with (nolock)
			    on mh.member_id = m.member_id
				inner join [group] g with(nolock)
				on mh.member_hierarchy_id = g.sponsor_id
				-- Modification sscherr begin (Add)
				inner join (SELECT COUNT(ep.event_participation_id) AS nb_participants, ep.event_id
								FROM event_participation ep  with(nolock)
									INNER JOIN member_hierarchy mh  with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
							GROUP BY ep.event_id) ecp 
				on e.event_id = ecp.event_id 
				-- Modification sscherr end
			    left outer join (
				    select
					    t.touch_id
					    ,t.event_participation_id
				    from 
					    touch t with (nolock)
				    inner join touch_info ti with (nolock)
				    on ti.touch_info_id = t.touch_info_id
				    and business_rule_id = 70
			    ) t
			    on t.event_participation_id = ep.event_participation_id
		    where 
			    -- e.create_date > '2012-10-31 21:30:00' -- la date du lancement
			    e.create_date > CONVERT(varchar(10), DATEADD(MM,-1, GETDATE()), 110) + ' 12:00 AM' -- MODIFIED BY JIRO (TAKE NO OLDER THAN A MONTH)
		    and	t.touch_id is null
		    and e.event_type_id = 1
			and e.active = 1
			and g.partner_id not in (719, 816, 832)
			and	mh.active = 1
			and m.deleted = 0
	    ) a
	    where nb_participants > 1
    	
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

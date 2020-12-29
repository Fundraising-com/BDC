USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business90]    Script Date: 02/14/2014 13:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2006/033/22
-- Description:	sponsor created from banner
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business90]
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
	    90
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
		    ep.event_participation_id
		    ,@touch_info_id as touch_info_id
		    ,0 as processed
		    ,getdate() as create_date
	    from 
		    event e with (nolock)
		    inner join event_participation ep  with(nolock)
		    on e.event_id = ep.event_id
		    inner join member_hierarchy mh with(nolock)
		    on mh.member_hierarchy_id = ep.member_hierarchy_id
		    inner join member m with (nolock)
			on mh.member_id = m.member_id
		    left outer join (
			    select
				    t.touch_id 
				    ,t.event_participation_id
			    from 
				    touch t with(nolock)
			    inner join touch_info ti with(nolock)
			    on ti.touch_info_id = t.touch_info_id
			    and business_rule_id =90
		    ) t
		    on t.event_participation_id = ep.event_participation_id
	    where 
		    mh.creation_channel_id = 30
	    and 	e.create_date > '2005-11-21'
	    and	t.touch_id is null
		and e.active = 1
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

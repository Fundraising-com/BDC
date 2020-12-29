USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business118]    Script Date: 02/14/2014 13:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2007/08/15
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business118]
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @touch_info_id int

    BEGIN TRAN
	
	INSERT INTO touch_info (
		business_rule_id
		, launch_date
		, create_date
	) VALUES (
		118
		, getdate()
		, getdate()
	)
	
	IF @@error <> 0
    BEGIN
	    ROLLBACK TRAN
    END
    ELSE
    BEGIN
    	
	    SET @touch_info_id = SCOPE_IDENTITY()

		INSERT INTO touch (
			event_participation_id
		    ,touch_info_id
		    ,processed
		    ,create_date
		) 
        select
		    ep.event_participation_id
		    ,@touch_info_id as touch_info_id
		    ,0 as processed
		    ,getdate() as create_date
	    from 
		    member_hierarchy mh with(nolock)
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
			    and business_rule_id =118
		    ) t
		    on t.event_participation_id = ep.event_participation_id
		    inner join event e with(nolock)
		    on e.event_id = ep.event_id
		    inner join creation_channel cc with(nolock)
		    on cc.creation_channel_id = mh.creation_channel_id
	    where 
		    --mh.create_date > '2007-09-15 21:30:00' -- la date du lancement
	    --and 	
			mh.creation_channel_id not in(2,30)
			and 	mh.parent_member_hierarchy_id is null
			and		t.touch_id is null
			and 	e.event_type_id = 1
			and 	cc.member_type_id = 1
			and		g.partner_id = 719
			and		e.active = 1 
			and		mh.active = 1
            and		m.deleted = 0
	
		IF @@ROWCOUNT = 0 -- no rows inserted
		BEGIN
			ROLLBACK TRANSACTION
		END
		ELSE
		BEGIN
			COMMIT TRANSACTION
		END
    END
END
GO

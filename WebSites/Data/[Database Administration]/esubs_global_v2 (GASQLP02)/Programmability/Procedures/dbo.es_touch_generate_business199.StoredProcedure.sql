USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business199]    Script Date: 02/14/2014 13:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Melissa Cote>
-- Create date: <October 03, 2011>
-- Description:	<Generates invitations to sponsor to redeem the $100 time for kids magazines>
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business199]
AS
BEGIN
   SET NOCOUNT ON;

   DECLARE @touch_info_id INT
   DECLARE @event_participation_id INT
   DECLARE @prize_item_id_1 INT, @prize_item_id_2 INT
	
   BEGIN TRAN

   INSERT INTO touch_info
   (
	    business_rule_id
	    ,launch_date
	    ,create_date
    )
	VALUES
    (
	    199
	    ,getdate()
	    ,getdate()
    )
	SET @touch_info_id = SCOPE_IDENTITY()

    IF @@error <> 0
    BEGIN
	    ROLLBACK TRANSACTION
    END
    ELSE
		BEGIN
			SELECT 
			    ep.event_participation_id
			into #tmp 
			from event_participation ep with(nolock)
			inner join dbo.event_total_amount eta with(nolock) on eta.event_id = ep.event_id
			inner join dbo.event e with(nolock) on e.event_id = ep.event_id 
			inner join dbo.event_group eg with(nolock) on eg.event_id = e.event_id 
			inner join dbo.[group] g with(nolock)	on eg.group_id = g.group_id 
			inner join member_hierarchy mh  with(nolock) on g.sponsor_id = mh.member_hierarchy_id and ep.member_hierarchy_id = mh.member_hierarchy_id
            left outer join (
			    select
				     t.touch_id
				    ,t.event_participation_id
			    from 
				    touch t with(nolock)
			    inner join touch_info ti with(nolock)
			    on ti.touch_info_id = t.touch_info_id
			    and business_rule_id = 199
		    ) t
		    on t.event_participation_id = ep.event_participation_id
			where g.partner_id = 832
                and t.touch_id is null 
                and total_amount >= 10                
				and e.active = 1

			INSERT INTO touch(event_participation_id,touch_info_id,processed,create_date)
			SELECT 
			    event_participation_id
			    , @touch_info_id as touch_info_id
			    , 0 as processed
			    , getdate() as create_date
			from #tmp

			IF @@ROWCOUNT = 0 -- no rows inserted
			BEGIN
				ROLLBACK TRANSACTION
				RETURN;
			END
			
			DECLARE _cursor CURSOR FOR 
			SELECT event_participation_id
			from #tmp

			OPEN _cursor;

			FETCH NEXT FROM _cursor 
			INTO @event_participation_id;

			WHILE @@FETCH_STATUS = 0
			BEGIN
				select 
					@prize_item_id_1 = max(case when id = 1 then prize_item_id end),
				    @prize_item_id_2 = max(case when id = 2 then prize_item_id end) 
				from (
					select ROW_NUMBER() OVER(ORDER BY pt.prize_item_id ASC) as id,
                           pt.prize_item_id
					from prize_item pt with(nolock)
					left join earned_prize pe with(nolock)
						on pt.prize_item_id = pe.prize_item_id
					where pe.prize_item_id is null
					  and pt.expiration_date > getdate() + 31
					  and pt.prize_id = 12
				) te;

			    /* Insert first code */
				Insert into earned_prize(prize_item_id, event_participation_id, create_date) VALUES (@prize_item_id_1, @event_participation_id, getdate());

				IF @@ROWCOUNT = 0 -- no rows inserted
				BEGIN
					ROLLBACK TRANSACTION
					RETURN;
				END

				/* Insert second code */
                Insert into earned_prize(prize_item_id, event_participation_id, create_date) VALUES (@prize_item_id_2, @event_participation_id, getdate());

				IF @@ROWCOUNT = 0 -- no rows inserted
				BEGIN
					ROLLBACK TRANSACTION
					RETURN;
				END			

				FETCH NEXT FROM _cursor 
				INTO @event_participation_id;
			END
			CLOSE _cursor;
			DEALLOCATE _cursor;
		    
			COMMIT TRAN;
		END 	
	END
GO

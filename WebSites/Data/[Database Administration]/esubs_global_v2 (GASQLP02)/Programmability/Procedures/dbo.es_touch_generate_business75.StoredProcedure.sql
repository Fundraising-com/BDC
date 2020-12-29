USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business75]    Script Date: 02/14/2014 13:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pavel Tarassov>
-- Create date: <July 13, 2010>
-- Description:	<Generates invitations to participants to redeem the movie code>
-- =============================================
CREATE  PROCEDURE [dbo].[es_touch_generate_business75]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
   SET NOCOUNT ON;

   DECLARE @touch_info_id int
	
   BEGIN TRAN

   INSERT INTO touch_info
   (
	    business_rule_id
	    ,launch_date
	    ,create_date
    )
	VALUES
    (
	    75
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
			INSERT INTO touch(event_participation_id,touch_info_id,processed,create_date)
			SELECT 
			     event_participation.event_participation_id
			    ,@touch_info_id as touch_info_id
			    ,0 as processed
			    ,getdate() as create_date
			  FROM event_participation join event on event_participation.event_id = event.event_id
			 INNER JOIN (
				SELECT member_hierarchy.parent_member_hierarchy_id
				  FROM member_hierarchy with (nolock) 
				 INNER JOIN creation_channel with (nolock) on creation_channel.creation_channel_id = member_hierarchy.creation_channel_id 
				   AND creation_channel.member_type_id = 2 and member_hierarchy.active = 1 and member_hierarchy.unsubscribe = 0
				 INNER JOIN event_participation on event_participation.member_hierarchy_id =member_hierarchy.parent_member_hierarchy_id
				 INNER JOIN [event] on event.event_id = event_participation.event_id
				 INNER JOIN [event_group] on event_group.event_id = event.event_id 
				 INNER JOIN [group] on [group].group_id = [event_group].group_id
				 INNER JOIN efundweb..[partner] on efundweb..[partner].partner_id = [group].partner_id 
				 WHERE member_hierarchy.create_date > '01-01-2010' and event.active = 1 and event.culture_code = 'en-US' and prize_eligible = 1
				 GROUP BY member_hierarchy.parent_member_hierarchy_id 
				HAVING count(member_hierarchy.member_hierarchy_id)  >= 12) t1 on event_participation.member_hierarchy_id = t1.parent_member_hierarchy_id
			 INNER JOIN ( 
			 	SELECT DISTINCT member_hierarchy.parent_member_hierarchy_id 
			  	  FROM member_hierarchy with (nolock) 
				 INNER JOIN creation_channel with (nolock) on creation_channel.creation_channel_id = member_hierarchy.creation_channel_id 
				   AND creation_channel.member_type_id = 2 and member_hierarchy.active = 1 and member_hierarchy.unsubscribe = 0
				 INNER JOIN event_participation with (nolock) on event_participation.member_hierarchy_id = member_hierarchy.parent_member_hierarchy_id
				 INNER JOIN dbo.es_get_valid_orders_items() es ON event_participation.event_participation_id = es.supp_id
				 WHERE member_hierarchy.create_date > '01-01-2010') t2 on t1.parent_member_hierarchy_id = t2.parent_member_hierarchy_id
			  LEFT JOIN (
			  	SELECT event_participation_id 
			  	  FROM touch with (nolock)  
			  	 INNER JOIN touch_info with (nolock) on touch.touch_info_id = touch_info.touch_info_id 
			  	   AND touch_info.business_rule_id = 75)t3 on event_participation.event_participation_id = t3.event_participation_id 
			  	   AND  t3.event_participation_id is null
			  LEFT JOIN (
			  	SELECT earned_prize.event_participation_id 
			  	  FROM earned_prize with (nolock) 
			  	 INNER JOIN prize_item with (nolock) on earned_prize.prize_item_id = prize_item.prize_item_id and prize_id = 6 )t4 on event_participation.event_participation_id = t4.event_participation_id  and  t4.event_participation_id is null
			 WHERE event.active = 1;
			
			IF @@ROWCOUNT = 0 -- no rows inserted
			BEGIN
				ROLLBACK TRANSACTION
			END
			ELSE
			BEGIN
				COMMIT TRAN
			END
		END 	
	END
GO

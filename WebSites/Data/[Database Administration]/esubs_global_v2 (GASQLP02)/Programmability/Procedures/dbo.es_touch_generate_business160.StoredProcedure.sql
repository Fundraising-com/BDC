USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business160]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pavel Tarassov>
-- Create date: <April 13, 2010>
-- Description:	<Generates touches for Active Sponsors whose campaigned 
-- was auto-created 21 days ago>
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business160]
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
	    160 -- to be changed
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
			SELECT TOP 4000
			    event_participation.event_participation_id
			    ,@touch_info_id as touch_info_id
			    ,0 as processed
			    , getdate() as create_date
			FROM [event]  with(nolock) inner join 
			event_group  with(nolock) on [event].event_id = event_group.event_id
			inner join [group]  with(nolock) on  [group].group_id = event_group.group_id
			inner join event_participation  with(nolock) on event.event_id = event_participation.event_id 
			and event_participation.participation_channel_id = 3 
			left outer join 
			(
				select distinct event_participation.event_id as eventid
				from touch	with(nolock) inner join touch_info
				on touch.touch_info_id = touch_info.touch_info_id
				inner join event_participation  with(nolock) on touch.event_participation_id = event_participation.event_participation_id 
				and business_rule_id = 160
			) T2 --- add proper business rule here
			on event.event_id = T2.eventid
			where [group].partner_id = 8 and [event].active = 1 and [group].external_group_id is not null 
			and T2.eventid is null and [event].start_date > '08-01-2013' and [event].start_date < dateadd(day, -21, getdate())
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

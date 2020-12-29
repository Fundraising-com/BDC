USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_email_sent_for_participant]    Script Date: 02/14/2014 13:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===================================================
-- Author:		JIRO HIDAKA
-- UPDATED date: 4/7/10 
-- Description:	Returns participant emails filtered by
--              event id and event particpation id

-- update by Melissa Cote 
-- update date 2010.09.30
-- add new business rule id

-- update by Jiro Hidaka
-- update date 2013.12.05
-- cleaned up business rule id
-- ===================================================
CREATE PROCEDURE [dbo].[es_get_event_email_sent_for_participant] 
	@event_id int,
    @eventParticipationID int
AS
BEGIN
--SELECT * FROM business_rule ORDER BY 1 DESC 
--SELECT * FROM dbo.email_template ORDER BY 1 DESC 

	select 
		Convert(datetime, Convert(varchar(2), month(launch_date)) + '/' + convert(varchar(2), day(launch_date)) + '/' + convert(varchar(4), year(launch_date))) as launch_date,
		(case 
				-- Maximum 30 caracteres for description		then 'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX'
				when business_rule_id IN (104)				then 'Supporter First Email'
				when business_rule_id IN (105)				then 'Supporter First Reminder'
				when business_rule_id IN (106)				then 'Supporter Second Reminder'
                when business_rule_id IN (153)				then 'Supporter Third Reminder'
                when business_rule_id IN (218)				then 'Supporter Final Reminder'
				when business_rule_id IN (107)				then 'Supporter Reminder'
				when business_rule_id IN (108)				then 'Supporter Personal Note'
			else '' end ) as email_desc
         , count(*) as email_sent
		from event_participation ep
			inner join member_hierarchy mh
				on mh.member_hierarchy_id = ep.member_hierarchy_id
			inner join member_hierarchy mh_child
				on mh_child.parent_member_hierarchy_id = mh.member_hierarchy_id
			inner join event_participation ep_child
				on ep_child.member_hierarchy_id = mh_child.member_hierarchy_id and ep_child.event_id = @event_id
			inner join touch t
				on t.event_participation_id = ep_child.event_participation_id
			inner join touch_info ti
				on ti.touch_info_id = t.touch_info_id
		where ep.event_participation_id = @eventParticipationID
  			and business_rule_id in (104,105,106,107,108,153,218)
			and t.processed <> 12
		group by 
			Convert(datetime,convert(varchar(2), month(launch_date)) + '/' + convert(varchar(2), day(launch_date)) + '/' + convert(varchar(4), year(launch_date)))	
			,business_rule_id
		order by
			Convert(datetime,convert(varchar(2), month(launch_date)) + '/' + convert(varchar(2), day(launch_date)) + '/' + convert(varchar(4), year(launch_date))) desc
			,business_rule_id
END
GO

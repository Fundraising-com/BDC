USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_eventcleanup]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================
-- Author:		Philippe Girard (UPDATE BY JIRO HIDAKA)
-- Create date: 2007/01/23 -> edited 2013-07-04
-- Description:	Disable old events with no activity
-- =====================================================
CREATE PROCEDURE [dbo].[es_eventcleanup]
AS
BEGIN
	SET NOCOUNT ON;
    
    declare @date datetime
    set @date = dateadd(yy,-3,getdate())
    
    update event
    set active = 0
    where event_id in (
        select e.event_id
        from event e
            left join (
                select ep.event_id
                     , ep.event_participation_id
                from event_participation ep
                where ep.create_date > @date
            ) email_activity
                on email_activity.event_id = e.event_id
            left join (
                select ep.event_id
                from  event_participation ep inner join
                      dbo.es_get_valid_orders_items() es on ep.event_participation_id = es.supp_id
                where es.create_date > @date
            ) sales
                on sales.event_id = e.event_id
			inner join [event_group] eg
				on eg.event_id = e.event_id
			inner join [group] g
				on g.group_id = eg.group_id
			left join featured_event fe
				on e.event_id = fe.event_id
        where e.create_date < @date
          and e.active = 1
          and email_activity.event_id is null
          and sales.event_id is null
          and fe.event_id is null
		  and g.partner_id not in (143, 605, 606, 607)
    )

END
GO

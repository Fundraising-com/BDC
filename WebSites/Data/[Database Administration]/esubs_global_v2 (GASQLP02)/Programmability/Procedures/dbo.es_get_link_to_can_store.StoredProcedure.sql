USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_link_to_can_store]    Script Date: 02/14/2014 13:05:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[es_get_link_to_can_store]  @event_participation_id int, @subdivision_code nvarchar(7)
AS
BEGIN
    SELECT
        e.event_id
        , e.event_type_id
        , e.event_name
        , e.start_date
        , e.end_date
        , st.account_number
        , st.opportunity_id
        , st.store_id
        , m.first_name + ' ' + m.last_name as [name]
        , m.email_address
        , CASE WHEN sm.member_id IS NULL THEN m.first_name + ' ' + m.last_name ELSE sm.first_name + ' ' + sm.last_name END as [parent_name]
        , CASE WHEN sm.member_id IS NULL THEN m.email_address ELSE sm.email_address END as parent_email_address
        , mh.member_hierarchy_id
        , st.aggregator_id
        , st.store_template_id
        , m.culture_code
        , ep.event_participation_id
    FROM event_participation as ep
        inner join event as e
            on ep.event_id = e.event_id
        inner join event_group as eg
            on eg.event_id = e.event_id
        inner join [group] as g
            on g.group_id = eg.group_id
        inner join partner as p
            on p.partner_id = g.partner_id
        inner join partner_store_template as pst
            on pst.partner_id = p.partner_id
        inner join store_template as st
            on st.store_template_id = pst.store_template_id
        inner join member_hierarchy as mh
            on mh.member_hierarchy_id = ep.member_hierarchy_id
        inner join member as m
            on m.member_id = mh.member_id
        left join member_hierarchy as smh
            on smh.member_hierarchy_id = mh.parent_member_hierarchy_id
        left join member as sm
            on sm.member_id = smh.member_id
    WHERE (e.end_date < GETDATE() OR e.end_date is null)
      AND st.culture_code = 'en-CA'
      AND ep.event_participation_id = @event_participation_id
      AND st.subdivision_code = @subdivision_code
    
END
GO

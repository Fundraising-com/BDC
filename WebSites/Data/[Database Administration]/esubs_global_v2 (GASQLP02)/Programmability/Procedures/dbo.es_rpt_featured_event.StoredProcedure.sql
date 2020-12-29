USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_featured_event]    Script Date: 02/14/2014 13:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2006/03/01
-- Description:	Fetch all the data for the 
--              featured group page
--
-- Ex: EXEC [dbo].[es_rpt_featured_event]
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_featured_event]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	
    select top 4 event_id
           , event_name
           , state
           , nb_member
           , nb_sub
           , amount from dbo.featured_event_mainpage
    
    /*select e.event_id
           , e.event_name
           , RIGHT(pa.subdivision_code,CHARINDEX('-', pa.subdivision_code)-1)  AS state
           , enb.total as nb_member
           , esub.total as nb_sub
           , esub.amount
        from event e
            inner join event_group eg
                on eg.event_id = e.event_id
            inner join [group] g
                on g.group_id = eg.group_id
            inner join payment_info pinfo
                on pinfo.group_id = eg.group_id
                and pinfo.event_id = eg.event_id
            left join postal_address pa
                on pa.postal_address_id = pinfo.postal_address_id
            left join subdivision sub
                on sub.subdivision_code = pa.subdivision_code
            inner join (
                select count(ep.event_id) as total, fe.event_id
                from event_participation ep
                    inner join featured_event fe on fe.event_id = ep.event_id
                group by fe.event_id
            ) enb on enb.event_id = e.event_id
            inner join (
                select SUM(es.quantity) as total, SUM(es.total_amount) as amount, fe.event_id
                from event_participation ep
                    inner join dbo.es_get_valid_orders_items() es on ep.event_participation_id = es.supp_id
                    inner join featured_event fe on fe.event_id = ep.event_id
                group by fe.event_id
            ) esub on esub.event_id = e.event_id
    where pinfo.active = 1
	  and e.active=1
    order by esub.amount desc
*/
END
GO

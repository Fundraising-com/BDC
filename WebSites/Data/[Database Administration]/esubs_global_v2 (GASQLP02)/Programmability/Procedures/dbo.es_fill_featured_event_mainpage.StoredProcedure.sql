USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_fill_featured_event_mainpage]    Script Date: 02/14/2014 13:05:11 ******/
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
CREATE PROCEDURE [dbo].[es_fill_featured_event_mainpage]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO featured_event_mainpage (
        event_id
        , event_name
        , state
        , nb_member
        , nb_sub
        , amount
    )
    select e.event_id
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
                select count(ep.event_id) as total, ep.event_id
                from event_participation ep
                group by ep.event_id
            ) enb on enb.event_id = e.event_id
            inner join (
                select SUM(es.quantity) as total, SUM(es.total_amount) as amount, ep.event_id
                from event_participation ep
                  inner join dbo.es_get_valid_orders_items() es on ep.event_participation_id = es.supp_id
                group by ep.event_id
            ) esub on esub.event_id = e.event_id
    where pinfo.active = 1
	and e.event_id in (1005065, 1002418, 771802, 782519, 1008133, 732099, 1016840, 1011130, 
                       818551, 729168, 729592, 737699, 1010598, 1008891, 826855, 1012035, 
                       785014, 1003740, 824765, 1007394, 833204, 1016373, 692226, 826297, 
                       1050970, 1004373, 1053335, 1050335, 728529, 1050966, 1013808, 
                       827884, 828681, 1051970, 737672, 1016311, 1010174, 1000298, 1021454, 
                       832140, 1012683, 1050323, 1008525, 831342)
	AND e.active = 1
    order by esub.amount desc

END
GO

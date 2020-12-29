USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_orders_for_campaign]    Script Date: 02/25/2015 12:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jean-Francois Lavigne
-- ALTER  date: 
-- Description:	rapport des supporters dans le participant zone
-- 
-- Modified by: Philippe Girard
-- Modified on: March 22, 2006
--
-- Modified by: Jiro Hidaka
-- Modified on: November 11, 2013
-- Description: Added orders from GA store database
--
--  exec [dbo].[cc_get_orders_for_campaign] 1519088
-- =============================================
ALTER PROCEDURE [dbo].[cc_get_orders_for_campaign]
    @event_id int
AS
BEGIN 

select
    mh.member_hierarchy_id 
	, m.first_name + ' ' + m.last_name as member_name
	, sum(tps.quantity) as nb_subs
	, sum(tps.price * tps.quantity) as amount
    , tps.first_name + ' ' + tps.last_name as customer_Name
    , tps.product_desc as CatalogItemTitle
    , tps.create_date as orderDate 
    , tps.order_id as OrderID
    , tps.order_id as eds_id
    , mh.parent_member_hierarchy_id 
    , parent.parent_name
    , dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
    , ep.event_participation_id
from
	event_participation ep
	inner join member_hierarchy mh
	    on mh.member_hierarchy_id = ep.member_hierarchy_id
    inner join member m
	    on m.member_id = mh.member_id
	left join
	    [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
	left join
		(select mh.member_hierarchy_id, u.first_name + ' ' + u.last_name as parent_name
		from member_hierarchy mh	    
		join member m
		   on m.member_id = mh.member_id
		join users u
		   on u.user_id = m.user_id
		) as parent on parent.member_hierarchy_id = mh.parent_member_hierarchy_id
where ep.event_id = @event_id and tps.quantity > 0
group by
     mh.member_hierarchy_id
	, m.first_name
	, m.last_name
    , tps.create_date 
    , tps.first_name 
    , tps.last_name 
    , tps.product_desc
    , tps.order_id
    , tps.order_item_id
    , mh.parent_member_hierarchy_id
    , parent.parent_name
    , ep.event_participation_id
order by 8

END

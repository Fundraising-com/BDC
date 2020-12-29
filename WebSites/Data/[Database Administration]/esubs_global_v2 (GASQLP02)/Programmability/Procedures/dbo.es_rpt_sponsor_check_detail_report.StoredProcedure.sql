USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_sponsor_check_detail_report]    Script Date: 02/14/2014 13:06:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: April 23, 2010
-- Description:	PROC FOR VARIABLE PROFIT CHECK DETAIL REPORT.
--				PROC NAME WILL CHANGE LATER
-- UPDATE MARCH 14, 2013 by Jiro Hidaka
--			part_name must come from users table
-- exec [dbo].[es_rpt_sponsor_check_detail_report] 1002418
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_sponsor_check_detail_report]
	@event_id int,
	@cheque_number int = NULL
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tps (
		  rownum int identity(1,1)
        , suppid int
        , payment_item_id int
        , product_type_name varchar(50)
		, order_detail_amount money
		, profit_percentage float
        , profit_amount money
		, order_date datetime
        , quantity int
        , price money
        , cheque_number int
        , profit_id int 
        , profit_range_id int
	)

	INSERT INTO #tps (
          et.suppid
        , payment_item_id
		, product_type_name       
        , order_detail_amount
		, profit_percentage
		, profit_amount
		, order_date
        , quantity
		, price
        , cheque_number
        , profit_id
        , profit_range_id
	)
	    select et.suppid, pit.payment_item_id, pt.product_type_name, pit.order_detail_amount, pit.profit_percentage,
               pit.profit_amount, o.order_date, od.quantity, od.price, p.cheque_number, pit.profit_id, pit.profit_range_id
			 from payment_info pin
			inner join payment p on pin.payment_info_id = p.payment_info_id
			left join payment_period pp on pp.payment_period_id = p.payment_period_id
			inner join payment_item pit on pit.payment_id = p.payment_id 
			inner join payment_payment_status pps on pps.payment_id = p.payment_id
			inner join (
				select payment_id, max(create_date) as create_date
				from payment_payment_status
				group by payment_id
				   ) ppsNew on ppsNew.payment_id = pps.payment_id and ppsNew.create_date = pps.create_date 	and payment_status_id in (2, 10)
			left join dbo.payment_batch pb on pb.payment_batch_id = p.payment_batch_id and confirmation_date is not null
			inner join qspfulfillment.dbo.[order_detail] od on od.order_detail_id = pit.qsp_order_detail_id
			inner join qspfulfillment.dbo.[order] o on od.order_id = o.order_id
			inner join qspecommerce.dbo.efundraisingtransaction et on o.order_id = et.orderid
			inner join qspfulfillment..catalog_item_detail cid on cid.catalog_item_detail_id = od.catalog_item_detail_id
			inner join qspfulfillment..catalog_item ci on ci.catalog_item_id = cid.catalog_item_id
			inner join qspfulfillment..product pr on pr.product_id = ci.product_id
			inner join qspfulfillment..product_type pt on pr.product_type_id = pt.product_type_id
		 where pin.event_id = @event_id and (p.cheque_number = @cheque_number or @cheque_number is null)
     		order by o.order_date
				, pit.order_detail_amount

	create index ix_payment_item_id on #tps (payment_item_id)

	-- supporters orders 
	select cheque_number,
    (case 
	-- sponsor order must be under his name
	when (mp.first_name + ' ' + mp.last_name) is null 
	then CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
	          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
	     END 
	-- participant orders must be under his name
	when cc.member_type_id = 2
	then CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
	          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
	     END
	else CASE WHEN (up.first_name + ' ' + up.last_name) IS NOT NULL THEN dbo.TitleCase(lower(up.first_name + ' ' + up.last_name))
	          ELSE dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
	     END 
	end) as part_name     
	, m.first_name + ' ' + m.last_name as supp_name
    , tps.payment_item_id
    , tps.product_type_name
	, tps.order_detail_amount
	, tps.profit_amount
    , tps.profit_percentage
	, convert(varchar(10), (tps.order_date), 101) as purchasedate
    , tps.quantity
    , tps.price
    , tps.profit_id
    , tps.profit_range_id
	from event_participation ep
--	inner join event_group eg on eg.event_id = ep.event_id 
--	inner join [event] e on e.event_id = eg.event_id 
--	inner join [group] g on g.group_id = eg.group_id 
	inner join #tps tps on tps.suppid = ep.event_participation_id
	-- enfant
	inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id 
	inner join member m on m.member_id = mh.member_id
	left join users u with (nolock) on m.user_id = u.user_id
	-- parent
	left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	left outer join member mp on mp.member_id = mhp.member_id
	left join users up with (nolock) on mp.user_id = up.user_id
	inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
	--where ep.event_id = @event_id
	--and mh.active = 1
	order by 1,2
END
GO

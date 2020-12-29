USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_sales_by_product_type]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: January 6, 2011
-- Description:	Calculates total amount sold by
--              product type and campaign
-- =============================================
CREATE PROCEDURE [dbo].[es_get_sales_by_product_type] 
	@product_type_id int,
	@event_id int,
    @event_participation_id int	= NULL
AS
BEGIN
	SET NOCOUNT ON;

    -- pre-generate the tps
    CREATE TABLE #tps (
	    rownum int identity(1,1)
	    , orderid int
	    , quantity int
	    , price money
	    , suppID int
        , product_type int
        , charge numeric(18,2)
        , updatedate datetime
        , catalog_type int
    )

	INSERT INTO #tps (
		orderid
	    , quantity
	    , price
	    , suppID
        , product_type
        , charge
	    , updatedate
        , catalog_type
	)
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
         , pt.product_type_id as product_type
         , COALESCE(pt.fulfillment_charge, 0) as charge
         , o.order_date as updatedate
         , xc.x_catalog_type_id as catalog_type
	from event_participation ep
		inner join qspecommerce.dbo.efundraisingtransaction et on et.suppid = ep.event_participation_id
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
        inner join qspfulfillment..catalog_item_detail cid on cid.catalog_item_detail_id = od.catalog_item_detail_id
		inner join qspfulfillment..catalog_item ci on ci.catalog_item_id = cid.catalog_item_id
        inner join qspecommerce.dbo.x_catalog xc on ci.catalog_id = xc.x_catalog_id
		inner join qspfulfillment..product p on p.product_id = ci.product_id
		inner join qspfulfillment..product_type pt on p.product_type_id = pt.product_type_id
		INNER JOIN dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
	where ep.event_id = @event_id and pt.product_type_id = @product_type_id
   	order by updatedate
               , od.price

	create index ix_suppid on #tps (suppid)

   
    create table #supp (
		event_participation_id int
	)

	IF @event_participation_id IS NOT NULL
	BEGIN
		INSERT INTO #supp (
			event_participation_id
		)
		select @event_participation_id
		union all
		select ep.event_participation_id
		from event_participation ep
				inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
				inner join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
				inner join event_participation epp on epp.member_hierarchy_id = mhp.member_hierarchy_id
		where epp.event_participation_id = @event_participation_id

		create index ix_event_participation_id on #supp (event_participation_id)
	END

	IF EXISTS(SELECT * FROM #supp)
	BEGIN
		select sum(case 
				when tps.catalog_type = 11 then 0
				else quantity end) as nb_items
			 , sum(case 
				when tps.catalog_type = 11 then 
					quantity * price
				else quantity * price end) as amount_sold
			 , sum(case 
				-- For Donation use 93.5% profit
				when tps.catalog_type = 11 then
					(case when tps.updatedate < '2011-04-01' then 
						quantity * price * 93.5/100.0
					else
						quantity * price * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
				else
				-- For all other product percent profit use event profit calculated field (January 6, 2011)
					quantity * (price - charge) * Isnull(e.profit_calculated, 30.0)/100.0 end) as profit
		from event_participation ep
			inner join [event] e on e.event_id = ep.event_id
			inner join #supp supp on supp.event_participation_id = ep.event_participation_id
			left outer join #tps tps on tps.suppid = supp.event_participation_id
            -- get donation profit from efrcommon.dbo.profit
			left outer join efrcommon.dbo.profit donation_profit with (nolock) 
				on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11
		where ep.event_id = @event_id
		group by ep.event_id
	END
	ELSE
	BEGIN
		select sum(case 
				when tps.catalog_type = 11 then 0
				else quantity end) as nb_items  
			 , sum(case 
				when tps.catalog_type = 11 then
					quantity * price
				else quantity * price end) as amount_sold
			 , sum(case 
				-- For Donation use 93.5% profit
				when tps.catalog_type = 11 then
					(case when tps.updatedate < '2011-04-01' then 
						quantity * price * 93.5/100.0
					else
						quantity * price * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
				else
				-- For all other product percent profit use event profit calculated field (January 6, 2011)
					quantity * (price - charge) * Isnull(e.profit_calculated, 30.0)/100.0 end) as profit
		from event_participation ep
			inner join [event] e on e.event_id = ep.event_id
			left outer join #tps tps on tps.suppid = ep.event_participation_id
			-- get donation profit from efrcommon.dbo.profit
			left outer join efrcommon.dbo.profit donation_profit with (nolock) 
				on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11
		where ep.event_id = @event_id
		group by ep.event_id
	END

END
GO

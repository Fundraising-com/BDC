USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_participant_sales_by_partner_detail]    Script Date: 02/14/2014 13:06:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pavel Tarassov>
-- Create date: <17-07-2009>
-- Description:	<Provides info for the sponsor how many emails each member sent, what kind of product
-- was purchased, quantities, profit>
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_participant_sales_by_partner_detail]
	@partner_id INT, 	@event_id INT
AS
BEGIN

	SET NOCOUNT ON;

	create table #tps (
		rownum int identity(1,1)
		, orderid INT
		, orderdate DATETIME
		, quantity int
		, price money
		, suppID int
		, product_type int 
		, product_desc VARCHAR(50)
		, charge numeric(18,2)
		, supporter varchar(50)
		, updatedate datetime
        , catalog_type int
	)

	INSERT INTO #tps (
		orderid
		, orderdate
		, quantity
		, price
		, suppID
		, product_type
		, product_desc
		, charge
		, supporter
		, updatedate
        , catalog_type
	)

	select o.order_id as orderid
		, o.create_date
		, od.quantity
		, od.price
		, et.suppid
		, pt.product_type_id as product_type
		, pt.product_type_name AS product_desc
		, COALESCE(pt.fulfillment_charge, 0) as charge
		, (case when ltrim(rtrim(isnull(em.recipient_name,''))) = '' 
			then em.email_address
			else em.recipient_name
			end) as supporter
		, o.order_date as updatedate
        , xc.x_catalog_type_id as catalog_type
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
		INNER JOIN event evt ON evt.event_id = ep.event_id
		INNER JOIN dbo.event_group eg ON eg.event_id = evt.event_id
		INNER JOIN dbo.[group] grp ON grp.group_id = eg.group_id
		left join qspfulfillment..email em on o.billing_email_id = em.email_id
		inner join qspfulfillment..catalog_item_detail cid on cid.catalog_item_detail_id = od.catalog_item_detail_id
		inner join qspfulfillment..catalog_item ci on ci.catalog_item_id = cid.catalog_item_id
		inner join qspecommerce.dbo.x_catalog xc on ci.catalog_id = xc.x_catalog_id
		inner join qspfulfillment..product p on p.product_id = ci.product_id
		inner join qspfulfillment..product_type pt on p.product_type_id = pt.product_type_id
		inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
	where grp.partner_id = @partner_id
	order by updatedate
			, od.price

	create index ix_suppid on #tps (suppid)

	select
		g.group_name, 
		tps.orderid,
		tps.orderdate, 
		(case 
			-- sponsor order must be under his name
			when (mp.first_name + ' ' + mp.last_name) is null 
			then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			-- participant orders must be under his name
			when cc.member_type_id = 2
			then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			else dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		end) as member_name
		, 
		(case
			when (m.first_name + ' ' + m.last_name) is null
			then dbo.TitleCase(lower(tps.supporter))
			else dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		end) as supporter_name 
		--, count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
		--	then m.member_id else NULL end ) as email_sent
		,COALESCE(quantity,0) as nb_subs
		,COALESCE(quantity * price,0) as amount
		,COALESCE(tps.product_type, 0) as product_type
		,tps.product_desc AS product_desc
	from event_participation ep
		inner join event_group eg on eg.event_id = ep.event_id 
		inner join [event] e on e.event_id = eg.event_id
		inner join [group] g on g.group_id = eg.group_id 
		-- get the partner profit percent
		--left outer join partner_payment_config ppc
		--on g.partner_id=ppc.partner_id
		--order
		left outer join #tps tps on tps.suppid = ep.event_participation_id
		-- enfant
		inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
		inner join member m on m.member_id = mh.member_id
		-- parent
		left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
		left outer join member mp on mp.member_id = mhp.member_id
		left join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
		-- get donation profit from efrcommon.dbo.profit
		left outer join efrcommon.dbo.profit donation_profit with (nolock) 
			on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11
	where mh.active = 1 
	AND g.partner_id = @partner_id
	AND tps.orderid IS NOT null
	--group by 
	--	g.group_name, 
	--	(case 
	--		-- sponsor order must be under his name
	--		when (mp.first_name + ' ' + mp.last_name) is null 
	--		then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
	--		-- participant orders must be under his name
	--		when cc.member_type_id = 2
	--		then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
	--		else dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
	--	end)
	--	,(case
	--		when (m.first_name + ' ' + m.last_name) is null
	--		then dbo.TitleCase(lower(tps.supporter))
	--		else dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
	--	end)
	--	, tps.product_type
	--	--having count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
	--	-- then m.member_id else NULL end ) > 0
	order by 1, 2

END
GO

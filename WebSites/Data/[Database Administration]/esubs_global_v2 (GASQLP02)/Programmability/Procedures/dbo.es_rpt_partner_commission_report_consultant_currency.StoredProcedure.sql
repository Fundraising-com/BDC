USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_commission_report_consultant_currency]    Script Date: 02/14/2014 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec es_rpt_partner_commission_report_consultant '2006-09-01', '2006-09-30 23:59:59'

/*
procedure qui affiche un rapport sommaire pour les partners
mod mcote 	2006-02-18 	- creation de table temporaire (optimisation)
				- select return the header of the report
				- creation d'une table temporaire pour les resultats du rapports
				- select return the detail of the report
				- select return the footer(sum) of the report

*/
--truncate #temp
--drop table #temp
CREATE     procedure [dbo].[es_rpt_partner_commission_report_consultant_currency] --'2010-9-1','2010-9-30'
		@start_date as datetime 
		, @end_date as datetime
as
BEGIN




        create table #temp (
			order_detail_id int,
             order_id int
            ,redeemed_amount money
            ,PRIMARY KEY (order_detail_id)
            ,UNIQUE (order_detail_id) 
		)
        CREATE INDEX IX_1 on #temp (order_id)
        insert into #temp
        select od.order_detail_id,
               t.Order_id,
               t.redeemed_amount / (select count(order_id) from qspfulfillment.dbo.order_detail where order_id = t.order_id group by order_id) as redeemed_amount
        from qspfulfillment.dbo.order_detail od with(nolock)
        inner join        
            (SELECT  Order_ID, Redeemed_Amount AS Redeemed_Amount
		     FROM	qspfulfillment.dbo.fn_discount_redeemed_amount(null,null,null,null,12)) t
         on od.order_id = t.order_id
/*
declare @start_date datetime
declare @end_date datetime
set @start_date = '2010-9-1'
set @end_date = '2010-9-30'
*/

	declare @end_date2 varchar(30)
	set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
	set @end_date = convert(datetime, @end_date2)


	-- pre-generate the tps
	create table #tps (
		rownum int identity(1,1)
		, act int default 0
		, orderid int
		, producttypeid int
		, product varchar(30)
		, quantity int
		, price money
		, sales money
		, freight money
        , discount money
		, subtotal money
		, salesminusprofit money
		, suppid int
		, event_id int
		, trxid INT
		, createdate datetime
	)
		
	INSERT INTO #tps (
	       orderid
	     , producttypeid
	     , product
	     , quantity
	     , price
	     , sales
	     , freight
         , discount
	     , subtotal
	     , salesminusprofit
	     , suppid
	     , event_id
		 , trxid
	     , createdate
	)
	select orderid
	     , producttypeid
	     , product
	     , quantity
	     , price
	     , sales
	     , freight
         , discount
	     , subtotal
	     , salesminusprofit
	     , suppid
	     , event_id
		 , trxid
	     , createdate
	from
	(
	  select o.order_id as orderid
		 , pt.product_type_id as producttypeid
		 , pt.product_type_name as product
		 , od.quantity
		 , od.price
		 , round(od.quantity*od.price,2) as Sales
         , t.redeemed_amount as discount
		 , round(pt.fulfillment_charge,2) as Freight
		 , round(od.quantity*od.price-ISNULL(pt.fulfillment_charge,0)-ISNULL(t.redeemed_amount,0),2) as SubTotal
		 , round((od.quantity*od.price-ISNULL(pt.fulfillment_charge,0)-ISNULL(t.redeemed_amount,0))*(1-CAST(ppc.profit_percentage as decimal(5,2))/100),2) as salesminusprofit
          
		 , ep.event_participation_id as suppid
	     , ep.event_id
         , et.id AS trxid
	     , et.createdate
	   from qspecommerce.dbo.efundraisingtransaction et with(nolock)
		inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od with(nolock) on od.order_id = o.order_id
		inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
		inner join [event_group] eg with(nolock) on ep.event_id = eg.event_id
		inner join [group] g with(nolock) on eg.group_id = g.group_id
		inner join [partner] prt with(nolock) on g.partner_id = prt.partner_id
		inner join [partner_payment_config] ppc with(nolock) on prt.partner_id = ppc.partner_id
		INNER JOIN [QSPFulfillment].[dbo].[catalog_item_detail] as cid with(nolock) ON od.catalog_item_detail_id = cid.catalog_item_detail_id
		INNER JOIN [QSPFulfillment].[dbo].[catalog_item] as ci with(nolock) ON cid.catalog_item_id = ci.catalog_item_id
		INNER JOIN [QSPFulfillment].[dbo].[Product] p with(nolock) ON ci.product_id = p.Product_id
		INNER JOIN [QSPFulfillment].[dbo].[Product_Type] pt with(nolock) ON p.product_type_id = pt.Product_type_id
		INNER JOIN dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id	
        left join #temp t on od.order_id = t.order_id
		WHERE (o.create_date between  @start_date and @end_date or o.create_date is null)
	) t
	order by  createdate
 		, price


	create index ix_suppid on #tps (suppid)

    -- trouver toutes les 1rst subs (activations)
    UPDATE #tps
	SET act = 1
	FROM (
		SELECT ep.event_id, MIN(et.id) AS activation_trx_id, MIN(rownum) AS min_rownum
		FROM #tps
			INNER JOIN event_participation ep with(nolock)
				ON ep.event_id = #tps.event_id
			INNER JOIN qspecommerce..efundraisingtransaction et with(nolock)
				ON et.suppID = ep.event_participation_id
		GROUP BY ep.event_id
		) t
	WHERE trxid = t.activation_trx_id AND rownum = min_rownum

	-- header du rapport
	select 	--'-Partner Name'
		--, '+Request'
		 'Consultant'
		, '+Activation'
		, 'Product'
		, 'Currency'
		, 'Qty'
		, '$Total Amount'
		, '$Freight'
		, '$Discount'
		, '$Sub Total'
		, '$Sales Minus Profit'
	from #tps 
	where rownum = 1

	-- pre-calculate the result 
	create table #rpt (
		 consultant varchar(200)
		, activation int
		, product varchar(30)
		, currency varchar(30)
		, quantity int
		, total_amount float
		, total_freight float
        , total_discount float
		, total_subtotal float
		, total_salesminusprofit float
	)
	insert into #rpt 
	(	 consultant
		, activation
    		, product
		, currency
		, quantity
		, total_amount
		, total_freight
        , total_discount
		, total_subtotal
		, total_salesminusprofit
	)

	SELECT 	 con.name as consultant
		, SUM( tps1.act) AS activation
		, tps1.product as product
		, l.country_code as currency
		, SUM( tps1.quantity) AS quantity 
		, SUM( tps1.sales) AS total_amount
		, SUM( tps1.freight) AS total_freight
        , SUM( tps1.discount) AS total_discount
		, SUM( tps1.subtotal) AS total_subtotal
		, SUM( tps1.salesminusprofit) AS total_salesminusprofit
	FROM  event_participation ep with(nolock)
	 LEFT OUTER JOIN #tps tps1 on ep.event_participation_id = tps1.suppid
	INNER JOIN member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	INNER JOIN member m with(nolock) on m.member_id = mh.member_id
	INNER JOIN event_group eg with(nolock) on eg.event_id = ep.event_id
	INNER JOIN [group] g with(nolock) on g.group_id = eg.group_id
	INNER JOIN partner p with(nolock) on p.partner_id = g.partner_id
	 LEFT JOIN efundraisingprod.dbo.lead l with(nolock) on l.lead_id = g.lead_id
	 LEFT JOIN efundraisingprod.dbo.consultant con with(nolock) on con.consultant_id = l.consultant_id
	group by con.name, product,l.country_code

	-- cleanup invalid rows
	delete from #rpt 
	where   (activation = 0 or activation is null)
		and (quantity = 0 or quantity is null)
		and (total_amount = 0 or total_amount is null)

	-- return the detail of the report
	select consultant
		, activation
		, product
		, currency
		, quantity 
		, total_amount
		, total_freight
        , total_discount
		, total_subtotal
		, total_salesminusprofit
	from #rpt
	order by consultant

END
GO

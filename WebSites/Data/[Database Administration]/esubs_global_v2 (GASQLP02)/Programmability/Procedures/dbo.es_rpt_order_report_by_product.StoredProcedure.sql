USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_order_report_by_product]    Script Date: 02/14/2014 13:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

Description:    procedure qui affiche un rapport sommaire pour les partners

Ex: exec [es_rpt_order_report_by_product] '2010-06-01', '2010-06-30 23:59:59'


exec [es_rpt_order_report_by_product] '2010-01-01', '2010-12-31 23:59:59'
exec [es_rpt_order_report_by_product] '2010-01-01', '2010-03-31 23:59:59'
exec [es_rpt_order_report_by_product] '2010-04-01', '2010-06-30 23:59:59'
exec [es_rpt_order_report_by_product] '2010-07-01', '2010-09-30 23:59:59'
exec [es_rpt_order_report_by_product] '2010-10-01', '2010-12-31 23:59:59'

exec [es_rpt_order_report_by_product] '2009-01-01', '2009-12-31 23:59:59'
exec [es_rpt_order_report_by_product] '2009-01-01', '2009-03-31 23:59:59'
exec [es_rpt_order_report_by_product] '2009-04-01', '2009-06-30 23:59:59'
exec [es_rpt_order_report_by_product] '2009-07-01', '2009-09-30 23:59:59'
exec [es_rpt_order_report_by_product] '2009-10-01', '2009-12-31 23:59:59'

exec [es_rpt_order_report_by_product] '2008-01-01', '2008-12-31 23:59:59'
exec [es_rpt_order_report_by_product] '2008-01-01', '2008-03-31 23:59:59'
exec [es_rpt_order_report_by_product] '2008-04-01', '2008-06-30 23:59:59'
exec [es_rpt_order_report_by_product] '2008-07-01', '2008-09-30 23:59:59'
exec [es_rpt_order_report_by_product] '2008-10-01', '2008-12-31 23:59:59'



create mcote     	2010-07-01

         
*/
CREATE PROCEDURE [dbo].[es_rpt_order_report_by_product]
    @start_date as datetime 
	, @end_date as datetime
AS
BEGIN

    declare @end_date2 varchar(30)
    set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
    set @end_date = convert(datetime, @end_date2)


    -- pre-generate the tps
    CREATE TABLE #tps (
	    rownum int identity(1,1)
	    , act int default 0
	    , source_name varchar(30)
	    , product varchar(30)
	    , orderid int
	    , quantity int
	    , price money
	    , suppID int
	    , event_id int
	    , trxid	int 
        , orderdate datetime
    )

	INSERT INTO #tps (
	       orderid
	     , source_name
	     , product
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , trxid
	     , orderdate
	)
	select orderid
	     , source_name
	     , product
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , trxid
	     , orderdate
	from
	(
	select o.order_id as orderid
	     , s.source_name as source_name
	     , pt.product_type_name as product
	     , od.quantity
	     , od.price
	     , et.suppID
	     , ep.event_id
	     , et.id as trxid
             , o.order_date as orderdate
	from qspecommerce.dbo.efundraisingtransaction et
		INNER JOIN qspfulfillment.dbo.[order] o ON o.order_id = et.orderid
		INNER JOIN qspfulfillment.dbo.[order_detail] od ON od.order_id = o.order_id
		INNER JOIN event_participation ep ON ep.event_participation_id = et.suppid
		INNER JOIN [QSPFulfillment].[dbo].[source] s ON s.source_id = o.source_id
		INNER JOIN [QSPFulfillment].[dbo].[catalog_item_detail] as cid ON od.catalog_item_detail_id = cid.catalog_item_detail_id
		INNER JOIN [QSPFulfillment].[dbo].[catalog_item] as ci ON cid.catalog_item_id = ci.catalog_item_id
		INNER JOIN [QSPFulfillment].[dbo].[Product] p ON ci.product_id = p.Product_id
		INNER JOIN [QSPFulfillment].[dbo].[Product_Type] pt ON p.product_type_id = pt.Product_type_id
		INNER JOIN dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
    		where o.order_date between @start_date and @end_date 
	) t
	order by orderdate,product,price


    create index ix_suppid on #tps (suppid)
  
    -- trouver toutes les 1rst subs (activations)
	UPDATE #tps
	SET act = 1
	FROM (
		SELECT ep.event_id, MIN(et.id) AS activation_trx_id, MIN(rownum) AS min_rownum
		FROM #tps
			INNER JOIN event_participation ep
				ON ep.event_id = #tps.event_id
			INNER JOIN qspecommerce..efundraisingtransaction et
				ON et.suppID = ep.event_participation_id
		GROUP BY ep.event_id
		) t
	WHERE trxid = t.activation_trx_id AND rownum = min_rownum
	
	
    -- header du rapport
    select   '-Partner Name'
	    ,'Site'
        , 'Product'
		, 'orders'
	    , '+Activation'
	    , '+Qty'
	    , '$Total Amount'
    from #tps 
    where rownum = 1

    -- pre-calculate the result 
    create table #rpt (
	       source_name varchar(200)
	    , product varchar(30)
	    , orders int 
	    , activation int
		, subs int
	    , total_amount float
    )

    insert into #rpt 
    (	  source_name
    	, product
    	, orders
    	, activation
		, subs
    	, total_amount	
	)
    select tps.source_name as source_name
    	, tps.product
  	    , Count(distinct tps.orderid) as orders
		, SUM( tps.act) AS activation
	    , SUM( tps.quantity) AS subs
	    , SUM( tps.price*tps.quantity) AS total_amount
    from  event_participation ep 
        left outer join #tps tps on ep.event_participation_id = tps.suppid
        inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
        inner join member m on m.member_id = mh.member_id
        --left outer join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
        inner join event_group eg on eg.event_id = ep.event_id
        inner join [group] g on g.group_id = eg.group_id and  g.partner_id not in (8,58,143,741,809,762,753,784,766,787,764,806,752)	
        inner join partner p on p.partner_id = g.partner_id
    where (tps.orderdate between @start_date  and @end_date )
	and eg.event_id not in (1234960, 1235927)

    group by tps.source_name, tps.product



    delete from #rpt 
    where   (activation = 0 or activation is null)
	    and (subs = 0 or subs is null)
	    and (total_amount = 0 or total_amount is null)

    -- return the detail of the report
    select product
		, Sum(orders) as  orders
	    , sum(activation) as activation
	    , sum(subs) as subs
	    , sum(total_amount) as total_amount
    from #rpt
    group by product
    order by product

END
GO

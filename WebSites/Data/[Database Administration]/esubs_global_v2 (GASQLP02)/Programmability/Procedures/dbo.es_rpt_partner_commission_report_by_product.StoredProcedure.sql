USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_commission_report_by_product]    Script Date: 02/14/2014 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

Description:    procedure qui affiche un rapport sommaire pour les partners

Ex: exec es_rpt_partner_commission_report_by_product '2006-02-01', '2006-03-01'

mod DP     	2009-10-02
                Cloned from es_rpt_partner_commission_report
                to include new product, which resulted in changes to the activation logic and number of columns

*/
CREATE PROCEDURE [dbo].[es_rpt_partner_commission_report_by_product]
    @start_date as datetime 
	, @end_date as datetime
AS
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
    from qspfulfillment.dbo.order_detail od
    inner join        
        (SELECT  Order_ID, Redeemed_Amount AS Redeemed_Amount
	     FROM	qspfulfillment.dbo.fn_discount_redeemed_amount(null,null,null,null,12)) t
     on od.order_id = t.order_id



    declare @end_date2 varchar(30)
    set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
    set @end_date = convert(datetime, @end_date2)


    -- pre-generate the tps
    CREATE TABLE #tps (
	    rownum int identity(1,1)
	    , act int default 0
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
	     , product
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , trxid
	     , orderdate
	)
	select orderid
	     , product
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , trxid
	     , orderdate
	from
	(
		SELECT order_id AS orderid, 
			product_type_desc AS product, 
			quantity, 
			price - isnull(redeemed_amount,0) - isnull(freight,0) as price,
			supp_id AS suppid,
			event_id, 
			order_item_id AS trxid,
			create_date AS orderdate
		FROM dbo.es_get_valid_orders_items()
		where create_date between @start_date and @end_date 
	) t
	order by orderdate,product,price


    create index ix_suppid on #tps (suppid)
  
    -- trouver toutes les 1rst subs (activations)
	UPDATE #tps
	SET act = 1
	FROM (
		SELECT egvoi.event_id, MIN(egvoi.order_item_id) AS activation_trx_id
		FROM #tps tps
		INNER JOIN dbo.es_get_valid_orders_items() egvoi
			ON egvoi.event_id = tps.event_id
		GROUP BY egvoi.event_id
		) t
	WHERE trxid = t.activation_trx_id
	
	
    -- header du rapport
    select   '-Partner Name'
	    ,'Site'
        , 'Product'
	    , '+Activation'
	    , '+Qty'
	    , '$Total Amount'
    from #tps 
    where rownum = 1

    -- pre-calculate the result 
    create table #rpt (
	      partner_name varchar(200)
	    , product varchar(30)
	    , activation int
	    , subs int
	    , total_amount float
    )

    insert into #rpt 
    (	  partner_name
    	, product
    	, activation
    	, subs
    	, total_amount	
	)
    select p.partner_name
    	, tps.product
  	    , SUM( tps.act) AS activation
	    , SUM( tps.quantity) AS subs
	    , SUM( tps.price*tps.quantity) AS total_amount
    from  event_participation ep 
        left outer join #tps tps on ep.event_participation_id = tps.suppid
        inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
        inner join member m on m.member_id = mh.member_id
        --left outer join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
        inner join event_group eg on eg.event_id = ep.event_id
        inner join [group] g on g.group_id = eg.group_id
        inner join partner p on p.partner_id = g.partner_id
    where (tps.orderdate between @start_date  and @end_date )
    group by p.partner_name, tps.product

    create index ix_partner_name on #rpt (partner_name)


    delete from #rpt 
    where   (activation = 0 or activation is null)
	    and (subs = 0 or subs is null)
	    and (total_amount = 0 or total_amount is null)

    -- return the detail of the report
    select partner_name
	    , product
	    , sum(activation) as activation
	    , sum(subs) as subs
	    , sum(total_amount) as total_amount
    from #rpt
    group by partner_name,product
    order by partner_name,product

END
GO

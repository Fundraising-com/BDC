USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_campaign_detail_p]    Script Date: 02/14/2014 13:06:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_rpt_partner_campaign_detail_p]
    @start_date datetime,
    @end_date datetime,
    @partner_id int
AS
BEGIN

DECLARE @Counter INT

    -- pre-generate the tps
    create table #tps (
        rownum int identity(1,1)
        , act int
        , orderid int
        , quantity int
        , price money
        , suppID int        
        , event_id int
        , charge numeric(18,2)
        , updatedate datetime
    )


	INSERT INTO #tps (
	       act
	     , orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , charge
	     , updatedate
	)
	select vw.act
	, vw.order_id as orderid
        , vw.quantity
        , vw.price
        , vw.supp_ID
        , vw.event_id
        , COALESCE(vw.freight, 0) as charge  -- pt.fulfillment_charge is replaced by vw.freight
        , vw.create_date as updatedate       -- o.order_date is replaced by vw.create_date
	 from dbo.es_get_valid_orders_items() vw
	where create_date between @start_date and @end_date
	    order by create_date 

    create index ix_suppid on #tps (suppid)

    -- header du rapport
    select --'-Partner Name'
           '-Campaign ID'
           , 'Group Name'
           --, '+Request'
           , '+Activation'
           , '+Subs'
           , '$Total Amount'
           , '$Profit'
           --from #tps 
           --where rownum = 1

    -- pre-calculate the result 
    create table #rpt (
        partner_name varchar(200)
        , event_id int
        , group_name varchar(200)
        , request int
        , activation int
        , subs int
        , total_amount float
        , profit float
    )


    insert into #rpt 
    ( 
        partner_name
        , ep.event_id
        , g.group_name
        , request
        , activation
        , subs
        , total_amount
        , profit
    )
    select p.partner_name
         , ep.event_id
         , g.group_name
         , 0 AS request
         , SUM( tps.act) AS activation
         , SUM( tps.quantity) AS subs
         , SUM( tps.price) AS total_amount
         , sum(case 
            -- Fin de 100% Profit sur first subs
            when tps.act = 1 and tps.updatedate > '2006-05-16' then 
                quantity * (price - charge) * (Isnull(ppc.profit_percentage, 40.0)/100.0) 
            -- 100% premier 25$ 40% reste de l'order
            when tps.act = 1 and tps.updatedate > '2005-10-16' then 
                (case when quantity * price > 25 then (((quantity * price) - 25) * (Isnull(ppc.profit_percentage, 40.0)/100.0)) + 25
                else quantity * price end)
            -- 100% maximum 25$
            when tps.act = 1 and tps.updatedate < '2005-10-16' then 
                (case when quantity * price > 25 then 25
                else quantity * price end)
            else quantity * (price - charge) * (Isnull(ppc.profit_percentage, 40.0)/100.0) end) as profit
    from event_participation ep 
        left outer join #tps tps on ep.event_participation_id = tps.suppid
        inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
        inner join member m on m.member_id = mh.member_id
        --left outer join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
        inner join event_group eg on eg.event_id = ep.event_id
        inner join [group] g on g.group_id = eg.group_id
        inner join partner p on p.partner_id = g.partner_id
        left outer join partner_payment_config ppc on p.partner_id=ppc.partner_id
    where (tps.updatedate between @start_date and @end_date or tps.updatedate is null )
        and p.partner_id = @partner_id
    group by p.partner_name
           , ep.event_id
           , g.group_name

    create index ix_partner_name on #rpt (partner_name)

    delete from #rpt 
    where (request = 0 or request is null)
        and (activation = 0 or activation is null)
        and (subs = 0 or subs is null)
        and (total_amount = 0 or total_amount is null)

    -- return the detail of the report
    select --partner_name
        event_id
        , group_name
        --, sum(request) as request
        , sum(activation) as activation
        , sum(subs) as subs
        , sum(total_amount) as total_amount
        , sum(profit) as profit
    from #rpt
    group by --partner_name
             event_id
             , group_name
    order by event_id --partner_name
            , group_name

END
GO

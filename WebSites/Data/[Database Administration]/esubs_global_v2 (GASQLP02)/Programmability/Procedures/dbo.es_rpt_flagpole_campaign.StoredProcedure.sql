USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_flagpole_campaign]    Script Date: 02/14/2014 13:06:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Description: 
Ex: exec es_rpt_flagpole_campaign '2006-12-01', '2006-12-31'
*/
CREATE PROCEDURE [dbo].[es_rpt_flagpole_campaign]
	@start_date DATETIME,
	@end_date DATETIME
AS
BEGIN

    declare @end_date2 varchar(30)
    set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
    set @end_date = convert(datetime, @end_date2)

    -- pre-generate the tps
    create table #tps (
	    rownum int identity(1,1)
	    , act int
	    , orderid int
	    , quantity int
	    , price money
	    , suppID int
	    , event_id int
        , updatedate datetime
    )

	INSERT INTO #tps (
		orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , updatedate
	)
	SELECT o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
	     , ep.event_id
         , o.order_date as updatedate
	FROM qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
		INNER JOIN dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
	--where createdate between @start_date  and @end_date
	ORDER BY updatedate
 		   , od.price

    create index ix_suppid on #tps (suppid)

    -- update tout les activation a 0
    update #tps set act = 0
    -- trouver toutes les 1rst subs (activations)
    update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

    -- header du rapport
    select 	'-Partner Name'
	    , 'Lead ID'
	    , 'QSP Acct'
	    , 'Event ID'
	    , 'Group Name'
	    , '+Request'
	    , '+Activation'
	    , '+Subs'
	    , '$Total Amount'
	    , '$Profit'
    from #tps 
    where rownum = 1

    /*
    -- pre-calculate the result 
    create table #rpt (
	    partner_name varchar(200)
	    , lead_id int
	    , acct_no int
	    , event_id int
	    , group_name varchar(200)
	    , request int
	    , activation int
	    , subs int
	    , total_amount float
	    , profit float
    )
    insert into #rpt 
    (	partner_name
	    , lead_id
	    , acct_no
	    , event_id
	    , group_name
	    , request
	    , activation
	    , subs
	    , total_amount
	    , profit
    )*/

    select p.partner_name
	    , l.lead_id
	    , ps.account_no
	    , ep.event_id
	    , g.group_name
	    , 0 AS request
	    , SUM( tps.act) AS activation
	    , SUM( tps.quantity) AS subs
	    , SUM( tps.price) AS total_amount
	    , 0 as profit
    from event_participation ep 
        left outer join #tps tps on ep.event_participation_id = tps.suppid
        inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
        inner join member m on m.member_id = mh.member_id
        --left outer join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
        inner join event_group eg on eg.event_id = ep.event_id
        inner join [group] g on g.group_id = eg.group_id
        inner join partner p on p.partner_id = g.partner_id
        inner join efundraisingprod..lead l on l.lead_id = g.lead_id
        inner join efundraisingprod..crm_static_past3seasons_new ps on l.matching_code = ps.qsp_account_matching_code
    where (tps.updatedate between @start_date  and @end_date or tps.updatedate is null )
    group by p.partner_name
	    , l.lead_id
	    , ps.account_no
	    , ep.event_id
	    , g.group_name

    /*
    create index ix_partner_name on #rpt (partner_name)

    delete from #rpt 
    where  
	    (request = 0 or request is null)
	    and (activation = 0 or activation is null)
	    and (subs = 0 or subs is null)
	    and (total_amount = 0 or total_amount is null)

    -- return the detail of the report
    select partner_name
	    , lead_id
	    , acct_no
	    , event_id
	    , group_name
	    , sum(request) as request
	    , sum(activation) as activation
	    , sum(subs) as subs
	    , sum(total_amount) as total_amount
	    , sum(profit) as profit
    from #rpt

    group by partner_name
	    , lead_id
	    , acct_no
	    , event_id
	    , group_name
    order by partner_name
	    , lead_id
	    , acct_no
	    , event_id
	    , group_name

    */
END
GO

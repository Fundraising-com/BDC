USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_commission_report_active]    Script Date: 02/14/2014 13:06:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
    Description:
    
    Ex: 
    exec es_rpt_partner_commission_report_p '07-01-2006', '08-01-2006', 143
    exec es_rpt_partner_commission_report_p '07-01-2006', '07-31-2006 23:59:59', 143
    select * from partner where partner_name like '%kappa%'

    mod pgirard     2006-12-21
                    changé createdate pour updatedate

    mod pgirard     2007-01-16
                    Ajouté le creation channel id 33

*/
CREATE PROCEDURE [dbo].[es_rpt_partner_commission_report_active]
		@start_date as datetime 
		, @end_date as datetime
		, @partner_id as int
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
        , createdate datetime,updatedate datetime
)
	INSERT INTO #tps (
		orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate,updatedate
	)
	select orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate,updatedate
	from
	(
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
	     , ep.event_id
             , et.createdate
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
	where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701 )
	union all
	select oh.ID as orderid
	     , sod.quantity
	     , cid.value as price
	     , et.suppID
	     , ep.event_id
	     , et.createdate,et.updatedate
 	from qspecommerce.dbo.efundraisingtransaction et 
		inner join qspstore.dbo.orderheader oh on oh.id = et.orderid
		inner join qspstore.dbo.suborderheader soh on oh.id = soh.orderheaderid 
		inner join qspstore.dbo.suborderdetail sod on soh.id = sod.suborderheaderid
		inner join qspstore.dbo.catalogitemdetail cid on cid.id = sod.catalogitemdetailid
		inner join event_participation ep on ep.event_participation_id = et.suppid
	WHERE oh.OrderTotal IS NOT NULL
	  AND oh.OrderStatusID NOT IN (1, 10, 11)
	  AND soh.ShipToAddressID <> 0
	  and oh.aggregatorid in (7,13)
	) t
	order by  createdate
 		, price


create index ix_suppid on #tps (suppid)

-- update tout les activation a 0
update #tps set act = 0
-- trouver toutes les 1rst subs (activations)
update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

-- header du rapport
select 	'-Partner Name'
	, '+Request'
	, '+Activation'
	, '+Subs'
	, '$Total Amount'
from #tps 
where rownum = 1

select 	e.event_name
	,(case 
		-- sponsor order must be under his name
		when (mp.first_name + ' ' + mp.last_name) is null 
			then (m.first_name + ' ' + m.last_name) 
		-- participant orders must be under his name
		when cc.member_type_id = 2
			then (m.first_name + ' ' + m.last_name)
		else (mp.first_name + ' ' + mp.last_name)
		end)  as member_name
	,count ( distinct case when mh.creation_channel_id in(12,14,29,33)
		then m.member_id else NULL end ) as email_sent
	,sum(quantity) as nb_subs
	,sum(quantity * price) as amount
	,sum(case 
		-- Fin de 100% Profit sur first subs
		when tps.act = 1 and createdate > '2006-05-16' then 
			quantity * price * .4 
		-- 100% premier 25$ 40% reste de l'order
		when tps.act = 1 and createdate > '2005-10-16' then 
			(case when quantity * price > 25  then (((quantity * price) - 25) * 0.4) + 25
			else quantity * price end)
		-- 100% maximum 25$
		when tps.act = 1 and createdate < '2005-10-16' then 
			(case when quantity * price > 25  then 25
			else quantity * price end)
		else quantity * price *(Isnull(ppc.profit_percentage, 40.0)/100.0) end) as profit
from event_participation ep
	inner join event e on e.event_id = ep.event_id and e.active = 1
	inner join event_group eg on eg.event_id = e.event_id 
	inner join [group] g on g.group_id = eg.group_id  and g.partner_id = @partner_id
	--inner join partner p on p.partner_id = g.partner_id
	-- order
        left outer join #tps tps on tps.suppid = ep.event_participation_id
	-- enfant
	inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	inner join member m on m.member_id = mh.member_id
	-- parent
	left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	left outer join member mp on mp.member_id = mhp.member_id
	left join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
             -- profit percentage according to partner
          left join partner_payment_config ppc on ppc.partner_id = g.partner_id
where 
(
(tps.createdate between @start_date  and @end_date) OR  (tps.updatedate between @start_date  and @end_date)
)
group by e.event_name
	,(case 
		-- sponsor order must be under his name
		when (mp.first_name + ' ' + mp.last_name) is null 
			then (m.first_name + ' ' + m.last_name) 
		-- participant orders must be under his name
		when cc.member_type_id = 2
			then (m.first_name + ' ' + m.last_name)
		else (mp.first_name + ' ' + mp.last_name)
		end) 
order by 1, 2
END
GO

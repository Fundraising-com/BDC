USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_sponsors_registered_dynamic]    Script Date: 02/14/2014 13:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
* Create by		: melissa cote
* Create date	: 2009-05-06 
*
* Description	: Registered Sponsors Report - Dynamic
*				(Sponsors that complete Step 1)
*
*				Definition:
*				Activation = At least 1 sub sold + 12 emails entered
*				KO = At least 1 participant invited from sponsor KO page
*				Part from KO = # of participants invited from sponsor KO page
*				KO Import = Import was used in sponsor KO page
*				Import = # of participants invited using Import in sponsor KO page
*				Manual = # of participants invited using Manual Entry in sponsor KO page
*				Part = # of participants total ** + Mel
*				Part Inv Sponsor = # of participants invited from sponsor KO page + CM
*				Active Part = # of participants that send at least 1 email or buy 1 sub
*				Part Send Emails = # of participants that send emails
*				Part Buys = # of participants that buy a sub
*				Supp = # of supporters invited by sponsor
*				Supp Buys = # of supporters from sponsor that buy a sub
*				Total Supp = # of supporters for the group (from sponsor + participant)
*				Subs = Total # of subs generated for the sponsor
*				Amount = Total $ amount generated for the sponsor
*
*		exec [es_rpt_sponsors_registered_dynamic] '2007-01-01', '2007-03-01'
*
*/
CREATE  PROCEDURE [dbo].[es_rpt_sponsors_registered_dynamic]
	@start_date datetime,
	@end_date datetime
AS
BEGIN

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
    select o.order_id as orderid
        , od.quantity
        , od.price
        , et.suppID
        , ep.event_id
        , o.order_date as updatedate
    from qspecommerce.dbo.efundraisingtransaction et with(nolock) 
        inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid
        inner join qspfulfillment.dbo.[order_detail] od with(nolock) on od.order_id = o.order_id
        inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
		inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id

    --where updatedate between @start_date and @end_date
    order by updatedate
           , od.price

    create index ix_suppid on #tps (suppid)

    -- update tout les activation a 0
    update #tps set act = 0
    -- trouver toutes les 1rst subs (activations)
    update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

	select 
		'Year'
		,'Month'
		,'Event ID'
		,'Partner Name'
		,'Group Type'
		,'Activation'
		,'KO'
		,'Part from KO'
		,'Manual'
		,'Import'
		,'KO Import'
		,'Part'
		,'Part Inv by Sponsor'
		,'Active Part'
		,'Part Send @'
		,'Part Buys'
		,'Supp'
		,'Supp Buys'
		,'Total Supp'
		,'Subs'
		,'Amount'
	 	
    select 
		 year (e.create_date) as year_e
		 , month (e.create_date) as month_e
		 , e.event_id
		 , p.partner_name 
		 , gt.description as group_type
		 , (case when count(tps.act) <> 0 and count(distinct case when mh.creation_channel_id in(7,8,9,10,17,18,20,22,23,32,35,36,38,40) then mh.member_hierarchy_id else null end) >= 12 then 1 else 0 end) AS activations
		 , count(distinct ko.event_id) as ko
		 , count(distinct case when mh.creation_channel_id in(7,20,23,38) 
				and DATEPART(year, ep.create_date) = DATEPART(year,e.create_Date) 
				AND DATEPART(dayofyear, ep.create_Date) = DATEPART(dayofyear, e.create_Date)
				and ko.event_id is not null
				then mh.member_id else null end) as nb_part_ko
		 , count(distinct case when mh.creation_channel_id in(7) 
				and DATEPART(year, ep.create_date) = DATEPART(year,e.create_Date) 
				AND DATEPART(dayofyear, ep.create_Date) = DATEPART(dayofyear, e.create_Date)
				and ko.event_id is not null
				then mh.member_id else null end) as nb_part_manual
	     , count(distinct case when mh.creation_channel_id in(23,38) 
				and DATEPART(year, ep.create_date) = DATEPART(year,e.create_Date) 
				AND DATEPART(dayofyear, ep.create_Date) = DATEPART(dayofyear, e.create_Date)
				and ko.event_id is not null
				then mh.member_id else null end) as nb_part_import
		, count(distinct case when mh.creation_channel_id in(23,38) 
				and DATEPART(year, ep.create_date) = DATEPART(year,e.create_Date) 
				AND DATEPART(dayofyear, ep.create_Date) = DATEPART(dayofyear, e.create_Date)
				and ko.event_id is not null
				then e.event_id else null end) as  nb_ko_import
		 , count(distinct case when mh.creation_channel_id in(7,8,9,10,17,18,20,22,23,32,35,36,38,40, 41) then mh.member_hierarchy_id else null end) as nb_part
		 , count(distinct case when mh.creation_channel_id in(7,20,23,35,36) then mh.member_hierarchy_id else null end) as nb_part_sp
		 , coalesce(act_part, 0) as act_part
		 , coalesce(send_part, 0) as send_part
		 , coalesce(buy_part, 0) as buy_part
	     --select * from creation_channel where member_type_id = 3
		 , count(distinct case when mh.creation_channel_id in(12,14,29,33,35) and mh.parent_member_hierarchy_id = g.sponsor_id then mh.member_id else null end) as nb_supp
		 , count(distinct case when mh.creation_channel_id in(12,14,29,33,35) and mh.parent_member_hierarchy_id = g.sponsor_id and tps.suppid is not null then mh.member_id else null end)as buy_supp
		 , count(distinct case when mh.creation_channel_id in(12,14,29,33,35) then mh.member_id else null end) as total_supp
		 , SUM( case when  mh.creation_channel_id in(7,20,23,35,36,12,14,29,33,35) and mh.parent_member_hierarchy_id = g.sponsor_id then coalesce(tps.quantity,0) else 0 end) AS sp_subs
         , SUM( case when  mh.creation_channel_id in(7,20,23,35,36,12,14,29,33,35) and mh.parent_member_hierarchy_id = g.sponsor_id then coalesce(tps.price,0) else 0 end ) AS sp_total_amount
		 , SUM( coalesce(tps.quantity,0)) AS subs
         , SUM( coalesce(tps.price,0)) AS total_amount


    from event_participation ep with(nolock) 
		inner join event e with(nolock) on e.event_id = ep.event_id 
        left outer join #tps tps on ep.event_participation_id = tps.suppid
        -- enfant
	    inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
        inner join member m with(nolock) on m.member_id = mh.member_id
		-- parent
	    left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp with(nolock) on mp.member_id = mhp.member_id
        --left outer join creation_channel cc with(nolock) on cc.creation_channel_id = mh.creation_channel_id
        inner join event_group eg with(nolock) on eg.event_id = ep.event_id
        inner join [group] g with(nolock) on g.group_id = eg.group_id
		inner join group_type gt with(nolock) on e.group_type_id = gt.group_type_id 
        inner join partner p with(nolock) on p.partner_id = g.partner_id
		left join (
				select 	event_id, count(event_participation_id) as nb_part, sum(act_part) as act_part, sum(send_part) as send_part, sum(buy_part) as buy_part 
				from (
					SELECT ep2.event_id
						, ep2.event_participation_id 
						, (case when mhk.parent_member_hierarchy_id is not null or tps.quantity is not null then 1 else 0 end) as act_part 
						, (case when mhk.parent_member_hierarchy_id is not null then 1 else 0 end) as send_part 
						, (case when tps.quantity is not null then 1 else 0 end) as buy_part
					from event_participation ep2 with(nolock) 
						inner join event e with(nolock) on e.event_id = ep2.event_id 
						inner join member_hierarchy mh with(nolock) on ep2.member_hierarchy_id = mh.member_hierarchy_id
						inner join creation_channel cc with(nolock) 
							on cc.creation_channel_id = mh.creation_channel_id 
							and cc.member_type_id=2 
							and cc.creation_channel_id in(7,20,23,38) 
						left outer join member_hierarchy mhk with(nolock) on mh.member_hierarchy_id = mhk.parent_member_hierarchy_id
						left outer join #tps tps on ep2.event_participation_id = tps.suppid 
						where DATEPART(year, ep2.create_date) = DATEPART(year,e.create_Date) 
							AND DATEPART(dayofyear, ep2.create_Date) = DATEPART(dayofyear, e.create_Date)
					group by  ep2.event_id
						, ep2.event_participation_id 
						, (case when mhk.parent_member_hierarchy_id is not null or tps.quantity is not null then 1 else 0 end) 
						, (case when mhk.parent_member_hierarchy_id is not null then 1 else 0 end)
						, (case when tps.quantity is not null then 1 else 0 end)
				) t --where nb_part <=1000 
				group by t.event_id
				) ko on ko.event_id = ep.event_id
				
	where (tps.updatedate between @start_date and @end_date or tps.updatedate is null )
		and e.create_date between @start_date and @end_date 
		and p.partner_id not in (143, 605, 719, 58, 753) --, 8, 752)
		-- without Kappa Delta, Alpha Omicron Pi, QSP, LLU
		and e.redirect is not null
		and e.culture_code = 'en-US'
		
	group by 
		 year (e.create_date) 
		 , month (e.create_date) 
		 , e.event_id
		 , p.partner_name 
		 , gt.description 
		 , act_part
		 , send_part
		 , buy_part
	     
		 --, (case when tps.act <> 0 and count(distinct case when mh.creation_channel_id in(7,8,9,10,17,18,20,22,23,32,35,36,38,40) then mh.member_hierarchy_id else null end) >= 12 then 1 else 0 end) 

END
GO

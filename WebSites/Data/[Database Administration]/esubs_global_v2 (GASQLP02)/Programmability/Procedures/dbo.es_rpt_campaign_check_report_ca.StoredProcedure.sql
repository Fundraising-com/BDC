USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_campaign_check_report_ca]    Script Date: 09/03/2014 13:28:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Description:    check report dans le campaign manager

Ex: EXEC es_rpt_campaign_check_report 11101112

mod mcote 	    2006-02-18 	
                - creation de table temporaire (optimisation)
				- obtention du 100% profit 1rst sub
				- modif relation parent enfant pour reporting
				  la modif consiste a donner le credit des ventes 
				  aux participants meme si ces dernier sont enfant 
				  du sponsor.
				- enlever le select imbriquer.

mod krystian	2006-03-03
                - modifie la selection pour les tables des headers

mod pgirard     2006-12-13
                - change le create_date pour le update_date

mod dnghiem 2007-10-01
                - Using end_date in payment period for payment period report.

mod dnghiem 2007-10-05
                - Recalculate profit percentage based on partner_payment_config.
    EXEC [dbo].[es_rpt_campaign_check_report_ca] 1472160
*/
ALTER PROCEDURE [dbo].[es_rpt_campaign_check_report_ca]
	@event_id int
AS
BEGIN   
    -- pre-generate the tps
    CREATE TABLE #tps (
	    rownum int identity(1,1)
	    , orderid int
	    , quantity int
	    , price money
        , amountNoTax money
	    , suppID int
        , updatedate datetime
    )

	INSERT INTO #tps (
		orderid
	    , quantity
	    , price
        , amountNoTax
	    , suppID
	    , updatedate
	)
	select tps.order_id as orderid
	     , 1 as quantity
	     , tps.price
         , tps.price - tps.tax - tps.freight - tps.other_fees as amountNoTax
         , tps.supp_id as suppID
         , tps.create_date as updatedate
    from  event_participation ep (nolock)
    join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
    where ep.event_id = @event_id

    create index ix_suppid on #tps (suppid)

    select  'Member Name', 'Supporter Name', '+Subscriptions', '$Amount Purchased', '$Amount Purchased Before Tax', '$Profit', 'Purchase Date'

    -- supporters orders 
    select 
	    (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then (m.first_name + ' ' + m.last_name) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then (m.first_name + ' ' + m.last_name)
		    else (mp.first_name + ' ' + mp.last_name)
		    end)  as part_name
	    , m.first_name + ' ' + m.last_name as supp_name
	    , quantity as nb_subs
	    , quantity * price as amount
        , quantity * amountNoTax as amount_gross
	    , quantity * amountNoTax*(Isnull(ppc.profit_percentage, 37.0)/100.0) as profit
	    , convert(varchar(10), (updatedate), 121) as updatedate
    from event_participation ep (nolock)
	    join event_group eg (nolock) on eg.event_id = ep.event_id 
	    join [group] g (nolock) on g.group_id = eg.group_id 
	    -- profit
	    join partner prt (nolock)
	      on prt.partner_id = g.partner_id
	    left join partner_payment_config ppc (nolock)
	      on ppc.partner_id = prt.partner_id
	    -- order
        join #tps tps on tps.suppid = ep.event_participation_id
	    -- enfant
	    join member_hierarchy mh (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id 
	    join member m (nolock) on m.member_id = mh.member_id
	    -- parent
	    left join member_hierarchy mhp (nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left join member mp (nolock) on mp.member_id = mhp.member_id
	    join creation_channel cc (nolock) on cc.creation_channel_id = mh.creation_channel_id
    where ep.event_id = @event_id
	 -- and mh.active = 1
    order by updatedate

    select top 1 'Check Number', 'Check Period', '$Check Amount'

    select p.cheque_number
--	    , (case when day(p.cheque_date) >= 15 then cast(month(p.cheque_date) as varchar(10)) + '/15/' + cast(year(p.cheque_date) as varchar(10))
--		    else  CAST((month(p.cheque_date) - 1) as varchar(10)) + '/15/' + cast(year(p.cheque_date) as varchar(10)) end) as check_date
		, Convert(varchar(10),Isnull(pp.end_date, p.cheque_date), 101)  as check_date
	    , p.paid_amount
    from payment_info pin (nolock)
        join payment p (nolock) on pin.payment_info_id = p.payment_info_id
	left join
	(
		select pps.payment_id, pps.payment_status_id
		from payment_payment_status pps
	       join
	       (
		select payment_id, max(create_date) as create_date
		from payment_payment_status (nolock)
		group by payment_id
	       ) ppsNew on ppsNew.payment_id = pps.payment_id and ppsNew.create_date = pps.create_date 
	) pcancel
	on pcancel.payment_id = p.payment_id
        left join payment_period pp (nolock)
             on pp.payment_period_id = p.payment_period_id
    where (event_id = @event_id and isnull(pcancel.payment_status_id, 2) <> 9)
    order by Isnull(pp.end_date, p.cheque_date)

END


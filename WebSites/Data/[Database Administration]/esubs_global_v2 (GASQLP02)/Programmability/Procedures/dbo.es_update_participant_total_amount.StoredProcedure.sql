USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_participant_total_amount]    Script Date: 12/10/2014 11:10:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
CREATE BY : Jason Warren M.H.
CREATE DATE : July 21, 2011

UPDATED BY Melissa COTE 
UPDATE DATE Sept 19, 2011 
DESCRIPTION : A job is scheduled to run this proc everyday for the purpose of improving the performance of
			  the 'Find My Participant' search. 
exec es_update_participant_total_amount
exec es_update_event_total_amount
select top 100 * from participant_total_amount order by 1 desc
*/
ALTER PROCEDURE [dbo].[es_update_participant_total_amount]
AS
BEGIN

    /* 1) Create TEMP table and load all the data */
	CREATE TABLE #temp (
	    rownum int identity(1,1)
	    , event_participation_id int
	    , participant_name varchar(201)
    )	

	insert into #temp (event_participation_id, participant_name)
	SELECT  event_participation_id, 
	        CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
			ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
			END as participant_name
	from member m with (nolock)
	join [users] u with (nolock) on m.user_id = u.user_id
	join member_hierarchy mh with (nolock) on mh.member_id = m.member_id
    join creation_channel cc with (nolock) on mh.creation_channel_id = cc.creation_channel_id 
	join event_participation ep with (nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
	join [event] e with (nolock) on  e.event_id = ep.event_id 
	where e.active = 1 and e.displayable = 1 and cc.member_type_id = 2 
        AND m.password is not null
		AND mh.active = 1
		AND m.deleted = 0 

	delete from participant_total_amount;
	
	--print('1');

	--DBCC CHECKIDENT('participant_total_amount', RESEED, 0);
	--print('2');

--select * from participant_total_amount

	insert into participant_total_amount (
			event_participation_id
			,participant_name
			,items
			,total_amount
			,total_supporters
			,total_donation_amount
			,total_donors
			,total_profit
			,create_date)
	select coalesce(a.event_participation_id, 0)
		, coalesce(t.participant_name, '')
		, coalesce(a.items, 0)
		, coalesce(a.total_amount, 0)
		, coalesce(a.total_supporters, 0)
		, coalesce(a.total_donation_amount, 0)
		, coalesce(a.total_donars, 0)
		, coalesce(a.total_profit, 0)
		, getdate()

	from #temp t INNER JOIN 
	( 
		SELECT 
			(case WHEN child.member_type_id = 3 and parent.event_participation_id is not null then parent.event_participation_id
					else  ep.event_participation_id end) as event_participation_id
			
			, sum(coalesce(qspstats.quantity, 0)) as items
			, sum(coalesce(qspstats.sub_total, 0)) AS total_amount
			, 0 /*count( distinct case when et.suppid is not null 
							  then  m.member_id else NULL end )*/ total_supporters
			, coalesce(sum(efrcstats.quantity * efrcstats.price), 0) AS total_donation_amount
			, 0/*count( distinct case when efro.ext_participation_id is not null 
							  then  m.member_id else NULL end ) */total_donars
			, COALESCE(sum(case 
				-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
				when qspstats.product_type_id = 18 and qspstats.store_id = 1 then
					(case when qspstats.create_date < '2011-04-01' then 
						sub_total * 93.5/100.0
					else
						sub_total * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
				
				when qspstats.item_type_id = 6 and qspstats.store_id = 10 then -- Personalize Products on GA store only are 25% profit
					qspstats.sub_total * 25.0/100.0
				else
				-- For all other product percent profit use event profit calculated field (January 6, 2011)
					sub_total * Isnull(e.profit_calculated, 40.0)/100.0 end),0) as total_profit
		FROM [event_participation] ep WITH (NOLOCK)	
			--INNER JOIN [event_group] eg WITH (NOLOCK) ON eg.event_id = ep.event_id
			INNER JOIN [event] e WITH (NOLOCK) ON e.event_id = ep.event_id
			--INNER JOIN [group] g WITH (NOLOCK) ON g.group_id = eg.group_id
			
			-- current childrens
			JOIN
			(
				SELECT mh.member_hierarchy_id, mh.parent_member_hierarchy_id, cc.member_type_id FROM [member_hierarchy] mh WITH (NOLOCK)
				  JOIN [member] m WITH (NOLOCK) ON m.member_id = mh.member_id
				  JOIN [creation_channel] cc WITH (NOLOCK) ON cc.creation_channel_id = mh.creation_channel_id
				  JOIN [event_participation] ep WITH (NOLOCK) ON mh.member_hierarchy_id = ep.member_hierarchy_id
				  JOIN event e WITH (NOLOCK) ON ep.event_id = e.event_id and e.active = 1
				 WHERE mh.active = 1
			) child ON child.member_hierarchy_id = ep.member_hierarchy_id
			-- current parents
			LEFT JOIN
			(
				SELECT mhp.member_hierarchy_id, epp.event_participation_id FROM [member_hierarchy] mhp WITH (NOLOCK) 
				  JOIN [member] mp WITH (NOLOCK) ON mp.member_id = mhp.member_id
				  JOIN [event_participation] epp WITH (NOLOCK) ON mhp.member_hierarchy_id = epp.member_hierarchy_id
				  JOIN event e WITH (NOLOCK) ON epp.event_id = e.event_id and e.active = 1
				WHERE mhp.active = 1
			) parent ON parent.member_hierarchy_id = child.parent_member_hierarchy_id
	       
	        -- QSP orders from qspecommerce db:
			left join [dbo].[es_get_valid_orders_items]() qspstats on qspstats.supp_id = ep.event_participation_id
			
			-- Get donation profit from efrcommon.dbo.profit
			left join efrcommon.dbo.profit donation_profit with (nolock) 
			on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11 -- donation ptid =  18

			-- EFR Donation orders from EFRECommerce db:
			left join (
				select efreo.ext_participation_id, efreod.quantity, efreod.price
				from EFRECommerce.dbo.[order] efreo WITH (NOLOCK)
				join EFRECommerce.dbo.order_detail efreod WITH (NOLOCK) ON efreo.order_id = efreod.order_id AND efreod.deleted = 0 AND efreod.product_id = 1
				join dbo.es_get_valid_efrecommerce_order_status() efreos ON efreo.status_id = efreos.status_id
				where efreo.deleted = 0 AND efreo.source_id = 1
			) efrcstats on efrcstats.ext_participation_id = ep.event_participation_id
	   GROUP BY (case WHEN child.member_type_id = 3 and parent.event_participation_id is not null then parent.event_participation_id
					else ep.event_participation_id end)
	) a on a.event_participation_id = t.event_participation_id 


	--print('3');

END

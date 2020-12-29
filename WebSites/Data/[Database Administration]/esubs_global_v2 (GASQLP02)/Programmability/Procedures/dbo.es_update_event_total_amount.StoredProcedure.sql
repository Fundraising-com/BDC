USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_event_total_amount]    Script Date: 02/14/2014 13:07:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
CREATE BY : Melissa Cote (Edited by Jiro Hidaka)
CREATE DATE : July 21, 2011
DESCRIPTION : A job is scheduled to run this proc everyday for the purpose of improving the performance of
			  the 'Find My Group' search. If the total amount raised per event is saved in a table then the
              application doesn't need to run a separate proc to do this calculation for every event that's
			  returned from the search result.

exec es_update_event_total_amount

select top 1000 * from event_total_amount order by 1 desc
*/
CREATE PROCEDURE [dbo].[es_update_event_total_amount]
AS
BEGIN

    /* 1) Create TEMP table and load all the data */
	CREATE TABLE #temp (
	    rownum int identity(1,1)
	    , event_id int
	    , items int
	    , total_amount money
	    , total_supporters int
            , total_donars int
	    , total_donation_amount money
	    , total_profit money
    )	

	--print('Start 1: ' + CAST(GETDATE() as char(30)));

	insert into #temp (event_id, items, total_amount, total_supporters, total_donation_amount, total_donars, total_profit)
	SELECT	e.event_id
		, sum(coalesce(qspstats.quantity, 0)) as items
		, sum(coalesce(qspstats.sub_total, 0)) AS total_amount
        , count( distinct case when qspstats.supp_id is not null 
				          then  m.member_id else NULL end ) total_supporters
    	, ISNULL(sum(case 
			when qspstats.product_type_id IN (18,999) and qspstats.store_id = 1 then qspstats.sub_total
			else 0 end),0) as total_donation_amount
        , coalesce(sum( case WHEN qspstats.product_type_id IN (18,999)  and qspstats.store_id = 1 THEN 1 ELSE 0 END),0) total_donars
		, coalesce(sum(case 
			-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
			when qspstats.product_type_id IN (18,999) and qspstats.store_id = 1 then
				(case when qspstats.create_date < '2011-04-01' then 
					sub_total * 93.5/100.0
				else
					sub_total * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
			when qspstats.item_type_id = 6 and qspstats.store_id = 10 then -- Personalize Products on GA store only are 25% profit
				qspstats.sub_total * 25.0/100.0
			
			else
			-- For all other product percent profit use event profit calculated field (January 6, 2011)
				sub_total * Isnull(e.profit_calculated, 40.0)/100.0 end),0) as total_profit
	from [event] e
		left join event_participation ep WITH (NOLOCK) on ep.event_id = e.event_id 
		-- QSP orders from qspecommerce db:
		left join [dbo].[es_get_valid_orders_items]() qspstats on qspstats.supp_id = ep.event_participation_id
		-- Total Number of Supporters:
		left join member_hierarchy mh WITH (NOLOCK) on mh.member_hierarchy_id = ep.member_hierarchy_id
	    left join member m WITH (NOLOCK) on m.member_id = mh.member_id
	    -- Get donation profit from efrcommon.dbo.profit
		left join efrcommon.dbo.profit donation_profit with (nolock) 
			on e.profit_group_id = donation_profit.profit_group_id AND qsp_catalog_type_id = 11
	--where e.event_id = @event_id 
	group by e.event_id;
    
	--print('End 1: ' + CAST(GETDATE() as char(30)));
	

	--print('Start 2: ' + CAST(GETDATE() as char(30)));

	/* 2) Delete all data in 'event_total_amount' table, reseed and reload the data from TEMP */
	delete from event_total_amount;
	
	DBCC CHECKIDENT('event_total_amount', RESEED, 0);
	
	insert into event_total_amount (event_id, items, total_amount, total_supporters, total_donars, total_donation_amount, total_profit, create_date)
	select	  event_id
			, items
			, total_amount
			, total_supporters
            , total_donars
			, total_donation_amount
			, total_profit
			, GETDATE()
	from #temp;

	--print('End 2: ' + CAST(GETDATE() as char(30)));

END
GO

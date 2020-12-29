USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_event_total_amount_by_event_id]    Script Date: 02/14/2014 13:07:52 ******/
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

select * from event_total_amount
*/
CREATE PROCEDURE [dbo].[es_update_event_total_amount_by_event_id]
@event_id int 
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
    )	

	--print('Start 1: ' + CAST(GETDATE() as char(30)));

	insert into #temp (event_id, items, total_amount, total_supporters, total_donation_amount, total_donars)
	SELECT	e.event_id
		, sum(coalesce(od.quantity, 0)) as items
		, sum(coalesce(od.quantity, 0) * coalesce(od.price, 0)) AS total_amount
        , count( distinct case when et.suppid is not null 
				          then  m.member_id else NULL end ) total_supporters
        , coalesce(sum(efreod.quantity * efreod.price), 0) AS total_donation_amount
        , count( distinct case when efreo.ext_participation_id is not null 
				          then  m.member_id else NULL end ) total_donars        
	from [event] e
		left join event_participation ep on ep.event_id = e.event_id 
		left join qspecommerce.dbo.efundraisingtransaction et on et.suppid = ep.event_participation_id
		left join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		left join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		left join dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id

		-- Total Number of Supporters:
		left join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    left join member m on m.member_id = mh.member_id

		-- Donation orders from EFRECommerce db:
		left join EFRECommerce.dbo.[order] efreo WITH (NOLOCK) ON efreo.ext_participation_id = ep.event_participation_id AND efreo.deleted = 0 AND efreo.source_id = 1
        	left join EFRECommerce.dbo.order_detail efreod WITH (NOLOCK) ON efreo.order_id = efreod.order_id AND efreod.deleted = 0 AND efreod.product_id = 1
		join dbo.es_get_valid_efrecommerce_order_status() efreos ON efreo.status_id = efreos.status_id

	where e.event_id = @event_id
	group by e.event_id;
    
	--print('End 1: ' + CAST(GETDATE() as char(30)));
	

	--print('Start 2: ' + CAST(GETDATE() as char(30)));

	/* 2) Delete all data in 'event_total_amount' table, reseed and reload the data from TEMP */
	delete from event_total_amount;
	
	DBCC CHECKIDENT('event_total_amount', RESEED, 0);
	
	insert into event_total_amount (event_id, items, total_amount, total_supporters, total_donars, total_donation_amount, create_date)
	select	  event_id
			, items
			, total_amount
			, total_supporters
            , total_donars
			, total_donation_amount
			, GETDATE()
	from #temp;

	--print('End 2: ' + CAST(GETDATE() as char(30)));

END
GO

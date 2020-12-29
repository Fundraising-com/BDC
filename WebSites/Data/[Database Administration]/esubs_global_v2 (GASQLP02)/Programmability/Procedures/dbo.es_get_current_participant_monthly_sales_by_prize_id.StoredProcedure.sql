USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_current_participant_monthly_sales_by_prize_id]    Script Date: 02/14/2014 13:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:		Jiro Hidaka
-- Create date: November 8 2010
-- Description:	Gets the current monthly sales for this participant
-- ================================================================
CREATE PROCEDURE [dbo].[es_get_current_participant_monthly_sales_by_prize_id] 
	@prize_id int, 
    @event_participation_id int 
AS
BEGIN

    -- get the time range for that prize

	declare @start_date datetime
	declare @end_date datetime

	select top 1 @end_date = expiration_date, @start_date = create_date
	from prize_item
	where prize_id = @prize_id
	and expiration_date > getdate()

	 
	-- pre-generate the tps
	CREATE TABLE #tps (
		rownum int identity(1,1)
		, orderid int
		, quantity int
		, price money
		, suppID int
		, updatedate datetime
	)

	INSERT INTO #tps (
		orderid
		, quantity
		, price
		, suppID
		, updatedate
	)
	select o.order_id as orderid
		 , od.quantity
		 , od.price
		 , et.suppID
		 , o.order_date as updatedate
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
		inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
	where o.order_date between @start_date and @end_date
	order by updatedate
		   , od.price
    
	create index ix_suppid on #tps (suppid)

	select ep.event_id    
		, m.first_name + ' ' +m.last_name as [supp_name]    
		, isnull(sum(tps.quantity),0) + isnull(sum(child.quantity),0) as quantity
		, isnull(sum(tps.price),0) + isnull(sum(child.price),0) as amount
		, min(m.create_date) as create_date
		, m.email_address
	from event_participation ep 
		inner join member_hierarchy mh
			on mh.member_hierarchy_id = ep.member_hierarchy_id
		inner join (
			select mh.parent_member_hierarchy_id
				  ,sum(tps.quantity) as quantity
				  , sum(tps.price) as price
				  , ep.event_id
			from member_hierarchy mh
				inner join event_participation ep
					on ep.member_hierarchy_id = mh.member_hierarchy_id
				left join (
					select suppID
						 , sum(quantity) as quantity
						 , sum(price) as price
					from #tps
					group by suppID
				) tps
					on tps.suppID = ep.event_participation_id
			group by mh.parent_member_hierarchy_id, ep.event_id
		) child 
			on child.parent_member_hierarchy_id = mh.member_hierarchy_id and child.event_id = ep.event_id
		left join (
				select suppID
					 , sum(quantity) as quantity
					 , sum(price) as price
				from #tps
				group by suppID
			) tps
			on tps.suppID = ep.event_participation_id
		inner join member m --with (index(0))
			on mh.member_id = m.member_id
	where mh.parent_member_hierarchy_id is not null and ep.event_participation_id = @event_participation_id
	group by ep.event_id, m.email_address, m.first_name , m.last_name
	order by 3 desc
END
GO

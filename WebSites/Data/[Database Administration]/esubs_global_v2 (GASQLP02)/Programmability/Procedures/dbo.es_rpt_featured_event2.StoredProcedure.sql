USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_featured_event2]    Script Date: 02/14/2014 13:06:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2006/03/01
-- Description:	Fetch all the data for the 
--              featured group page.
--				Display last 3 months sales
--
-- Ex: EXEC [dbo].[es_rpt_featured_event2]
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_featured_event2]
AS
BEGIN
	SET NOCOUNT ON;

    -- pre-generate the tps
    create table #tps (
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
	select orderid
	     , quantity
	     , price
	     , suppID
	     , updatedate
	from
	(
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
         , o.create_date as updatedate
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
        --inner join featured_event fe on fe.event_id = ep.event_id
	where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701 )
      and o.update_date > DATEADD(m, -3, getdate())
	) t
	order by updatedate
 		, price

    create index ix_suppid on #tps (suppid)

    -- Main query
    select TOP 15 e.event_id
           , e.event_name
           , RIGHT(pa.subdivision_code,CHARINDEX('-', pa.subdivision_code)-1)  AS state
           , enb.total as nb_member
           , tps.total as nb_sub
           , tps.amount
        from event e
            inner join event_group eg
                on eg.event_id = e.event_id
            inner join [group] g
                on g.group_id = eg.group_id
            inner join payment_info pinfo
                on pinfo.group_id = eg.group_id
                and pinfo.event_id = eg.event_id
            left join postal_address pa
                on pa.postal_address_id = pinfo.postal_address_id
            left join subdivision sub
                on sub.subdivision_code = pa.subdivision_code
            inner join (
                select count(ep.event_id) as total, ep.event_id
                from event_participation ep
                group by ep.event_id
            ) enb on enb.event_id = e.event_id
            inner join (
                select e.event_id
                       , count(tps.quantity) as total
                       , sum(tps.quantity * tps.price) as amount
                from event e
                    inner join event_participation ep
                        on ep.event_id = e.event_id
                    inner join #tps tps
                        on tps.SuppID = ep.event_participation_id
                group by e.event_id
                having min(tps.updatedate) >= DATEADD(m, -3, getdate())
            ) tps on tps.event_id = e.event_id
    where pinfo.active = 1
    order by tps.amount desc

END



select e.event_id, Min(et.CreateDate), count(et.SuppID)
from event e
    inner join event_participation ep
        on ep.event_id = e.event_id
    inner join qspecommerce..efundraisingtransaction et
        on et.SuppID = ep.event_participation_id
GROUP By e.event_id
having min(et.createdate) > DATEADD(m, -3, getdate())
GO

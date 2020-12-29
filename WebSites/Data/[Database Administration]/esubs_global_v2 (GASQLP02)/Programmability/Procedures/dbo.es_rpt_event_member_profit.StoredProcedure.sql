USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_event_member_profit]    Script Date: 02/14/2014 13:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_rpt_event_member_profit] @event_id int

as 

begin 

 --pre-generate the tps

create table #tps 

(

rownum int identity(1,1)

, orderid int

, quantity int

, price money

, suppID int

, charge numeric(18,2)

, createdate datetime

)

INSERT INTO #tps 

(

orderid

, quantity

, price

, suppID

, charge

, createdate

)

select orderid

, quantity

, price

, suppID

, charge

, createdate

from

(

select o.order_id as orderid

, od.quantity

, od.price

, et.suppID

, COALESCE(pt.fulfillment_charge, 0) as charge

, et.createdate

from qspecommerce.dbo.efundraisingtransaction et

inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid

inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id

inner join event_participation ep on ep.event_participation_id = et.suppid

inner join qspfulfillment..catalog_item_detail cid on cid.catalog_item_detail_id = od.catalog_item_detail_id

inner join qspfulfillment..catalog_item ci on ci.catalog_item_id = cid.catalog_item_id

inner join qspfulfillment..product p on p.product_id = ci.product_id

inner join qspfulfillment..product_type pt on p.product_type_id = pt.product_type_id


where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701 ) 

and ep.event_id = @event_id

) t

order by createdate

, price

 

create index ix_suppid on #tps (suppid)

--select 'Member Name', 'Supporter Name', 'Products', '$Amount Purchased', '$Profit', 'Purchase Date'

-- supporters orders 

select 
--'pppp' as'Member Name', 'ttt' as 'Supporter Name', 3 as 'Products',11.00 as '$Amount Purchased',9.89 as '$Profit', '2009-07-12 15:09:09.270' as'Purchase Date'
(case 

-- sponsor order must be under his name

when (mp.first_name + ' ' + mp.last_name) is null 

then (m.first_name + ' ' + m.last_name) 

-- participant orders must be under his name

when cc.member_type_id = 2

then (m.first_name + ' ' + m.last_name)

else (mp.first_name + ' ' + mp.last_name)

end) as 'Member Name'

, m.first_name + ' ' + m.last_name as 'Supporter Name'

, quantity as 'Products'

, quantity * price as '$Amount Purchased'

, quantity * (price - charge) * ppc.profit_percentage / 100 as '$Profit'

, convert(varchar(10), (createdate), 121) as 'Purchase Date'

from event_participation ep

inner join event_group eg on eg.event_id = ep.event_id 

inner join [group] g on g.group_id = eg.group_id 

inner join partner_payment_config ppc on ppc.partner_id = g.partner_id

inner join #tps tps

on tps.suppid = ep.event_participation_id

inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id 

inner join member m on m.member_id = mh.member_id

left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id

left outer join member mp on mp.member_id = mhp.member_id

inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id

where ep.event_id = @event_id

and mh.active = 1

order by createdate

END
GO

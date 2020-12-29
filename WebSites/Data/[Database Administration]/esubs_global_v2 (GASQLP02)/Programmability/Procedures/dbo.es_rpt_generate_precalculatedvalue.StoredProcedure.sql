USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_generate_precalculatedvalue]    Script Date: 02/14/2014 13:06:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2006/03/01
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_generate_precalculatedvalue]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO precalculatedvalue (sales_amount_grand_total, update_date)
    select SUM(quantity * price), getdate()
	from
	(
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	from qspfulfillment.dbo.[order] o
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
        inner join qspfulfillment..catalog_item_detail cid on cid.catalog_item_detail_id = od.catalog_item_detail_id
        inner join qspfulfillment..catalog_item ci on ci.catalog_item_id = cid.catalog_item_id
        inner join qspfulfillment..product p on p.product_id = ci.product_id
	where o.order_status_id in ( 701 )
      and p.product_type_id = 1
      and o.source_id IN (2,3,4)
	--union all
	--select oh.ID as orderid
	--     , sod.quantity
	--     , cid.value as price
 --	from qspstore.dbo.orderheader oh
	--	inner join qspstore.dbo.suborderheader soh on oh.id = soh.orderheaderid 
	--	inner join qspstore.dbo.suborderdetail sod on soh.id = sod.suborderheaderid
	--	inner join qspstore.dbo.catalogitemdetail cid on cid.id = sod.catalogitemdetailid
	--WHERE oh.OrderTotal IS NOT NULL
	--  AND soh.ShipToAddressID <> 0
	--  and oh.aggregatorid in (7,13)
 --     AND oh.AggregatorId IN (0,6,7,8,9,13) 
 --     AND OrderStatusID BETWEEN 2 AND 9
 --     AND year(oh.orderdate) >= 2004
 --     AND month(oh.orderdate) >= 1
 --     AND day(oh.orderdate) >= 1
	) t

	
END
GO

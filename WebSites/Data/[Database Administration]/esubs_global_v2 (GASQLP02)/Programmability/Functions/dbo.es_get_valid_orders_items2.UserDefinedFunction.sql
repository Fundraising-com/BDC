USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_valid_orders_items2]    Script Date: 02/14/2014 13:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_get_valid_orders_items2] ()
RETURNS @retOrders TABLE (rownum int identity(1,1) PRIMARY KEY NOT NULL
		, act int
		, order_id int
		, order_item_id int
		, quantity int
		, price money
		, total_amount money
		, sub_total money
		, tax money
		, freight money
		, redeemed_amount money
		, supp_id int
		, supp_name varchar(255)
	    	, first_name varchar(255)
	    	, last_name varchar(255)
		, event_id int
		, item_type_id int
		, product_id int 
		, product_desc varchar(255)
		, product_type_id int
		, product_type_desc varchar(255)
		, create_date datetime
		, store_id int)
AS 
BEGIN
	

	INSERT INTO @retOrders (
	      order_id
	    , order_item_id
	    , quantity
	    , price
	    , total_amount
	    , sub_total
	    , tax
	    , freight
	    , redeemed_amount
	    , supp_ID
	    , supp_name
	    , first_name
	    , last_name
	    , event_id
	    , item_type_id
	    , product_id  
	    , product_desc 
	    , product_type_id 
	    , product_type_desc
	    , create_date
	    , store_id
	)
	
	select o.order_id 
	     , od.order_detail_id
	     , sum(od.quantity)
	     , od.price
	     , sum(od.quantity * od.price)
	     , SUM((od.quantity * od.price) -  COALESCE(ISNULL(pt.fulfillment_charge,0), 0) - COALESCE(ISNULL(t.redeemed_amount,0), 0))
	     , 0
	     , sum(COALESCE(pt.fulfillment_charge, 0))
	     , t.redeemed_amount
	     , et.suppID
	     , (case when ltrim(rtrim(isnull(em.recipient_name,''))) = ''  then em.email_address else em.recipient_name end) 
	     , cust.first_name
	     , cust.last_name
	     , ep.event_id
	     , 0 -- No need to populate this value for QSP sales -- used only for GA sales
	     , p.product_id
	     , p.product_name
	     , pt.product_type_id
	     , product_type_name
             , et.createdate
             , 1
	from qspecommerce.dbo.efundraisingtransaction et with(nolock)
		inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[customer] cust on cust.customer_id = o.customer_id
		inner join qspfulfillment.dbo.[order_detail] od with(nolock) on od.order_id = o.order_id
		INNER JOIN esubs_global_v2.dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
		inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
		INNER JOIN [QSPFulfillment].[dbo].[catalog_item_detail] as cid ON od.catalog_item_detail_id = cid.catalog_item_detail_id
		INNER JOIN [QSPFulfillment].[dbo].[catalog_item] as ci ON cid.catalog_item_id = ci.catalog_item_id
		INNER JOIN [QSPFulfillment].[dbo].[Product] p ON ci.product_id = p.Product_id
		INNER JOIN [QSPFulfillment].[dbo].[Product_Type] pt ON p.product_type_id = pt.Product_type_id
		left join qspfulfillment..email em on o.billing_email_id = em.email_id
		left join (
			select  ra.Order_ID
				, od.Order_detail_id
				, ra.redeemed_amount / (select count(order_id) from qspfulfillment.dbo.order_detail where order_id = ra.order_id group by order_id) AS Redeemed_Amount 
			from	qspfulfillment.dbo.fn_discount_redeemed_amount(null,null,null,null,12) ra
			inner join qspfulfillment.dbo.order_detail od with(nolock) on od.order_id = ra.order_id 
			) t on od.order_id = t.order_id and od.order_detail_id = t.order_detail_id
	GROUP BY o.order_id 
	     , od.order_detail_id
	     , od.price
	     , t.redeemed_amount
	     , et.suppID
	     , (case when ltrim(rtrim(isnull(em.recipient_name,''))) = ''  then em.email_address else em.recipient_name end) 
	     , cust.first_name
	     , cust.last_name
	     , ep.event_id
	     , p.product_id
	     , p.product_name
	     , pt.product_type_id
	     , product_type_name
         , et.createdate
	order by  createdate
 		, sum(od.quantity * od.price)


	INSERT INTO @retOrders (
	      order_id
	    , order_item_id
	    , quantity
	    , price
	    , total_amount
	    , sub_total
	    , tax
	    , freight
	    , redeemed_amount
	    , supp_id
	    , supp_name
	    , first_name
	    , last_name
 	    , event_id
	    , item_type_id
	    , product_id  
	    , product_desc 
	    , product_type_id 
	    , product_type_desc 
	    , create_date
	    , store_id
	)
	
	SELECT
		  vw.CustomerOrderID
		, vw.CustomerOrderDetailID
		, vw.Quantity
		, vw.OfferPrice
		, vw.TotalSaleAmount
		, vw.OrderDetailAmount
		, vw.OrderDetailTax
		, vw.OrderDetailFreight
		, 0 -- Redeemed Amount
		, vw.ExternalSupporterIdentifier
		, vw.SendToName -- As Supporter Name
		, vw.FirstName
		, vw.LastName
		, ep.event_id
		, vw.ItemTypeID
		, vw.ItemID 
		, vw.ItemDescShort
		, vw.ProductLineID
		, vw.ProductLineDesc
		, vw.DateTimeCreated
		, 10 -- Store Source ID
	FROM [GA].[AR].[vw_CustomerOrderDetail] vw
       INNER JOIN event_participation ep with(nolock) on ep.event_participation_id = vw.ExternalSupporterIdentifier
       INNER JOIN dbo.es_get_valid_order_state() os ON os.order_status_id = vw.CustomerOrderStateID
       INNER JOIN dbo.es_get_valid_order_ar_state() oas ON oas.order_status_id = vw.CustomerOrderARStateID
	
--create index ix_suppid on @retOrders (suppid)

-- update tout les activation a 0
update @retOrders set act = 0
-- trouver toutes les 1rst subs (activations)
update @retOrders set act = 1 where rownum in (select min(rownum) as rownum from @retOrders group by event_id)


RETURN
		
END
GO

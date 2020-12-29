USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [DW].[PrimeStagingSalesData_CurrentDay]    Script Date: 02/14/2014 13:08:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

Description:    This procedure pulls the sales data from QSP Store and GA store, and preps it for a merge into the live data table

Ex: exec [DW].[PrimeStagingSalesData_CurrentDay]
    select * from DW.es_valid_orders_items_staging
*/

DROP PROCEDURE [DW].[PrimeStagingSalesData_CurrentDay]
GO

USE [esubs_global_v2]
GO

/****** Object:  StoredProcedure [DW].[PrimeStagingSalesData_CurrentDay]    Script Date: 05/09/2019 16:20:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [DW].[PrimeStagingSalesData_CurrentDay]
AS
BEGIN

DECLARE @CurrentDate DATE 

SELECT @CurrentDate = CAST(GETDATE() AS DATE)

DELETE DW.es_valid_orders_items_staging WHERE create_date > @CurrentDate 

PRINT 'Deletion of DW.es_valid_orders_items_staging  completed'

 		
 PRINT 'Insert of QSP order data into DW.es_valid_orders_items_staging  completed'
 
 INSERT INTO DW.es_valid_orders_items_staging (
		  order_id
		, order_item_id
		, quantity
		, price
		, total_amount
		, sub_total
		, tax
		, freight
		, handling_fee
		, redeemed_amount
		, supp_ID
		, supp_name
		, first_name
		, last_name
		, email_address
		, event_id
		, item_type_id
		, product_id  
		, product_desc 
		, product_type_id 
		, product_type_desc
		, create_date
		, store_id
	)
	SELECT o.order_id 
			, od.order_detail_id
			, sum(od.quantity)
			, od.price
			, COALESCE(SUM(od.quantity * od.price), 0)
			, COALESCE(SUM(od.quantity * od.price), 0)
			, 0 -- tax
			, 0 -- freight
			, 0 -- handling fee
			, 0 -- redeemed_amount
			, ep.event_participation_id
			, (case when ltrim(rtrim(isnull(em.recipient_name,''))) = ''  then em.email_address else em.recipient_name end) 
			, m.first_name
			, m.last_name
			, em.email_address
			, ep.event_id
			, 0 -- item_type_id
			, 0 -- product_id 
			, null -- product_desc 
			, 0 -- product_type_id
			, null -- product_type_desc
			, o.create_date
			, 1 -- store_id  
		FROM [event_participation] ep WITH (NOLOCK)
			INNER JOIN [event_group] eg WITH (NOLOCK) ON eg.event_id = ep.event_id 
			INNER JOIN [event] e WITH (NOLOCK) ON e.event_id = eg.event_id
			INNER JOIN [group] g WITH (NOLOCK) ON g.group_id = eg.group_id		
			-- enfant
			INNER JOIN [member_hierarchy] mh WITH (NOLOCK) ON mh.member_hierarchy_id = ep.member_hierarchy_id
			INNER JOIN [member] m WITH (NOLOCK) ON m.member_id = mh.member_id
			LEFT OUTER JOIN users u with (nolock) on m.user_id = u.user_id
			INNER JOIN [creation_channel] cc WITH (NOLOCK) ON cc.creation_channel_id = mh.creation_channel_id
			-- parent
			LEFT OUTER JOIN [member_hierarchy] mhp WITH (NOLOCK) ON mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
			LEFT OUTER JOIN [member] mp WITH (NOLOCK) ON mp.member_id = mhp.member_id
			LEFT OUTER JOIN users up with (nolock) on mp.user_id = up.user_id
			-- Donation orders from EFRECommerce db
			LEFT JOIN EFRECommerce.dbo.[order] o WITH (NOLOCK) ON o.ext_participation_id = ep.event_participation_id
			LEFT JOIN EFRECommerce.dbo.order_detail od WITH (NOLOCK) ON o.order_id = od.order_id AND od.deleted = 0 AND od.product_id = 1
			LEFT JOIN EFRECommerce.dbo.email em WITH (NOLOCK) ON o.billing_email_id = em.email_id
			LEFT JOIN dbo.es_get_valid_efrecommerce_order_status() efreos ON o.status_id = efreos.status_id
		WHERE o.create_date > @CurrentDate AND mh.active = 1 AND o.deleted = 0 AND o.source_id = 1
		GROUP BY o.order_id 
			 , od.order_detail_id
			 , od.price
			 , ep.event_participation_id
			 , (case when ltrim(rtrim(isnull(em.recipient_name,''))) = ''  then em.email_address else em.recipient_name end) 
			 , m.first_name
			 , m.last_name
			 , em.email_address
			 , ep.event_id
			 , o.create_date
		order by  o.create_date
 			, sum(od.quantity * od.price)

  PRINT 'Insert of EFR donation data into DW.es_valid_orders_items_staging  completed'
 
 INSERT INTO DW.es_valid_orders_items_staging (
 	      order_id
 	    , order_item_id
 	    , quantity
 	    , price
 	    , total_amount
 	    , sub_total
 	    , tax
 	    , freight
 	    , handling_fee
 	    , redeemed_amount
 	    , supp_ID
 	    , supp_name
 	    , first_name
 	    , last_name
 	    , email_address
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
		, vw.OrderDetailHandlingFee
		, 0 -- Redeemed Amount
		, vw.ExternalSupporterIdentifier
		, vw.SendToName -- As Supporter Name
		, vw.FirstName
		, vw.LastName
		, vw.EmailAddress
		, ep.event_id
		, vw.ItemTypeID
		, vw.ItemID 
		, vw.ItemDescShort
		, MIN(vw.ProductLineID) -- we do this because a combo may have more than one PL and we can only handle one row per cod
		, MIN(vw.ProductLineDesc) -- we do this because a combo may have more than one PL and we can only handle one row per cod
		, vw.DateTimeCreated
		, 10 -- Store Source ID
	FROM [GA].[AR].[vw_CustomerOrderDetail] vw
       INNER JOIN event_participation ep with(nolock) on ep.event_participation_id = vw.ExternalSupporterIdentifier
       INNER JOIN dbo.es_get_valid_order_state() os ON os.order_status_id = vw.CustomerOrderStateID
       INNER JOIN dbo.es_get_valid_order_ar_state() oas ON oas.order_status_id = vw.CustomerOrderARStateID
       WHERE vw.DateTimeCreated > @CurrentDate
GROUP BY  vw.CustomerOrderID
		, vw.CustomerOrderDetailID
		, vw.Quantity
		, vw.OfferPrice
		, vw.TotalSaleAmount
		, vw.OrderDetailAmount
		, vw.OrderDetailTax
		, vw.OrderDetailFreight
		, vw.OrderDetailHandlingFee
		, vw.ExternalSupporterIdentifier
		, vw.SendToName -- As Supporter Name
		, vw.FirstName
		, vw.LastName
		, vw.EmailAddress
		, ep.event_id
		, vw.ItemTypeID
		, vw.ItemID 
		, vw.ItemDescShort
		, vw.DateTimeCreated
		, IsRenewal  

 PRINT 'Insert of GA order data into DW.es_valid_orders_items_staging  completed'
  
  
END

GO

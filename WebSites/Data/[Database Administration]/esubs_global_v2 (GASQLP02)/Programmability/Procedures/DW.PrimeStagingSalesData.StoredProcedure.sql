USE [esubs_global_v2]
GO

/****** Object:  StoredProcedure [DW].[PrimeStagingSalesData]    Script Date: 06/28/2017 08:56:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*

Description:    This procedure pulls the sales data from QSP Store and GA store, and preps it for a merge into the live data table

Ex: exec es_rpt_partner_commission_report_by_product '2006-02-01', '2006-03-01'

*/
DROP PROCEDURE [DW].[PrimeStagingSalesData]
GO

USE [esubs_global_v2]
GO

/****** Object:  StoredProcedure [DW].[PrimeStagingSalesData]    Script Date: 05/09/2019 16:18:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [DW].[PrimeStagingSalesData]
AS
BEGIN

TRUNCATE TABLE DW.es_valid_orders_items_staging 

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
	    , IsRenewal
	)
	select
	order_id
	,order_item_id
	,quantity
	,price
	,total_amount
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
    , IsRenewal
	FROM DW.es_valid_orders_items_archive (NOLOCK)
 		
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
		, IsRenewal
	)
	SELECT efrcstats.order_id 
			, efrcstats.order_detail_id
			, sum(efrcstats.quantity)
			, efrcstats.price
			, COALESCE(SUM(efrcstats.quantity * efrcstats.price), 0)
			, COALESCE(SUM(efrcstats.quantity * efrcstats.price), 0)
			, 0 -- tax
			, 0 -- freight
			, 0 -- handling fee
			, 0 -- redeemed_amount
			, ep.event_participation_id
			, (case when ltrim(rtrim(isnull(efrcstats.recipient_name,''))) = ''  then efrcstats.email_address else efrcstats.recipient_name end) 
			, m.first_name
			, m.last_name
			, efrcstats.email_address
			, ep.event_id
			, 0 -- item_type_id
			, 0 -- product_id 
			, 'Donation' -- product_desc 
			, 999 -- product_type_id
			, 'Donation' -- product_type_desc
			, efrcstats.create_date
			, 1 -- store_id
			, 0 -- IsRenewal
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
			LEFT JOIN (
				SELECT o.order_id, od.order_detail_id, o.ext_participation_id, od.quantity, od.price, email_address, recipient_name, o.create_date
				FROM EFRECommerce.dbo.[order] o WITH (NOLOCK)
				JOIN EFRECommerce.dbo.order_detail od WITH (NOLOCK) ON o.order_id = od.order_id AND od.deleted = 0 AND od.product_id = 1
				JOIN EFRECommerce.dbo.email em WITH (NOLOCK) ON o.billing_email_id = em.email_id
				JOIN dbo.es_get_valid_efrecommerce_order_status() efreos ON o.status_id = efreos.status_id
			) efrcstats on efrcstats.ext_participation_id = ep.event_participation_id
		WHERE mh.active = 1 AND efrcstats.order_id IS NOT NULL
		GROUP BY efrcstats.order_id 
			 , efrcstats.order_detail_id
			 , efrcstats.price
			 , ep.event_participation_id
			 , (case when ltrim(rtrim(isnull(efrcstats.recipient_name,''))) = ''  then efrcstats.email_address else efrcstats.recipient_name end) 
			 , m.first_name
			 , m.last_name
			 , efrcstats.email_address
			 , ep.event_id
			 , efrcstats.create_date
		order by  efrcstats.create_date
 			, sum(efrcstats.quantity * efrcstats.price)
 
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
	    , IsRenewal
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
		, IsRenewal
	FROM [GA].[AR].[vw_CustomerOrderDetail] vw
	    INNER JOIN event_participation ep with(nolock) on ep.event_participation_id = vw.ExternalSupporterIdentifier
       INNER JOIN dbo.es_get_valid_order_state() os ON os.order_status_id = vw.CustomerOrderStateID
       INNER JOIN dbo.es_get_valid_order_ar_state() oas ON oas.order_status_id = vw.CustomerOrderARStateID
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

END


GO


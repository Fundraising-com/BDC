USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_valid_orders_items_by_order_item_id]    Script Date: 02/14/2014 13:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_get_valid_orders_items_by_order_item_id] (@order_item_id INT)
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
		, handling_fee money
		, redeemed_amount money
		, supp_id int
		, supp_name varchar(255)
	    	, first_name varchar(255)
	    	, last_name varchar(255)
	    	, EmailAddress varchar(255)
		, event_id int
		, item_type_id int
		, product_id int 
		, product_desc varchar(255)
		, product_type_id int
		, product_type_desc varchar(255)
		, create_date datetime
		, store_id int
		, isrenewal bit)
AS 
BEGIN
	

	INSERT INTO @retOrders (
	      act
	    , order_id
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
	    , EmailAddress
	    , event_id
	    , item_type_id
	    , product_id  
	    , product_desc 
	    , product_type_id 
	    , product_type_desc
	    , create_date
	    , store_id
	    , isrenewal
	)
	SELECT 
	      act
	    , order_id
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
	    , isrenewal
	FROM DW.es_valid_orders_items (nolock)
	WHERE order_item_id = @order_item_id
	
RETURN
		
END
GO

USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_valid_orders_items_by_event_id_and_date_range]    Script Date: 02/14/2014 13:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
select * from [dbo].[es_get_valid_orders_items_by_event_id_and_date_range] (1401541, '01/01/2012', '05/31/2012')
*/
CREATE FUNCTION [dbo].[es_get_valid_orders_items_by_event_id_and_date_range] (@event_id int, @fromdate datetime, @todate datetime)
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
		, other_fees money
		, redeemed_amount money
		, supp_id int
		, supp_name varchar(255)
	    , first_name varchar(255)
	    , last_name varchar(255)
	    , email_address varchar(255)
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
		  act
		, order_id
		, order_item_id
		, quantity
	    , price
	    , total_amount
	    , sub_total
	    , tax
	    , freight
	    , other_fees
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
	FROM DW.es_valid_orders_items tps
       WHERE event_id = @event_id
	     AND tps.create_date between @fromdate and @todate

RETURN
		
END
GO

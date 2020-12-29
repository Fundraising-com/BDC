USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_valid_orders_items_by_partner_id_and_date_range]    Script Date: 03/20/2015 10:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER FUNCTION [dbo].[es_get_valid_orders_items_by_partner_id_and_date_range] (@partner_id int, @fromdate datetime, @todate datetime)
RETURNS @retOrders TABLE (rownum int identity(1,1) PRIMARY KEY NOT NULL
		, act int
		, order_id int
		, order_item_id int
		, quantity int
		, total_amount money
		, sub_total money
		, tax money
		, freight money
		, commission money
		, redeemed_amount money
		, supp_id int
		, supp_name varchar(255)
		, event_id int
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
	    , total_amount
	    , sub_total
	    , tax
	    , freight
	    , commission
	    , redeemed_amount
	    , supp_ID
	    , supp_name
	    , event_id
	    , product_id  
	    , product_desc 
	    , product_type_id 
	    , product_type_desc
	    , create_date
	    , store_id
	)
	--(Sales amount – tax – freight) – (sales amount x Gross Profit %)
	    SELECT  act
          , order_id
          , order_item_id
          , quantity
          , total_amount
          , sub_total
          , tax
          , freight
          ,(case
				when tps.item_type_id = 6 and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
                       (sub_total - tax - tps.freight) - (tps.sub_total * 0.25)
                 else
                 -- For all other product percent profit use event profit calculated field (January 6, 2011)
                 (sub_total - tax - tps.freight) - (sub_total * Isnull(e.profit_calculated, 40.0)/100.0)
            end) as commission
          , redeemed_amount
          , supp_ID
          , supp_name
          , tps.event_id
          , product_id  
          , product_desc 
          , product_type_id 
          , product_type_desc
          , tps.create_date
          , store_id
      FROM DW.es_valid_orders_items tps 
      INNER JOIN event_group eg with (nolock) on eg.event_id = tps.event_id
        INNER JOIN [group] g with (nolock) on g.group_id = eg.group_id 
        JOIN event e on tps.event_id = e.event_id
      WHERE g.partner_id = @partner_id
        AND tps.create_date between @fromdate and @todate
RETURN
		
END		


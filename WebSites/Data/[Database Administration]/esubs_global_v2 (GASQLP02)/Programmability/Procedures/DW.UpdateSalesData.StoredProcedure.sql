USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [DW].[UpdateSalesData]    Script Date: 02/14/2014 13:08:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

Description:    This procedure does a merge of data from staging to the live table. It is done as a merge so the table is not having to be truncated.

*/

CREATE PROCEDURE [DW].[UpdateSalesData]
AS
BEGIN

BEGIN TRAN;
MERGE DW.es_valid_orders_items AS T
USING DW.es_valid_orders_items_staging AS S
ON (T.order_id = S.order_id AND T.order_item_id = S.order_item_id) 
WHEN NOT MATCHED BY TARGET
    THEN INSERT
    (order_id, order_item_id, quantity, price, total_amount, sub_total, tax, freight, handling_fee, redeemed_amount, supp_ID, supp_name, first_name, last_name, email_address, event_id, item_type_id, product_id  , product_desc , product_type_id , product_type_desc, create_date, store_id, isrenewal)
    VALUES
    (S.order_id, S.order_item_id, S.quantity, S.price, S.total_amount, S.sub_total, S.tax, S.freight, S.handling_fee, S.redeemed_amount, S.supp_ID, S.supp_name, S.first_name, S.last_name, S.email_address, S.event_id, S.item_type_id, S.product_id  , S.product_desc , S.product_type_id , S.product_type_desc, S.create_date, S.store_id, s.IsRenewal)
WHEN MATCHED 
    THEN UPDATE 
    SET T.quantity 	= S.quantity,
		T.price 	= S.price,
		T.total_amount 	= S.total_amount,
		T.sub_total 	= S.sub_total,
		T.tax 		= S.tax,
		T.freight 	= S.freight,
		T.handling_fee  = S.handling_fee,
		T.redeemed_amount = S.redeemed_amount,
		T.supp_ID 	= S.supp_ID,
		T.supp_name 	= S.supp_name,
		T.first_name 	= S.first_name,
		T.last_name 	= S.last_name,
		T.email_address = S.email_address,
		T.event_id 	= S.event_id,
		T.item_type_id  = S.item_type_id, 
		T.product_id   	= S.product_id,  
		T.product_desc  = S.product_desc, 
		T.product_type_id  = S.product_type_id, 
		T.product_type_desc = S.product_type_desc,
		T.create_date 	= S.create_date,
		T.store_id 	= S.store_id,
		T.IsRenewal     = S.IsRenewal
WHEN NOT MATCHED BY SOURCE
    THEN DELETE;
--OUTPUT $action, inserted.*, deleted.*;-- unremm this line to see the output
--ROLLBACK TRAN;
COMMIT TRAN;


------------------------------
-- Set the activations
------------------------------
update DW.es_valid_orders_items  set act = 0
update DW.es_valid_orders_items  set act = 1 where rownum in (select min(rownum) as rownum from DW.es_valid_orders_items group by event_id)

END
GO

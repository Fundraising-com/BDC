USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_shopping_cart_item_by_id]    Script Date: 02/14/2014 13:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Shopping_cart_item
CREATE PROCEDURE [dbo].[efrstore_get_shopping_cart_item_by_id] @Shopping_cart_id int AS
begin

select Shopping_cart_id, Scratch_book_id, Carrier_id, Shipping_option_id, Quantity, Client_uploaded_img, Group_name from Shopping_cart_item where Shopping_cart_id=@Shopping_cart_id

end
GO

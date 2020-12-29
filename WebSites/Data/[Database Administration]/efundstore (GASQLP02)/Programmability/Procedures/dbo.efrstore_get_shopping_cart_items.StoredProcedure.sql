USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_shopping_cart_items]    Script Date: 02/14/2014 13:05:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Shopping_cart_item
CREATE PROCEDURE [dbo].[efrstore_get_shopping_cart_items] AS
begin

select Shopping_cart_id, Scratch_book_id, Carrier_id, Shipping_option_id, Quantity, Client_uploaded_img, Group_name from Shopping_cart_item

end
GO

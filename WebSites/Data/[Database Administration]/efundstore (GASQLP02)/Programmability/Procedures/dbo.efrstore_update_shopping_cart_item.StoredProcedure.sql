USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_shopping_cart_item]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Shopping_cart_item
CREATE PROCEDURE [dbo].[efrstore_update_shopping_cart_item] @Shopping_cart_id int, @Scratch_book_id int, @Carrier_id tinyint, @Shipping_option_id tinyint, @Quantity smallint, @Client_uploaded_img varchar(50), @Group_name varchar(50) AS
begin

update Shopping_cart_item set Scratch_book_id=@Scratch_book_id, Carrier_id=@Carrier_id, Shipping_option_id=@Shipping_option_id, Quantity=@Quantity, Client_uploaded_img=@Client_uploaded_img, Group_name=@Group_name where Shopping_cart_id=@Shopping_cart_id

end
GO

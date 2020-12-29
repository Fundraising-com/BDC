USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_shopping_cart_item]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Shopping_cart_item
CREATE PROCEDURE [dbo].[efrstore_insert_shopping_cart_item] @Shopping_cart_id int OUTPUT, @Scratch_book_id int, @Carrier_id tinyint, @Shipping_option_id tinyint, @Quantity smallint, @Client_uploaded_img varchar(50), @Group_name varchar(50) AS
begin

insert into Shopping_cart_item(Scratch_book_id, Carrier_id, Shipping_option_id, Quantity, Client_uploaded_img, Group_name) values(@Scratch_book_id, @Carrier_id, @Shipping_option_id, @Quantity, @Client_uploaded_img, @Group_name)

select @Shopping_cart_id = SCOPE_IDENTITY()

end
GO

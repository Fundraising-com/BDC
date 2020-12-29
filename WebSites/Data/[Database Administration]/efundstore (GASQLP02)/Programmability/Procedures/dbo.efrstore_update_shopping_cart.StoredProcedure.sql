USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_shopping_cart]    Script Date: 02/14/2014 13:06:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Shopping_cart
CREATE PROCEDURE [dbo].[efrstore_update_shopping_cart] @Shopping_cart_id int, @Visitor_log_id int, @Online_user_id int, @Shopping_cart_code char(1), @Date_created datetime AS
begin

update Shopping_cart set Visitor_log_id=@Visitor_log_id, Online_user_id=@Online_user_id, Shopping_cart_code=@Shopping_cart_code, Date_created=@Date_created where Shopping_cart_id=@Shopping_cart_id

end
GO

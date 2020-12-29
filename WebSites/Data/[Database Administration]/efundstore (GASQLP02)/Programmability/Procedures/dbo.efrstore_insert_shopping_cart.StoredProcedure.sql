USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_shopping_cart]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Shopping_cart
CREATE PROCEDURE [dbo].[efrstore_insert_shopping_cart] @Shopping_cart_id int OUTPUT, @Visitor_log_id int, @Online_user_id int, @Shopping_cart_code char(1), @Date_created datetime AS
begin

insert into Shopping_cart(Visitor_log_id, Online_user_id, Shopping_cart_code, Date_created) values(@Visitor_log_id, @Online_user_id, @Shopping_cart_code, @Date_created)

select @Shopping_cart_id = SCOPE_IDENTITY()

end
GO

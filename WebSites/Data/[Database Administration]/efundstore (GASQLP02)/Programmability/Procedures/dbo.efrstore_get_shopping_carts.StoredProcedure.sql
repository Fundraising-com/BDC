USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_shopping_carts]    Script Date: 02/14/2014 13:05:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Shopping_cart
CREATE PROCEDURE [dbo].[efrstore_get_shopping_carts] AS
begin

select Shopping_cart_id, Visitor_log_id, Online_user_id, Shopping_cart_code, Date_created from Shopping_cart

end
GO

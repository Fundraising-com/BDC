USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_shopping_cart_codes]    Script Date: 02/14/2014 13:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Shopping_cart_code
CREATE PROCEDURE [dbo].[efrstore_get_shopping_cart_codes] AS
begin

select Shopping_cart_code, Description from Shopping_cart_code

end
GO

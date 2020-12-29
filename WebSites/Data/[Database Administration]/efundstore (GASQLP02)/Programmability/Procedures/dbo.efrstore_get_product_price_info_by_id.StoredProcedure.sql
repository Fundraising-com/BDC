USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_price_info_by_id]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product_price_info
CREATE PROCEDURE [dbo].[efrstore_get_product_price_info_by_id] @Product_id int AS
begin

select Product_id, Country_code, Effective_date, Product_class_id, Unit_price from Product_price_info where Product_id=@Product_id

end
GO

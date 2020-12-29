USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_price_infos]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product_price_info
CREATE PROCEDURE [dbo].[efrstore_get_product_price_infos] AS
begin

select Product_id, Country_code, Effective_date, Product_class_id, Unit_price from Product_price_info

end
GO

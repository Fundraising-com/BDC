USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_product_price_info]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Product_price_info
CREATE PROCEDURE [dbo].[efrstore_update_product_price_info] @Product_id int, @Country_code varchar(10), @Effective_date datetime, @Product_class_id int, @Unit_price decimal(15, 4) AS
begin

update Product_price_info set Country_code=@Country_code, Effective_date=@Effective_date, Product_class_id=@Product_class_id, Unit_price=@Unit_price where Product_id=@Product_id

end
GO

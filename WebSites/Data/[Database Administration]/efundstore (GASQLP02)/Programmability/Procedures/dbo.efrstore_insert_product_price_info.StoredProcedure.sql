USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_product_price_info]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Product_price_info
CREATE PROCEDURE [dbo].[efrstore_insert_product_price_info] @Product_id int OUTPUT, @Country_code varchar(10), @Effective_date datetime, @Product_class_id int, @Unit_price decimal(15, 4) AS
begin

insert into Product_price_info(Country_code, Effective_date, Product_class_id, Unit_price) values(@Country_code, @Effective_date, @Product_class_id, @Unit_price)

select @Product_id = SCOPE_IDENTITY()

end
GO

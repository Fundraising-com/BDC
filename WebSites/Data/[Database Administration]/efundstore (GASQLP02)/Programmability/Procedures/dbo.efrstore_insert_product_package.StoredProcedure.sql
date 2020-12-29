USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_product_package]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Product_package
CREATE   PROCEDURE [dbo].[efrstore_insert_product_package] @Product_id int, @Package_id int, @Display_order int, @Display int, @create_date datetime AS
begin

insert into Product_package(product_id, Package_id, Display_order, Display, Create_date) values(@product_id, @Package_id, @Display_order, @Display, @Create_date)

select @Product_id = SCOPE_IDENTITY()

end
GO

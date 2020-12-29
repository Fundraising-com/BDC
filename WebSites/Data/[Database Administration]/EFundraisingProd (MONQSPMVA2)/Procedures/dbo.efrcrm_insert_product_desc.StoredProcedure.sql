USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_product_desc]    Script Date: 02/14/2014 13:07:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Product_desc
CREATE PROCEDURE [dbo].[efrcrm_insert_product_desc] @Product_desc_id int OUTPUT, @Product_id int, @Language_id tinyint, @Product_name varchar(100), @Product_short_desc varchar(300), @Product_long_desc varchar(1000), @Product_small_img varchar(25), @Product_large_img varchar(25), @Available_online bit AS
begin

insert into Product_desc(Product_id, Language_id, Product_name, Product_short_desc, Product_long_desc, Product_small_img, Product_large_img, Available_online) values(@Product_id, @Language_id, @Product_name, @Product_short_desc, @Product_long_desc, @Product_small_img, @Product_large_img, @Available_online)

select @Product_desc_id = SCOPE_IDENTITY()

end
GO

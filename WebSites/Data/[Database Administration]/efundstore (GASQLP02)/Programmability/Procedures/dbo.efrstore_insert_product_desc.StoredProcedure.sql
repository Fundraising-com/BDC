USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_product_desc]    Script Date: 02/14/2014 13:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Product_desc
CREATE   PROCEDURE [dbo].[efrstore_insert_product_desc] @Product_id int, @Culture_code nvarchar(10), @Template_id int, @Name varchar(100), @Short_desc varchar(1000), @Long_desc varchar(4000), @Extra_desc varchar(4000), @Page_name varchar(100), @Page_title varchar(200), @Image_name varchar(100), @Image_alt_text varchar(100), @Display_order int, @Enabled bit, @Configuration varchar(1000), @Create_date datetime AS
begin

insert into Product_desc(product_id, Culture_code, Template_id, Name, Short_desc, Long_desc, Extra_desc, Page_name, Page_title, Image_name, Image_alt_text, Display_order, Enabled, Configuration, Create_date) values(@product_id, @Culture_code, @Template_id, @Name, @Short_desc, @Long_desc, @Extra_desc, @Page_name, @Page_title, @Image_name, @Image_alt_text, @Display_order, @Enabled, @Configuration, @Create_date)

select @Product_id = SCOPE_IDENTITY()

end
GO

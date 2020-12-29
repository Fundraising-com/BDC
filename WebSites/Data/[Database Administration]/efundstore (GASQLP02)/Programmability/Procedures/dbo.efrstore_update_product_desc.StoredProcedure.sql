USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_product_desc]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Product_desc
CREATE   PROCEDURE [dbo].[efrstore_update_product_desc] @Product_id int, @Culture_code nvarchar(10), @Template_id int, @Name varchar(100), @Short_desc varchar(1000), @Long_desc varchar(4000), @Extra_desc varchar(4000), @Page_name varchar(100), @Page_title varchar(200), @Image_name varchar(100), @Image_alt_text varchar(100), @Display_order int, @Enabled bit, @Configuration varchar(1000), @Create_date datetime AS
begin

update Product_desc set Template_id=@Template_id, Name=@Name, Short_desc=@Short_desc, Long_desc=@Long_desc, Extra_desc=@Extra_desc, Page_name=@Page_name, Page_title=@Page_title, Image_name=@Image_name, Image_alt_text=@Image_alt_text, Display_order=@Display_order, Enabled=@Enabled, Configuration=@Configuration, Create_date=@Create_date where Product_id=@Product_id and culture_code = @culture_code

end
GO

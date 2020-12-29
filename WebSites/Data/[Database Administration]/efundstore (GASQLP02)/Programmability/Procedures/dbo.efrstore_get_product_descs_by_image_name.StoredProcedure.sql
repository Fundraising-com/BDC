USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_descs_by_image_name]    Script Date: 02/14/2014 13:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_desc
CREATE PROCEDURE [dbo].[efrstore_get_product_descs_by_image_name] 
                   @image_name varchar(50)
AS
begin

select Product_id, Culture_code, Template_id, Name, Short_desc, Long_desc, Extra_desc, Page_name, Page_title, Image_name, Image_alt_text, Display_order, Enabled, Configuration, Create_date 
from Product_desc
where image_name = @image_name 
order by Display_order asc

end
GO

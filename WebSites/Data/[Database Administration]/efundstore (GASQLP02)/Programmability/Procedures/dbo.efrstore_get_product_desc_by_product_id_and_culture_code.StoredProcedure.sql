USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_desc_by_product_id_and_culture_code]    Script Date: 02/14/2014 13:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product_desc
create   PROCEDURE [dbo].[efrstore_get_product_desc_by_product_id_and_culture_code] 
                   @product_id int
                  ,@culture_code varchar(5)
AS
begin

select Product_id, Culture_code, Template_id, Name, Short_desc, Long_desc, Extra_desc, Page_name, Page_title, Image_name, Image_alt_text, Display_order, Enabled, Configuration, Create_date 
from Product_desc
where product_id = @product_id and culture_code = @culture_code

end
GO

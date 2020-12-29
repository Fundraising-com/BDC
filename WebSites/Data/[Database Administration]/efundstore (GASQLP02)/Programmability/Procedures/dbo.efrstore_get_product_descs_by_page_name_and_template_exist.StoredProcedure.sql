USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_descs_by_page_name_and_template_exist]    Script Date: 02/14/2014 13:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product_desc
CREATE PROCEDURE [dbo].[efrstore_get_product_descs_by_page_name_and_template_exist]
@page_name VARCHAR(100)  
AS
BEGIN

SELECT  Product_id, Culture_code, Template_id, [Name], Short_desc, Long_desc, Extra_desc, Page_name, Page_title, Image_name, Image_alt_text, Display_order, Enabled, Configuration, Create_date 
FROM  Product_desc
WHERE Page_name = @page_name AND Template_id IS NOT NULL
order by Display_order asc

END
GO

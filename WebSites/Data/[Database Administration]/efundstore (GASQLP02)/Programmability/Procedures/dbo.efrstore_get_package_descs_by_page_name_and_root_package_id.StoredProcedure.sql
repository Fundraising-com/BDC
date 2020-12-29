USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_descs_by_page_name_and_root_package_id]    Script Date: 02/14/2014 13:05:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_desc
CREATE PROCEDURE [dbo].[efrstore_get_package_descs_by_page_name_and_root_package_id]
@page_name VARCHAR(100),
@root_package_id INT
AS
BEGIN

SELECT Package_id, Culture_code, Template_id, [name], Short_desc, Long_desc, Extra_desc, Page_name, Page_title, Image_name, Image_alt_text, Display_order, Enabled, Configuration, Create_date
FROM Package_desc
WHERE Page_name = @page_name
order by Display_order asc

END
GO

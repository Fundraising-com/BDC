USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_descs_by_package_id_and_page_name]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Package
CREATE  PROCEDURE [dbo].[efrstore_get_package_descs_by_package_id_and_page_name]
                  @Package_id int,
                  @Page_name varchar(100)
AS
begin

select Package_id, Culture_code, Template_id, [name], Short_desc, Long_desc, Extra_desc, Page_name, Page_title, Image_name, Image_alt_text, Display_order, Enabled, Configuration, Create_date
from Package_Desc
where Package_id <> @Package_id and page_name = @Page_name
order by Display_order asc

end
GO

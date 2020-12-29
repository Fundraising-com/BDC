USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_descs]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_desc
CREATE PROCEDURE [dbo].[efrstore_get_package_descs] AS
begin

select Package_id, Culture_code, Template_id, 'name', Short_desc, Long_desc, Extra_desc, Page_name, Page_title, Image_name, Image_alt_text, Display_order, Enabled, Configuration, Create_date from Package_desc
order by Display_order asc

end
GO

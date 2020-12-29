USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_desc_by_id]    Script Date: 02/14/2014 13:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Package_desc
CREATE PROCEDURE [dbo].[efrstore_get_package_desc_by_id] @Package_id int AS
begin

select Package_id, Culture_code, Template_id, [name], Short_desc, Long_desc, Extra_desc, Page_name, Page_title, Image_name, Image_alt_text, Display_order, Enabled, Configuration, Create_date from Package_desc where Package_id=@Package_id

end
GO

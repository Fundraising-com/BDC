USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_package_desc]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Package_desc
CREATE   PROCEDURE [dbo].[efrstore_insert_package_desc] @Package_id int, @Culture_code nvarchar(10), @Template_id int, @Name varchar(100), @Short_desc varchar(1000), @Long_desc varchar(4000), @Extra_desc varchar(4000), @Page_name varchar(100), @Page_title varchar(200), @Image_name varchar(100), @Image_alt_text varchar(100), @Display_order int, @Enabled bit, @Configuration varchar(1000), @Create_date datetime AS
begin

insert into Package_desc(package_id, Culture_code, Template_id, Name, Short_desc, Long_desc, Extra_desc, Page_name, Page_title, Image_name, Image_alt_text, Display_order, Enabled, Configuration, Create_date) values(@package_id,@Culture_code, @Template_id, @Name, @Short_desc, @Long_desc, @Extra_desc, @Page_name, @Page_title, @Image_name, @Image_alt_text, @Display_order, @Enabled, @Configuration, @Create_date)

select @Package_id = SCOPE_IDENTITY()

end
GO

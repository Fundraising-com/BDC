USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_package_template]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Package_template
CREATE PROCEDURE [dbo].[efrstore_insert_package_template] @Package_template_id smallint OUTPUT, @Package_template_desc varchar(50) AS
begin

insert into Package_template(Package_template_desc) values(@Package_template_desc)

select @Package_template_id = SCOPE_IDENTITY()

end
GO

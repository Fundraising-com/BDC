USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_template_by_id]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Package_template
CREATE PROCEDURE [dbo].[efrstore_get_package_template_by_id] @Package_template_id smallint AS
begin

select Package_template_id, Package_template_desc from Package_template where Package_template_id=@Package_template_id

end
GO

USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_templates]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_template
CREATE PROCEDURE [dbo].[efrstore_get_package_templates] AS
begin

select Package_template_id, Package_template_desc from Package_template

end
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_package_templatess]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_templates
CREATE PROCEDURE [dbo].[efrcrm_get_package_templatess] AS
begin

select Package_template_id, Package_template_desc from Package_templates

end
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_organization_type_descs]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Organization_type_desc
CREATE PROCEDURE [dbo].[efrcrm_get_organization_type_descs] AS
begin

select Organization_type_id, Language_id, Description from Organization_type_desc

end
GO

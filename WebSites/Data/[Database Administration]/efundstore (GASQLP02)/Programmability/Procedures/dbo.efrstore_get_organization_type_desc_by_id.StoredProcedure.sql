USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_organization_type_desc_by_id]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Organization_type_desc
CREATE PROCEDURE [dbo].[efrstore_get_organization_type_desc_by_id] @Organization_type_id int AS
begin

select Organization_type_id, Culture_code, Description from Organization_type_desc where Organization_type_id=@Organization_type_id

end
GO

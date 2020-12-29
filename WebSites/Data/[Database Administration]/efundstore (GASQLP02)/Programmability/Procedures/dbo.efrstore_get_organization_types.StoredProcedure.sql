USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_organization_types]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Organization_type
CREATE PROCEDURE [dbo].[efrstore_get_organization_types] AS
begin

select Organization_type_id, Party_type_id, Description from Organization_type

end
GO

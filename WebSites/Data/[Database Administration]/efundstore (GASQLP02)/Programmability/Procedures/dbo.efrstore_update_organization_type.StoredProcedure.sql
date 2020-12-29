USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_organization_type]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Organization_type
CREATE PROCEDURE [dbo].[efrstore_update_organization_type] @Organization_type_id tinyint, @Party_type_id tinyint, @Description varchar(50) AS
begin

update Organization_type set Party_type_id=@Party_type_id, Description=@Description where Organization_type_id=@Organization_type_id

end
GO

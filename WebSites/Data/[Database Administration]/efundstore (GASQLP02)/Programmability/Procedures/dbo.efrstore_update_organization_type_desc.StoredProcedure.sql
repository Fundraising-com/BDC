USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_organization_type_desc]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Organization_type_desc
CREATE PROCEDURE [dbo].[efrstore_update_organization_type_desc] @Organization_type_id tinyint, @Culture_code nvarchar(10), @Description varchar(200) AS
begin

update Organization_type_desc set Culture_code=@Culture_code, Description=@Description where Organization_type_id=@Organization_type_id

end
GO

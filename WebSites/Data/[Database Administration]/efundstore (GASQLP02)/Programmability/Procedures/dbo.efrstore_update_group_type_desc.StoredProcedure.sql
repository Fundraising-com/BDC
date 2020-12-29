USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_group_type_desc]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Group_type_desc
CREATE PROCEDURE [dbo].[efrstore_update_group_type_desc] @Group_type_id tinyint, @Culture_code nvarchar(10), @Description varchar(100) AS
begin

update Group_type_desc set Culture_code=@Culture_code, Description=@Description where Group_type_id=@Group_type_id

end
GO

USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_group_type]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Group_type
CREATE PROCEDURE [dbo].[efrstore_update_group_type] @Group_type_id tinyint, @Party_type_id tinyint, @Description varchar(50) AS
begin

update Group_type set Party_type_id=@Party_type_id, Description=@Description where Group_type_id=@Group_type_id

end
GO

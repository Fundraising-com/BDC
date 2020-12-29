USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_update_group]    Script Date: 02/14/2014 13:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Group
CREATE PROCEDURE [dbo].[rc_update_group] @Group_id smallint, @Group_name varchar(50), @Active_directory bit AS
begin

update [Group] set Group_name=@Group_name, Active_directory=@Active_directory where Group_id=@Group_id

end
GO

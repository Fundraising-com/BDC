USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_get_group_by_name]    Script Date: 02/14/2014 13:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Group
CREATE PROCEDURE [dbo].[rc_get_group_by_name] @Group_name varchar(50) AS
begin

select Group_id, Group_name, Active_directory from [Group] where Group_name=@Group_name

end
GO

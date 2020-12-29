USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_update_groups_user]    Script Date: 02/14/2014 13:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Groups_user
CREATE PROCEDURE [dbo].[rc_update_groups_user] @Group_id smallint, @User_name varchar(100) AS
begin

update Groups_user set User_name=@User_name where Group_id=@Group_id

end
GO

USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_get_groups_users]    Script Date: 02/14/2014 13:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Groups_user
CREATE PROCEDURE [dbo].[rc_get_groups_users] AS
begin

select Group_id, User_Name from Groups_user

end
GO

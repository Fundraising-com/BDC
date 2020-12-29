USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_get_user_by_user_name]    Script Date: 02/14/2014 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for User
CREATE PROCEDURE [dbo].[rc_get_user_by_user_name] @User_name varchar(100) AS
begin

select User_name, Password, External_id, External_name from [User] where User_name=@User_name

end
GO

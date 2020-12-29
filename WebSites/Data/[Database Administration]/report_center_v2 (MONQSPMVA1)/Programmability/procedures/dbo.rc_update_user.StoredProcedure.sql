USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_update_user]    Script Date: 02/14/2014 13:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for User
CREATE PROCEDURE [dbo].[rc_update_user] @User_name varchar(100), @Password varchar(50), @External_id varchar(50), @External_name varchar(100) AS
begin

update [User] set Password=@Password, External_id=@External_id, External_name=@External_name where User_name=@User_name

end
GO

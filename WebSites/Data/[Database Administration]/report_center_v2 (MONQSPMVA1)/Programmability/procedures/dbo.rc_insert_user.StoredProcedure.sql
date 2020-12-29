USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_insert_user]    Script Date: 02/14/2014 13:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for User
CREATE PROCEDURE [dbo].[rc_insert_user] @User_name varchar(100), @Password varchar(50), @External_id varchar(50), @External_name varchar(100) AS
begin

insert into [User](User_name, Password, External_id, External_name) values(@User_name, @Password, @External_id, @External_name)

--select @User_id = SCOPE_IDENTITY()

end
GO

USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_insert_groups_user]    Script Date: 02/14/2014 13:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Groups_user
CREATE PROCEDURE [dbo].[rc_insert_groups_user] @Group_id int OUTPUT, @User_Name varchar(100) AS
begin

insert into Groups_user(User_Name) values(@User_Name)

select @Group_id = SCOPE_IDENTITY()

end
GO

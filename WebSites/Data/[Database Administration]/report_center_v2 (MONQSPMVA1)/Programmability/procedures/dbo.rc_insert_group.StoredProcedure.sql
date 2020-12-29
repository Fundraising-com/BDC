USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_insert_group]    Script Date: 02/14/2014 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Group
CREATE PROCEDURE [dbo].[rc_insert_group] @Group_id int OUTPUT, @Group_name varchar(50), @Active_directory bit AS
begin

insert into [Group](Group_name, Active_directory) values(@Group_name, @Active_directory)

select @Group_id = SCOPE_IDENTITY()

end
GO

USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_group_group_status]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Group_group_status
CREATE PROCEDURE [dbo].[es_insert_group_group_status] @Group_id int OUTPUT, @Group_status_id int, @Create_date datetime AS
begin

insert into Group_group_status(Group_id, Group_status_id, Create_date) values(@Group_id, @Group_status_id, @Create_date)

end
GO

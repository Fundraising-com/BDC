USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_group_group_status]    Script Date: 02/14/2014 13:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Group_group_status
CREATE PROCEDURE [dbo].[es_update_group_group_status] @Group_id int, @Group_status_id int, @Create_date datetime AS
begin

update Group_group_status set Group_status_id=@Group_status_id, Create_date=@Create_date where Group_id=@Group_id

end
GO

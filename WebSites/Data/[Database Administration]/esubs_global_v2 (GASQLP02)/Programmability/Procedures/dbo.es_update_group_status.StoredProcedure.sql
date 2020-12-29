USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_group_status]    Script Date: 02/14/2014 13:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Group_status
CREATE PROCEDURE [dbo].[es_update_group_status] @Group_status_id int, @Description varchar(50) AS
begin

update Group_status set Description=@Description where Group_status_id=@Group_status_id

end
GO

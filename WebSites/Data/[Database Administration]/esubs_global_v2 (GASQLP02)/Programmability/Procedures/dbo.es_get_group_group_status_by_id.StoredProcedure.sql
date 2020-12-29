USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_group_status_by_id]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Group_group_status
CREATE PROCEDURE [dbo].[es_get_group_group_status_by_id] @Group_id int AS
begin

select top 1 Group_id, Group_status_id, Create_date 
from Group_group_status 
where Group_id=@Group_id
order by create_date desc
end
GO

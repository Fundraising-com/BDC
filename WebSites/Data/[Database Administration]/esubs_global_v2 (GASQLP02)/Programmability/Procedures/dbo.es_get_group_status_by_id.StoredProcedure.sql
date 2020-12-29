USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_status_by_id]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Group_status
CREATE PROCEDURE [dbo].[es_get_group_status_by_id] @Group_status_id int AS
begin

select Group_status_id, Description from Group_status where Group_status_id=@Group_status_id

end
GO

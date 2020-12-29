USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_status_by_group_id]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Group_status
CREATE PROCEDURE [dbo].[es_get_group_status_by_group_id] 
@Group_id INT
 AS
BEGIN

--SELECT 1 as Group_status_id, 'New' as [Description]
SELECT gs.Group_status_id, gs. [Description]
FROM Group_status gs 
	INNER JOIN 
	(
		select ggs.group_id, ggs.group_status_id
		from group_group_status ggs
		inner join (
		select group_id, max(create_date) as create_date 
		from group_group_status 
		group by group_id
		) ggs2 on ggs.group_id = ggs2.group_id and ggs.create_date = ggs2.create_date
	) ggs ON gs.group_status_id = ggs.group_status_id
WHERE ggs.group_id=@Group_id

END
GO

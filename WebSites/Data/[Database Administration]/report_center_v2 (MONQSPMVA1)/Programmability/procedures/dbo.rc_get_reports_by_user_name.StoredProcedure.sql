USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_get_reports_by_user_name]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Reports_group
CREATE PROCEDURE [dbo].[rc_get_reports_by_user_name] @User_Name varchar(100) AS
begin

SELECT distinct T.*
FROM
(
	SELECT
		r.report_id,
		r.report_label,
		r.report_desc,
		r.report_sp,
		r.displayable,
		' ' as database_name,
		' ' as user_name,
		' ' as password,
		' ' as server_name,
		gu.group_id
	FROM
		groups_user gu
	INNER JOIN [group] g on gu.group_id = g.group_id
	INNER JOIN reports_group rg on (rg.group_id = g.group_id)
	INNER JOIN reports r on (r.report_id = rg.report_id  and r.displayable = 1)
	WHERE
	gu.user_name=@User_Name and  gu.group_id <> 0
	
	UNION
	
	SELECT
		r.report_id,
		r.report_label,
		r.report_desc,
		r.report_sp,
		r.displayable,
		' ' as database_name,
		' ' as user_name,
		' ' as password,
		' ' as server_name,
		gu.group_id
	FROM
		groups_user gu, reports r 
	WHERE gu.user_name=@User_Name and gu.group_id=0  and r.displayable = 1
)  T
ORDER BY
T.report_label



end
GO

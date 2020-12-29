USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_get_reports_by_group_name]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Reports_group
CREATE PROCEDURE [dbo].[rc_get_reports_by_group_name] @Group_name varchar(100) AS
begin

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
		g.group_id
	FROM
	 [group] g
	INNER JOIN reports_group rg on (rg.group_id = g.group_id)
	INNER JOIN reports r on (r.report_id = rg.report_id  and r.displayable = 1)
	WHERE g.group_name = @Group_name
ORDER BY
r.report_label



end
GO

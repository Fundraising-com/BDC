USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_get_reports]    Script Date: 02/14/2014 13:07:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author		: GC
Project		: Report Center
Description	: Gets all reports
*/
CREATE PROCEDURE [dbo].[rc_get_reports]
AS
BEGIN
SELECT
	r.report_id,
	r.report_label,
	r.report_desc,
	r.report_sp,
	r.displayable,
	d.database_name,
	d.user_name,
	d.password,
	s.server_name
FROM
	reports r
INNER JOIN
	databases d
	ON r.database_id = d.database_id
INNER JOIN
	servers s
	ON r.server_id = s.server_id
WHERE
	r.displayable = 1
ORDER BY
	r.report_label
END
GO

USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_get_report_parameters]    Script Date: 02/14/2014 13:07:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author		: GC
Project		: Report Center
Description	: Gets all parameters for a report by his ID
*/
CREATE PROCEDURE [dbo].[rc_get_report_parameters]
	@reportID int
AS
BEGIN
SELECT
	rp.report_param_id,
	pc.param_ctrl_name,
	rp.param_label,
	rp.param_desc,
	rp.param_name,
	rp.param_type,
	rp.nullable,
	rp.param_default_value,
	rp.param_sql_query
FROM
	report_param rp
INNER JOIN
	param_ctrl pc
	ON rp.param_ctrl_id = pc.param_ctrl_id
INNER JOIN
	report_param_value rpv
	ON rp.report_param_id = rpv.report_param_id
INNER JOIN
	reports r
	ON rpv.report_id = r.report_id
WHERE
	r.report_id = @reportID
AND
	rpv.displayable = 1
ORDER BY
	rp.report_param_id
END
GO

USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_get_reports_group_by_id]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Reports_group
CREATE PROCEDURE [dbo].[rc_get_reports_group_by_id] @Group_id int AS
begin

select Report_id, Group_id from Reports_group where Group_id=@Group_id


end
GO

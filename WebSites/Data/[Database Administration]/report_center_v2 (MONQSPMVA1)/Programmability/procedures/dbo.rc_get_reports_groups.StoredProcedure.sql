USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_get_reports_groups]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Reports_group
CREATE PROCEDURE [dbo].[rc_get_reports_groups] AS
begin

select Report_id, Group_id from Reports_group

end
GO

USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_update_reports_group]    Script Date: 02/14/2014 13:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Reports_group
CREATE PROCEDURE [dbo].[rc_update_reports_group] @Report_id smallint, @Group_id smallint AS
begin

update Reports_group set Group_id=@Group_id where Report_id=@Report_id

end
GO

USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[rc_insert_reports_group]    Script Date: 02/14/2014 13:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Reports_group
CREATE PROCEDURE [dbo].[rc_insert_reports_group] @Report_id int OUTPUT, @Group_id smallint AS
begin

insert into Reports_group(Group_id) values(@Group_id)

select @Report_id = SCOPE_IDENTITY()

end
GO

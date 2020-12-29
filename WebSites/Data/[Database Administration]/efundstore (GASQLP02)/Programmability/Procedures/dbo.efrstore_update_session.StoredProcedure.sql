USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_session]    Script Date: 02/14/2014 13:06:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Session
CREATE PROCEDURE [dbo].[efrstore_update_session] @Session_id int, @Visitors_log_id int, @Date_created datetime AS
begin

update Session set Visitors_log_id=@Visitors_log_id, Date_created=@Date_created where Session_id=@Session_id

end
GO

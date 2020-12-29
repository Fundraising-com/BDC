USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_session_by_id]    Script Date: 02/14/2014 13:05:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Session
CREATE PROCEDURE [dbo].[efrstore_get_session_by_id] @Session_id int AS
begin

select Session_id, Visitors_log_id, Date_created from Session where Session_id=@Session_id

end
GO

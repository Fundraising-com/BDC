USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_sessions]    Script Date: 02/14/2014 13:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Session
CREATE PROCEDURE [dbo].[efrstore_get_sessions] AS
begin

select Session_id, Visitors_log_id, Date_created from Session

end
GO

USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_visitors_log_from_session]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 26, 2004
Description:	This stored procedure returns the visitors_log_id in the 
		sessions table associated with passed session_id.
*/
CREATE PROCEDURE [dbo].[get_visitors_log_from_session]
	@intSessionID INT
AS
DECLARE @intVisitorsLogID INT
SET @intVisitorsLogID = (
	SELECT 
		visitors_log_id 
	FROM 	sessions 
	WHERE 
		session_id = @intSessionID
)
IF @@ERROR = 0 
	RETURN( @intVisitorsLogID )
ELSE
	RETURN( -1 )
GO

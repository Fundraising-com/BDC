USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[create_session]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 26, 2004
Description:	This stored procedure creates a session from the visitors_lod_id.
		The session_id is returned if no error occurs.
*/
CREATE PROCEDURE [dbo].[create_session]
	@intVisitorsLogID INT
AS
INSERT INTO sessions ( visitors_log_id )
VALUES ( @intVisitorsLogID )
IF @@ERROR = 0
	RETURN( @@IDENTITY )
ELSE
	RETURN( -1 )
GO

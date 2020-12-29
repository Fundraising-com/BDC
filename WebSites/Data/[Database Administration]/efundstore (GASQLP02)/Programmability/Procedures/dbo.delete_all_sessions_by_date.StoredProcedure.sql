USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[delete_all_sessions_by_date]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 26, 2004
Description:	This stored procedure removes the session and all its items
		that are older than the passed date.
*/
CREATE PROCEDURE [dbo].[delete_all_sessions_by_date]
	@dteSessionCreated  DATETIME
AS
DECLARE @intErrorCode INT
SET @intErrorCode = @@ERROR

BEGIN TRANSACTION
IF @intErrorCode = 0 
BEGIN
	DELETE
	FROM	session_items
	WHERE 
		session_id in (
		SELECT 
			session_id 
		FROM	sessions
		WHERE
			date_created < @dteSessionCreated 
		)
	SET @intErrorCode = @@ERROR
END

IF @intErrorCode = 0 
BEGIN
	DELETE
		session_id 
	FROM	sessions
	WHERE
		date_created < @dteSessionCreated 
	SET @intErrorCode = @@ERROR
END

IF @intErrorCode = 0 
	COMMIT TRANSACTION
ELSE
	ROLLBACK TRANSACTION
GO

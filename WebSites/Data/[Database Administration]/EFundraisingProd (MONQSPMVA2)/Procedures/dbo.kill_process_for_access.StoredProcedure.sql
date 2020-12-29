USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[kill_process_for_access]    Script Date: 02/14/2014 13:08:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	January 26, 2003
Modified by :	Fblais
Modified on: 	2005-01-25
Description:	This stored procedure kills all the processes on the database based on the passed username.
		It finds all the system process id's for this user and kills all their processes.
*/
CREATE PROCEDURE [dbo].[kill_process_for_access]
	
AS


-- Declare the variables to store the values returned by FETCH.
DECLARE @intSPID INT
DECLARE @intCounter SMALLINT
DECLARE process_cursor CURSOR FAST_FORWARD FOR
--SELECT spid FROM master..sysprocesses WHERE loginame = @strUsername
SELECT spid 
FROM master..sysprocesses 
WHERE (loginame LIKE 'EFUNDRAISING\%'  OR loginame LIKE 'RDIGEST\%')
 AND 	login_time < DATEADD(mi, -30, getdate())
 AND	last_batch < DATEADD(mi, -30, getdate())
 AND 	program_name = 'Microsoft® Access'
 AND	status = 'sleeping'

SET @intCounter = 0
OPEN process_cursor

FETCH NEXT FROM process_cursor INTO @intSPID

-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
WHILE @@FETCH_STATUS = 0
BEGIN
	-- Kill Process.
	EXEC( 'KILL ' +@intSPID )
	PRINT CONVERT( VARCHAR(5), @intCounter + 1 ) + '. Process ' + CONVERT( VARCHAR(10), @intSPID ) + ' has been killed.'
	
	-- This is executed as long as the previous fetch succeeds.
	SET @intCounter = @intCounter + 1
	FETCH NEXT FROM process_cursor INTO @intSPID
END

PRINT ''
PRINT CONVERT( VARCHAR(5), @intCounter ) + ' processes have been killed.'

CLOSE process_cursor
DEALLOCATE process_cursor
GO

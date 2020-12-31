USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[prc_gen_CreateGrants]    Script Date: 06/07/2017 09:20:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[prc_gen_CreateGrants] AS

/* ------------------------------------------------------------
PROCEDURE: prc_gen_CreateGrants 

DESCRIPTION: Grants Execute permissions on all procs in database
for Login MyLogin 

AUTHOR: Brian Lockwood 3/15/00 5:38:48 PM 

-updated on 9/13/2004 by John Pappas:
  Changed proc to grant execute permissions to PROC_EXEC group 
  instead of single login. 
------------------------------------------------------------ */

DECLARE @ExecSQL varchar(100)

DECLARE curGrants CURSOR FOR

SELECT 'GRANT EXECUTE ON ' + NAME + ' TO PROC_EXEC' -- Replace MyLogin with the name of your new Login 
FROM SYSOBJECTS 
WHERE TYPE = 'P' 
AND LEFT(NAME,2) <> 'sp' -- system procs 
AND LEFT(NAME,2) <> 'dt' -- VSS procs


OPEN curGrants

FETCH NEXT FROM curGrants

INTO @ExecSQL


WHILE @@FETCH_STATUS = 0

BEGIN -- this will loop thru all your own procs and grant Execute privileges on each one

Exec(@ExecSQL)
IF @@ERROR <> 0
BEGIN
RETURN 1 -- return 1 if there is an error 
END 

Print @ExecSQL

FETCH NEXT FROM curGrants INTO @ExecSQL

END

CLOSE curGrants
DEALLOCATE curGrants
GO

USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_ftp_getfile]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
-- =============================================
-- Author:		Philippe Girard
-- Create date: July 17th, 2006
-- Description:	Get all file from a ftp site.
-- =============================================

Example usage: 
    exec pr_ftp_PutFile 
		    @FTPServer = 'myftpsite' ,
		    @FTPUser = 'username' ,
		    @FTPPWD = 'password' ,
		    @FTPPath = '/dir1/' ,
		    @SourcePath = 'c:\vss\mywebsite\' ,
		    @workdir = 'c:\temp\'

*/
CREATE PROCEDURE [dbo].[pr_ftp_getfile]
    @FTPServer varchar(128)
    , @FTPUser varchar(128)
    , @FTPPWD varchar(128)
    , @FTPPath varchar(128)
    , @TargetPath varchar(128)
    , @workdir varchar(128)
    , @commandout varchar(2000) OUTPUT
AS
BEGIN
    
    SET NOCOUNT ON

    declare	@cmd varchar(1000)
    declare @workfilename varchar(128)
    DECLARE @id int
	
	select @workfilename = 'ftpcmd.txt'
	
	-- deal with special characters for echo commands
	select @FTPServer = replace(replace(replace(@FTPServer, '|', '^|'),'<','^<'),'>','^>')
	select @FTPUser = replace(replace(replace(@FTPUser, '|', '^|'),'<','^<'),'>','^>')
	select @FTPPWD = replace(replace(replace(@FTPPWD, '|', '^|'),'<','^<'),'>','^>')
	select @FTPPath = replace(replace(replace(@FTPPath, '|', '^|'),'<','^<'),'>','^>')
	
	select	@cmd = 'echo ' + 'open ' + @FTPServer + ' > ' + @workdir + @workfilename
    
    --INSERT INTO #tmp
	exec master..xp_cmdshell @cmd

	select	@cmd = 'echo ' + @FTPUser + '>> ' + @workdir + @workfilename

    --INSERT INTO #tmp
	exec master..xp_cmdshell @cmd

	select	@cmd = 'echo ' + @FTPPWD + '>> ' + @workdir + @workfilename

    --INSERT INTO #tmp
	exec master..xp_cmdshell @cmd

    select @cmd = 'echo lcd ' + @TargetPath + '>> ' + @workdir + @workfilename

    --INSERT INTO #tmp
    EXEC master..xp_cmdshell @cmd

    select @cmd = 'echo cd ' + @FTPPath + '>> ' + @workdir + @workfilename

    --INSERT INTO #tmp
    EXEC master..xp_cmdshell @cmd

	select	@cmd = 'echo ' + 'mget *' + '>> ' + @workdir + @workfilename

    --INSERT INTO #tmp
	exec master..xp_cmdshell @cmd

    select @cmd = 'echo mdelete *' + '>> ' + @workdir + @workfilename

    EXEC master..xp_cmdshell @cmd

	select	@cmd = 'echo ' + 'quit' + '>> ' + @workdir + @workfilename

    --INSERT INTO #tmp
	exec master..xp_cmdshell @cmd
	
	select @cmd = 'ftp -i -s:' + @workdir + @workfilename
	
	create table #a (id int identity(1,1), s varchar(1000))

	insert INTO #a
	exec master..xp_cmdshell @cmd
    
    select @cmd = 'del ' + @workdir + @workfilename
    
    --INSERT INTO #tmp
    exec master..xp_cmdshell @cmd
    
    
    SET @commandout = ''	

    WHILE EXISTS(select s from #a WHERE s IS NOT NULL)
    BEGIN
        SELECT TOP 1 @id = id, @commandout = @commandout + char(13) + char(10) + CASE WHEN s IS NULL THEN '' ELSE s END
        FROM #a
        WHERE s IS NOT NULL
        ORDER BY id

        DELETE FROM #a WHERE id = @id
    END

    SET NOCOUNT OFF

END
GO

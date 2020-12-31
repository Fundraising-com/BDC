USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateArchiveDirectory]    Script Date: 06/07/2017 09:19:48 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[pr_CreateArchiveDirectory]
	@subDir varchar(200)
as

	Declare	@Today 		DATETIME,
		@Command 		VARCHAR(255),
		@ArchiveDirectory 	VARCHAR(200),
		@Date               		VARCHAR(20)

	CREATE TABLE #tParentDir (Line_Id INT Identity, Line_Text VARCHAR(8000) )
	CREATE TABLE #tSubDir     (Line_Id INT Identity, Line_Text VARCHAR(8000) )

	-- First check for existance and create archive directory
	SELECT  @Today=GetDate()
	
	SELECT @Date = DatePart(YEAR,@Today)
	
	IF(DatePart(MONTH, @Today) < 10)
	BEGIN
		SELECT @Date = @Date + '_0' +  Cast(DatePart(MONTH,@Today) AS VARCHAR(2))
	END
	ELSE
	BEGIN
		SELECT @Date = @Date + '_' +  Cast(DatePart(MONTH,@Today) AS VARCHAR(2))
	END
	
	
	IF(DatePart(DAY, @Today) < 10)
	BEGIN
		SELECT @Date = @Date + '_0' +  Cast(DatePart(DAY,@Today) AS VARCHAR(2))
	END
	ELSE
	BEGIN
		SELECT @Date = @Date + '_' +  Cast(DatePart(DAY,@Today) AS VARCHAR(2))
	END

	--Directory to create in (Parent directory)
	SELECT @ArchiveDirectory='E:\Projects\PayLater\PayLaterArchives\'
		
	
	--command to check if there is sub directory 
	SELECT @Command = 'DIR '+@ArchiveDirectory + '*.'
	
	--get the result
	INSERT INTO #tParentDir
	EXEC Master..xp_cmdshell @Command
	
	--Check if Dir for date exists by checking the dir name with date
	SELECT Right(Line_Text,10) 
	FROM #tParentDir WHERE Line_Text LIKE '%<DIR>%'
	AND Right(Line_Text,10) = @Date

	IF @@RowCount = 0
	BEGIN
		--Select  'Directory for date does not Exists'
		--Create Directory and sub directory for tracking files
		
		SELECT @Command = 'MKDIR E:\Projects\PayLater\PayLaterArchives\'+@Date+'\'+@subDir
	
		EXEC Master..xp_cmdshell @Command
	END 
	ELSE
	BEGIN
		--Dir for date exists so Check if Tracking directory Exists with in directory
		SELECT @Command = 'DIR '+'E:\Projects\PayLater\PayLaterArchives\'+@Date +'\*.'
		
		INSERT INTO #tSubDir
		EXEC Master..xp_cmdshell @Command

		SELECT Right(Line_Text,20) 
		FROM #tSubDir 
		WHERE Line_Text LIKE '%<DIR>%'
		AND Line_Text LIKE @subDir

		IF @@RowCount = 0
		BEGIN
			--SELECT  ' Tracking SubDir not Exists'
			--Create sub directory for tracking files
			SELECT @Command = 'MKDIR E:\Projects\PayLater\PayLaterArchives\'+@Date+'\'+@subDir
			
			EXEC Master..xp_cmdshell @Command
		END 

	END 


	DROP TABLE #tParentDir
	DROP TABLE #tSubDir
GO

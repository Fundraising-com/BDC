USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProcessResolveReportFiles]    Script Date: 06/07/2017 09:20:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
create procedure [dbo].[pr_ProcessResolveReportFiles]
as
	set nocount on
	DECLARE  	@TextLine 		VARCHAR(8000),
			@Command 		VARCHAR(255),
			@i			INT,
			@PtrVal			VARBINARY(16),
			@XMLDoc 		INT,
			@SQLStatement  	nVARCHAR(4000),
			@Date               		VARCHAR(20),
			@Filename         	VARCHAR(200),
			@FilenameWithOutPath  VARCHAR(200),
			@Donefilename 		VARCHAR(200),
			@ArchiveDirectory 	VARCHAR(200),
			@CountDone		INT,
			@Priority		VARCHAR(20),
			@Today 		DATETIME,
			@errorFlag 		 INT ,
			@ToEmailList                 VARCHAR(500),
			@CCEmailList                 VARCHAR(500)

	Exec dbo.pr_CreateArchiveDirectory 'FromResolveReports'
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

	CREATE TABLE #tResults  (Line_Id INT Identity, Line_Text VARCHAR(8000) )
	SELECT @Command = 'Dir E:\Projects\PayLater\Nightly\FromResolveReports\*.* '

	INSERT INTO #tResults
	EXEC Master..xp_cmdshell @Command

	CREATE TABLE #X (ID INT PRIMARY KEY,
			 F TEXT)
	
	INSERT INTO #X VALUES (1, '')
	

	SELECT @PtrVal = TEXTPTR(F)	FROM #X WHERE ID = 1

	DECLARE cResolveReport CURSOR FOR
		SELECT line_text FROM #tResults  WHERE line_text LIKE '%.xls'		
		--and line_text not like '%.DONE%'
		ORDER BY line_id
	
	OPEN cResolveReport

	FETCH NEXT FROM cResolveReport INTO @TextLine
	
	WHILE @@Fetch_Status = 0
	BEGIN
		SELECT @filename = 'E:\Projects\PayLater\Nightly\FromResolveReports\'+Substring(@TextLine,40, Len(@textline)-39)

		exec qspcanadacommon.[dbo].[Send_EMAIL_ATTACH] 	
			'OrderTracking@qsp.com','carmine.moscardini@rd.com,karen.tracy@rd.com', 'resolve tracking report file', '',
				@filename


		FETCH NEXT FROM cResolveReport INTO @TextLine

	end	

	close cResolveReport
	deallocate cResolveReport
	DROP TABLE #tResults
	DROP TABLE #X

	Select @archivedirectory ='e:\Projects\Paylater\PayLaterArchives\'
	Select @archivedirectory=@archivedirectory+@date+'\FromResolveReports\'

	-- Move all the files into the archive
	SELECT @command = 'MOVE /y E:\Projects\PayLater\Nightly\FromResolveReports\*.*  '+@archivedirectory --  DONOT have privs for deleting in folder 
	--SELECT @command = 'COPY /y E:\Projects\PayLater\Nightly\FromResolveReports\*.*  '+@archivedirectory  --FOR TESTING

	EXEC master..xp_cmdshell @command
GO

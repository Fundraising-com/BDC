USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProcessUnigistixTrackingFiles]    Script Date: 06/07/2017 09:20:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE         PROCEDURE [dbo].[pr_ProcessUnigistixTrackingFiles]
AS
	-- Grab tracking files from Resolve
	---EXEC pr_PullResolveTrackingFiles

	SET NOCOUNT ON

	
	DECLARE  		@TextLine 		VARCHAR(8000),
				@Command 		VARCHAR(255),
				@i			INT,
				@PtrVal			VARBINARY(16),
				@XMLDoc 		INT,
				@SQLStatement  	nvarchar(4000),
				@Date               		VARCHAR(20),
				@Filename         	VARCHAR(200),
				@FilenameWithOutPath  VARCHAR(200),
				@Donefilename 		VARCHAR(200),
				@ArchiveDirectory 	VARCHAR(200),
				@CountDone		INT,	
				@Today 		DATETIME,
				@TempPDF  varchar(200)

	--CREATE TABLE #tParentDir (Line_Id INT Identity, Line_Text VARCHAR(8000) )
	--CREATE TABLE #tSubDir     (Line_Id INT Identity, Line_Text VARCHAR(8000) )

	CREATE TABLE #Temp_Unigistix
	(
		FileSize 			INT,
		FileReceived 			VARCHAR(250),
		ReceivedTime 			VARCHAR(50)		
	)	

	CREATE TABLE #Temp_VerifyUnigistix
	(
		FileSize 			INT,
		FileReceived 			VARCHAR(250),
		ReceivedTime 			VARCHAR(50)	
	)	
	CREATE TABLE #X (ID INT PRIMARY KEY, F TEXT)

	CREATE TABLE #tResults  (Line_Id INT Identity, Line_Text VARCHAR(8000) )
	
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
		
	/*
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
		Select  'Directory for date does not Exists'
		--Create Directory and sub directory for tracking files
		
		SELECT @Command = 'MKDIR E:\Projects\PayLater\PayLaterArchives\'+@Date+'\FromUnigistixTracking'
	
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
		AND Line_Text LIKE '%FromUnigistixTracking'

		IF @@RowCount = 0
		BEGIN
			SELECT  ' Tracking SubDir not Exists'
			--Create sub directory for tracking files
			SELECT @Command = 'MKDIR E:\Projects\PayLater\PayLaterArchives\'+@Date+'\FromUnigistixTracking'
			
			EXEC Master..xp_cmdshell @Command
		END 

	END 

	DROP TABLE #tParentDir
	DROP TABLE #tSubDir
	*/
	exec dbo.pr_CreateArchiveDirectory 'FromUnigistixTracking'
	Declare  @errorFlag  int 


	--Get files to process
	SELECT @Command = 'Dir E:\Projects\PayLater\Nightly\FromUnigistixTracking\*.* '
	
	INSERT INTO #tResults
	EXEC Master..xp_cmdshell @Command
	
	
	INSERT INTO #X VALUES (1, '')
	

	SELECT @PtrVal = TEXTPTR(F)	FROM #X WHERE ID = 1

	SELECT Line_Text FROM #tResults ORDER BY Line_Id

	DECLARE cUnigistixTracker CURSOR FOR
		SELECT line_text FROM #tResults  WHERE line_text LIKE '%.xml'		
		--and line_text not like '%.DONE%'
		ORDER BY line_id
	
	OPEN cUnigistixTracker

	FETCH NEXT FROM cUnigistixTracker INTO @TextLine
	
	WHILE @@Fetch_Status = 0
	BEGIN
		SET @ErrorFlag =0


		SELECT @filename = substring(@TextLine,40, len(@textline)-39)
		SET @filenameWithOutPath = @filename
		SELECT @filename = 'E:\Projects\PayLater\Nightly\FromUnigistixTracking\'+@filename	
		--Import the file and see in the order stage tracking where there is a file w/this name
--print @filename
		delete from #temp_Unigistix
		delete from #Temp_VerifyUnigistix
		exec ImportXMLFile @filename,
					'#temp_Unigistix',
						'/Acknowledge/FILE', 
						'Size int,							
						FileName  varchar(2000),
						ReceivedTime  varchar(200)'
		-- q these up so we can check for errors
		insert into #Temp_VerifyUnigistix select * from #temp_Unigistix

		update #temp_Unigistix  set FileReceived =  'e:\projects\paylater\nightly\ToUnigistix\'+rtrim(FileReceived)
		update #Temp_VerifyUnigistix  set FileReceived =  'e:\projects\paylater\nightly\ToUnigistix\'+rtrim(FileReceived)
--select * from #temp_Unigistix

		-- update the tracking table
		update OrderStageTracking set PDFAckTPL=1, PDFAckFileSize=FileSize
			from #temp_Unigistix, OrderStageTracking
			where OrderStageTracking.PDFFilename = rtrim(FileReceived)
		
		update OrderStageTracking set BatchAckTPL=1, BatchAckFileSize=FileSize
			from #temp_Unigistix, OrderStageTracking
			where OrderStageTracking.BatchFilename = FileReceived
			
		update OrderStageTracking set CampaignAckTPL=1, CampaignAckFileSize=FileSize
			from #temp_Unigistix, OrderStageTracking
			where OrderStageTracking.CampaignFilename = FileReceived				

	FETCH NEXT FROM cUnigistixTracker INTO @TextLine
		 
	END
	CLOSE cUnigistixTracker
	DEALLOCATE cUnigistixTracker

	Set @archivedirectory =@archivedirectory+@date+'\FromUnigistixTracking\'

	Select @archivedirectory 

	-- move all the files into the archive
	SELECT @command = 'MOVE /y E:\Projects\PayLater\Nightly\FromUnigistixTracking\*.*  '+@archivedirectory
	--SELECT @command = 'COPY /y E:\Projects\PayLater\Nightly\FromResolveTracking\*.*  '+@archivedirectory
	SELECT @command
	exec master..xp_cmdshell @command	
	--Select Distinct Substring(TrackingFileName,50,len(TrackingFileName)-49),TRANSMISSIONSEQUENCE,ErrorMessage
	--from #temp_Error

	-- make sure there are no zero bytes and that if we thought we send something Unigistix got it

	declare @ZeroByteCount int
	select @ZeroByteCount=0
	select @ZeroByteCount=count(*) from #Temp_VerifyUnigistix where FileSize=0
	if(@ZeroByteCount <> 0)
	begin
		Exec QSPCanadaCommon..Send_EMail  'OrderTracking@qsp.com','qsp-qspfulfillment-dev@qsp.com', 'Error in Unigistix tracking file', '0 byte file detected'

		Insert Into QSPCanadaCommon.dbo.SystemErrorLog (ErrorDate,ProcName,Desc1,Desc2)
			values(GetDate(), 'pr_ProcessUnigistixTrackingFiles', 'Zero byte file issue', 'Zero byte file issue');
	end	
	drop table #temp_Unigistix
	drop table #Temp_VerifyUnigistix
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ArchiveQSPToUnigisitxAckFile]    Script Date: 06/07/2017 09:19:43 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_ArchiveQSPToUnigisitxAckFile]
 AS

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
	Set @archivedirectory =@archivedirectory+@date+'\ToUnigistixTracking\'
	exec dbo.pr_CreateArchiveDirectory 'ToUnigistixTracking'

	SELECT @command = 'MOVE /y E:\Projects\PayLater\Nightly\ToUnigistixTracking\*.*  '+@archivedirectory
	--SELECT @command = 'COPY /y E:\Projects\PayLater\Nightly\FromResolveTracking\*.*  '+@archivedirectory
	SELECT @command
	exec master..xp_cmdshell @command
GO

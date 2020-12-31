USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[UpdateRemitAPAcknowledgement]    Script Date: 06/07/2017 09:17:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Muhammad Siddiqui	
-- Create date: April 2, 2008
-- Description:	This proc will update the acknowledgement of Remit Ap cheque file trnsmission
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRemitAPAcknowledgement]  
AS
DECLARE @Filetoprocess 	VARCHAR(100),
		@AckFilePath	VARCHAR(200),
		@ArchivePath	VARCHAR(200),
		@command 		VARCHAR(255),
		@Message		VARCHAR(500),
		@RemitBatchID	INT,
		@DateTransmitToTD	DATETIME
			
	
	--Get name of file to process
	SET @AckFilePath   	= 'e:\projects\paylater\Nightly\ToMoveIT\' 
	SET @ArchivePath   	='e:\projects\paylater\paylaterarchives\RemitAPCheque\'
						
	SELECT @command     = 'DIR ' + @AckFilePath
	
	CREATE TABLE #tFileName  (Name VARCHAR(100) )
	INSERT #tFileName EXEC master..xp_cmdshell @command
	
	SELECT [Name] into #a FROM #tFileName WHERE [Name] LIKE '%.MOVED'

	DECLARE cAckFiles CURSOR FOR
	SELECT RTRIM(SUBSTRING(Name,CHARINDEX('RDA_TD_CHK' ,[Name]),70))FROM #a 

	OPEN cAckFiles
	FETCH NEXT FROM cAckFiles INTO @Filetoprocess
	WHILE @@FETCH_STATUS = 0 
	BEGIN

		IF IsNull(@Filetoprocess,'')=''
		BEGIN
	    	   SET @Message = 'Nothing to update'
	    	   EXEC QSPCanadaCommon..Send_EMail  'UpdateRemitAPAcknowledgment@qsp.com','Muhammad_Siddiqui@readersdigest.com', 'Error in RemitAPCheque status Update',@Message
	    	   RETURN
		END

		--Date Transmit to TD
		SELECT  @DateTransmitToTD=CONVERT(DATETIME,SUBSTRING(RIGHT(@Filetoprocess,16),1,6))FROM #a

		--SELECT @Filetoprocess,@RemitBatchID,@DateTransmitToTD 

		SELECT top 1 * FROM QSPCanadaFinance.dbo.REMIT_AP_INTERFACELOG
		WHERE CONVERT(DATETIME,CONVERT(VARCHAR(10),RunDate,101)) = @DateTransmitToTD
		AND AcknowledgeDate ='01/01/1995'

		IF @@ROWCOUNT =0
		BEGIN
		   SET @Message = 'Processed acknowledge file exists. No record to update in AP log'
  		   EXEC QSPCanadaCommon..Send_EMail  'UpdateRemitAPAcknowledgment@qsp.com','Muhammad_Siddiqui@readersdigest.com', 'Error in RemitAPCheque status Update',@Message
		END
		ELSE
		BEGIN
	
			UPDATE QSPCanadaFinance.dbo.REMIT_AP_INTERFACELOG
			SET AcknowledgeDate= @DateTransmitToTD
			WHERE CONVERT(DATETIME,CONVERT(VARCHAR(10),RunDate,101)) = @DateTransmitToTD
			AND AcknowledgeDate= '01/01/1995'

			--MoveToArchive
			SET @ArchivePath = @ArchivePath+@Filetoprocess
			SET @AckFilePath = @AckFilePath+@Filetoprocess
			SELECT @command = 'MOVE ' + @AckFilePath + '  '+ @ArchivePath
			EXEC master..xp_cmdshell @command
			SELECT @command

			SET @Message = 'Remit AP cheque file '+ @Filetoprocess +' has been successfully archived.'
  			EXEC QSPCanadaCommon..Send_EMail  'UpdateRemitAPAcknowledgment@qsp.com','Muhammad_Siddiqui@readersdigest.com', 'Error in RemitAPCheque status Update',@Message
		END

	FETCH NEXT FROM cAckFiles INTO @Filetoprocess
	END

	CLOSE cAckFiles
	DEALLOCATE cAckFiles	
	DROP TABLE #a
	DROP TABLE #tFileName

SET ANSI_NULLS OFF
GO

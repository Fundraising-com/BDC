USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProcessResolveTrackingFiles]    Script Date: 06/07/2017 09:20:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE          PROCEDURE [dbo].[pr_ProcessResolveTrackingFiles]
AS
	-- Grab tracking files from Resolve
	---EXEC pr_PullResolveTrackingFiles

SET NOCOUNT ON

CREATE TABLE #Temp_Resolve
	(
		SnapshotDate			DATETIME,
		TotalReceived 			INT,
		TransmissionSequence		INT,
		ReceiptDate 			DATETIME,
		ImageDate 			DATETIME,
		DataCaptureDate 		DATETIME,
		VerificationDate	 		DATETIME,
		EditDate 			DATETIME,
		TransmitDate	 		DATETIME,
		Stage 				INT,
		CampaignID 			INT,
		GroupID			INT,	
		GroupName 			VARCHAR(50),
		FMID 				VARCHAR(4),
		FMName 			VARCHAR(50),
		OrderID 			INT,
		ScanCount 			INT,
		ErrorCode                       INT,
		Units                           INT
	)	

	CREATE TABLE #Temp_Error
	(
		SnapShotDate			DATETIME  ,
		TrackingFileName		VARCHAR(200),
		CampaignID 			INT,
		TransmissionSequence	 	INT,
		OrderID 			INT,
		ErrorMessage 			VARCHAR(1000),
		Priority				VARCHAR(20),
		QSPError			VARCHAR(1)
	)	

		
	CREATE TABLE #tResults  (Line_Id INT Identity, Line_Text VARCHAR(8000) )

	--If transmitted
	DECLARE  @OrderList TABLE
	(
			CampaignId  		INT,
			CampaignIdFromFile	INT,
			AccountId 		INT ,
			AccountIdFromFile	INT ,
			OrderId 			INT ,
			OrderIdFromFile		INT ,
			CAFmId			VARCHAR(4),
			FmIdFromFile		VARCHAR(4),
			CAStatus		INT,
			CAStartDate		DATETIME,
			CAEnddate		DATETIME,
			Imagedate		DATETIME,
			Datacapturedate	DATETIME,
			VerificationDate		DATETIME,
			Editdate			DATETIME,
			TransmitDate		DATETIME,
			Stage			INT
	 )	

	
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
			@ToEmailList                 VARCHAR(1000),
			@CCEmailList                 VARCHAR(1000),
			@FiscalStart		DATETIME,
			@FiscalEnd		DATETIME

	Select @ToEmailList = 'qsp-operations-canada@qsp.com,resolve@qsp.com'
	Select @CCEmailList = 'qsp-qspfulfillment-dev@qsp.com'

	SELECT @archivedirectory = 'E:\Projects\PayLater\PayLaterArchives\'
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

	Exec pr_CreateArchiveDirectory 'FromResolveTracking'

------------------------------------------------------------------Finshed creating Directories ------------------------------------------

	--Get Fiscal Start and end date
	SELECT @FiscalStart=StartDate, @FiscalEnd=Enddate
	FROM [QSPCanadaCommon].[dbo].[Season]
	WHERE Getdate() BETWEEN Startdate AND Enddate  
	AND Season = 'Y'  

	--Get files to process
	SELECT @Command = 'Dir E:\Projects\PayLater\Nightly\FromResolveTracking\*.* '
	
	INSERT INTO #tResults
	EXEC Master..xp_cmdshell @Command
	
	CREATE TABLE #X (ID INT PRIMARY KEY,
			 F TEXT)
	
	INSERT INTO #X VALUES (1, '')
	

	SELECT @PtrVal = TEXTPTR(F)	FROM #X WHERE ID = 1

		SELECT line_text FROM #tResults  WHERE line_text LIKE '%.xml'		


	DECLARE cResolveTracker CURSOR FOR
		SELECT line_text FROM #tResults  WHERE line_text LIKE '%.xml'		
		--and line_text not like '%.DONE%'
		ORDER BY line_id
	
	OPEN cResolveTracker

	FETCH NEXT FROM cResolveTracker INTO @TextLine
	
	WHILE @@Fetch_Status = 0
	BEGIN
		SET @ErrorFlag =0

		SELECT @filename = Substring(@TextLine,40, Len(@textline)-39)

		SET @filenameWithOutPath = @filename
		SELECT @donefilename = '%'+@filename +'.DONE'
		-- Do we have a .DONE file for this one
		SELECT @CountDone=Count(*) FROM #tResults WHERE line_text LIKE @donefilename

		IF(@CountDone > - 1)    -- (MS Nov 17, 2006 Load even if no done file)
		BEGIN
			SET @errorFlag =0
		
			SELECT    @filename = 'E:\Projects\PayLater\Nightly\FromResolveTracking\'+@filename					
			DECLARE @transmissionID   INT
			DECLARE @SnapShotDate  VARCHAR(20)
			DECLARE @maxtransmissionID INT	
			DECLARE @m VARCHAR(400)
			DECLARE @cnt INT	

			--truncate table #temp_Resolve		
			DELETE FROM #temp_Resolve	
			DELETE FROM @OrderList

			EXEC ImportXMLFile @filename,
					'#temp_Resolve',
						'/RESOLVEORDERSTAGE/ORDERS/ORDER', 
						'SNAPSHOTDATE datetime ''../../SNAPSHOT/SNAPSHOTDATE'',
						TOTALRECEIVED int ''../../SNAPSHOT/TOTALRECEIVED'',
						TRANSMISSIONSEQUENCE int ''../../SNAPSHOT/TRANSMISSIONSEQUENCE'',
						RECEIPTDATE datetime,
						IMAGEDATE datetime,
						DATACAPTUREDATE datetime,
						VERIFICATIONDATE datetime,
						EDITDATE datetime,
						TRANSMITDATE datetime,
						STAGE int,
						CAMPAIGNID int,
						GROUPID int,	
						GROUPNAME varchar(50),
						FMID varchar(4),
						FMNAME varchar(50),
						ORDERID int,
						SCANCOUNT int,
						ERRORCODE int,
						UNITS int'
				
				--Check if Stage is blank 
				SELECT @cnt=Count(*)
				FROM #temp_Resolve WHERE IsNull(Stage,0)=0 
				
				IF @cnt  > 0
				BEGIN
					SET @Priority = 'HIGH'
					SELECT  @m = 'Blank Stage in file, Tracking screen will not work, immediate action required '
					INSERT INTO #temp_Error VALUES (@SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,@m,@Priority, 'N')				END
		
				SELECT @cnt = Count(*) ,@transmissionID=IsNull(TRANSMISSIONSEQUENCE,0) FROM #temp_Resolve 
				WHERE ( IsNull(SNAPSHOTDATE,'01/01/1995') = '01/01/1995'  OR 
					   IsNull(RECEIPTDATE, '01/01/1995') = '01/01/1995'  
					)
					OR ( Datediff(WEEK,SNAPSHOTDATE,Getdate()) > 2 OR Datediff(WEEK,RECEIPTDATE,Getdate()) > 2)
				GROUP BY TRANSMISSIONSEQUENCE
				IF @cnt  > 0
				BEGIN
					SET @Priority = 'HIGH'
					SELECT  @m = 'Blank or older than two week Snapshot date / Receipt date'	
					INSERT INTO #temp_Error VALUES (null,@filenameWithOutPath,0,@transmissionID,0,@m, @Priority, 'N')
				END
			
				--No DONE file set error message
				IF @CountDone =0
				BEGIN
					SET @Priority='HIGH'
					SELECT @m = '"DONE" file has not been detected' --+@donefilename				
					INSERT INTO #temp_Error VALUES (Null,@filenameWithOutPath,0,Null,0,@m,@Priority, 'N')
				END

				--Get the snapshot date
				SELECT TOP 1 @SnapShotDate= SNAPSHOTDATE FROM #temp_Resolve	
					
				--Checking Transmission seq #
				SELECT @cnt = Count(*) FROM #temp_Resolve 
				WHERE IsNull(TRANSMISSIONSEQUENCE,0) = 0
 				IF @cnt  > 0
				BEGIN
					SET @Priority = 'HIGH'
					SELECT  @m = 'Blank Trans SEQ # in file '	
					INSERT INTO #temp_Error VALUES (@SnapShotDate,@filenameWithOutPath,0,0,0,@m, @Priority, 'N')
				END
				ELSE
				BEGIN
					SELECT @transmissionID=isNull(TRANSMISSIONSEQUENCE,0) ,@SnapShotDate= SNAPSHOTDATE 
					FROM #temp_Resolve

					SELECT @maxtransmissionID=Max(TransmissionSequence) FROM QSPCanadaOrdermanagement..OrderStageTracking

					IF(@maxtransmissionID <> @transmissionID-1)
					BEGIN		
						SET @Priority = 'HIGH'
						SELECT @m = 'TRANSMISSION SEQUENCE ERROR detected, Last Seq # '+Ltrim(Str(@maxtransmissionID))+ ' while loading: '+Ltrim(Str(@transmissionID)) 	
					
						INSERT INTO #temp_Error VALUES (@SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,@m,@Priority, 'N')
						
					END
				END	

				--Check if CA or Account is blank it cannot be blank even in any stage
				SELECT @cnt=Count(*)
				FROM #temp_Resolve WHERE IsNull(CAMPAIGNID,0)=0 or IsNull(GroupId,0)=0
				
				IF @cnt  > 0
				BEGIN
					SET @Priority = 'HIGH'
					SELECT  @m = 'Blank Campaign No., or Account No. in file '
					INSERT INTO #temp_Error VALUES (@SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,@m,@Priority, 'N')				END

				SELECT @cnt=Count(*)
				FROM #temp_Resolve WHERE stage NOT BETWEEN 59000 AND 59007
				
				IF @cnt  > 0
				BEGIN
					SET @Priority = 'HIGH'
					SELECT  @m = 'Invalid stage # in file '
					INSERT INTO #temp_Error VALUES (@SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,@m,@Priority, 'N')
				END

				SET @Priority = 'HIGH'	
				INSERT INTO  @OrderList
				SELECT  
					c.id,a.campaignId,c.BilltoAccountiD,a.GroupId,b.Orderid,a.OrderId,c.FmId,a.FMID,
					c.Status,c.StartDate,c.Enddate,Imagedate,
					Datacapturedate,VerificationDate,Editdate,TransmitDate,Stage
				FROM #temp_Resolve a
				Left Join QSPcanadacommon..Campaign c ON a.campaignId=c.id 
				Left Join QSPCanadaOrdermanagement..batch b ON (a.orderId =b.orderid AND a.campaignId=b.campaignid AND a.Groupid=b.accountid)
				
			
				INSERT INTO #temp_Error
				SELECT @SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,
				--Case Stage When 59005 Then
				 'Non Existant or Invalid '+',Account ('+Ltrim(str(AccountIdFromFile))+')'+' ,CA ('+Ltrim(Str(CampaignIdFromFile))+')'+' or FM ID ('+FMIdFromFile+') ',@Priority, 'N'
				--Else  'Non Existant or Invalid '+',Account ('+Ltrim(str(AccountIdFromFile))+')'+' ,CA ('+Ltrim(str(CampaignIdFromFile))+')'+' or FM ID ('+FMIdFromFile+') ' End
				FROM @OrderList
				WHERE ( --OrderIdFromFile    <> IsNull(OrderId,0)   OR
					     AccountIdFromFile    <> IsNull(AccountId,0) OR
	             			                  CampaignIdFromFile  <> IsNull(CampaignId,0) OR
					    FMIdFromFile <> IsNull(CAFmId,0)
	       				)	
				
				--INSERT INTO #temp_Error
				--SELECT @SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,  'CA Status not ''APPROVED'' for CA ('+Ltrim(Str(CampaignIdFromFile))+')',@Priority, 'N'
				--Disabled Sept 21 2006 MS
				-- 'CA Status not ''APPROVED'' for CA ('+Ltrim(Str(CampaignIdFromFile))+') or CA already ended',@Priority, 'N'
				--FROM @OrderList
				--WHERE (CAStatus <> 37002 )	--Approved
				--Disabled Sept 21 2006 MS
				--OR CAEndDAte < Cast (Convert(VARCHAR(10),GetDate(),101) AS DATETIME)

				--Campaign out of  Current Fiscal Issue#1006
				-- if fiscal start and end exists check else insert in error log
				IF IsNull(@FiscalStart, '01/01/1950')  =  '01/01/1950' OR   IsNull(@FiscalEnd, '01/01/1950')  =  '01/01/1950'
				BEGIN
					SET @m = 'Blank fiscal start or end dates in database '
					INSERT INTO #temp_Error VALUES (@SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,@m,@Priority, 'Y')
			
				END 
				ELSE
				BEGIN
					INSERT INTO #temp_Error
					SELECT Distinct @SnapShotDate,@filenameWithOutPath,CampaignIdFromFile,@transmissionID,0,  'CA does not belong to current Fiscal ('+Ltrim(Str(CampaignIdFromFile))+')',@Priority, 'N'
					FROM @OrderList
					WHERE ( (CAStartDate  Not Between  @FiscalStart And  @FiscalEnd )       --OR	 ( CAEnddate   Not Between   @FiscalStart And  @FiscalEnd )  MS Nov14 06
						 )
				END
			
				INSERT INTO #temp_Error
				SELECT @SnapShotDate,@filenameWithOutPath,0,@transmissionID,0, 
					'Stage ''Data Imaged'', blank or older than two week Imaged date',@Priority, 'N'
				FROM @OrderList
				WHERE ( IsNull(Imagedate,'01/01/1995')='01/01/1995' OR  Datediff(WEEK,Imagedate,Getdate()) > 2)
				AND Stage=59001 

				INSERT INTO #temp_Error
				SELECT @SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,
					 'Stage ''Data captured'', blank or older than two week capture date',@Priority, 'N'
				FROM @OrderList
				WHERE ( IsNull(Datacapturedate,'01/01/1995')='01/01/1995' OR  Datediff(week,Datacapturedate,Getdate()) > 2)
				AND Stage=59002 

				INSERT INTO #temp_Error
				SELECT @SnapShotDate,@filenameWithOutPath,0,@transmissionID,0, 
					'Stage ''Data Verified'', blank or older than two week verification date',@Priority, 'N'
				FROM @OrderList
				WHERE ( IsNull(VERIFICATIONDATE,'01/01/1995')='01/01/1995' OR  Datediff(week,VERIFICATIONDATE,Getdate()) > 2)
				AND Stage=59003 
				
				INSERT INTO #temp_Error
				SELECT @SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,
					'Stage ''Data Edited'', blank or older than two week edit date ',@Priority, 'N'
				FROM @OrderList
				WHERE ( IsNull(EDITDATE,'01/01/1995')='01/01/1995' OR  Datediff(week,EDITDATE,Getdate()) > 2)
				AND Stage=59004 


				/*
				**  KT 7/20/06 Removed the following check - due to timing it's most likely that the order file will come at night and lag behind this
				** file -- additional control is in another job
				*/
				
				/*INSERT INTO #temp_Error
				SELECT @SnapShotDate,@filenameWithOutPath,0,@transmissionID,0, 
					'Stage ''Transmitted'', blank OrderId or Order already exists',@Priority, 'N'
				FROM @OrderList
				WHERE  (IsNull(OrderIdFromFile,0)= 0 OR OrderIdFromFile=IsNull(OrderID,0))
				AND Stage=59005
				*/
		
				INSERT INTO #temp_Error
				SELECT @SnapShotDate,@filenameWithOutPath,0,@transmissionID,0, 
					'Stage ''Transmitted'', blank or older than two week Transmit Date',@Priority, 'N'
				FROM @OrderList
				WHERE ( IsNull(TransmitDate,'01/01/1995')='01/01/1995' or  Datediff(week,TransmitDate,getdate()) > 2)
				AND Stage=59005

				Declare @Totalreceived INT 					
				SELECT @cnt= count(*)
				FROM @OrderList

				SELECT TOP 1 @Totalreceived= Totalreceived 
				FROM #temp_Resolve

				IF @Totalreceived <> @Cnt
				BEGIN
					INSERT INTO #temp_Error
					SELECT @SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,'Total received does not match with # of orders',@Priority, 'N'
				END

				DECLARE @TotalErrorInFile INT

				SELECT @TotalErrorInFile=Count(*) FROM #temp_Error	
				
				INSERT  OrderStageTracking
				(
					StageDate,
					CampaignID,
					OrderID,
					FMID,
					Stage,
					Scancount,
					GroupID,
					GroupName,
					FMName,
					ReceiptDate,
					ImageDate,
					DataCaptureDate,
					VerificationDate,
					EditDate,
					TransmitDate,
					TransmissionSequence,
					TotalReceived,
					ResolveFilename,
					ResolveFileInError,
					Units
				)
				SELECT SNAPSHOTDATE,
					CAMPAIGNID,
					ORDERID,
					FMID,
					STAGE,
					SCANCOUNT,
					GROUPID,
					GROUPNAME,
					FMNAME,
					RECEIPTDATE,
					IMAGEDATE,
					DATACAPTUREDATE,
					VERIFICATIONDATE,
					EDITDATE,
					TRANSMITDATE,
					TRANSMISSIONSEQUENCE,
					TOTALRECEIVED,
					@filenameWithOutPath,
					CASE @TotalErrorInFile WHEN 0 THEN 'N' ELSE 'Y' END,
					UNITS
				FROM #temp_Resolve

				IF @@Error > 0 
				BEGIN
					SET @Priority='HIGH'
					SET @m = 'Cannot Insert in table OrderStageTracking from file '
					INSERT INTO #temp_Error VALUES (@SnapShotDate,@filenameWithOutPath,0,@transmissionID,0,@m,@Priority, 'Y')
			
				END 
				ELSE
				BEGIN

					--Added Sept 26, 2006 MS "Stage Received Issue #965"
					--Update the zero OrderId for RECEIVED stage
					--Disabled MS Nov 10				
					/*DECLARE @CA int
					DECLARE @OrderId int
					DECLARE @ReceiptDate datetime
					DECLARE @TrackingId int
					DECLARE @ScanCount int

					DECLARE AllCa CURSOR FOR
					SELECT DISTINCT OrderiD,CampaignID,ReceiptDate,ScanCount FROM #temp_Resolve  Where Stage > 59000 AND OrderId > 0

					OPEN AllCa
					FETCH NEXT FROM AllCa INTO @OrderId,@CA, @ReceiptDate,@ScanCount
		
					WHILE @@Fetch_Status = 0
					BEGIN
											
						SELECT Top 1 @TrackingId= Id FROM QSPCanadaOrdermanagement.dbo.OrderStageTracking
						WHERE CampaignId=@CA AND Stage = 59000 AND OrderId = 0 AND ReceiptDate =@ReceiptDate

						IF @TrackingId <> 0
						BEGIN
							UPDATE QSPCanadaOrdermanagement.dbo.OrderStageTracking
							SET OrderId = @OrderId, ScanCount=@ScanCount
							WHERE Id=@TrackingId

							
						END
								
					FETCH NEXT FROM AllCa INTO @OrderId,@CA, @ReceiptDate,@ScanCount
					END

					CLOSE AllCa
					DEALLOCATE AllCa */


					--File processed create Received file in same folder regardless of errors in file using DONE file
					SELECT @command = 'COPY /y E:\Projects\PayLater\Apps\EmptyResolveAck.txt   E:\Projects\PayLater\Nightly\ToResolveTracking\'+@filenameWithOutPath+'.RECEIVED'
					SELECT @command
					EXEC master..xp_cmdshell @command
					
				END	
			END
------------------------------------------------if No done file ---------------------------
			--already checking (MS NOV 17, 2006)
			--ELSE IF @CountDone =0
			--No DONE file set error message
			--BEGIN
						
				--SET @Priority='HIGH'
				--SELECT @m = '"DONE" file has not been detected' --+@donefilename				
				--INSERT INTO #temp_Error VALUES (Null,@filenameWithOutPath,0,Null,0,@m,@Priority, 'N')
			--END
			--Consolidate error messages for one Email per file
			SELECT @cnt=Count(*) FROM #temp_Error WHERE TrackingFileName = @filenameWithOutPath
			IF @Cnt>0
			BEGIN
				SELECT @cnt=Count(*) FROM #temp_Error WHERE TrackingFileName = @filenameWithOutPath --AND  QSPError='N'
				IF @Cnt>0
				BEGIN
					--Select 'NONQSPERROR'
					DECLARE @ErrorMessageText	VARCHAR(8000)
					DECLARE @Leng		INT
					DECLARE @ErrorList 		VARCHAR(1000)
				
					SET @ErrorMessageText='Error(s) found in File '+@filenameWithOutPath+
							      ' Seq# '+ Ltrim(Str(IsNull(@transmissionID,0)))+ CHAR(13)+CHAR(13)
				
					DECLARE Errors Cursor For
	    				SELECT DISTINCT Priority,ErrorMessage FROM #temp_Error WHERE TrackingFileName = @filenameWithOutPath AND  QSPError='N'
	
	    				OPEN Errors
		    			FETCH  NEXT FROM Errors INTO @Priority,@ErrorList
			
					WHILE @@Fetch_Status = 0 AND IsNull(@Leng,0) < 7600 --Max is 8000
	    				BEGIN
						SET @ErrorMessageText= IsNull(@ErrorMessageText,'')+'Priority: '+ @Priority+ CHAR(13)+ @ErrorList+ CHAR(13)
						SET @Leng = IsNull(@Leng,0)+Len(@ErrorMessageText)+Len(@Priority)+12 --Word priority+spaces
						
					FETCH  NEXT FROM Errors  INTO @Priority, @ErrorList
					END
	
					INSERT INTO QSPCanadaCommon.dbo.SystemErrorLog (ErrorDate,ProcName,Desc1,Desc2)
					VALUES( GetDate(),'dbo.pr_ProcessResolveTrackingFiles',@filenameWithOutPath,Substring(@ErrorMessageText,1,1000))
	
					CLOSE Errors
					DEALLOCATE Errors
			
					EXEC QSPCanadaCommon..Send_EMail  'OrderTracking@qsp.com','qsp-qspfulfillment-dev@qsp.com', 'Error in Order tracking file', @ErrorMessageText
				END
				
				--Error other than data (SQL error)
				SELECT @cnt=Count(*) FROM #temp_Error WHERE TrackingFileName = @filenameWithOutPath --AND  QSPError='Y'
				IF @Cnt>0
				BEGIN
					DECLARE @ErrorMessageText1	VARCHAR(8000)
					DECLARE @Leng1		INT
					DECLARE @ErrorList1 		VARCHAR(1000)
				
					SET @ErrorMessageText1='Error(s) found while processing file '+@filenameWithOutPath+
							                 ' Seq# '+ Ltrim(Str(IsNull(@transmissionID,0)))+ CHAR(13)+CHAR(13)
				
					DECLARE QSPErrors Cursor For
	    				SELECT DISTINCT Priority,ErrorMessage FROM #temp_Error WHERE TrackingFileName = @filenameWithOutPath AND QSPError='Y'
	
	    				OPEN QSPErrors
		    			FETCH  NEXT FROM QSPErrors INTO @Priority,@ErrorList1
			
					WHILE @@Fetch_Status = 0 AND IsNull(@Leng1,0) < 7600 --Max is 8000
	    				BEGIN
						
						SELECT @ErrorMessageText1= IsNull(@ErrorMessageText,'')+'Priority: '+ @Priority+ CHAR(13)+ @ErrorList1+ CHAR(13)
						SET @Leng1 = IsNull(@Leng1,0)+Len(@ErrorMessageText1)+Len(@Priority)+12 --Word priority+spaces
						
					FETCH  NEXT FROM QSPErrors  INTO @Priority, @ErrorList1
					END
	
					INSERT INTO QSPCanadaCommon.dbo.SystemErrorLog (ErrorDate,ProcName,Desc1,Desc2)
					VALUES( GetDate(),'dbo.pr_ProcessResolveTrackingFiles',@filenameWithOutPath,Substring(@ErrorMessageText1,1,1000))
	
					CLOSE QSPErrors
					DEALLOCATE QSPErrors

					EXEC QSPCanadaCommon..Send_EMail  'OrderTracking@qsp.com','qsp-qspfulfillment-dev@qsp.com', 'Error in Order tracking file', @ErrorMessageText1
				END
				
			END
			

			FETCH NEXT FROM cResolveTracker INTO @TextLine
			 
		END
		CLOSE cResolveTracker
	
		DEALLOCATE cResolveTracker

		Select @archivedirectory =@archivedirectory+@date+'\FromResolveTracking\'

		-- Move all the files into the archive
		  SELECT @command = 'MOVE /y E:\Projects\PayLater\Nightly\FromResolveTracking\*.*  '+@archivedirectory --  DONOT have privs for deleting in folder 

--print @archivedirectory
		  --SELECT @command = 'COPY /y E:\Projects\PayLater\Nightly\FromResolveTracking\*.*  '+@archivedirectory  --FOR TESTING
		
		EXEC master..xp_cmdshell @command	
	
--		SELECT * FROM #temp_Error

	DROP TABLE #tResults
	DROP TABLE #X
	DROP TABLE #temp_Resolve
	DROP TABLE #temp_Error
GO

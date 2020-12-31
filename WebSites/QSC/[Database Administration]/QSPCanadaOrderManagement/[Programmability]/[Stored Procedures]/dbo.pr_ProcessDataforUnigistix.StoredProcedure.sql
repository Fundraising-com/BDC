USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProcessDataforUnigistix]    Script Date: 06/07/2017 09:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[pr_ProcessDataforUnigistix]  
	@runorderid int,
	@runftp int,
	@prod int
AS


/*
**  2/24/06 - Added in the OrderStageTrack processing
** 3/05/06 - Remove the runFTP
** 11/28/06 MS  Added stage column in OrderStageTracking insert  
*/
DECLARE	@sqlcommand 	  	NVARCHAR(4000),
	@OutputDirectory                        Varchar(100),
             @BatchFileName 		VARCHAR(256),
	@ProgramFileName 	varchar(256),
	@BatchID 		INT,
	@orderID                	INT,
	@BatchDate 		DATETIME,
	@CampaignID		INT,
	@Lang			VARCHAR(2),
	@CharDate 		VARCHAR(12),
	@BatchProc 		NVARCHAR(1000),
	@ProgramsProc 	NVARCHAR(1000),
	@BatchTimeString	VARCHAR(8) ,
	@FileDate                       DATETIME,
	@output		VARCHAR(8000),
	@orderstageid   int

 --verify if order is good to go
 Declare  @ErrorMsg varchar(1000), @HasError int , @EmailSubject varchar(300),@RecExist int

Set @RecExist = 0

 --Exec   QSPCanadaOrderManagement.dbo.pr_VerifyOrder  @runorderid, @ErrorMsg  output , @HasError output 

 IF @HasError  = 1 
   Begin
 	Select 'Order# '+ str(@runorderid)+' - XML not generated due to following Errors'+char(13)+ @ErrorMsg 
	Set @EmailSubject = 'Order# '+ str(@runorderid)+' - XML not generated'


           	Select top 1 @RecExist = 1  
	From  QspCanadaCommon.dbo.SystemErrorLog 
	Where OrderID = @runorderid
	 and ProcName = 'pr_ProcessDataforUnigistix' 
	 and Desc1 like '%XML Not generated%'
	 and isFixed <> 1 

	IF @RecExist <> 1 
	   Begin
	         Insert into QspCanadaCommon.dbo.SystemErrorLog 
		   ( ErrorDate,OrderID,CampaignID,ProcName,Desc1,Desc2,IsReviewed,IsFixed) 
	         values ( getdate(),@runorderid,Null, 'pr_ProcessDataforUnigistix','XML Not generated and Batch is sent to under review state',@ErrorMsg,0,0 ) 
 	  End	
    
            Exec QSPCanadaCommon.dbo.Send_EMail  'pr_ProcessDataforUnigistix@qsp.com','qsp-qspfulfillment-dev@qsp.com', @EmailSubject, @ErrorMsg,'qsp-qspfulfillment-dev@qsp.com'

   End 
 Else
    Begin

	if(@prod = 1)
	begin
		select @OutputDirectory = 'e:\projects\paylater\nightly\ToUnigistix\'
	end
	else
	begin
		select @OutputDirectory = 'e:\projects\paylater\nightly\ToUnigistix_Test\'
	end

	DECLARE Cur_Batches CURSOR FOR

	--SELECT ALL APPROVED BATCHES, NOT SENT TO TPL
	 SELECT distinct b.ID as BatchID, 
		b.Date as BatchDate, 
		b.OrderID as OrderID,
		c.id as CampaignID,
		c.Lang as Lang
	 FROM   QSPCanadaOrderManagement..Batch as b,
		QSPCanadaCommon..Campaign c,
		QSPCanadaOrderManagement..BatchDistributionCenter as bdc,
		QSPCanadaOrderManagement..CustomerOrderHeader coh,
		QSPCanadaOrderManagement..CustomerOrderDetail COD
	 WHERE	coh.OrderBatchDate = b.Date
		and OrderBatchID = b.ID
		and b.campaignid = c.id
		and bdc.BatchDate = b.Date
		and bdc.BatchID = b.id
		--and bdc.StatusInstance=40010 --UNCOMMENT AFTER TEST
		and cod.CustomerOrderheaderInstance = coh.Instance
		--and b.StatusInstance= 40010 -- Approved UNCOMMENT AFTER TEST
	              and bdc.DistributionCenterID = 2 -- UNCOMMENT AFTER TEST
		and cod.DistributionCenterID = 2  --UNCOMMENT AFTER TEST
		and cod.DistributionCenterID = bdc.DistributionCenterID -- UNCOMMENT AFTER TEST
		and cod.StatusInstance = 509 -- Pending to TPL -- UNCOMMENT AFTER TEST
		and orderid =@runorderid 
	ORDER BY orderid

OPEN Cur_Batches
	   
FETCH NEXT FROM Cur_Batches  INTO  @BatchID , @BatchDate , @orderID, @CampaignID, @Lang
WHILE @@FETCH_Status = 0               

	BEGIN 

	Select  @FileDate = GetDate()

	print @orderid
	Set @CharDate  =    '''' + Convert(varchar(10),@FileDate,101)  + '''' -- adding single quotes on the left n right side   

 	Set @BatchProc  	= 'EXEC QSPCanadaOrderManagement.dbo.pr_GenerateXMLOutput ' + Cast(@orderID as varchar)--+','+@CharDate        
	Set @ProgramsProc  	= 'EXEC QSPCanadaOrderManagement.dbo.pr_ProcessDataforUnigistix_Programs_Query ' + Cast(@orderID as varchar)--+','+@CharDate   
	
	Set @BatchTimeString = Convert(varchar(10),@FileDate,108)
	Set @BatchTimeString = Substring(@BatchTimeString,1,2) + Substring(@BatchTimeString,4,2)+Substring(@BatchTimeString,7,2)      

	Set @BatchFileName 	= 'UnigistixOutputBatch_'+Cast(@orderID as varchar)+'_'+Convert(varchar(10),@FileDate,112)+@BatchTimeString+'.xml'

	Set @ProgramFileName 	= 'UnigistixOutputProgramsInfoForBatch_'+Cast(@orderID as varchar)+'_'+Convert(varchar(10),@FileDate,112)+@BatchTimeString+'.xml'     

	SELECT * into ##UnigistixOrderStaging FROM vw_UnigistixOrderStaging WHERE OrderID = @OrderID


	-- 
	--start processing for batch data---
	Set @sqlcommand  = ' bcp  "'+@BatchProc+'"  queryout "'+@OutputDirectory+@BatchFileName+' " -c -q -T -r -w'        
--print @sqlcommand
	Exec master..xp_cmdshell @sqlcommand  
	DROP TABLE ##UnigistixOrderStaging

	---start processing campaign programs data----
	Set @sqlcommand  = ' bcp  "'+@ProgramsProc+'"  queryout "'+@OutputDirectory+@ProgramFileName+' " -c -q -T -r -w'   
	Exec master..xp_cmdshell @sqlcommand


	--MS March 09, 2007 added to populate Date fields etc to avoid error in tracking screen
	Declare @receiptDate Datetime
	Declare @ImageDate Datetime
	Declare @DataCaptureDate Datetime
	Declare @VerificationDate Datetime
	Declare @editdate Datetime
	Declare @transmitdate Datetime
	Declare @ScanCount int
	Declare @FileName Varchar(200)

	Select 	@receiptDate=Max(receiptDate),
		@ImageDate=Max(ImageDate),
		@DataCaptureDate=Max(DataCaptureDate),
		@VerificationDate=Max(VerificationDate),
		@editdate=Max(editdate),
		@transmitdate=Max(transmitdate),
		@ScanCount =Min(ScanCount),
		@FileName=Max(ResolveFilename)
	from orderstagetracking 
	where orderid=@orderID


	-- Update the OrderStageTracking table
	Insert OrderStageTracking
	(
		StageDate,
		CampaignID,
		OrderID,		
		BatchFilename,
		CampaignFilename,
		Stage, 
		Scancount,
		GroupID,
		GroupName,
		FMID,
		FMName,
		receiptDate,
		ImageDate,
		DataCaptureDate,
		VerificationDate,
		editdate,
		transmitdate,
		ResolveFilename
	)
	select Convert(varchar(10),GetDate(),101),
		@CampaignID,
		@orderID,
		@OutputDirectory+@BatchFileName,
		@OutputDirectory+@ProgramFileName,
		59006, 					--At Unigisitx MS(Nov 28, 06)
		@ScanCount,
		a.id,
		a.Name,
		c.FMID,
		f.lastname + ' ' + f.firstname,
		@receiptDate,
		@ImageDate,
		@DataCaptureDate,
		@VerificationDate,
		@editdate,
		@transmitdate,
		@FileName
		from qspcanadacommon..campaign c, qspcanadacommon..caccount a,
			qspcanadacommon..fieldmanager f
			where c.id = @CampaignID and a.id=billtoaccountid
				and f.FMID = C.FMID
				
	-- Update Batch sent to TPL
	Update  QSPCanadaOrderManagement..BatchDistributionCenter
 	Set StatusInstance = 40012 -- sent to TPL
	Where 	BatchID  	= @BatchID
	and 	BatchDate	= @BatchDate
	and     DistributionCenterID=2

	Update Batch Set StatusInstance = 40012 -- sent to TPL
		Where Date = @BatchDate and ID = @BatchID


	-- and 509 for the detail that is non mag
	update QSPCanadaOrderManagement..CustomerOrderDetail set StatusInstance=509 from
		QSPCanadaOrderManagement..BatchDistributionCenter as BatchDistributionCenter,
		QSPCanadaOrderManagement..CustomerOrderHeader COH,
		QSPCanadaOrderManagement..CustomerOrderDetail COD
	 	Where
			OrderBatchDate = BatchDistributionCenter.BatchDate
			and OrderBatchID = BatchDistributionCenter.BatchID
			and BatchID  	= @BatchID
			and BatchDate	= @BatchDate
			and CustomerOrderheaderInstance = Instance
			and COD.StatusInstance = 511 -- Picked
			and ProductType  <> 46001

	   SET  @BatchID = NULL -- initializing variable within loop
	   SET @BatchDate =  NULL


	   FETCH NEXT FROM Cur_Batches  INTO  @BatchID , @BatchDate , @orderID, @CampaignID, @Lang

	END -- end while loop for cursor

CLOSE Cur_Batches
DEALLOCATE Cur_Batches

--FTP All THE XML AND PDF TO UNIGISTIX

if(@runftp = 1)
begin
	DECLARE @RunID INT
	
	SELECT @RunID = coalesce(max(RunID),0) + 1 FROM FTPOutputUnigistix
	
	INSERT INTO FTPOutputUnigistix (Line) EXEC MASTER..XP_CMDSHELL 'E:\projects\paylater\apps\QSPCanadaFTP.exe --config E:\projects\paylater\apps\QSPCanadaFTP.ini --host 10.100.106.25 --user qsp --pass qspp@y '
	
	UPDATE FTPOutputUnigistix SET RunID = @RunID WHERE RunID IS NULL
	
	declare @line varchar(1024)
	set @output = ''
	declare  c1 cursor for select line from FTPOutputUnigistix WHERE RunID = @RunID order by id
	open c1
	fetch next from c1 into @line
	while @@fetch_status <> -1
	begin
		select @output = @output + coalesce(@line,'') + '
	'
		fetch next from c1 into @line
	end
	close c1
	deallocate c1
	
	
	exec MASTER..XP_SMTP_SENDMAIL 
		    @FROM 	= 'ftp_output@qsp.com',
		    @TO 	= 'qsp-qspfulfillment-dev@qsp.com',
		    @priority 	= 'NORMAL',
		    @subject 	= 'QSP Canada FTP Output - Unigistix',
		    @type 	= 'text/plain',
		    @message  	= @output,
		    @server 	= 'nasmtp.us.rdigest.com'
 End

End
GO

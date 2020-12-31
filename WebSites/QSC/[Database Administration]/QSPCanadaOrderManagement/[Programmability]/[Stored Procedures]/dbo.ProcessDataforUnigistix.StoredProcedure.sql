USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ProcessDataforUnigistix]    Script Date: 06/07/2017 09:20:44 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE                              PROCEDURE [dbo].[ProcessDataforUnigistix] AS

Declare	@sqlcommand 	  	varchar(4000),
        	@BatchFileName 	varchar(256),
	@ProgramFileName 	varchar(256),
	@BatchID 		int,
	@orderID                int,
	@BatchDate 		datetime,
	@CharDate 		varchar(12),
	@BatchProc 		varchar(1000),
	@ProgramsProc 	varchar(1000),
	@BatchTimeString	varchar(8) 
	
 set nocount on
 Declare Cur_Batches Cursor  For
	 Select distinct Batch.ID as BatchID, Batch.Date as BatchDate, Batch.OrderID as OrderID
	 From    QSPCanadaOrderManagement..Batch as Batch,
		QSPCanadaOrderManagement..BatchDistributionCenter as BatchDistributionCenter,
		QSPCanadaOrderManagement..CustomerOrderHeader COH,
		QSPCanadaOrderManagement..CustomerOrderDetail COD
	 Where
		OrderBatchDate = Date
		and OrderBatchID = ID
		and BatchDistributionCenter.BatchDate = Date
		and BatchDistributionCenter.BatchID = id
--		and BatchDistributionCenter.StatusInstance=40010
		and CustomerOrderheaderInstance = Instance
--		and Batch.StatusInstance= 40010 -- Approved
		and BatchDistributionCenter.DistributionCenterID = 2
		and COD.DistributionCenterID = 2
	--and orderqualifierid<>39002
	--	and Batch.OrderID = 89339


		and COD.StatusInstance = 509 -- Pickable
order by orderid

	OPEN Cur_Batches
	   
	   FETCH NEXT FROM Cur_Batches  INTO  @BatchID , @BatchDate , @orderID
           WHILE @@FETCH_Status = 0               

             BEGIN 

		exec PrepStagingTablesForUnigistix @orderID
		update UnigistixOrderStaging set Type=46002  where orderid=@orderID and productcode='441'
		update UnigistixOrderStaging set type = 46008 where type=46013 and orderid=@orderID
		update UnigistixOrderStaging set type = 46008 where type=46014 and orderid=@orderID

		/* make temp staging table so open xml is happy 
		   and this is in a unigistix friendly format*/
--	if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[unigistixtemp]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [unigistixtemp]

		Select 	
			Batch.OrderID as OrderID,
			 Batch.CampaignID,
		        	Campaign.ProgramID   as ProgramID,
		        	Campaign.GroupProfit,
			Campaign.IsPreCollect,
			Program.Name as programName,
			ProgramType.Description as ProgramTypeDesc   into unigistixtemp
			 From 	QSPCanadaOrderManagement..Batch as Batch,
			QSPCanadaCommon..CampaignProgram as Campaign,
			QSPCanadaCommon..Program as Program,
			QSPCanadaCommon..CodeDetail as ProgramType
			Where 	Batch.Campaignid  = Campaign.CampaignID
			and Campaign.programID = Program.id 
			and Program.ProgramTypeID = ProgramType.Instance
			and orderid=@orderID
			and Campaign.DeletedTF = 0
			Order by Program.ID

	Set @CharDate  =    '''' + Convert(varchar(10),@BatchDate,101)  + '''' -- adding single quotes on the left n right side   

 	Set @BatchProc  	= 'EXEC QSPCanadaOrderManagement.dbo.CreateUnigistixXMLOutput ' + Cast(@orderID as varchar)--+','+@CharDate        
	Set @ProgramsProc  	= 'EXEC QSPCanadaOrderManagement.dbo.ProcessDataforUnigistix_Programs_Query ' + Cast(@orderID as varchar)--+','+@CharDate   
	
	Set @BatchTimeString = Convert(varchar(10),@BatchDate,108)
	Set @BatchTimeString = Substring(@BatchTimeString,1,2) + Substring(@BatchTimeString,4,2)+Substring(@BatchTimeString,7,2)      

	Set @BatchFileName 	= 'UnigistixOutputBatch'+Cast(@BatchID as varchar)+'_'+Convert(varchar(10),@BatchDate,112)+@BatchTimeString+'.xml'
	Set @ProgramFileName 	= 'UnigistixOutputProgramsInfoForBatch'+Cast(@BatchID as varchar)+'_'+Convert(varchar(10),@BatchDate,112)+@BatchTimeString+'.xml'     


--start processing for batch data---

	Set @sqlcommand  = ' bcp  "'+@BatchProc+'"  queryout "e:\projects\paylater\nightly\ToUnigistix\'+@BatchFileName+' " -c -q -T -r -w'        

--	Set @sqlcommand  = ' bcp  "EXEC QSPCanadaOrderManagement.dbo.CreateUnigistixXMLOutput "  queryout "e:\projects\paylater\nightly\'+@BatchFileName+' " -U "sshah" -P "sshah" -c -r -t'        

	Exec master..xp_cmdshell @sqlcommand  

	--update batch w/filename

---end processing batch data------


---start processing campaign programs data----


	 Set @sqlcommand  = ' bcp  "'+@ProgramsProc+'"  queryout "e:\projects\paylater\nightly\ToUnigistix\'+@ProgramFileName+' " -c -q -T -r -w'   

	 Exec master..xp_cmdshell @sqlcommand

----end processing campaign programs----
	


--	Exec master..xp_cmdshell 'copy c:\work\prog_header.txt+c:\work\Programs_Info.xml+c:\work\prog_footer.txt c:\work\programs_output.xml /B' -- adding the tag files to fix strange error

	Update  QSPCanadaOrderManagement..BatchDistributionCenter
 	Set StatusInstance = 40012 -- sent to TPL
	Where 	BatchID  	= @BatchID
	and 	BatchDate	= @BatchDate
	and     DistributionCenterID=2
--	and     QSPProductLine not in (46006,46007,46001)

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
			and COD.StatusInstance = 509 -- Pickable
			and ProductType not in (46001)

	   SET  @BatchID = NULL -- initializing variable within loop
	   SET @BatchDate =  NULL
	   FETCH NEXT FROM Cur_Batches  INTO  @BatchID , @BatchDate , @orderID   
              END -- end while loop for cursor

	CLOSE Cur_Batches
	DEALLOCATE Cur_Batches
GO

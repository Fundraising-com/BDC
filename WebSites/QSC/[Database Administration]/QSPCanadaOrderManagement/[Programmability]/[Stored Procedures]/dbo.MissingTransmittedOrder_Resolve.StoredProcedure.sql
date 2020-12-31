USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[MissingTransmittedOrder_Resolve]    Script Date: 06/07/2017 09:19:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MissingTransmittedOrder_Resolve] 
As
Begin
	
	EXECUTE AS LOGIN='SNA\GAProxyUser';
	
    Set   NOCOUNT ON

    Declare @FileName 		    Varchar(100)
    Declare @Query    		    Varchar(2000)

    Declare @Cnt			    Int 
    Declare @Cmd  		        Varchar(1000)
    Declare @ColList  		    Varchar(200)
	Declare @TemplateFileName 	Varchar(100)
	Declare @FilePath 		    Varchar(200)  

	Declare @SubjectLine		Varchar(100) 
	Declare @EmailList 		    Varchar(500)
	Declare @ErrorMessageText	Varchar(500) 

	Declare	@SeasonStartDate 	DateTime
	Declare	@SeasonEndDate   	DateTime
	Declare @RunDate 		    DateTime
  
	Set @RunDate = Getdate()

	Set @ColList= 'OrderId,CampaignId,GroupID,GroupName,FMID,FMName'
	Set @FilePath= 'Q:\projects\paylater\QSPCAFinance\MissingTransmittedOrder\'
	Set @TemplateFileName = @FilePath+'TemplateMissingResolveOrder.xls'
 	Set @fileName= @FilePath+'MissingResolveOrder.xls'

	--Set @EmailList = 'qsp-qspfulfillment-dev@qsp.com;debby.gallie@dhltd.com;terri.smalheiser@dhltd.com;qsp-operations-canada@qsp.com'
	Set @EmailList = 'jmiles@gafundraising.com'--'qsp-qspfulfillment-dev@qsp.com;qspcanadafieldsupport@dhltd.com;terri.smalheiser@dhltd.com;qsp-operations-canada@qsp.com'
	Set @SubjectLine = 'Missing D+H Orders'

	Select @SeasonStartDate=Startdate,@SeasonEndDate=Enddate
	From QSPCanadaCommon.dbo.Season
	Where 	@RunDate between StartDate and EndDate
	And Season <>'Y'

	If @@Rowcount <> 1 or @@Error <> 0
	Begin
		Set @ErrorMessageText = 'Error selecting season start and End'
		Exec QSPCanadaCommon..Send_EMail 'MissingTransmittedOrder_Notification@qsp.com', @EmailList ,@SubjectLine,@ErrorMessageText
		Return
	End
	
	/*Set @query= 	'Select OrderId,CampaignId,GroupID,Groupname,FMID,Fmname
		     	     From QSPCanadaOrderManagement.dbo.OrderStageTracking
			         Where Stage=59005
			         And orderid not in (Select Orderid From QSPCanadaOrderManagement.dbo.Batch b)
			         And orderid not in (Select Orderid From QSPCanadaOrderManagement.dbo.OrderStageTracking Where Stage=59008)
			         And Convert(DateTime,Convert(Varchar(10),StageDate,101) ,101) >= '+Convert(Varchar(10),@SeasonStartDate,101)

	SELECT @Cnt =	 COUNT(*)
		     	     From QSPCanadaOrderManagement.dbo.OrderStageTracking
			         Where Stage=59005
			         And orderid not in (Select Orderid From QSPCanadaOrderManagement.dbo.Batch b)
			         And orderid not in (Select Orderid From QSPCanadaOrderManagement.dbo.OrderStageTracking Where Stage=59008)
			         And Convert(DateTime,Convert(Varchar(10),StageDate,101) ,101) >= Convert(Varchar(10),@SeasonStartDate,101)
*/
	--EXECUTE AS LOGIN='SNA\GAProxyUser';

	Select OrderId,CampaignId,GroupID,Groupname,FMID
	Into tempdb.#Orders
	From QSPCanadaOrderManagement.dbo.OrderStageTracking
	Where Stage=59005
	And orderid not in (Select Orderid From QSPCanadaOrderManagement.dbo.Batch b)
	And orderid not in (Select Orderid From QSPCanadaOrderManagement.dbo.OrderStageTracking Where Stage=59008)
	And Convert(DateTime,Convert(Varchar(10),StageDate,101) ,101) >= Convert(Varchar(10),@SeasonStartDate,101)
	Order by OrderID
	
	SELECT	@Cnt = COUNT(*)
	From	#Orders

	--EXECUTE AS LOGIN='SNA\GAProxyUser';
select user

	SELECT @Cmd = 'Copy '+@templateFileName+'  '+ @fileName
	--EXEC MASTER..XP_CMDSHELL @cmd

	declare @sqlcommand nvarchar(2048)
	set @sqlcommand = 'bcp "#Orders" out ' + @filename + ' -c -q -T'
	print @sqlcommand

	--EXECUTE AS LOGIN='SNA\GAProxyUser';
	exec master..xp_cmdshell @sqlcommand
select user

        --Set @cmd = 'Insert CA_EXCEL_IMPORT...[ExcelTable$] ' +  ' ( '+@colList+' ) '+ @query
        --Exec (@cmd)

        --Select @Cnt= Count(*) From CA_EXCEL_IMPORT...ExcelTable$ 

         If @Cnt > 0
         Begin
		Set @ErrorMessageText = 'Hello,'+Char(10)+Char(10)+'Order tracking record for these orders indicated that these were transmitted successfully, although QSP Inc. did not receive these orders.'+Char(10)+Char(10)+'Thank You.'+Char(10)+'QSP Inc.' +Char(10)
					
		Exec  QSPCanadaCommon.dbo.Send_EMAIL_ATTACH 'MissingResolveOrders@qsp.com', @EmailList,@SubjectLine,@ErrorMessageText,@fileName
          End
          --Else
          --Begin
		--To ensure job runs without failure everyday
		--Set @ErrorSubjectLine = 'Job ran successfully with no missing Order '+Convert(Varchar(20),@RunDate)
		--Exec QSPCanadaCommon..Send_EMail 'MissingTransmittedOrder_Notification@qsp.com', @ErrorEmailList ,@ErrorSubjectLine,Null
		
		--End
	drop table #Orders
	 Set  NOCOUNT OFF

End
GO

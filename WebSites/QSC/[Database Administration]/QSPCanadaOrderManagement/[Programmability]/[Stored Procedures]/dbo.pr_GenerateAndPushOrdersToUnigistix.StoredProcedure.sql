USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateAndPushOrdersToUnigistix]    Script Date: 06/07/2017 09:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_GenerateAndPushOrdersToUnigistix]
as
declare @aOrderID int
--verify if order is good to go
Declare  @ErrorMsg varchar(1000), @HasError int , @EmailSubject varchar(300),@RecExist int

declare aUniPush cursor for

select distinct top 25 orderid
--distinct orderid
from batch ,customerorderheader,customerorderdetail
where  orderbatchdate=date and orderbatchid=id 
and customerorderheaderinstance=instance
and producttype <> 46001
and date >='7/1/06'
and batch.statusinstance=40010
and orderqualifierid not in ( 39006,39018, 39017)
and ordertypecode not in (41002)
--MS Oct 11, 2007 Issue #3632
--and  qspcanadaordermanagement.dbo.UDF_PDFGenerationStatus(batch.orderid)=1
and qspcanadaordermanagement.dbo.UDF_PDFGenerationStatus(CASE orderqualifierid 
							            WHEN 39009 THEN 500001
							            ELSE batch.orderid 
							            END)=1

and orderid not in (1030360,1030361)
--and orderid=500016
order by orderid

open aUniPush
fetch next from aUniPush into @aOrderID

while (@@fetch_status <> -1)
begin
	
	select 
	customerorderdetail.statusinstance,orderid,customerorderdetail.*,productcode,productname,quantity
	from batch ,customerorderheader,customerorderdetail
	where orderid in(
	@aOrderID
	
	)
	and orderbatchdate=date and orderbatchid=id 
	and customerorderheaderinstance=instance
	and producttype <> 46001
	and (customerorderdetail.statusinstance not in(513, 509) or quantity =0) 
--	and customerorderdetail.statusinstance <> 513
--	and customerorderdetail.statusinstance=510
	order by customerorderheaderinstance


	Set @ErrorMsg =''
	Set @HasError =0
	Set @EmailSubject='' 
	Set @RecExist = 0
	
	Exec   QSPCanadaOrderManagement.dbo.pr_VerifyOrder  @aOrderID, @ErrorMsg  output , @HasError output 
	
	IF @HasError  = 1 
	Begin
		Select 'Order# '+ str(@aOrderID)+' - XML not generated due to following Errors'+char(13)+ @ErrorMsg 
		Set @EmailSubject = 'Order# '+ str(@aOrderID)+' - XML not generated'
		
		
		Select top 1 @RecExist = 1  
			From  QspCanadaCommon.dbo.SystemErrorLog 
			Where OrderID = @aOrderID
			 and ProcName = 'Push Uni' 
			 and Desc1 like '%XML Not generated%'
			 and isFixed <> 1 
	
		IF @RecExist <> 1 
		Begin
			Insert into QspCanadaCommon.dbo.SystemErrorLog 
			( ErrorDate,OrderID,CampaignID,ProcName,Desc1,Desc2,IsReviewed,IsFixed) 
			values ( getdate(),@aOrderID,Null, 'Push order to uni','Order not pushed',@ErrorMsg,0,0 ) 
		End	
		
		Exec QSPCanadaCommon.dbo.Send_EMail  'UniPush@qsp.com','qsp-qspfulfillment-dev@qsp.com', @EmailSubject, @ErrorMsg,'qsp-qspfulfillment-dev@qsp.com'
		
	End 

	else --if(@@rowcount = 0)
	begin

	        print @aOrderID
	
		select *  from ReportRequestBatch where batchorderid= @aOrderID	
		
		if(@@rowcount = 0)
		begin
			exec dbo.pr_ProcessDataforUnigistix @aOrderID,0,1
		end	
		else
		begin
			Declare @PDFAtQSP int
		
			select @PDFAtQSP = IsNull(IsQSPPrint,1)  from ReportRequestBatch where batchorderid= @aOrderID	
				
			DECLARE	@sqlcommand 	  	NVARCHAR(4000)
		
				

			if(@PDFAtQSP = 0)
			begin

				delete from PDFOutputUnigistix where line like '%'+cast(@aOrderID as varchar)+'%not found'
				--Merge PDF for this order
				SET @sqlcommand = 'E:\projects\paylater\apps\QSPPDFMerger.exe --db 3k117 --moveto  E:\projects\paylater\nightly\tounigistix\ --orderid ' + cast(@aOrderID as varchar)

				INSERT INTO PDFOutputUnigistix (Line)  EXEC MASTER..XP_CMDSHELL @sqlcommand
				INSERT INTO PDFOutputUnigistix (Line)  EXEC MASTER..XP_CMDSHELL @sqlcommand
				--EXEC MASTER..XP_CMDSHELL  'E:\projects\paylater\apps\QSPPDFMerger.exe --db 3k05 --moveto  E:\projects\paylater\nightly\tounigistix\ --orderid 500962'
				select * from  PDFOutputUnigistix where line like '%'+cast(@aOrderID as varchar)+'%not found'
		
				if(@@rowcount >0)
				begin
		
					declare @m varchar(200)
					select @m = 'Missing PDF '+cast(@aOrderID as varchar)
					
					exec msdb.dbo.sp_send_dbmail @profile_name='QSP IT Canada', 
                             @recipients='qsp-qspfulfillment-dev@qsp.com',
                             @subject='QSP Canada PDF Output - Unigistix',
                             @body='@m'

                    /*
					exec MASTER..XP_SMTP_SENDMAIL 
					    @FROM 	= 'ftp_output@qsp.com',
					    @TO 	= 'qsp-qspfulfillment-dev@qsp.com',
					    @priority 	= 'NORMAL',
					    @subject 	= 'QSP Canada PDF Output - Unigistix',
					    @type 	= 'text/plain',
					    @message  	= @m,
					    @server 	= 'nasmtp.us.rdigest.com'
					*/
		
				end
				else
				begin
					exec dbo.pr_ProcessDataforUnigistix @aOrderID,0,1
					update OrderStageTracking set PDFFilename='e:\projects\paylater\nightly\ToUnigistix\'+ cast(@aOrderID as varchar)+'.pdf'
							where OrderID = @aOrderID
				end
		
			 end
			 else
			 begin

				exec dbo.pr_ProcessDataforUnigistix @aOrderID,0,1
	
			 end
		
		end		
	end
	
	
	fetch next from aUniPush into @aOrderID

end


close aUniPush
deallocate aUniPush

--exec dbo.pr_PushUnigistixFiles Disable MS Sept 17, 2008
GO

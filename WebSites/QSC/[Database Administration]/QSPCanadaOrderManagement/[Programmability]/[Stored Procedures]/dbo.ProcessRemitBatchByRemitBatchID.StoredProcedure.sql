USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ProcessRemitBatchByRemitBatchID]    Script Date: 06/07/2017 09:20:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[ProcessRemitBatchByRemitBatchID]
  @tempRBid int = 293
AS
SET NOCOUNT ON

print 'ProcessRemitBatch'

declare @Date datetime,
        @ID int,
        @Country_Code CountryCode_UDDT,
        @Status int,
        @Filename varchar(200),
        @FulfillmentHouseNbr varchar(3),
        @TotalBasePrice decimal(10,2),
        @TotalUnits int,
        @TotalCatalogPrice decimal(10,2),
        @TotalItemPrice decimal(10,2),
        @TotalCHADD int,
        @TotalCancelled int,
        @error int,
        @command varchar(255),
        @path varchar(200),
        @path_zip varchar(255),
        @sqlcommand nvarchar(2048)

declare c1 cursor for
select rb.ID
  from QSPCanadaOrderManagement.dbo.RemitBatch rb
 where rb.Status = 42000 
and rb.id=@tempRBid 

--Get Date
declare @now datetime
set @now = getdate()


--Create folder for zipped files
--SET @path_zip = '\\PCI-FNP01.pci.swgao.int\SWCorpFTP$\QSPCanada\Nightly\Remit\Regeneration\' + cast(@tempRBid as varchar) + '_' + cast(datepart(YYYY,@now) as varchar)+ '-' + cast(datepart(MM,@now) as varchar)+ '-' + cast(datepart(DD,@now) as varchar)+ '_' + cast(datepart(HH,@now)as varchar)+ 'H' + cast(datepart(N,@now)as varchar) + '.zipped'
SET @path_zip = 'Q:\Projects\Paylater\Remit\Regeneration\' + cast(@tempRBid as varchar) + '_' + cast(datepart(YYYY,@now) as varchar)+ '-' + cast(datepart(MM,@now) as varchar)+ '-' + cast(datepart(DD,@now) as varchar)+ '_' + cast(datepart(HH,@now)as varchar)+ 'H' + cast(datepart(N,@now)as varchar) + '.zipped'
SET @command = 'mkdir ' + @path_zip
EXEC MASTER..XP_CMDSHELL @command

--Create output folder
--SET @path = '\\PCI-FNP01.pci.swgao.int\SWCorpFTP$\QSPCanada\Nightly\Remit\Regeneration\' + cast(@tempRBid as varchar) + '_' + cast(datepart(YYYY,@now) as varchar)+ '-' + cast(datepart(MM,@now) as varchar)+ '-' + cast(datepart(DD,@now) as varchar)+ '_' + cast(datepart(HH,@now)as varchar)+ 'H' + cast(datepart(N,@now)as varchar)
SET @path = 'Q:\Projects\Paylater\Remit\Regeneration\' + cast(@tempRBid as varchar) + '_' + cast(datepart(YYYY,@now) as varchar)+ '-' + cast(datepart(MM,@now) as varchar)+ '-' + cast(datepart(DD,@now) as varchar)+ '_' + cast(datepart(HH,@now)as varchar)+ 'H' + cast(datepart(N,@now)as varchar)
SET @command = 'mkdir ' + @path
EXEC MASTER..XP_CMDSHELL @command



open c1
fetch next from c1 into @ID

while @@fetch_status = 0
begin
    begin tran

    -- So CDS prints clear date ok - set date first KET 9/25/05
    update QSPCanadaOrderManagement.dbo.RemitBatch
       set 
	Date = GetDate(),
	[Count] = [Count] + 1   
     where ID = @ID

    exec @error = ProcessOrderFile @ID
    commit tran --jfp

    exec WriteOrderFile @ID,@path

    update QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory
       set Status = 42001
     where RemitBatchId = @ID
       and Status = 42000

    update QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory
       set Status = 42003
     where RemitBatchId = @ID
       and Status = 42002

    update QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory
       set Status = 42007
     where RemitBatchId = @ID
       and Status = 42006

    select @TotalBasePrice = sum(codrh.BasePrice),
           @TotalUnits = count(*),
           @TotalCatalogPrice = sum(CatalogPrice),
           @TotalItemPrice = sum(ItemPriceTotal)
      from QSPCanadaOrderManagement.dbo.CustomerRemitHistory crh,
           QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory codrh
     where crh.RemitBatchId = @ID
       and codrh.RemitBatchId = crh.RemitBatchId
       and codrh.CustomerRemitHistoryInstance = crh.Instance
       and codrh.status=42001

    select @TotalCancelled = count(*)
      from QSPCanadaOrderManagement.dbo.CustomerRemitHistory crh,
           QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory codrh
     where crh.RemitBatchId = @ID
       and codrh.RemitBatchId = crh.RemitBatchId
       and codrh.CustomerRemitHistoryInstance = crh.Instance
       and codrh.Status = 42002

    select @TotalCHADD = count(*)
      from QSPCanadaOrderManagement.dbo.CustomerRemitHistory crh
     where crh.RemitBatchId = @ID
       and crh.StatusInstance = 42006

    update QSPCanadaOrderManagement.dbo.RemitBatch
       set Status = 42001,
	   Date = GetDate(),
           TotalBasePrice = @TotalBasePrice,
           TotalUnits = @TotalUnits,
           TotalCHADD = @TotalCHADD,
           TotalCancelled = @TotalCancelled,
           TotalCatalogPrice = @TotalCatalogPrice,
           TotalItemPrice = @TotalItemPrice
     where ID = @ID

--    update QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory
--       set Status = 42001
--    where RemitBatchId = @ID
--       and Status = 42000

--jfp    if @error = 0
--jfp    begin
--jfp        commit tran
--jfp    end
--jfp    else
--jfp    begin
--jfp        rollback tran
--jfp        exec QSPCanadaCommon.dbo.Send_CDONTS_MAIL 'Remit Batch Process','nicolas.hamel@rd.com','Remit batch error','An error occured in the remit batch process'
--jfp    end

    fetch next from c1 into @ID
end
-- ProduceGiftCards
--exec sp_GenerateGiftCardFile  

close c1
deallocate c1

create table ##tempZip 
(id int identity,line nvarchar(1024))

insert into ##tempZip (line) values('cd C:\Program Files\Winzip')
insert into ##tempZip (line) select 'wzzip "' + @path_zip +  '/' + filename + '.zip" "' + @path +'/' + filename +'"' as line  from remitbatch rb, qspcanadaproduct..fulfillment_house fh where fh.ful_nbr = rb.fulfillmenthousenbr and rb.id =@tempRBid and totalunits >0

set @sqlcommand = 'bcp "select line from ##tempZip order by id" queryout "' + @path_zip +'\autozip.bat" -c -q -T'
exec master..xp_cmdshell @sqlcommand

set @sqlcommand = @path_zip + '\autozip.bat'
exec master..xp_cmdshell @sqlcommand
drop table ##tempZip

return
GO

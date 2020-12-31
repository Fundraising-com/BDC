USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ProcessOrderFile]    Script Date: 06/07/2017 09:20:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcessOrderFile] (@RemitBatchId int) AS

SET NOCOUNT ON

print 'ProcessOrderFile ' + cast(@RemitBatchId as varchar)

declare @FileName varchar(200),
        @SQLStatement nvarchar(MAX),
        @FulfillmentHouseName varchar(256),
        @InterfacemediaID int,
        @CountryCode varchar(2),
        @TextLine varchar(2048),
        @PtrVal varbinary(16),
        @i int,
        @error int

set @error = 0

if not exists(select * from OrderOutput where RemitBatchId = @RemitBatchId)
begin
    if @@error > @error set @error = @@error

    select @FulfillmentHouseName = fh.Ful_Name,
           @CountryCode = fh.CountryCode,
	@InterfacemediaID = InterfaceMediaID
      from QSPCanadaProduct.dbo.FULFILLMENT_HOUSE fh,
           QSPCanadaOrderManagement.dbo.RemitBatch rb
     where rb.ID = @RemitBatchId
       and fh.Ful_Nbr = rb.FulfillmentHouseNbr
    if @@error > @error set @error = @@error

    declare c2 cursor for
    select il.SQLStatement
      from QSPCanadaOrderManagement.dbo.InterfaceLayout il,
           QSPCanadaProduct.dbo.FULFILLMENT_HOUSE fh,
           QSPCanadaOrderManagement.dbo.RemitBatch rb
     where rb.ID = @RemitBatchId
       and fh.Ful_Nbr = rb.FulfillmentHouseNbr
       and il.InterfaceLayoutId = fh.InterfaceLayoutId
       and il.InterfaceMediaId = fh.InterfaceMediaId
     order by il.SequenceId
    if @@error > @error set @error = @@error



    create table ##t1 (id int not null identity, textline varchar(2048))
    if @@error > @error set @error = @@error

    open c2
    if @@error > @error set @error = @@error

    fetch next from c2 into @SQLStatement

    if @@error > @error set @error = @@error

    while @@fetch_status = 0 and @error = 0
    begin
	
        set @SQLStatement = 'insert into ##t1(textline) ' + replace(@SQLStatement, '@RemitBatchId', cast(@RemitBatchId as varchar))

        -- dumps the result in ##t1
	
        print 'exec sp_executesql @SQLStatement'
        exec sp_executesql @SQLStatement
        if @@error > @error set @error = @@error

        fetch next from c2 into @SQLStatement
        if @@error > @error set @error = @@error
    end

    close c2
    if @@error > @error set @error = @@error

    deallocate c2
    if @@error > @error set @error = @@error

    -- Update the line counters
    update ##t1 set textline = replace(textline, '?????????????', right('0000000000000' + cast(id as varchar), 13))
    if @@error > @error set @error = @@error

    -- Creates the entry for the blob
    insert into OrderOutput values (@RemitBatchId, '')
    if @@error > @error set @error = @@error

    select @PtrVal = textptr(FileContent)
      from OrderOutput
     where RemitBatchId = @RemitBatchId
    if @@error > @error set @error = @@error

    -- Fills the blob
    declare c3 cursor for
    select TextLine from ##t1 order by Id
    if @@error > @error set @error = @@error

    open c3
    if @@error > @error set @error = @@error

    fetch next from c3 into @TextLine
    if @@error > @error set @error = @@error

    while @@fetch_status = 0 and @error = 0
    begin
        set @TextLine = @TextLine + char(13) + char(10)

        select @i = i from (select i = datalength(FileContent) from OrderOutput where RemitBatchId = @RemitBatchId) xyx
        if @@error > @error set @error = @@error

        updatetext OrderOutput.FileContent @PtrVal @i 0 @TextLine
        if @@error > @error set @error = @@error

        fetch next from c3 into @TextLine
        if @@error > @error set @error = @@error
    end
 
    close c3
    if @@error > @error set @error = @@error

    deallocate c3
    if @@error > @error set @error = @@error

    drop table ##t1
    if @@error > @error set @error = @@error

    set @FileName = rtrim(@FulfillmentHouseName) +'_'+
                    right('0' + rtrim(cast(datepart(mm, getdate()) as char(2))), 2) +
                    right('0' + rtrim(cast(datepart(dd, getdate()) as char(2))), 2) +
                    cast(datepart(yyyy, getdate()) as char(4))  + 
                    right('0' + rtrim(cast(datepart(hh, getdate()) as char(2))), 2) +
                    right('0' + rtrim(cast(datepart(mi, getdate()) as char(2))), 2) + 
                    right('0' + rtrim(cast(datepart(ss, getdate()) as char(2))), 2) 

   if @InterfacemediaID = 32001
   begin
	set @FileName = @FileName + '.xls'
   end
   else if @InterfacemediaID = 32002
   begin
	set @FileName = @FileName + '.csv'
   end
   else  if @InterfacemediaID = 32003
   begin
	set @FileName = @FileName + '.txt'
   end

    update RemitBatch
       set Status = 42001,
           FileName = @FileName
     where ID = @RemitBatchId
    if @@error > @error set @error = @@error

return @error

end
GO

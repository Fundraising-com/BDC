USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[OrdersGeneration]    Script Date: 06/07/2017 09:19:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[OrdersGeneration] @RemitBatchDate datetime, @RemitBatchId int, @CountryCode int AS

declare @Filename varchar(50),
        @SelectStatement varchar(1024),
        @ViewStatement varchar(1024),
        @bcpStatement varchar(1024),
        @textline varchar(1024),
        @ptrval varbinary(16),
        @i int

select @Filename = rb.Filename,
       @SelectStatement = il.SelectStatement
  from QSPCanadaOrderManagement.dbo.InterfaceLayout il,
       QSPCanadaProduct.dbo.Fulfillment_House fh,
       QSPCanadaOrderManagement.dbo.RemitBatch rb
 where il.InterfaceLayoutId = fh.InterfaceLayoutId
   and fh.ful_nbr = rb.FulfillmentHouseNbr
   and rb.Id = @RemitBatchId

set @ViewStatement = 'create view v1 as ' + @SelectStatement
exec master..xp_cmdshell @ViewStatement

set @bcpStatement = 'bcp "v1" out "c:\' + @filename + '-c -q -U"jpaquet" -P"jpaquet"'
exec  master..xp_cmdshell @bcpStatement

insert into OrderOutput(RemitBatchId,FileContent) values(@RemitBatchId, '')

select @ptrval = textptr(FileContent)
  from OrderOutput
 where RemitBatchId = @RemitBatchId

declare c1 cursor for select text from v1
open c1
fetch next from c1 into @textline
while @@fetch_status = 0
begin
    set @textline = @textline + char(13) + char(10)

    select @i = i from (select i = datalength(FileContent) from OrderOutput where RemitBatchId = RemitBatchId) xyx
    updatetext OrderOutput.FileContent @ptrval @i 0 @textline

    fetch next from c1 into @textline
end
close c1
deallocate c1

drop view v1
GO

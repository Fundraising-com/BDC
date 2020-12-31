USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[WriteOrderFile]    Script Date: 06/07/2017 09:20:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WriteOrderFile] (@RemitBatchId int, @Path varchar(200)) AS

SET NOCOUNT ON

print 'WriteOrderFile ' + cast(@RemitBatchId as varchar)

declare @sqlcommand nvarchar(2048),
        @textline varchar(2048),
        @ptrval varbinary(16),
        @i int,
        @pos_crlf int,
        @text_length int,
        @tempstr varchar(2048),
        @filename varchar(256),
        @temptable varchar(50)

select @filename = filename
  from RemitBatch
 where ID = @RemitBatchId

set @temptable =  '##' + replace(newid(), '-', '_')
set @sqlcommand = 'create table ' + @temptable + ' (instance int identity(1,1), textline varchar(2048))'
exec sp_executesql @sqlcommand

select @text_length = datalength(filecontent)
  from OrderOutput
 where RemitBatchId = @RemitBatchId

set @i = 1

while @i < @text_length
begin
    select @tempstr  = substring(filecontent, @i, @i + 2048)
      from OrderOutput
     where RemitBatchId = @RemitBatchId -- take the next row

    set @pos_crlf = charindex(char(13) + char(10), @tempstr,0)
    set @textline = substring(@tempstr, 0, @pos_crlf)

    set @sqlcommand = 'insert into ' + @temptable + ' values(''' + replace(@textline, '''', '''''') + ''')'
    exec sp_executesql @sqlcommand

    set @i = @i + @pos_crlf + 1
end


set @sqlcommand = 'bcp "select textline from ' + @temptable + ' order by instance" queryout "' + @Path +'\' + @filename + '" -c -q -T'
exec master..xp_cmdshell @sqlcommand


set @sqlcommand = 'drop table ' + @temptable
exec sp_executesql @sqlcommand
GO

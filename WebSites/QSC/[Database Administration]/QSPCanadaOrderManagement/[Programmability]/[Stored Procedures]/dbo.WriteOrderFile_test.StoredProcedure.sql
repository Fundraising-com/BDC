USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[WriteOrderFile_test]    Script Date: 06/07/2017 09:20:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[WriteOrderFile_test] (@RemitBatchId int) AS


--just testing ....by saqib
SET NOCOUNT ON
print 'WriteOrderFile ' + cast(@RemitBatchId as varchar)

declare @sqlcommand nvarchar(1024),
        @textline varchar(1024),
        @ptrval varbinary(16),
        @i int,
        @pos_crlf int,
        @text_length int,
        @tempstr varchar(1024),
        @filename varchar(256),
        @temptable varchar(50)

select @filename = filename
  from RemitBatch
 where ID = @RemitBatchId

set @temptable =  '##' + replace(newid(), '-', '_')
set @sqlcommand = 'create table ' + @temptable + ' (textline varchar(1024))'
exec sp_executesql @sqlcommand

select @text_length = datalength(filecontent)
  from OrderOutput
 where RemitBatchId = @RemitBatchId



set @i = 1

while @i < @text_length
begin
    select @tempstr  = substring(filecontent, @i, @i + 1024)
      from OrderOutput
     where RemitBatchId = @RemitBatchId -- take the next row

    set @pos_crlf = charindex(char(13) + char(10), @tempstr,0)
    set @textline = substring(@tempstr, 0, @pos_crlf)

    set @sqlcommand = 'insert into ' + @temptable + ' values(''' + @textline + ''')'
    exec sp_executesql @sqlcommand

    set @i = @i + @pos_crlf + 1
end

set @filename = 'saqib_test.txt'

--set @sqlcommand = 'bcp "' + @temptable + '" out "c:\work\' + @filename + '" -c -q -U sshah -P sshah'
--set @sqlcommand = 'bcp "' + @temptable + '" out "c:\work\' + @filename + '" -c -q -U sshah -P sshah'

 set @sqlcommand = 'bcp "SELECT * FROM qspcanadacommon..tax"   queryout "c:\work\authors_06-30-01.txt" -U sshah -P sshah -c'


--exec master..xp_cmdshell 'bcp qspcanadacommon.[dbo].[tax] out c:\work\tax.txt -c -U "sshah" -P "sshah" '

exec master..xp_cmdshell @sqlcommand




--SELECT 'Test 5'

set @sqlcommand = 'drop table ' + @temptable
exec sp_executesql @sqlcommand
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_xml_concat]    Script Date: 06/07/2017 09:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCedure [dbo].[sp_xml_concat]
  @hdl int OUT,
  @table sysname,
  @column sysname
AS

EXEC('
SET TEXTSIZE 4000
DECLARE
  @cnt int,
  @c nvarchar(4000)
DECLARE
  @declare varchar(8000),
  @assign varchar(8000),
  @concat varchar(8000)

SELECT @c = CONVERT(nvarchar(4000),'+@column+') FROM '+@table+'

SELECT @declare = ''DECLARE'',
       @concat = '''''''''''''''',
       @assign = '''',
       @cnt = 0

WHILE (LEN(@c) > 0) BEGIN
  SELECT @declare = @declare + '' @c''+CAST(@cnt as nvarchar(15))
      +'' nvarchar(4000),'',
    @assign = @assign + ''SELECT @c''+CONVERT(nvarchar(15),@cnt)
        +''= SUBSTRING(' + @column+',''+ CONVERT(nvarchar(15),
        1+@cnt*4000)+ '', 4000) FROM '+@table+' '',
    @concat = @concat + ''+@c''+CONVERT(nvarchar(15),@cnt)
  SET @cnt = @cnt+1
  SELECT @c = CONVERT(nvarchar(4000),SUBSTRING('+@column+',
      1+@cnt*4000,4000)) FROM '+@table+'
END

IF (@cnt = 0) SET @declare = ''''
ELSE SET @declare = SUBSTRING(@declare,1,LEN(@declare)-1)

SET @concat = @concat + ''+''''''''''''''

EXEC(@declare+'' ''+@assign+'' ''+
''EXEC(
''''DECLARE @hdl_doc int
  EXEC sp_xml_preparedocument @hdl_doc OUT, ''+@concat+''
    DECLARE hdlcursor CURSOR GLOBAL FOR SELECT @hdl_doc AS
        DocHandle'''')''
)
')
OPEN hdlcursor
FETCH hdlcursor INTO @hdl
DEALLOCATE hdlcursor
GO

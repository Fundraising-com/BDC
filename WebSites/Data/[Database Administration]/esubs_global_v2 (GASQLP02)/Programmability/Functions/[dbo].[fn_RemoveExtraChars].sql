USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_RemoveExtraChars]    Script Date: 03/11/2015 14:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[fn_RemoveExtraChars] (@NAME nvarchar(50))
RETURNS nvarchar(50)
AS
BEGIN
  declare @TempString nvarchar(100)
  set @TempString = @NAME 
  --set @TempString = LOWER(@TempString)
  --set @TempString =  replace(@TempString,' ', '')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'À', 'A')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'Ç', 'C')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'â', 'a')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'È', 'E')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'É', 'E')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'Ê', 'E')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'ë', 'e')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'Î', 'I')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'Ô', 'O')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'ô', 'o')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'Ù', 'U')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'ù', 'u')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'Û', 'U')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'û', 'u')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'à', 'a')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'è', 'e')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'é', 'e')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'ê', 'e')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'ì', 'i')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'ò', 'o')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'ù', 'u')
  set @TempString =  replace(@TempString COLLATE Latin1_General_CS_AS,'ç', 'c')
  set @TempString =  replace(@TempString,'''', '')
  set @TempString =  replace(@TempString,'`', '')
  --set @TempString =  replace(@TempString,'-', '')
  return @TempString
END

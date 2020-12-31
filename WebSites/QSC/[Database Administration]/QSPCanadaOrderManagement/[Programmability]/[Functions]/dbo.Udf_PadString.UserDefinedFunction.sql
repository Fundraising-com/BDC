USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[Udf_PadString]    Script Date: 06/07/2017 09:21:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE Function [dbo].[Udf_PadString](@InputString Varchar(128),@PadChar Char(1), @PadLength Int, @PadTo Char(1))
Returns Varchar(512)
As
Begin

Declare @OutPutString Varchar(512)
Set @OutPutString =''

If Upper(@PadTo) ='L' 
Begin
Set @OutPutString =Right(Replicate(@PadChar,@PadLength)+@InputString,@PadLength)
End


If Upper(@PadTo) ='R' 
Begin
Set @OutPutString =Left(@InputString+Replicate(@PadChar,@PadLength),@PadLength)
End


If Upper(@PadTo) Not In ('R' , 'L' )
Begin
Set @OutPutString = NULL
End	

Return @OutPutString

End
GO

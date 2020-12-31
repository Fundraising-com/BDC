USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[SplitElementFromList]    Script Date: 06/07/2017 09:20:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[SplitElementFromList]  @List Varchar(8000)  Output, @Element Varchar(4000) Output , @Seperator Varchar(10), @SeperatorLength Int 

  AS
	Declare @ActualSeperatorLength Int,
		@SeperatorPosition 	 Int

	If IsNull(@SeperatorLength,0) =0
	Begin
		Set @ActualSeperatorLength = Len(@Seperator)
	End
	Else
	Begin
		Set @ActualSeperatorLength =@SeperatorLength
	End 

	Set @SeperatorPosition = CharIndex(@Seperator,@List)

	If IsNull(@SeperatorPosition ,0) <=0
	Begin
		Set @SeperatorPosition =Len(@List)+1
	End

	Set @Element = Substring(@List,1,@SeperatorPosition - 1)
	Set @List = Substring(@List,(@SeperatorPosition + @ActualSeperatorLength), Len(@List))
GO

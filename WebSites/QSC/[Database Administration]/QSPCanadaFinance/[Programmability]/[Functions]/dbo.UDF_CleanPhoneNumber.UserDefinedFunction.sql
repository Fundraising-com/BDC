USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_CleanPhoneNumber]    Script Date: 06/07/2017 09:17:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_CleanPhoneNumber]         (	   @PNo Varchar(20)  , @seperateBy varchar(1)   )

RETURNS Varchar(20)  AS  

BEGIN 


Declare @CleanNo varchar(20)
Declare @Nolength int
Declare @cnt int
Declare @Char varchar(1)
Declare @FormatedNo Varchar(20)
Declare @isTollFree Varchar(1)

Set @isTollFree ='N'

Set @cnt=1
Select @Nolength = Len(@Pno) 
While  @Nolength > 0
Begin
	Select @Char=Substring(@Pno,@cnt,1)
	If ASCII(@Char) between 48 and 57
	Begin
	   Set @CleanNo=IsNull(@CleanNo,'')+Substring(@Pno,@cnt,1)
	End
Set @NoLength=@NoLength-1
SET @cnt=@cnt+1
End
If IsNull(@seperateBy ,'')=''
Begin
   Set @FormatedNo = @CleanNo
End
Else
Begin
	-- If Tol free
	If Substring(@CleanNo,1,2) ='18'
	Begin
		Set @FormatedNo = Substring(@CleanNo,1,1)+@seperateBy+Substring(@CleanNo,2,3)+@seperateBy+Substring(@CleanNo,5,3)+@seperateBy+Substring(@CleanNo,8,4)
	End 
	Else
	Begin
	Set @FormatedNo = Substring(@CleanNo,1,3)+@seperateBy+Substring(@CleanNo,4,3)+@seperateBy+Substring(@CleanNo,7,4)
	End
End

  RETURN @FormatedNo
  
END
GO

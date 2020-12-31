USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[LogGLError]    Script Date: 06/07/2017 09:17:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[LogGLError]   @TransType Varchar(10), 
					@TransId Varchar(10),
					@Message Varchar(200)

As
Declare  @ErrorDateTime DateTime

Begin
	
	Set @ErrorDateTime = GetDate()
	If @Message is Not Null
	Begin
		Insert into QSPCanadaFinance.dbo.GL_ErrorLog Values (@TransType,@TransId,@Message,@ErrorDateTime)
	End
	
	
End
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterBatchUpdateStatus]    Script Date: 06/07/2017 09:20:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SwitchLetterBatchUpdateStatus]
	@BatchID int,
	@IsLocked int,
	@IsPrinted int
AS

--Saqib- April 2006
--update the Switch letter isLocked status to Open/unlocked  for selected batch from switchletter.aspx
-- please dont assign code 9999 to any variable in this proc as application is sneding that instead of null


IF @IsLocked = 1

 Begin
	Update  dbo.SwitchLetterBatch
	Set IsLocked  = 1
	Where Instance  = @BatchID
 End


IF @IsPrinted = 1 

Begin

 Update  dbo.SwitchLetterBatch
 Set IsPrinted = 1 , DatePrinted = GetDate()
 Where Instance  = @BatchID
 and isLocked  = 1

End
GO

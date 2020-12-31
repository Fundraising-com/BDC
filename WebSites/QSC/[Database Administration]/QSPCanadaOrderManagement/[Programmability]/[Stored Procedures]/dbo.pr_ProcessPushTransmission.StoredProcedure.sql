USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProcessPushTransmission]    Script Date: 06/07/2017 09:20:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
**  
*/
CREATE  PROCEDURE [dbo].[pr_ProcessPushTransmission]
	@RunID int
 AS
	set nocount on
	declare @TransID int
	-- Log the transmission attemp
	insert TransmissionLog
	(
		TransmissionDate
	)
	select GetDate()

	select @TransID = Scope_Identity()



	select  substring(line, patindex('%=>%',line)+2, patindex('%<=%',line)-(patindex('%=>%',line)+2))as FName into
		#FilesTransmitted
		FROM FTPOutputUnigistix  where   Line Like 'Uploaded file =>%' and RunID= @RunID	

	update OrderStageTracking set TransmissionID=@TransID, Stage = 59006
		 from #FilesTransmitted, OrderStageTracking where FName=BatchFileName
GO

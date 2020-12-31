USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[UpdateEnvelope]    Script Date: 06/07/2017 09:20:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateEnvelope] 
	@lInstance int,
	@aOrderBatchDate datetime,
	@lOrderBatchID int,
	@lTeacherInstance int,
	@dReportedEnvelopeAmount numeric,
	@aDateCreated datetime,
	@zUserIDCreated varchar(4),
	@DateChanged datetime,
	@zUserIDChanged varchar(4)

AS

Update envelope Set 
TeacherInstance = @lTeacherInstance,
ReportedEnvelopeAmount = @dReportedEnvelopeAmount,
DateChanged = @DateChanged ,
UserIDChanged = @zUserIDChanged
where Instance = @lInstance
GO

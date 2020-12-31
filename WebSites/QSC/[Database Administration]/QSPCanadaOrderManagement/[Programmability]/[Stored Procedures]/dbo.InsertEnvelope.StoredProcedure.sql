USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[InsertEnvelope]    Script Date: 06/07/2017 09:19:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[InsertEnvelope] 
	@lInstance int,
	@aOrderBatchDate datetime,
	@lOrderBatchID int,
	@lTeacherInstance int,
	@dReportedEnvelopAmount numeric,
	@aDateCreated datetime,
	@zUserIDCreated varchar(4),
	@DateChanged datetime,
	@zUserIDChanged varchar(4),
	@lReportedNumberOfOrderForms int,
	@zIsIncentive varchar(1)

AS

insert into Envelope values
(@lInstance,@aOrderBatchDate,@lOrderBatchID,@lTeacherInstance,
@dReportedEnvelopAmount,@aDateCreated,
@zUserIDCreated,@DateChanged,@zUserIDChanged, @lReportedNumberOfOrderForms,@zIsIncentive)

insert into EnvelopeTest values
(@lInstance,@aOrderBatchDate,@lOrderBatchID,@lTeacherInstance,
@dReportedEnvelopAmount,@aDateCreated,
@zUserIDCreated,@DateChanged,@zUserIDChanged, @lReportedNumberOfOrderForms,@zIsIncentive)
GO

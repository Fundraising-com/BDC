USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetEnvelopeByInstance]    Script Date: 06/07/2017 09:19:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetEnvelopeByInstance] 
	@instance int
AS

Select Instance,OrderBatchDate, OrderBatchID, TeacherInstance,
ReportedEnvelopeAmount,DateCreated,
UserIDCreated, DateChanged, UserIDChanged
from Envelope where Instance = @instance
GO

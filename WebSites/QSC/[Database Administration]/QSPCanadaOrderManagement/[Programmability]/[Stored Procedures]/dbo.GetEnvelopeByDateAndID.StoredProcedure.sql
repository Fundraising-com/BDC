USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetEnvelopeByDateAndID]    Script Date: 06/07/2017 09:19:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetEnvelopeByDateAndID] 
	@batchdate datetime,
	@batchid int
AS

Select * from Envelope where OrderBatchDate = @batchdate and OrderBatchID = @batchid
GO

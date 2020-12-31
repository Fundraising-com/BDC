USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_AdjustmentBatch_UpdateStatus]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_AdjustmentBatch_UpdateStatus]

	@iID			int,
	@iStatus		int,
	@iUserID		int

AS

UPDATE	AdjustmentBatch
SET		Status = @iStatus,
		ChangeUserID = @iUserID,
		ChangeDate = GetDate()
WHERE	ID = @iID
GO

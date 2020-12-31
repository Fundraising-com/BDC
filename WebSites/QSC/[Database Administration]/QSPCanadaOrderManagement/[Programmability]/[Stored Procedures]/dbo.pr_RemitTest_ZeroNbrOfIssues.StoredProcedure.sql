USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_ZeroNbrOfIssues]    Script Date: 06/07/2017 09:20:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_ZeroNbrOfIssues]

@iRunID		int = 0

AS

SELECT		codrh.*
FROM		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb
WHERE		rb.ID = codrh.RemitBatchID
AND		codrh.Status IN (42000, 42001)
AND		(codrh.NumberOfIssues = 0
OR		codrh.Quantity = 0)
AND		rb.RunID = @iRunID
GO

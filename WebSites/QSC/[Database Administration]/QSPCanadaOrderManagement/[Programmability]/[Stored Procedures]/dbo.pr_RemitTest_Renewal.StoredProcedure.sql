USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Renewal]    Script Date: 06/07/2017 09:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pr_RemitTest_Renewal]

@iRunID		int = 0

AS

IF EXISTS
(
	SELECT		codrh.*
	FROM		CustomerOrderDetailRemitHistory codrh,
			RemitBatch rb
	WHERE		rb.ID = codrh.RemitBatchID
	AND		(codrh.Renewal is null
	OR		codrh.Renewal NOT IN ('R', 'N'))
	AND		rb.RunID = @iRunID
)
	SELECT 1
ELSE
	SELECT 0
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Renewal_Fix]    Script Date: 06/07/2017 09:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pr_RemitTest_Renewal_Fix]

@iRunID		int = 0

AS

UPDATE		codrh
SET		codrh.Renewal = 'N'
FROM		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb
WHERE		rb.ID = codrh.RemitBatchID
AND		(codrh.Renewal is null
OR		codrh.Renewal NOT IN ('R', 'N'))
AND		rb.RunID = @iRunID
GO

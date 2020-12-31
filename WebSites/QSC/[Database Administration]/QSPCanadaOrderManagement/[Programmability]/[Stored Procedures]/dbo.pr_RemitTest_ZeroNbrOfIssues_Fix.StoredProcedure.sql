USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_ZeroNbrOfIssues_Fix]    Script Date: 06/07/2017 09:20:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_ZeroNbrOfIssues_Fix]

@iRunID		int = 0

AS

SELECT		crh.Instance AS CustomerRemitHistoryInstance,
		codrh.CustomerOrderHeaderInstance,
		codrh.TransID

INTO 		#ZeroNbrOfIssues

FROM		CustomerOrderDetailRemitHistory codrh,
		CustomerRemitHistory crh,
		RemitBatch rb
WHERE		rb.ID = codrh.RemitBatchID
AND		codrh.CustomerRemitHistoryInstance = crh.Instance
AND		codrh.Status IN (42000, 42001)
AND		(codrh.NumberOfIssues = 0
OR		codrh.Quantity = 0)
AND		rb.RunID = @iRunID


DELETE		codrh
FROM		CustomerOrderDetailRemitHistory codrh,
		#ZeroNbrOfIssues zi
WHERE		zi.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		zi.TransID = codrh.TransID


DELETE		crh
FROM		CustomerRemitHistory crh,
		#ZeroNbrOfIssues zi
WHERE		zi.CustomerRemitHistoryInstance = crh.Instance


UPDATE	cod
SET		cod.StatusInstance = 502
FROM		CustomerOrderDetail cod,
		#ZeroNbrOfIssues zi
WHERE	zi.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
AND		zi.TransID = cod.TransID


DROP TABLE	#ZeroNbrOfIssues
GO

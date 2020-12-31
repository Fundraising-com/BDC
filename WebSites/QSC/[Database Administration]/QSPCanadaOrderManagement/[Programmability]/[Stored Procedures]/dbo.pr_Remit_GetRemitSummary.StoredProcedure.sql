USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Remit_GetRemitSummary]    Script Date: 06/07/2017 09:20:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---  What's about to go
CREATE  PROCEDURE [dbo].[pr_Remit_GetRemitSummary]

@iRunID		int = 0

AS

SELECT		cd.Description,
		COUNT(*) AS NumberOfOrders
FROM		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb,
		CustomerOrderHeader coh,
		Batch b,
		QSPCanadaCommon..CodeDetail cd
WHERE		codrh.CustomerOrderHeaderInstance = coh.Instance
AND		codrh.Status IN (42000, 42001)
AND		codrh.RemitBatchID = rb.ID
AND		coh.OrderBatchDate = b.Date
AND		coh.OrderBatchID = b.ID
AND		b.OrderQualifierID = cd.Instance
AND		rb.RunID = @iRunID
GROUP BY	cd.Description
ORDER BY	COUNT(*) DESC
GO

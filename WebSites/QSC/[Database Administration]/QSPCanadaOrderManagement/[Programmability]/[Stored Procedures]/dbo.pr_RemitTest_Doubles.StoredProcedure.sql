USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Doubles]    Script Date: 06/07/2017 09:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_Doubles]

@iRunID		int

AS
DECLARE @NumberOfDuplicates int

SELECT @NumberOfDuplicates = COUNT(*) 
FROM
(
	SELECT		coh.Instance,
			b.OrderID,
			crh.LastName,
			crh.FirstName,
			codrh.RemitCode, -- TitleCode was changed to RemitCode 
			cd.Description,
			COUNT(*) AS NumberOfCopies
	FROM		CustomerOrderDetailRemitHistory codrh, 
			CustomerRemitHistory crh,
			RemitBatch rb,
			Batch b,
			CustomerOrderHeader coh,
			QSPCanadaCommon..CodeDetail cd
	WHERE		codrh.Status IN (42000, 42001)
	AND		codrh.RemitBatchID = rb.ID
	AND		codrh.CustomerRemitHistoryInstance = crh.Instance
	AND		coh.OrderBatchDate = b.Date
	AND		coh.OrderBatchID = b.ID
	AND		b.OrderQualifierID = cd.Instance
	AND		codrh.CustomerOrderHeaderInstance = coh.Instance
	AND		rb.RunID = @iRunID
	GROUP BY	coh.Instance,
			b.OrderID,
			crh.LastName,
			crh.FirstName,
			codrh.RemitCode, -- TitleCode was changed to RemitCode 
			cd.Description
	HAVING		COUNT(*) > 1
) AS D

IF @NumberOfDuplicates >= 1000 
	SELECT 1
ELSE 
	SELECT 0
GO

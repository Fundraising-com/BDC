USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_PostalCode]    Script Date: 06/07/2017 09:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_PostalCode]

@iRunID		int = 0

AS

-- Postal code format verification
SELECT		rb.RunID,
		cd.Description,
		codrh.CustomerOrderHeaderInstance,
		codrh.TransID,
		codrh.Status,
		crh.LastName,
		crh.FirstName,
		crh.Address1,
		COALESCE(crh.Address2, '') AS Address2,
		crh.City,
		crh.State,
		crh.Zip,
		rb.FileName
FROM		CustomerRemitHistory crh,
		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb,
		CustomerOrderHeader coh,
		Batch b,
		QSPCanadaCommon..CodeDetail cd
WHERE		codrh.CustomerRemitHistoryInstance = crh.Instance
AND		rb.ID = codrh.RemitBatchID
AND		coh.Instance = codrh.CustomerOrderHeaderInstance
AND		b.ID = coh.OrderBatchID
AND		b.Date = coh.OrderBatchDate
AND		cd.Instance = b.OrderQualifierID
AND		codrh.Status NOT IN (42004, 42010)
AND		(LEN(RTRIM(crh.Zip)) <> 6
OR		NOT crh.Zip LIKE '[A-Z][0-9][A-Z][0-9][A-Z][0-9]')

AND		rb.RunID = @iRunID

ORDER BY	rb.RunID,
		rb.FileName,
		codrh.CustomerOrderHeaderInstance,
		codrh.TransID
GO

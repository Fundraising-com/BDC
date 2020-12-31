USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_PostalCode_Fix]    Script Date: 06/07/2017 09:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_PostalCode_Fix]

@iRunID		int = 0

AS

SELECT		crh.Instance AS CustomerRemitHistoryInstance,
		codrh.CustomerOrderHeaderInstance,
		codrh.TransID
INTO		#BadPostal
FROM		CustomerRemitHistory crh,
		CustomerOrderDetailRemitHistory codrh,
		RemitBatch rb
WHERE		codrh.CustomerRemitHistoryInstance = crh.Instance
AND		rb.ID = codrh.RemitBatchID
AND		codrh.Status NOT IN (42004, 42010)
AND		(LEN(RTRIM(crh.Zip)) <> 6
OR		NOT crh.Zip LIKE '[A-Z][0-9][A-Z][0-9][A-Z][0-9]')

AND		rb.RunID = @iRunID


DELETE		codrh
FROM		CustomerOrderDetailRemitHistory codrh,
		#BadPostal bp
WHERE		bp.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		bp.TransID = codrh.TransID


DELETE		crh
FROM		CustomerRemitHistory crh,
		#BadPostal bp
WHERE		bp.CustomerRemitHistoryInstance = crh.Instance


UPDATE	cod
SET		cod.StatusInstance = 502
FROM		CustomerOrderDetail cod,
		#BadPostal bp
WHERE	bp.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
AND		bp.TransID = cod.TransID

DROP TABLE	#BadPostal
GO

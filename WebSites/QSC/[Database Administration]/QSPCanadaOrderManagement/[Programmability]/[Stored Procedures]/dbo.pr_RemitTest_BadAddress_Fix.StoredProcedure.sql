USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_BadAddress_Fix]    Script Date: 06/07/2017 09:20:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_RemitTest_BadAddress_Fix]

@iRunID		int

AS

SELECT		crh.Instance AS CustomerRemitHistoryInstance,
		codrh.CustomerOrderHeaderInstance,
		codrh.TransID
INTO		#BadProvinces
FROM		CustomerOrderDetailRemitHistory codrh, 
		RemitBatch rb,
		CustomerRemitHistory crh,
		CustomerOrderHeader coh
WHERE		codrh.Status IN (42000, 42001)
AND		codrh.RemitBatchID = rb.ID
AND		rb.RunID = @iRunID
AND		codrh.CustomerRemitHistoryInstance = crh.Instance
AND		coh.Instance = codrh.CustomerOrderHeaderInstance
AND		crh.State NOT IN
		(SELECT		Province
		FROM		QspCanadaCommon..TaxRegionProvince)


DELETE		codrh
FROM		CustomerOrderDetailRemitHistory codrh,
		#BadProvinces bp
WHERE		bp.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		bp.TransID = codrh.TransID


DELETE		crh
FROM		CustomerRemitHistory crh,
		#BadProvinces bp
WHERE		bp.CustomerRemitHistoryInstance = crh.Instance


UPDATE	cod
SET		cod.StatusInstance = 502
FROM		CustomerOrderDetail cod,
		#BadProvinces bp
WHERE	bp.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
AND		bp.TransID = cod.TransID


DROP TABLE	#BadProvinces
GO

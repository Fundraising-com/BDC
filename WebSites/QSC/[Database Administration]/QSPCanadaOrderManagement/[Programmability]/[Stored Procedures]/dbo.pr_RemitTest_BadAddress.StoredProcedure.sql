USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_BadAddress]    Script Date: 06/07/2017 09:20:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_RemitTest_BadAddress]

@iRunID		int

AS

SELECT	

cdQualifier.Description AS Qualifier,
coh.Instance,
codrh.TransID,
crh.RemitBatchID,
crh.Instance,
crh.CustomerInstance,
cdStatus.Description AS Status,
crh.LastName,
crh.FirstName,
COALESCE(crh.Address1, '') AS Address1,
COALESCE(crh.Address2, '') AS Address2,
COALESCE(crh.City, '') AS City,
COALESCE(crh.State, '') AS State,
COALESCE(crh.Zip, '') AS Zip,
crh.ZipPlusFour,
crh.DateModified,
crh.UserIDModified

FROM		CustomerOrderDetailRemitHistory codrh, 
		RemitBatch rb,
		CustomerRemitHistory crh,
		CustomerOrderHeader coh,
		Batch b,
		CodeDetail cdQualifier,
		CodeDetail cdStatus
WHERE		codrh.Status IN (42000, 42001, 42006)
AND		codrh.RemitBatchID = rb.ID
AND		rb.RunID = @iRunID
AND		codrh.CustomerRemitHistoryInstance = crh.Instance
AND		coh.Instance = codrh.CustomerOrderHeaderInstance
AND		b.ID = coh.OrderBatchID
AND		b.Date = coh.OrderBatchDate
AND		cdQualifier.Instance = b.OrderQualifierID
AND		cdStatus.Instance = codrh.Status
AND		crh.State NOT IN
		(SELECT		Province
		FROM		QspCanadaCommon..TaxRegionProvince)
GO

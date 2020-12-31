USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetailRemitHistory_SelectMissingRecords]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetailRemitHistory_SelectMissingRecords]
		
AS

SELECT		codrh.*,
			crh.*
FROM		CustomerOrderDetailRemitHistory codrh
LEFT JOIN	CustomerRemitHistory crh
				ON	crh.Instance = codrh.CustomerRemitHistoryInstance
				AND	codrh.RemitBatchID = crh.RemitBatchID --if not the same it will not remit
JOIN		QSPCanadaCommon.dbo.Season s
				ON	GETDATE() BETWEEN s.StartDate AND s.EndDate
				AND	s.Season IN ('F', 'S')
				AND	codrh.DateChanged BETWEEN s.StartDate AND s.EndDate
WHERE		crh.Instance IS NULL

UNION ALL

SELECT		codrh.*,
			crh.*
FROM		CustomerRemitHistory crh
LEFT JOIN	CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerRemitHistoryInstance = crh.Instance
				AND	codrh.RemitBatchID = crh.RemitBatchID --if not the same it will not remit
JOIN		QSPCanadaCommon.dbo.Season s
				ON	GETDATE() BETWEEN s.StartDate AND s.EndDate
				AND	s.Season IN ('F', 'S')
				AND	crh.DateModified BETWEEN s.StartDate AND s.EndDate
WHERE		codrh.CustomerRemitHistoryInstance IS NULL
AND			crh.DateModified < DATEADD(mi, -1, GETDATE())
ORDER BY	codrh.CustomerOrderHeaderInstance,
			codrh.TransID,
			crh.Instance
GO

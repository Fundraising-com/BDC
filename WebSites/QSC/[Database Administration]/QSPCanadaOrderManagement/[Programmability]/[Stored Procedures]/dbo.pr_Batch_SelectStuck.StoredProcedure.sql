USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Batch_SelectStuck]    Script Date: 06/07/2017 09:19:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Batch_SelectStuck]

AS

SELECT	b.*
FROM	Batch b
JOIN	QSPCanadaCommon.dbo.Season s
			ON	GetDate() BETWEEN s.StartDate AND s.EndDate
			AND	s.Season IN ('Y')
			AND	b.[Date] BETWEEN s.StartDate AND s.EndDate
WHERE	(ISNULL(b.StatusInstance, 40001) in (40001,	--New
											40002,	--In Process
											40003,	--Under Review
											40004,  --Approved
											40006,	--CC Pending
											40009)  --Pickable	
AND		b.[Date] <= DATEADD(DAY, -5, GETDATE()))
--OR		(b.Statusinstance = 40010 AND b.[Date] <= DATEADD(DAY, -15, GETDATE()))
AND		b.OrderID NOT IN (

	SELECT DISTINCT  
		 A.OrderId
	FROM
		Batch A,
		CustomerOrderHeader B,
		CustomerOrderDetail C,
		BatchDistributionCenter D,
		QspCanadaCommon.dbo.Campaign  ca 
	WHERE
		D.StatusInstance IN (40010,40014)
		and a.campaignid  = ca.id
		and A.StatusInstance not in (40013,40005)
		and A.Date >='7/1/05'
		and A.Date = B.OrderBatchDate
		and A.ID = B.OrderBatchID
		and D.BatchDate = Date
		and D.BatchID = A.ID
		and C.StatusInstance not in (508, 513)
		and C.delFlag <> 1
		and B.Instance = C.CustomerorderHeaderInstance
		and C.DistributionCenterID in (1)

)
GO

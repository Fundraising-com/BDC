USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[OrderStageReceipt_SelectMissing]    Script Date: 06/07/2017 09:19:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[OrderStageReceipt_SelectMissing]

AS

SELECT		osrExist.OrderID,
			osrExist.CreateDate AS LatestOrderStageReceiptCreateDate,
			stageDesc.Description AS LatestOrderStageReceiptStageID
FROM		OrderStageReceipt osrExist
JOIN		QSPCanadaCommon..CodeDetail stageDesc
				ON	stageDesc.Instance = osrExist.StageID
LEFT JOIN	OrderStageReceipt osrComplete
				ON	osrComplete.OrderID = osrExist.OrderID
				AND	osrComplete.StageID >= 59005
				AND	osrComplete.StatusID = 2 --2: Processed
LEFT JOIN	Batch b
				ON	b.OrderID = osrExist.OrderID
WHERE		ISNULL(osrExist.OrderID, 0) > 0
			AND	osrExist.CreateDate < DATEADD(DAY, -5, GETDATE())
			AND	osrExist.StatusID = 2 --2: Processed
			AND	osrExist.StageID = (SELECT	MAX(osrMax.StageID)
									FROM	OrderStageReceipt osrMax
									WHERE	osrMax.OrderID = osrExist.OrderID)
AND			osrComplete.OrderStageReceiptID IS NULL
AND			b.OrderID IS NULL
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Batch_SelectMissingFakeLandedOrders]    Script Date: 06/07/2017 09:19:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Batch_SelectMissingFakeLandedOrders]

AS

SELECT		DISTINCT
			camp.ID CampaignID,
			campStat.Description CampaignStatus,
			camp.StartDate,
			camp.EndDate,
			camp.CookieDoughDeliveryDate,
			fm.Firstname + ' ' + fm.Lastname FM,
			QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign(camp.ID) Programs
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CodeDetail campStat ON campStat.Instance = camp.Status
JOIN		QSPCanadaCommon..CampaignProgram cp ON cp.CampaignID = camp.ID AND cp.DeletedTF = 0
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = camp.FMID
LEFT JOIN	Batch bL ON bL.CampaignID = camp.ID AND bL.OrderQualifierID = 39001
LEFT JOIN	(Batch bO
				JOIN	CustomerOrderHeader coh ON coh.OrderBatchID = bO.ID AND coh.OrderBatchDate = bO.Date
				JOIN	CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = coh.Instance
												AND cod.DistributionCenterID = 1 AND bO.OrderQualifierID = 39009 AND cod.IsShippedToAccount = 1
												AND cod.StatusInstance NOT IN (501, 506, 508)
			) ON bO.Campaignid = camp.ID
WHERE		camp.StartDate >= '2016-07-01'
AND			camp.EndDate <= GETDATE()
AND			camp.OnlineOnlyPrograms = 1
AND			bL.OrderID IS NULL
AND			(bO.OrderID IS NOT NULL OR (cp.programid in (42) AND camp.Status IN (37002)))
ORDER BY	camp.EndDate
GO

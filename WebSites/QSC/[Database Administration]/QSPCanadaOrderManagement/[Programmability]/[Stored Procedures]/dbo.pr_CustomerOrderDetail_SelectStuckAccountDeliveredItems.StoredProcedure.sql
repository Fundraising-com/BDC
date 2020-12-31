USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetail_SelectStuckAccountDeliveredItems]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetail_SelectStuckAccountDeliveredItems]

AS

SELECT		CASE
				WHEN camp.Status NOT IN (37002) THEN 'Campaign Cancelled'
				WHEN camp.EndDate <= DATEADD(dd, -14, GETDATE()) AND bl2.OrderID IS NULL THEN 'Landed Order Not Arrived and Campaign Ended'
				WHEN bL.OrderID IS NOT NULL THEN 'Landed Order Already Shipped'
			END AS 'Issue',
			b.AccountID,
			b.CampaignID,
			camp.StartDate CampaignStartDate,
			camp.EndDate CampaignEndDate,
			bL.OrderID LandedOrderID,
			b.OrderID OnlineOrderID,
			b.Date OnlineOrderDate,
			cod.CustomerOrderHeaderInstance,
			cod.TransID,
			cod.ProductCode,
			cod.ProductName,
			cod.Quantity,
			cod.Price,
			cod.StatusInstance,
			cod.Recipient,
			s.FirstName + ' ' + s.LastName StudentName
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
LEFT JOIN	Batch bL ON bL.CampaignID = b.CampaignID AND bL.OrderQualifierID IN (39001) AND bL.ID < 20000 AND bL.StatusInstance IN (40013)
LEFT JOIN	Batch bL2 ON bL2.CampaignID = b.CampaignID AND bL2.OrderQualifierID IN (39001) AND bL2.ID < 20000
LEFT JOIN	Student s ON s.Instance = coh.StudentInstance
WHERE		cod.IsShippedToAccount = 1
AND			b.OrderQualifierID = 39009
AND			cod.StatusInstance NOT IN (506, 508)
AND			cod.DelFlag = 0
AND			(bL.OrderID IS NOT NULL OR camp.Status NOT IN (37002) OR (camp.EndDate <= DATEADD(dd, -14, GETDATE()) AND bl2.OrderID IS NULL))
ORDER BY	camp.EndDate,
			coh.Instance
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_InternetOrder_SelectMissing]    Script Date: 06/07/2017 09:20:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InternetOrder_SelectMissing]

AS

DECLARE	@CurrentFYStartDate	DATETIME

SELECT	@CurrentFYStartDate = seas.StartDate
FROM	QSPCanadaCommon..Season seas
WHERE	GETDATE() BETWEEN seas.StartDate AND seas.EndDate
AND		seas.Season IN ('Y')

PRINT @CurrentFYStartDate

DECLARE	@ToDate	DATETIME
SET @ToDate = DATEADD(DAY, -4, GETDATE())
PRINT @ToDate

SELECT		cart.EDS_Order_ID AS InternetOrderID,
			s.Source_Name AS OrderType,
			o.Order_Date AS OrderDate,
			o.Order_Status_ID,
			os.Order_Status_Name,
			camp.Fulf_Campaign_ID CampaignID,
			acc.Fulf_Account_ID AccountID,
			b.OrderID AS LandedOrderID,
			b.DateCreated AS LandedOrderDate
FROM		QSPFulfillment.dbo.[ORDER] o
JOIN		QSPFulfillment.dbo.Campaign camp
				ON	camp.Campaign_ID = o.Campaign_ID
JOIN		QSPFulfillment.dbo.Account acc
				ON	acc.Account_ID = camp.Account_ID
JOIN		QSPEcommerce.dbo.Cart cart
				ON	cart.X_Order_ID = o.Order_ID
JOIN		QSPFulfillment.dbo.Source s
				ON	o.Source_ID = s.Source_ID
JOIN		QSPFulfillment.dbo.Order_Status os
				ON	os.Order_Status_ID = o.Order_Status_ID
LEFT JOIN	InternetOrderID ioi
				ON	ioi.InternetOrderID = cart.Eds_Order_ID
LEFT JOIN	Batch b
				ON	b.CampaignID = camp.Fulf_Campaign_ID
				AND	b.OrderQualifierID = 39001
				AND	b.StatusInstance NOT IN (40005)
WHERE		o.Order_Date BETWEEN @CurrentFYStartDate AND @ToDate
AND			o.Order_Status_ID IN (101, 201, 301, 304, 401, 501, 601, 701)
AND			s.Source_Group_ID = 3
AND			ioi.InternetOrderID IS NULL
ORDER BY	o.Order_Date
GO

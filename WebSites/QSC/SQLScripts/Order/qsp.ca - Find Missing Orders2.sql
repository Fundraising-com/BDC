USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetail_SelectMissingOnlineSubs]    Script Date: 11/03/2008 14:40:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[pr_InternetOrder_SelectMissing]  
  
AS  
  
CREATE TABLE [#goodOrders](
	[InternetOrderID] [int] NOT NULL)  
  
INSERT INTO [#goodOrders]  
   ([InternetOrderID])  
SELECT	DISTINCT ioi.InternetOrderID
FROM	CustomerOrderDetail cod
JOIN	InternetOrderID ioi
			ON	ioi.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
JOIN	QSPCanadaCommon.dbo.Season seas
			ON	GetDate() BETWEEN seas.StartDate AND seas.EndDate
			AND	seas.Season <> 'Y'
WHERE	cod.CreationDate BETWEEN seas.StartDate AND seas.EndDate
AND		cod.DelFlag <> 1

SELECT		cart.EDS_Order_ID AS InternetOrderID,
			s.Source_Name AS OrderType,
			o.Order_Date AS OrderDate
FROM		COM_OLTP1.QSPFulfillment.dbo.[ORDER] o
JOIN		COM_OLTP1.QSPEcommerce.dbo.Cart cart
				ON	cart.X_Order_ID = o.Order_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Source s
				ON	o.Source_ID = s.Source_ID
JOIN		QSPCanadaCommon.dbo.Season seas
				ON	GetDate() BETWEEN seas.StartDate AND seas.EndDate
				AND	seas.Season <> 'Y'
				AND	o.Order_Date BETWEEN seas.StartDate AND seas.EndDate
LEFT JOIN	#goodOrders go
				ON	go.InternetOrderID = cart.Eds_Order_ID
WHERE		o.Order_Date <= DATEADD(day, 2, getDate())
AND			o.Order_Status_ID IN (201, 301, 401, 501, 601, 701)
AND			s.Source_Group_ID = 3
AND			go.InternetOrderID IS NULL
ORDER BY	o.Order_Date

DROP TABLE #goodSubs

GO
GRANT EXECUTE ON [dbo].[pr_CustomerOrderDetail_SelectMissingOnlineSubs] TO [PROC_EXEC]
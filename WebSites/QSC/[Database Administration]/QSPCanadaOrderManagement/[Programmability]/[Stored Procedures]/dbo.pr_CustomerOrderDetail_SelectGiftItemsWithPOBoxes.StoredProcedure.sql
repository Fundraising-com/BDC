USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetail_SelectGiftItemsWithPOBoxes]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetail_SelectGiftItemsWithPOBoxes]

AS

SELECT		b.AccountID,
			b.CampaignID,
			camp.StartDate CampaignStartDate,
			camp.EndDate CampaignEndDate,
			b.OrderID,
			b.Date,
			cd.Description OrderType,
			cod.CustomerOrderHeaderInstance,
			cod.TransID,
			cod.ProductCode,
			cod.ProductName,
			cod.Quantity,
			cod.Price,
			cod.StatusInstance,
			cod.Recipient,
			cust.Address1,
			cust.Address2,
			cust.City,
			cust.State Province,
			cust.Email,
			cust.Phone
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CodeDetail cd ON cd.Instance = b.OrderQualifierID
JOIN		Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
LEFT JOIN	Incident inc 
				ON inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance 
				AND inc.TransID = cod.TransID 
				AND inc.ProblemCodeInstance = 308
WHERE		(cust.Address1 LIKE '%PO BOX%' OR cust.Address1 LIKE '%BOITE%')
AND			cod.ProductType IN (46002, 46020)
AND			cod.StatusInstance NOT IN (501, 502, 506, 508)
AND			cod.IsShippedToAccount = 0
AND			inc.ProblemCodeInstance IS NULL
ORDER BY	cod.CustomerOrderHeaderInstance, cod.TransID
GO

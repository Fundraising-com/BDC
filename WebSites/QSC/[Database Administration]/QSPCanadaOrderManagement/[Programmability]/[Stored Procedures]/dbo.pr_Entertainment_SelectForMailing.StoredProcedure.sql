USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Entertainment_SelectForMailing]    Script Date: 06/07/2017 09:19:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Entertainment_SelectForMailing]

AS

SELECT		b.Date, b.AccountID, acc.Name, b.CampaignID, b.OrderID, cod.CustomerOrderHeaderInstance, cod.TransID, cd.Description OrderStatus,
			cod.Recipient CustomerName,
			rtrim(left(coalesce(cod.Recipient,''), charindex(' ', coalesce(cod.Recipient,''),charindex(' ', coalesce(cod.Recipient,''),1)))) as CustomerFirstName,
			ltrim(right(coalesce(cod.Recipient,''), len(replace(coalesce(cod.Recipient,''), ' ', '_')) - coalesce(charindex(' ', coalesce(cod.Recipient,''),1), 0))) as CustomerLastName,
			cust.Address1, cust.Address2, cust.City, cust.State Province, cust.Zip, cust.Phone CustomerPhone, cust.Email CustomerEmail,
			cod.ProductCode, cod.ProductName, cod.Quantity, cod.Price
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
JOIN		Customer cust ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
									WHEN 0 THEN coh.CustomerBillToInstance
									ELSE		cod.CustomerShipToInstance
								END
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc ON acc.Id = camp.ShipToAccountID
LEFT JOIN	Incident inc ON inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance AND inc.TransID = cod.TransID AND inc.ProblemCodeInstance = 298
JOIN		QSPCanadaCommon..CodeDetail cd ON cd.Instance = cod.StatusInstance
WHERE		cod.ProductType = 46024
AND			cod.DelFlag = 0
AND			b.StatusInstance NOT IN (40005)
AND			cod.DistributionCenterID IS NULL
AND			inc.ProblemCodeInstance IS NULL
AND			cod.StatusInstance IN (508, 513)
ORDER BY	b.Date
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Customer_SelectMissingEmailAddress]    Script Date: 06/07/2017 09:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Customer_SelectMissingEmailAddress]

AS

SELECT		CASE cod.StatusInstance WHEN 507 THEN 'Approved' ELSE 'Non-Approved' END SubscriptionStatus,
			cod.CustomerOrderHeaderInstance,
			cod.TransID,
			b.Date,
			cust.Instance CustomerInstance,
			cod.ProductCode,
			cod.ProductName,
			CASE b.OrderQualifierID WHEN 39009 THEN 'Online' WHEN 39001 THEN 'Main' WHEN 39002 THEN 'Supplementary' ELSE 'Unknown' END OrderQualifier,
			CASE b.OrderQualifierID WHEN 39009 THEN ioi.InternetOrderID ELSE lo.LandedOrderID END CustomerOrderID,
			cod.Recipient CustomerName,
			rtrim(left(coalesce(cod.Recipient,''), charindex(' ', coalesce(cod.Recipient,''),charindex(' ', coalesce(cod.Recipient,''),1)))) as CustomerFirstName,
			ltrim(right(coalesce(cod.Recipient,''), len(replace(coalesce(cod.Recipient,''), ' ', '_')) - coalesce(charindex(' ', coalesce(cod.Recipient,''),1), 0))) as CustomerLastName,
			cust.Address1 CustomerAddress1,
			cust.Address2 CustomerAddress2,
			cust.City CustomerCity,
			cust.State CustomerProvince,
			cust.Zip CustomerPostalCode,
			cust.Phone CustomerPhone
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.Date = coh.OrderBatchDate
JOIN		Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
LEFT JOIN	InternetOrderID ioi
				ON	ioi.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	LandedOrder lo
				ON	lo.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
LEFT JOIN	Incident inc 
				ON inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance 
				AND inc.TransID = cod.TransID 
				AND inc.ProblemCodeInstance = 299
WHERE		cod.ProductCode LIKE 'D%'
AND			cod.ProductType = 46001
AND			ISNULL(cust.Email, '') = ''
AND			ISNULL(cod.DelFlag, 0) = 0
AND			ISNULL(codrh.Status, 0) NOT IN (42004)
AND			cod.StatusInstance IN (501, 507)
AND			inc.ProblemCodeInstance IS NULL
AND			cod.CreationDate >= '2015-07-01'
AND			(cod.CustomerOrderHeaderInstance > 13393501)
ORDER BY	cod.CustomerOrderHeaderInstance, cod.TransID
GO

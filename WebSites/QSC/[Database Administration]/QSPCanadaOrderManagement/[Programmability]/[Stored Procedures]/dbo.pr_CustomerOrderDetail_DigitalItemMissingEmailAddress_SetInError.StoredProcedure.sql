USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetail_DigitalItemMissingEmailAddress_SetInError]    Script Date: 06/07/2017 09:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetail_DigitalItemMissingEmailAddress_SetInError]

	@OrderID	INT

AS

UPDATE		cod
SET			StatusInstance = 501
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
WHERE		b.OrderID = @OrderID
AND			cod.ProductCode LIKE 'D%'
AND			cod.ProductType = 46001
AND			ISNULL(cust.Email, '') = ''
AND			ISNULL(cod.DelFlag, 0) = 0
AND			cod.StatusInstance NOT IN (500)
GO

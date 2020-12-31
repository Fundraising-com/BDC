USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetStaffOrdersForTime_Regular]    Script Date: 06/07/2017 09:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetStaffOrdersForTime_Regular]
AS
SELECT		cod.CustomerOrderHeaderInstance,
			cod.TransID,
			LEFT(COALESCE(cod.Recipient, ''), CHARINDEX(' ', COALESCE(cod.Recipient, ''), CHARINDEX(' ', COALESCE(cod.Recipient, ''), 1))) AS RecipientFirstName,
			LTRIM(RIGHT(COALESCE(cod.Recipient, ''), LEN(REPLACE(COALESCE(cod.Recipient, ''), ' ', '_')) - COALESCE(CHARINDEX(' ', COALESCE(cod.Recipient, ''), 1), 0))) AS RecipientLastName,
			c.Address1,
			COALESCE(c.Address2, '') AS Address2,
			c.City,
			c.State AS Province,
			c.Zip AS PostalCode,
			p.Product_Sort_Name AS MagazineTitle,
			CONVERT(numeric(10, 2), cod.Price / 2) AS Price,
			pd.Nbr_Of_Issues AS NbrOfIssues,
			COALESCE(p.Lang, 'EN') AS Language,
			cod.CreationDate,
			COALESCE(rb.RunID, 0) AS RunID
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
LEFT JOIN	CustomerPaymentHeader cph
				ON	cph.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	CreditCardPayment ccp
				ON	ccp.CustomerPaymentHeaderInstance = cph.Instance
				AND	ccp.StatusInstance NOT IN (19000, 19003)
JOIN		Customer c
				ON	c.Instance =
					CASE cod.CustomerShipToInstance
						WHEN 0 THEN	coh.CustomerBillToInstance
						ELSE		cod.CustomerShipToInstance
					END
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
LEFT JOIN	RemitBatch rb
				ON	rb.FulfillmentHouseNbr = p.Fulfill_House_Nbr
				AND	rb.Status = 42001
				AND	rb.Date >= cod.CreationDate
				AND	rb.ID =
				(SELECT		TOP 1
							rb2.ID
				FROM		RemitBatch rb2
				WHERE		rb2.FulfillmentHouseNbr = p.Fulfill_House_Nbr
				AND			rb2.Status = 42001
				AND			rb2.Date >= cod.CreationDate
				ORDER BY	rb2.ID)
WHERE		cod.StatusInstance = 515
AND			ccp.CustomerPaymentHeaderInstance IS NULL
GO

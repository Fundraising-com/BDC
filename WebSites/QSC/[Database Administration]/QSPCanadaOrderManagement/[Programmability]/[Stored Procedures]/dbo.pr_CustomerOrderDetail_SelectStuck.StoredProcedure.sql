USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetail_SelectStuck]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetail_SelectStuck]

AS

SELECT		cod.*, b.*
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.orderbatchid = b.ID
				AND	coh.orderbatchdate = b.[date]
LEFT JOIN	CustomerPaymentHeader cph
				ON	cph.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	CreditCardPayment ccp
				ON	ccp.CustomerPaymentHeaderInstance = cph.Instance
JOIN		CustomerOrderDetail cod 
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaCommon.dbo.Season s
				ON	GETDATE() BETWEEN s.StartDate AND s.EndDate
				AND	s.Season IN ('Y')
				AND	b.[Date] BETWEEN s.StartDate AND s.EndDate
WHERE		b.StatusInstance in (40004, 40010, 40011, 40012, 40013, 40014)
AND			cod.StatusInstance in	(500,		--Order Detail Good
									502,		--Order Detail Paid
									504)		--Order Detail Paid Pending
AND			cod.ProductType NOT IN (46017, 46021) --Processing Fee, Shipping Fee
AND			cod.DelFlag = 0
AND			ISNULL(ccp.StatusInstance,0) NOT IN (19001, 19002, 19005)
ORDER BY	cod.CustomerOrderHeaderInstance, cod.TransID
GO

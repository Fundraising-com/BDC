USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectRejectedCreditCards]    Script Date: 06/07/2017 09:20:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectRejectedCreditCards] AS


SELECT	coh.Instance,
		--case ca.IsStaffOrder when 0 then sum(cod.Price) else sum(cod.Price * ca.StaffOrderDiscount / 100.00) end as Price, MS March 25, 2007 
		convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) AS Price,
		ccp.CreditCardNumber,
		ccp.ExpirationDate

FROM		CustomerOrderHeader coh,
		CustomerOrderDetail cod,
		QSPCanadaCommon..Campaign ca,
		CustomerPaymentHeader cph,
		CreditCardPayment ccp

WHERE	cod.CustomerOrderHeaderInstance = coh.Instance
AND		ca.ID = coh.CampaignID
AND		cph.CustomerOrderHeaderInstance = coh.Instance
AND		ccp.CustomerPaymentHeaderInstance = cph.Instance
AND		(coh.PaymentMethodInstance = 50003 or coh.PaymentMethodInstance = 50004)
AND		cph.StatusInstance <> 600
AND		ccp.StatusInstance <> 19000
AND		ccp.StatusInstance <> 19003
AND		ccp.CreditCardNumber <> '0000000000000000'

GROUP BY	coh.Instance,
		cod.Price,
		ca.IsStaffOrder,
		ca.StaffOrderDiscount,
		coh.CampaignID,
		ccp.CreditCardNumber,
		ccp.ExpirationDate

ORDER BY	coh.Instance
GO

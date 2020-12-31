USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_Order]    Script Date: 06/07/2017 09:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_Order] AS
SELECT		coh.Instance AS CustomerOrderHeaderInstance,
			coh.StatusInstance,
			coh.PaymentMethodInstance,
			cod.TransID,
			cod.StatusInstance AS CustomerOrderDetailStatusInstance,
			cod.ShipmentID,
			iod.InternetOrderID,
			cusBillTo.Instance AS CustomerBillToInstance,
			cusShipTo.Instance AS CustomerShipToInstance,
			b.ID AS BatchID,
			b.AccountID,
			b.StatusInstance AS BatchStatusInstance,
			b.OriginalStatusInstance AS BatchOriginalStatusInstance,
			b.OrderTypeCode,
			b.BillToAddressID AS BatchBillToAddressID,
			b.ShipToAddressID AS BatchShipToAddressID,
			b.ShipToAccountID AS BatchShipToAccountID,
			b.BillToFMID AS BatchBillToFMID,
			b.ShipToFmID AS BatchShipToFmID,
			b.OrderQualifierID,
			b.PaymentBatchID AS BatchPaymentBatchID,
			b.OrderID,
			s.Instance AS StudentInstance,
			ps.ID AS ProgramSectionID,
			pm.Program_ID,
			pm.Status AS ProgramStatus,
			pd.MagPrice_Instance AS PricingDetailsInstance,
			pd.Status AS PricingDetailsStatus,
			pd.TaxRegionID AS PricingDetailsTaxRegionID,
			p.Product_Instance,			
			pub.Pub_Status,
			codrh.Status AS CustomerOrderDetailRemitHistoryStatus,
			codrh.CurrencyID AS CODRHCurrencyID,
			crh.Instance AS CustomerRemitHistoryInstance,
			rb.ID AS RemitBatchID,
			rb.Status AS RemitBatchStatus,
			rb.FulfillmentHouseNbr,
			rb.RunID,
			c.ID AS CampaignID,
			fm.FMID,
			c.Status AS CampaignStatus,
			c.IncentivesBillToID,
			c.BillToAccountID,
			c.ShipToCampaignContactID,
			c.ShipToAccountID AS CampaignShipToAccountID,
			c.BillToCampaignContactID AS CampaignBillToCampaignContactID,
			c.SuppliesCampaignContactID AS CampaignSuppliesCampaignContactID,
			c.IncentivesDistributionID,
			c.PhoneListID,
			c.SuppliesAddressID,
			acc.ID,
			cph.Instance AS CustomerPaymentHeaderInstance,
			cph.PaymentBatchID,
			cph.StatusInstance AS CustomerPaymentHeaderStatusInstance,
			ccp.ReasonCode,
			slb.Instance AS SwitchLetterBatchInstance,
			inv.invoice_id
FROM		CustomerOrderHeader coh
LEFT JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	QSPCanadaFinance..INVOICE inv
				ON inv.invoice_id = cod.InvoiceNumber
LEFT JOIN	InternetOrderID iod
				ON	iod.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	batch b
				ON	b.ID = coh.OrderBatchID
				AND	coh.OrderBatchDate = b.[Date]
LEFT JOIN	Student s
				ON	s.Instance = coh.StudentInstance
LEFT JOIN	QSPCanadaProduct..ProgramSection ps
				ON	ps.ID = cod.programSectionID
LEFT JOIN	QSPCanadaProduct..Program_Master pm
				ON	pm.Program_ID = ps.Program_ID
LEFT JOIN	QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
LEFT JOIN	QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
LEFT JOIN	QSPCanadaProduct..Publishers pub
				ON	pub.Pub_Nbr = p.Pub_Nbr
LEFT JOIN	Customer cusBillTo
				ON	cusBillTo.Instance = coh.CustomerBillToInstance
LEFT JOIN	Customer cusShipTo
				ON	cusShipTo.Instance = cod.CustomerShipToInstance
LEFT JOIN	CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
LEFT JOIN	CustomerRemitHistory crh
				ON	crh.Instance = codrh.CustomerRemitHistoryInstance
LEFT JOIN	RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
LEFT JOIN	QSPCanadaCommon..Campaign c
				ON	c.ID = coh.CampaignID
LEFT JOIN	QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = c.FMID
LEFT JOIN	QSPCanadaCommon..CAccount acc
				ON	acc.ID = b.AccountID
LEFT JOIN	CustomerPaymentHeader cph
				ON	cph.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	CreditCardPayment ccp
				ON	ccp.CustomerPaymentHeaderInstance = cph.Instance
LEFT JOIN	SwitchLetterBatchCustomerOrderDetail slbcod
				ON	slbcod.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	slbcod.TransID = cod.TransID
LEFT JOIN	SwitchLetterBatch slb
				ON	slb.Instance = slbcod.SwitchLetterBatchInstance
GO

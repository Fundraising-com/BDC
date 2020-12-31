USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GiftCardDetailAll]    Script Date: 06/07/2017 09:18:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GiftCardDetailAll] AS
(SELECT		ISNULL(codrh.RemitBatchID, 0) RemitBatchID, 
			cod.CustomerOrderHeaderInstance,
			cod.TransID,
			b.CampaignID, 
			b.OrderID, 
			ISNULL(codrh.TitleCode, cod.ProductCode) TitleCode,
			ISNULL(codrh.MagazineTitle, cod.ProductName) MagazineTitle, 
			ISNULL(codrh.Lang, p.Lang) Lang,
			ISNULL(codrh.NumberOfIssues, cod.Quantity) NumberOfIssues,
			ISNULL(codrh.SupporterName, cod.SupporterName) SupporterName,
			ISNULL(crh.FirstName, cust.FirstName) FirstName,
			ISNULL(crh.LastName, cust.LastName) LastName,
			ISNULL(crh.Address1, cust.Address1) Address1,
			ISNULL(crh.Address2, cust.Address2) Address2,
			ISNULL(crh.City, cust.City) City,
			ISNULL(crh.State, cust.State) State,
			ISNULL(crh.Zip, cust.Zip) Zip,
			a.Name as GroupName,
			ISNULL(rb.RunID, 0) RunID,
			c.IsStaffOrder,
			ISNULL(codrh.GiftOrderType, cod.GiftCD) GiftOrderType,
			cod.CreationDate
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
JOIN		Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign c ON c.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount a ON a.Id = c.BillToAccountID
JOIN		QSPCanadaProduct..Pricing_Details pd ON pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p ON p.Product_Instance = pd.Product_Instance
LEFT JOIN	CustomerOrderDetailRemitHistory codrh ON codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance AND codrh.TransID = cod.TransID
LEFT JOIN	CustomerRemitHistory crh ON crh.Instance = codrh.CustomerRemitHistoryInstance
LEFT JOIN	RemitBatch rb ON rb.ID = codrh.RemitBatchID
WHERE		cod.IsGift = 1
AND			ISNULL(cod.IsGiftCardSent, 0) = 0
AND			(codrh.Status = 42001 OR (cod.ProductType = 46023 AND cod.StatusInstance = 508))
AND			p.Status = 30600	-- Ben - 03/07/2006 : Remove inactive title even they were sent in past remits
)
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetSubInfo]    Script Date: 06/07/2017 09:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetSubInfo]
AS
SELECT		s.LastName AS StudentLastName, 
			s.FirstName AS StudentFirstName, 
			coh.StudentInstance,
			b.OrderID,			
			ioid.InternetOrderID,
			crh.CustomerInstance, 
			crh.LastName AS RecipientLastName, 
			crh.FirstName AS RecipientFirstName, 
			crh.LastName AS CustomerLastName,
			crh.FirstName AS CustomerFirstName,
			crh.Address1, 
			crh.Address2, 
			crh.City AS CustomerCity, 
			crh.State AS CustomerState, 
			crh.Zip AS CustomerZip, 
			codrh.CustomerOrderHeaderInstance, 
			codrh.TransID, 
			cod.ProductCode as TitleCode,
			codrh.MagazineTitle AS Title, 
			codrh.NumberOfIssues AS IssuesSent, 
			codrh.CatalogPrice,
			--CONVERT(numeric(10,2),
			--CASE ca.IsStaffOrder	WHEN 0 THEN	cod.Price  ELSE		cod.Price * ca.StaffOrderDiscount / 100.00	END) AS Price, --MS March 23, 2007 No more Staff Discount
			convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) AS Price,
			codrh.ItemPriceTotal,
			codrh.CurrencyID,
			codrh.BasePrice,
			cod.OverrideProduct,
			cod.DelFlag,
			cd.Description AS Status,
			rb.ID AS RemitBatchID,
			rb.[Date] AS RemitBatchDate,
			rb.RunID,
			rb.Count AS RemitBatchCount,
			rb.status AS RemitBatchStatus,
			cod.CreationDate AS SubscriptionDate,
			b.CampaignID,
			cdBatch.Description AS OrderStatus,
			codrh.DateChanged AS DateSub,
			cdQualifier.Description AS QualifierName,
			cd.Instance AS SubStatusInstance,
			cdProductType.Description AS ProductType,
			coh.AccountID,
			crh.Instance AS CustomerRemitHistoryInstance,
			coh.OrderBatchDate,
			coh.OrderBatchID,
			p.Status AS ProductStatus,
			cod.ProductType AS ProductTypeInstance,
			ISNULL(coh.ToteID,0) AS ToteID,					
			CASE WHEN b.OrderQualifierID in (39001,39002) THEN lo.LandedOrderID 
				ELSE CASE	WHEN b.OrderQualifierID = 39009 THEN ioid.InternetOrderID
							ELSE 0 	END
			END AS CustomerOrderID,
			cod.InvoiceNumber as InvoiceNumber
FROM        CustomerOrderDetailRemitHistory codrh
INNER JOIN	CodeDetail cd
				ON	cd.Instance = codrh.Status
INNER JOIN	CustomerRemitHistory crh
				ON	crh.Instance = codrh.CustomerRemitHistoryInstance
				AND	crh.RemitBatchID = codrh.RemitBatchID
INNER JOIN	RemitBatch rb
				ON	rb.ID = crh.RemitBatchID
INNER JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
INNER JOIN	CodeDetail cdProductType
				ON	cdProductType.Instance = cod.ProductType
INNER JOIN	QSPCanadaProduct..Pricing_details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
INNER JOIN	QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
INNER JOIN	CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
INNER JOIN	Customer c
				ON	c.Instance = coh.CustomerBillToInstance
INNER JOIN	Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.[Date] = coh.OrderBatchDate
LEFT JOIN	InternetOrderID ioid
				ON b.OrderQualifierID = 39009 AND ioid.CustomerOrderHeaderInstance = coh.Instance
INNER JOIN	CodeDetail cdBatch
				ON	cdBatch.Instance = b.StatusInstance
INNER JOIN	CodeDetail cdQualifier
				ON	cdQualifier.Instance = b.OrderQualifierID
INNER JOIN	Student s
				ON	s.Instance = coh.StudentInstance
INNER JOIN	QSPCanadaCommon..Campaign ca
				ON	ca.ID = b.CampaignID
LEFT JOIN	LandedOrder lo
				ON b.OrderQualifierID IN (39001,39002) AND lo.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
WHERE		codrh.CustomerRemitHistoryInstance =
			(SELECT		TOP 1
						x.CustomerRemitHistoryInstance
			FROM		CustomerOrderDetailRemitHistory x 
			WHERE		x.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
			AND			x.TransID = codrh.TransID
			ORDER BY	x.CustomerRemitHistoryInstance DESC)
GO

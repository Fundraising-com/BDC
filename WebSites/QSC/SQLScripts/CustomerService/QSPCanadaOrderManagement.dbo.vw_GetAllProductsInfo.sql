USE [QSPCanadaOrderManagement]
GO

ALTER VIEW [dbo].[vw_GetAllProductsInfo]

AS

SELECT		s.LastName AS StudentLastName, 
			s.FirstName AS StudentFirstName, 
			coh.studentinstance, b.OrderID,
			ioi.internetorderID,
			c.Instance as customerinstance, 
			cod.Recipient AS RecipientName,
			left(coalesce(cod.Recipient,''), charindex(' ', coalesce(cod.Recipient,''),charindex(' ', coalesce(cod.Recipient,''),1))) as RecipientFirstName,
			ltrim(right(coalesce(cod.Recipient,''), len(replace(coalesce(cod.Recipient,''), ' ', '_')) - coalesce(charindex(' ', coalesce(cod.Recipient,''),1), 0))) as RecipientLastName,
			c.LastName AS CustomerLastName,
			c.FirstName AS CustomerFirstName,
			c.Address1, 
			c.Address2, 
			c.City AS CustomerCity, 
			c.State AS CustomerState, 
			c.Zip AS CustomerZip, 
			cod.CustomerOrderHeaderInstance, 
			cod.TransID, 
			cod.ProductCode as titlecode, cod.productname AS Title, 
			cod.Quantity AS IssuesSent, 
			cod.CatalogPrice, 
			convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) AS Price, 
			cod.Price AS ITEMPRICETOTAL,
			cod.price as baseprice,
			cod.DelFlag,
			0 as CurrencyID, 
			cod.OverrideProduct, 
			cd.Description AS Status, 
			0 AS RemitBatchID, 
			'' AS RemitBatchDate,
			0 as remitbatchstatus,
			0 AS RunID,
			0 AS RemitBatchCount,
			cod.CreationDate AS SubscriptionDate, 
			b.CampaignID,
			cdBatch.Description as OrderStatus, 
			cod.ChangeDate as  DateSub,
			cdQualifier.Description as QualifierName, 
			cd.Instance AS SubStatusInstance, 
			ISNULL(cdProductType.Description, 'Unknown') AS ProductType,
			coh.AccountID, 
			0 as customerremithistoryinstance, 
			coh.OrderBatchDate, 
			coh.OrderBatchID,
			p.status as productstatus,
			cod.producttype as producttypeinstance,
			ISNULL(coh.ToteID,0) as ToteID,
			CASE WHEN b.OrderQualifierID in (39001,39002) THEN lo.LandedOrderID 
				ELSE CASE	WHEN b.OrderQualifierID = 39009 THEN ioi.InternetOrderID
							ELSE 0 	END
			END AS CustomerOrderID
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	b.ID = coh.OrderBatchID
				AND	b.[Date] = coh.OrderBatchDate
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		Student s
				ON	s.Instance = coh.StudentInstance
JOIN		CodeDetail cdBatch
				ON	cdBatch.instance = b.statusinstance
JOIN		CodeDetail cdQualifier
				ON	cdQualifier.Instance = b.OrderQualifierID
LEFT JOIN	CodeDetail cdProductType
				ON	cdProductType.Instance = cod.ProductType
JOIN		Customer c
				ON	c.Instance = coh.CustomerBillToInstance
JOIN		CodeDetail cd
				ON	cd.Instance = cod.StatusInstance	
JOIN		QSPCanadaCommon..Campaign ca
				ON	ca.ID = coh.CampaignID
LEFT JOIN	(QSPCanadaProduct..Pricing_Details pd
JOIN			QSPCanadaProduct..Product p
					ON	p.product_instance = pd.product_instance)
				ON	pd.magprice_instance = cod.pricingdetailsid
LEFT JOIN	InternetOrderID ioi
				ON b.OrderQualifierID = 39009 AND coh.Instance = ioi.CustomerOrderHeaderInstance
LEFT JOIN	LandedOrder lo
				ON b.OrderQualifierID IN (39001,39002) AND lo.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
WHERE		((cod.StatusInstance NOT IN (507, 508) AND cod.ProductType = 46001) OR ISNULL(cod.ProductType,0) <> 46001)



GO



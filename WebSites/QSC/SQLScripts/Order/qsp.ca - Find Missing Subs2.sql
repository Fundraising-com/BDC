CREATE TABLE	[#goodSubs](
				[CustomerOrderHeaderInstance] [int] NOT NULL,
				[TransID] [int] NOT NULL,
				[Recipient] [varchar](81) NULL,
				[MagPrice_Instance] [int] NOT NULL,
				[Product_Instance] [int] NOT NULL,
				[RemitCode] [varchar](20) NULL)

INSERT INTO	[#goodSubs]
			([CustomerOrderHeaderInstance],
			[TransID],
			[Recipient],
			[MagPrice_Instance],
			[Product_Instance],
			[RemitCode])
SELECT		cod.CustomerOrderHeaderInstance,
			cod.TransID,
			cod.Recipient,
			pd.MagPrice_Instance,
			p.Product_Instance,
			p.RemitCode
INTO		#goodSubsJeff
FROM		CustomerOrderDetail cod
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
WHERE		cod.CreationDate >= '2006-10-26'
AND			cod.ProductType = 46001


SELECT		cart.eds_Order_ID AS InternetOrderID,
			ioi.CustomerOrderHeaderInstance,
			ci.Catalog_Item_Code AS UMC,
			o.order_Date AS OrderDate,
			o.Order_Status_ID AS OrderStatus,
			pa.First_Name + ' ' + pa.Last_Name AS Recipient,
			pa.Address1,
			pa.Address2,
			pa.City,
			province.subdivision_name_1 AS Province
			pa.Zip AS PostalCode
FROM		COM_OLTP1.QSPFulfillment.dbo.[ORDER] o
JOIN		COM_OLTP1.QSPEcommerce.dbo.Cart cart
				ON	cart.x_Order_ID = O.order_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Order_Detail od
				ON	od.Order_ID = o.Order_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Source s
				ON	o.Source_ID = s.Source_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Catalog_Item_Detail cid
				ON	cid.Catalog_Item_Detail_ID = od.Catalog_Item_Detail_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Catalog_Item ci
				ON	ci.Catalog_Item_ID = cid.Catalog_Item_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Shipment_Group sg
				ON	sg.Shipment_Group_ID = od.Shipment_Group_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Postal_Address pa
				ON	pa.Postal_Address_ID = sg.Shipping_Postal_Address_ID
JOIN		Subdivision province
				ON province.Subdivision_Code = pa.Subdivision_Code
LEFT JOIN	InternetOrderID ioi
				ON	ioi.InternetOrderID = cart.Eds_Order_ID
LEFT JOIN	#goodSubs gs
				ON	gs.CustomerOrderHeaderInstance = ioi.CustomerOrderHeaderInstance
				AND	gs.RemitCode = CASE ci.catalog_item_code WHEN '8212' THEN '2110' ELSE ci.catalog_item_code END
WHERE		o.Order_Date >= '2006-10-30'--DATEADD(month, -1, getDate())
AND			o.Order_Date <= DATEADD(day, -3, getDate())
AND			o.Order_Status_ID IN (201, 301, 401, 501, 601, 701)
AND			s.Source_Group_ID = 3
AND			gs.Product_Instance IS NULL
ORDER BY	o.Order_Date

DROP TABLE	#goodSubs

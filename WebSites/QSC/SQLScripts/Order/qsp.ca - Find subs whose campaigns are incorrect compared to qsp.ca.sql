drop table #goodsubs
CREATE TABLE	[#goodSubs](
				[CustomerOrderHeaderInstance] [int] NOT NULL,
				[TransID] [int] NOT NULL,
				[Recipient] [varchar](81) NULL,
				[MagPrice_Instance] [int] NOT NULL,
				[Product_Instance] [int] NOT NULL,
				[RemitCode] [varchar](20) NULL, [CampaignID] [int] not null,)

INSERT INTO	[#goodSubs]
			([CustomerOrderHeaderInstance],
			[TransID],
			[Recipient],
			[MagPrice_Instance],
			[Product_Instance],
			[RemitCode], [CampaignID])
SELECT		cod.CustomerOrderHeaderInstance,
			cod.TransID,
			cod.Recipient,
			pd.MagPrice_Instance,
			p.Product_Instance,
			p.RemitCode, c.id
FROM		CustomerOrderDetail cod
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
JOIN		QSPCanadaCommon.dbo.Season s
				ON	GetDate() BETWEEN s.StartDate AND s.EndDate
				AND	s.Season <> 'Y'
				AND	cod.CreationDate BETWEEN s.StartDate AND s.EndDate
join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join qspcanadacommon.dbo.campaign c on c.id = coh.campaignid
WHERE		cod.ProductType in (46001, 46006)
AND			cod.DelFlag <> 1

SELECT		cart.eds_Order_ID AS InternetOrderID,
gs.campaignid as fulfCampaign,
camp.fulf_Campaign_ID as qspcaCampaign,
			ioi.CustomerOrderHeaderInstance,
			gs.TransID,
			ci.Catalog_Item_Code AS UMC,
			ci.Catalog_Item_Name AS ProductName,
			CASE s.Source_ID WHEN 16 THEN 'Faculty Discount' WHEN 19 THEN 'Switch Letter' ELSE 'Regular' END AS OrderType,
			o.order_Date AS OrderDate,
			pa.First_Name + ' ' + pa.Last_Name AS Recipient,
			pa.Address1,
			pa.Address2,
			pa.City,
			province.subdivision_name_1 AS Province,
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
JOIN		COM_OLTP1.QSPFulfillment.dbo.Subdivision province
				ON province.Subdivision_Code = pa.Subdivision_Code
JOIN		QSPCanadaCommon.dbo.Season seas
				ON	GetDate() BETWEEN seas.StartDate AND seas.EndDate
				AND	seas.Season <> 'Y'
				AND	o.Order_Date BETWEEN seas.StartDate AND seas.EndDate
join COM_OLTP1.QSPFulfillment.dbo.Campaign camp
				ON	camp.Campaign_ID = o.Campaign_ID
 JOIN	(InternetOrderID ioi
				JOIN	CustomerOrderHeader coh
							ON	coh.Instance = ioi.CustomerOrderHeaderInstance
				JOIN	Batch b
							ON	b.ID = coh.OrderBatchID
							AND	b.[Date] = coh.OrderBatchDate
							AND	b.StatusInstance <> 40005) --Cancelled
				JOIN	CustomerOrderDetailRemitHistory codrh
							ON	codrh.CustomerOrderHeaderInstance = coh.Instance
							AND	codrh.Status not in (42004)
				ON	ioi.InternetOrderID = cart.Eds_Order_ID
JOIN	#goodSubs gs
				ON	gs.CustomerOrderHeaderInstance = ioi.CustomerOrderHeaderInstance
				AND	gs.RemitCode = CASE ci.catalog_item_code WHEN '8212' THEN '2110' ELSE ci.catalog_item_code END
WHERE		o.Order_Date <= DATEADD(day, 0, getDate())
AND			o.Order_Status_ID IN (201, 301, 401, 501, 601, 701)
AND			s.Source_Group_ID = 3
--AND			gs.Product_Instance IS NULL
and gs.campaignID <> camp.fulf_Campaign_ID


/*and cart.eds_order_id not in (
51498080,

51467276,
51471761,
51477263,
51477290,

51466236,
51468815,
51473324,
51479742,

51465780,
51465846,

51439951,
51448713,
51385070)*/

ORDER BY	o.Order_Date


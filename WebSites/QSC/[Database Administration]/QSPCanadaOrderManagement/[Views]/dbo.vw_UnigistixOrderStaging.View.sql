USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_UnigistixOrderStaging]    Script Date: 06/07/2017 09:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_UnigistixOrderStaging] AS

	select  distinct c.Lang,
		coh.instance as CustomerOrderHeaderInstance, 
		cod.transid,
		b.OrderID,
		0 as EnvelopeID,--coalesce(oem.EnvelopeID, 0) as EnvelopeID,
		coh.StudentInstance,
		--MS Oct 03, 2007 Issue# 3579
		--If BHE item from Inter Order then customerBillto only when Shipto is zero
		--coh.CustomerBillToInstance as CustomerInstance,
		Case b.orderQualifierid 
		When 39009 Then Case cod.ProductType 
				    When 46006 Then Case IsNull(cod.CustomerShipToInstance,0)
						When 0 Then coh.CustomerBillToInstance 
						Else cod.CustomerShipToInstance
						End
				    When 46007 Then Case IsNull(cod.CustomerShipToInstance,0)
						When 0 Then coh.CustomerBillToInstance 
						Else cod.CustomerShipToInstance
						End
				    When 46012 Then Case IsNull(cod.CustomerShipToInstance,0)
						When 0 Then coh.CustomerBillToInstance 
						Else cod.CustomerShipToInstance
						End
				    Else coh.CustomerBillToInstance 
				    End
		Else coh.CustomerBillToInstance
		End as CustomerInstance,
		0 as QtyShipped, -- shipped
		CASE WHEN cod.DistributionCenterID = 2 THEN '100502871Z0009' WHEN cod.ProductType in (46001 ,46006) THEN ''END as ReplacedItemCode,
		0 as ReplacedItemQty,
		0 as PaymentStatusInstance,
		cod.Recipient,
		--MS Sept 26, 2007 Issue#3509
		--CASE WHEN cod.producttype in (46008, 46013,46014,46015) THEN 'LEVEL'+substring(cod.ProductCode,3,1) ELSE cod.ProductCode END AS ProductCode,
		CASE WHEN cod.producttype in (46008, 46013,46014,46015) THEN 'LEVEL'+p.Prize_Level ELSE cod.ProductCode END AS ProductCode,
		CASE WHEN cod.ProductType <> '46001' THEN pdesc.Product_description_alt ELSE p.Product_Sort_Name END as ProductName,  -- product name fill in w/productdescription
		Quantity as QuantityOrdered,
		cod.Renewal,
		cod.Price,
		cod.SupporterName,
		cod.PriceOverrideID,
		cod.CatalogPrice,	
		CASE WHEN cod.ProductType <> '46001' THEN p.OracleCode ELSE pd.Product_Code END as OracleCode,  -- Per unigistix put product code into oracle code
		--CASE WHEN cod.producttype in (46008, 46013,46014,46015) THEN 'LEVEL'+substring(cod.ProductCode,3,1) ELSE pd.Product_Code END as CatalogProductCode,
		CASE WHEN cod.producttype in (46008, 46013,46014,46015) THEN 'LEVEL'+p.Prize_Level ELSE pd.Product_Code END as CatalogProductCode,
		CASE WHEN cod.productcode='441' THEN 46002 WHEN cod.ProductType = 46013 THEN 46008 WHEN cod.ProductType = 46014 THEN 46008 ELSE cod.ProductType END as Type,
		--CASE WHEN cod.producttype in (46008, 46013,46014,46015) THEN substring(cod.ProductCode,3,1) ELSE '' END as LevelCode,--COD.Productcode,
		CASE WHEN cod.producttype in (46008, 46013,46014,46015) THEN p.Prize_Level ELSE '' END as LevelCode,--COD.Productcode,
		cod.StatusInstance
	FROM	QSPCanadaOrderManagement..CustomerOrderHeader coh,
		QSPCanadaOrderManagement..CustomerOrderDetail cod, 
		QSPCanadaOrderManagement..Batch b,
		QSPCanadaProduct..Product p ,
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaCommon..Campaign c,
		QSPCanadaProduct..ProductDescription pdesc

		--LEFT JOIN QSPCanadaOrderManagment..OrderInEnvelopeMap oem ON oem.CustomerOrderHeaderInstance = coh.Instance
	WHERE	coh.orderbatchdate = b.date 
		and coh.orderbatchid = b.id
		and coh.instance = cod.customerorderheaderinstance
		and b.CampaignID = c.ID
		and cod.PricingDetailsID = pd.MagPrice_instance
/*		and p.Product_code = pd.Product_code
		and p.Product_year = pd.Pricing_year
		and p.Product_Season = pd.Pricing_Season
*/
		and p.Product_instance=pd.product_instance
		--and (cod.ProductType in(46001,46006) OR cod.DistributionCenterID = 2)
		--and cod.DistributionCenterID = 2
		and (pd.OracleCode *= pdesc.Product_Code 
		AND c.Lang *= pdesc.language_code)
		AND cod.ProductType <> 46001
		AND COD.StatusInstance=509
GO

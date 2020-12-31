USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Invoice_GetOrderDetails]    Script Date: 06/07/2017 09:17:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Invoice_GetOrderDetails]()

RETURNS TABLE

AS

RETURN
(
	SELECT		inv.Invoice_ID,
				cod.ProductCode,
				ISNULL(RTRIM(prodDes.Product_Description_Alt), cod.ProductName) AS ProductName,
				cod.CatalogPrice,
				ISNULL(prod.IsQSPExclusive, 0) AS IsQSPExclusive,
				ps.Type AS SectionType,
				cod.ProductType,
				camp.Lang,
				b.OrderQualifierID,
				CASE b.OrderQualifierID
					WHEN 39009 THEN	2
					WHEN 39013 THEN 3
					WHEN 39015 THEN 3
					ELSE			1
				END AS InvoiceGridID,
				CASE cod.ProductType
					WHEN 46001 THEN	cod.Quantity
					ELSE 0
				END AS NumIssues,
				CASE cod.ProductType
					WHEN 46001 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Magazines'
										ELSE			'Magazines'
									END
					WHEN 46002 THEN	CASE ps.Type
										WHEN 6 THEN	CASE camp.Lang
														WHEN 'FR' THEN	'Douceurs exquises'
														ELSE			'Sweet Sensations'
													END
										ELSE		CASE camp.Lang
														WHEN 'FR' THEN	'Cadeaux'
														ELSE			'Gift'
													END
										END
					WHEN 46003 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Cadeaux sans Tax'
										ELSE			'Gift without Tax'
									END
					WHEN 46004 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Approvisionnements De Champ'
										ELSE			'Field Supplies'
									END
					WHEN 46005 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Cadeaux'
										ELSE			'Gift'
									END
					WHEN 46006 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Magazines'
										ELSE			'Magazines'
									END
					WHEN 46007 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Magazines'
										ELSE			'Magazines'
									END
					WHEN 46008 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Prix De Kanata'
										ELSE			'Kanata Prizes'
									END
					WHEN 46009 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Magazines'
										ELSE			'Magazines'
									END
					WHEN 46010 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Magazines'
										ELSE			'Magazines'
									END
					WHEN 46011 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Magazines'
										ELSE			'Magazines'
									END
					WHEN 46012 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Magazines'
										ELSE			'Magazines'
									END
					WHEN 46013 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Prix De Kanata'
										ELSE			'Kanata Prizes'
									END
					WHEN 46014 THEN	CASE camp.Lang
										WHEN 'FR' THEN	'Prix De Kanata'
										ELSE			'Kanata Prizes - Detail'
									END
					WHEN 46018 then CASE camp.lang
										when 'FR' then 'Pâte à biscuit'
										else  'Cookie Dough'
									END
					WHEN 46019 then CASE camp.lang
										when 'FR' then 'Maïs Soufflé'
										else  'Popcorn'
									END
					WHEN 46020 then CASE camp.lang
										when 'FR' then 'Bijoux / Bloom'
										else  'Jewellery / Bloom'
									END
					WHEN 46022 then CASE camp.lang
										when 'FR' then 'Chandelles'
										else  'Candles'
									END
					WHEN 46023 then CASE camp.lang
										when 'FR' then 'To Remember This'
										else  'To Remember This'
									END
					WHEN 46024 then CASE camp.lang
										when 'FR' then 'Divertissement'
										else  'Entertainment Card'
									END

					ELSE			''				
				END AS ProductTypeName,
				CASE cod.ProductType
					WHEN 46001 THEN (CONVERT(NUMERIC(10, 2), cod.Price)) --Mag
					WHEN 46002 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --Gift
					WHEN 46003 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --WFC
					WHEN 46004 THEN (CONVERT(NUMERIC(10, 2), (cod.Price) / cod.Quantity)) --Field Supplies
					WHEN 46005 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --Food
					WHEN 46006 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --Book
					WHEN 46007 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --Music
					WHEN 46008 THEN (CONVERT(NUMERIC(10, 2), (cod.Price) / cod.Quantity)) --Incentives
					WHEN 46009 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --New Business
					WHEN 46010 THEN (CONVERT(NUMERIC(10, 2), cod.Price)) --MMB
					WHEN 46011 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --National
					WHEN 46012 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --Video
					WHEN 46013 THEN (CONVERT(NUMERIC(10, 2), (cod.Price) / cod.Quantity)) --Incentives - Magazine
					WHEN 46014 THEN (CONVERT(NUMERIC(10, 2), (cod.Price) / cod.Quantity)) --Incentives - Gift
					WHEN 46015 THEN (CONVERT(NUMERIC(10, 2), (cod.Price) / cod.Quantity)) --Incentives - Food
					WHEN 46018 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --cookie dough
					WHEN 46019 THEN (CONVERT(NUMERIC(10, 2), (cod.Price) / cod.Quantity)) --Popcorn
					WHEN 46020 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --
					WHEN 46022 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --Candles
					WHEN 46023 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --TRT
					WHEN 46024 THEN (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)) --Entertainment
					ELSE 0			
				END AS Price,
				CASE cod.ProductType
					WHEN 46001 THEN COUNT(*) 	   --Mag
					WHEN 46002 THEN SUM(cod.Quantity) --Gift
					WHEN 46003 THEN SUM(cod.Quantity) --WFC
					WHEN 46004 THEN SUM(cod.Quantity) --Field Supplies
					WHEN 46005 THEN SUM(cod.Quantity) --Food
					WHEN 46006 THEN SUM(cod.Quantity) --Book
					WHEN 46007 THEN SUM(cod.Quantity) --Music
					WHEN 46008 THEN SUM(cod.Quantity) --Incentives
					WHEN 46009 THEN SUM(cod.Quantity) --New Business
					WHEN 46010 THEN COUNT(*) --MMB
					WHEN 46011 THEN SUM(cod.Quantity) --National
					WHEN 46012 THEN SUM(cod.Quantity) --Video
					WHEN 46013 THEN SUM(cod.Quantity) --Incentives - Magazine
					WHEN 46014 THEN SUM(cod.Quantity) --Incentives - Gift
					WHEN 46015 THEN SUM(cod.Quantity) --Incentives - Food
					WHEN 46018 THEN SUM(cod.Quantity) --ICookie dough
					WHEN 46019 THEN SUM(cod.Quantity) --Popcorn
					WHEN 46020 THEN SUM(cod.Quantity) --					
					WHEN 46022 THEN SUM(cod.Quantity) --Candles					
					WHEN 46023 THEN SUM(cod.Quantity) --TRT
					WHEN 46024 THEN SUM(cod.Quantity) --Entertainment
					ELSE 0			
				END AS QTYOrdered,
				CASE cod.ProductType
					WHEN 46001 THEN CASE cod.StatusInstance
										WHEN 507 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN (COUNT(*))
															ELSE			0
														END
										WHEN 508 THEN	SUM(cod.QuantityShipped) --508: eBooks Shipped
										WHEN 512 THEN	0 --512: Unremittable
										When 514 THEN	0 --514: Library Order
										ELSE			0
									END --Mag
					WHEN 46002 THEN CASE WHEN cod.ProductCode LIKE 'DO%' THEN SUM(cod.Quantity) ELSE SUM(cod.QuantityShipped) END --Gift
					WHEN 46003 THEN SUM(cod.QuantityShipped) --WFC
					WHEN 46004 THEN SUM(cod.QuantityShipped) --Field Supplies
					WHEN 46005 THEN SUM(cod.QuantityShipped) --Food
					WHEN 46006 THEN CASE cod.StatusInstance
										WHEN 508 THEN	CASE ISNULL(ccp.StatusInstance, 19000) --508: Order Detail Shipped
															WHEN 19000 THEN	SUM(cod.QuantityShipped)
															ELSE			0
														END
										ELSE			0			
									END --Book
					WHEN 46007 THEN SUM(cod.QuantityShipped) --Music
					WHEN 46008 THEN SUM(cod.QuantityShipped) --Incentives
					WHEN 46009 THEN SUM(cod.QuantityShipped) --New Business
					WHEN 46010 THEN COUNT(*) --MMB
					WHEN 46011 THEN SUM(cod.QuantityShipped) --National
					WHEN 46012 THEN SUM(cod.QuantityShipped) --Video
					WHEN 46013 THEN SUM(cod.QuantityShipped) --Incentives - Magazine
					WHEN 46014 THEN SUM(cod.QuantityShipped) --Incentives - Gift
					WHEN 46015 THEN SUM(cod.QuantityShipped) --Incentives - Food
					WHEN 46018 THEN SUM(cod.QuantityShipped) --CookieDough
					WHEN 46019 THEN SUM(cod.QuantityShipped) --Popcorn
					WHEN 46020 THEN SUM(cod.QuantityShipped) --Jewellery
					WHEN 46022 THEN SUM(cod.QuantityShipped) --Candles
					WHEN 46023 THEN CASE cod.StatusInstance
										WHEN 508 THEN	SUM(cod.Quantity)
										ELSE			0
									END --TRT
					WHEN 46024 THEN SUM(cod.QuantityShipped) --Entertainment
					ELSE 0			
				END AS QuantityShipped,
				CASE cod.ProductType
					WHEN 46001 THEN CASE cod.StatusInstance
										WHEN 502 THEN	CASE ISNULL(ccp.StatusInstance, 19000)	   
															WHEN 19000 THEN COUNT(*) * (CONVERT(NUMERIC(10, 2), cod.Price))
															ELSE 0
														END
										WHEN 507 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN COUNT(*) * CONVERT(NUMERIC(10, 2), cod.Price)
															ELSE			0
														END
										WHEN 508 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN COUNT(*) * CONVERT(NUMERIC(10, 2), cod.Price)
															ELSE			0
														END
										WHEN 512 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN	COUNT(*) * (CONVERT(NUMERIC(10, 2), cod.Price))
															ELSE			0
														END
										WHEN 514 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN COUNT(*) * (CONVERT(NUMERIC(10, 2), cod.Price))
															ELSE 0
														END
										WHEN 515 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN COUNT(*) * (CONVERT(NUMERIC(10, 2), cod.Price))
															ELSE 0
														END
										ELSE			0
									END --Magazine
					WHEN 46002 THEN	SUM(CASE WHEN cod.ProductCode LIKE 'DO%' THEN cod.Quantity ELSE cod.QuantityShipped END * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity))) --Gift
					WHEN 46003 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity))) --WFC
					WHEN 46004 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2), (cod.Price) / cod.Quantity))) --Kanata Order may have FS item
					WHEN 46005 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity))) --Food
					WHEN 46006 THEN CASE cod.StatusInstance
										WHEN 508 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)))
															ELSE			0
														END
										WHEN 513 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN SUM(cod.Quantity * (CONVERT(NUMERIC(10, 2),cod.Price / cod.Quantity)))
															ELSE			0
														END
										ELSE			0
									END --Book
					WHEN 46007 THEN	CASE cod.StatusInstance
										WHEN 508 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)))
															ELSE 0
														END
										WHEN 513 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN SUM(cod.Quantity * (CONVERT(NUMERIC(10, 2),cod.Price / cod.Quantity)))
															ELSE 0
														END
										ELSE 0			
									END  --BHE
					WHEN 46008 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2),(cod.Price) / cod.Quantity))) --Incentives
					WHEN 46009 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2),cod.Price / cod.Quantity))) --New Business
					WHEN 46010 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2),cod.Price / cod.Quantity))) --MMB
					WHEN 46011 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2),cod.Price / cod.Quantity))) --National
					WHEN 46012 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2),cod.Price / cod.Quantity))) --Video
					WHEN 46013 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2),(cod.Price) / cod.Quantity))) --Incentives - Magazine
					WHEN 46014 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2),(cod.Price) / cod.Quantity))) --Incentives - Gift
					WHEN 46015 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2),(cod.Price) / cod.Quantity))) --Incentives - Food
					WHEN 46018 THEN	CASE cod.StatusInstance
										WHEN 508 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity)))
															ELSE 0
														END
										WHEN 513 THEN	CASE ISNULL(ccp.StatusInstance, 19000)
															WHEN 19000 THEN SUM(cod.Quantity * (CONVERT(NUMERIC(10, 2),cod.Price / cod.Quantity)))
															ELSE 0
														END
										ELSE 0			
									END  --BHE
					WHEN 46019 THEN	SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2),(cod.Price) / cod.Quantity))) --Popcorn
					WHEN 46020 THEN	SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity))) --Jewellery
					WHEN 46022 THEN	SUM(cod.QuantityShipped * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity))) --Candles
					WHEN 46023 THEN	SUM(cod.Quantity * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity))) --TRT
					WHEN 46024 THEN	SUM(cod.Quantity * (CONVERT(NUMERIC(10, 2), cod.Price / cod.Quantity))) --Entertainment

				END AS TotalPrice
				,PostageAmount = SUM(dbo.UDF_GetPostageAmount(cod.PricingDetailsID))
	FROM		Invoice inv
	JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
					ON	cod.InvoiceNumber = inv.INVOICE_ID
	JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
					ON	coh.Instance = cod.CustomerOrderHeaderInstance
	JOIN		QSPCanadaOrderManagement..Batch b
					ON	coh.OrderBatchDate = b.Date
					AND	coh.OrderBatchID = b.ID
	LEFT JOIN	QSPCanadaOrderManagement..CustomerPaymentHeader cph
					ON	cph.CustomerOrderHeaderInstance = coh.Instance 
	LEFT JOIN	QSPCanadaOrderManagement..CreditCardPayment ccp
					ON	ccp.CustomerPaymentHeaderInstance = cph.Instance
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	JOIN		QSPCanadaProduct..Pricing_Details pd
					ON	pd.MagPrice_Instance = cod.PricingDetailsID
	JOIN		QSPCanadaProduct..ProgramSection ps
					ON	ps.ID = pd.ProgramSectionID
	JOIN		QSPCanadaProduct..Product prod
					ON	prod.Product_Instance = pd.Product_Instance
	LEFT JOIN	QSPCanadaProduct..ProductDescription prodDes
					ON	prodDes.Product_Code = pd.Product_Code
					AND	prodDes.Language_Code = camp.Lang
	JOIN		QSPCanadaCommon..CodeDetail cdOQ
					ON	cdOQ.Instance = b.OrderQualifierID
	WHERE		cod.ProductType NOT IN (46017, 46021)
	AND			ISNULL(cod.IsVoucherRedemption, 0) = 0	
	GROUP BY	inv.Invoice_ID,
				cod.ProductCode,
				ISNULL(RTRIM(prodDes.Product_Description_Alt), cod.ProductName),
				cod.CatalogPrice,
				ISNULL(prod.IsQSPExclusive, 0),
				ps.Type,
				cod.ProductType,
				camp.Lang,
				b.OrderQualifierID,
				cdOQ.Description,
				cod.Quantity,
				cod.Price,
				cod.QuantityShipped,
				cod.StatusInstance,
				ISNULL(ccp.StatusInstance, 19000),
				--pd.PostageAmount,
				cod.PricingDetailsID
)
GO

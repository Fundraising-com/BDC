USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[PrepOrderDetailForUnigistix]    Script Date: 06/07/2017 09:20:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PrepOrderDetailForUnigistix]
	@orderID int,
	@lang varchar(2)
as

---------------
-- Push all mag out
-- then only items for this distribution center
---------------

insert UnigistixOrderStaging
	(CustomerOrderHeaderInstance,
		TransID,
		OrderID,
		EnvelopeID,
		StudentInstance,
		CustomerInstance,
		QtyShipped,
		ReplacedItemCode,
		ReplacedItemQty,
		PaymentStatusInstance,
		Recipient,
		ProductCode,
		ProductName,
		QuantityOrdered,
		Renewal,
		Price,
		SupporterName,
		PriceOverrideID,
		CatalogPrice,
		OracleCode,
		CatalogProductCode,
		Type,
		LevelCode,
		StatusInstance
	)
	select coh.instance, 
		transid,
		@orderID,
		0,
		StudentInstance,
		CustomerBillToInstance,
		0, -- shipped
		'',
		0,
		0,-----Payment.StatusInstance,
		Recipient,
		cod.ProductCode,
		Product.Product_Sort_Name,  -- product name fill in w/productdescription
			Quantity,
		Renewal,
		Price,
		SupporterName,
		PriceOverrideID,
		cod.CatalogPrice,	
		PricingDetail.Product_Code,  -- Per unigistix put product code into oracle code
		PricingDetail.Product_Code,
		ProductType,
		'',--COD.Productcode,
		COD.StatusInstance
		 from
			customerorderheader coh,
			--left outer join QSPCanadaOrderManagement..customerpaymentheader Payment
			--on coh.Instance=Payment.CustomerOrderHeaderInstance,
			customerorderdetail cod, batch,
			QSPCanadaProduct.dbo.Pricing_Details as PricingDetail,
			QSPCanadaProduct.dbo.Product as Product
			where 
				coh.orderbatchdate=date 
				and coh.orderbatchid=id
				and coh.instance = cod.customerorderheaderinstance
				and cod.PricingDetailsID = MagPrice_instance
				and Product.Product_code=PricingDetail.Product_code
				and Product.Product_year = PricingDetail.Pricing_year
				and Product.Product_Season = PricingDetail.Pricing_Season
				and COD.ProductType in(46001 ,46006)
				and orderid=@orderid
				--and orderid=38817
			order by coh.instance, cod.transid

	insert UnigistixOrderStaging
		(CustomerOrderHeaderInstance,
			TransID,
			OrderID,
			EnvelopeID,
			StudentInstance,
			CustomerInstance,
			QtyShipped,
			ReplacedItemCode,
			ReplacedItemQty,
			PaymentStatusInstance,
			Recipient,
			ProductCode,
			ProductName,
			QuantityOrdered,
			Renewal,
			Price,
			SupporterName,
			PriceOverrideID,
			CatalogPrice,
			OracleCode,
			CatalogProductCode,
			Type,
			LevelCode,
			StatusInstance
		)
		select coh.instance, 
			transid,
			@orderID,
			0,
			StudentInstance,
			CustomerBillToInstance,
			0, -- shipped
			'100502871Z0009',
			0,
			0,-----Payment.StatusInstance,
			Recipient,
			cod.ProductCode,
			ProductName,  -- product name fill in w/productdescription
			Quantity,
			Renewal,
			Price,
			SupporterName,
			PriceOverrideID,
			cod.CatalogPrice,	
			PricingDetail.OracleCode,
			PricingDetail.Product_Code,
			ProductType,
			'',--COD.Productcode,
			COD.StatusInstance
			 from
				customerorderheader coh,
				--left outer join QSPCanadaOrderManagement..customerpaymentheader Payment
				--		on coh.Instance=Payment.CustomerOrderHeaderInstance,
				customerorderdetail cod, batch,
					QSPCanadaProduct..Pricing_Details as PricingDetail
				where 
					coh.orderbatchdate=date 
					and coh.orderbatchid=id
					and coh.instance = cod.customerorderheaderinstance
					and cod.PricingDetailsID = MagPrice_instance
					and COD.DistributionCenterID = 2 -- unigistix
					and orderid=@orderid
					order by coh.instance, cod.transid

		update  UnigistixOrderStaging set ProductName = Product_description_alt
			from UnigistixOrderStaging, 
				QSPCanadaProduct..ProductDescription
				where language_code=@lang and Oraclecode=Product_Code 
					and UnigistixOrderStaging.type <> 46001
					and UnigistixOrderStaging.orderid=@orderid

--13,14,15 for MAG, Music and Food		
		update  UnigistixOrderStaging set 
			--MS Sept 26, 2007
			--LevelCode = substring(cod.ProductCode,3,1),
			--Productcode = 'LEVEL'+substring(cod.ProductCode,3,1),
			--CatalogProductCode = 'LEVEL'+substring(cod.ProductCode,3,1)
			LevelCode = p.Prize_Level ,
			Productcode = 'LEVEL'+p.Prize_Level ,
			CatalogProductCode = 'LEVEL'+p.Prize_Level 
			from UnigistixOrderStaging, 
				customerorderdetail cod,
				qspcanadaproduct..pricing_details as pd,
				qspcanadaproduct..product p
				where  cod.customerorderheaderinstance=UnigistixOrderStaging.customerorderheaderinstance
					and cod.transid= UnigistixOrderStaging.transid
					and pd.magprice_Instance=cod.pricingdetailsid
					and pd.product_Instance=p.product_Instance
					and producttype in (46008, 46013,46014,46015)
					and orderid=@orderid
					and UnigistixOrderStaging.orderid=@orderid

		Update UnigistixOrderStaging set EnvelopeID = OrderInEnvelopeMap.EnvelopeID
			from UnigistixOrderStaging, OrderInEnvelopeMap where 
				OrderID=@orderid
				and OrderInEnvelopeMap.CustomerOrderHeaderInstance=UnigistixOrderStaging.customerorderheaderinstance
--and OrderInEnvelopeMap.envelopeid<11094

		Select CustomerOrderHeaderInstance, TransID, StudentInstance, 0 as EnvID into #EnvFixup
			 from UnigistixOrderStaging where EnvelopeID=0
				and orderid = @orderid

		update #EnvFixup set EnvId = EnvelopeID
			from #EnvFixup, UnigistixOrderStaging
				where UnigistixOrderStaging.OrderID = @orderid
					and #EnvFixup.StudentInstance=UnigistixOrderStaging.StudentInstance
					and EnvelopeID <> 0



		update UnigistixOrderStaging set EnvelopeID = EnvId
			from #EnvFixup, UnigistixOrderStaging
			where 	#EnvFixup.CustomerOrderHeaderInstance =UnigistixOrderStaging.CustomerOrderHeaderInstance
				and 	#EnvFixup.TransID   = UnigistixOrderStaging.TransID   

		drop table #EnvFixup
/*
		update  UnigistixOrderStaging set LevelCode = 
			from UnigistixOrderStaging, 
				customerorderheader coh,
				Batch,
				customerorderdetail cod,
				QSPCanadaProduct..Pricing_details pd
				where  cod.customerorderheaderinstance=UnigistixOrderStaging.customerorderheaderinstance
					and cod.transid= UnigistixOrderStaging.transid
					and coh.orderbatchdate=date 
					and coh.orderbatchid=id
					and coh.instance=cod.customerorderheaderinstance
					and coh.instance=UnigistixOrderStaging.customerorderheaderinstance
					and pd.magprice_instance = cod.pricingdetailsid
					and producttype=46008
					and orderid=@orderid

*/
GO

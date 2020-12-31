USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateTransactionsForOracleInventory]    Script Date: 06/07/2017 09:19:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       procedure [dbo].[pr_CreateTransactionsForOracleInventory]
as
	/* Prep tables that will be used to seed oracle inventory interface tables */
	

	declare @shipbatchid int


	/* Get the records from shipment order that we haven't pushed up yet */
	select  P.OracleCode, 
		Sum(QuantityShipped) as QuantityShipped,
		count(*) as NumOrder, 
		QPL.ProductLineNumber,
		 qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(P.OracleCode,
			 CustomerOrderDetail.DistributionCenterID) as DistributionCenterCode 
			into #pr_CreateTransactionsForOracleInventory
		from    CustomerOrderDetail,
			CustomerOrderHeader,
			Batch, 
			QSPCanadaProduct..Pricing_Details as PD,
			QSPCanadaProduct..Product as P ,
		        ShipmentOrder,
			QSPCanadaCommon..QSPProductLine QPL
		where CustomerOrderHeaderInstance=Instance
		      and OrderBatchDate=Date
		      and OrderBatchID = Batch.id
	      	      and ShipmentOrder.OrderID = Batch.OrderID
		      and ShipmentOrder.ShipmentID = CustomerOrderDetail.ShipmentID
		      and ShipmentOrder.DistributionCenterID=CustomerOrderDetail.DistributionCenterID
		      and MagPrice_instance = PricingDetailsID
--		      and P.Product_Code=PD.Product_Code
		      and P.Product_Code=CustomerOrderDetail.ProductCode
--		      and Product_year = PD.Pricing_year
--		      and Product_Season = PD.Pricing_Season
		      and P.product_instance=pd.product_instance
		      and ShipmentOrder.IsShipmentBatchCreated=0
		      and Customerorderdetail.ProductType<>46001
		      and P.ProductLine+46000=CustomerOrderDetail.ProductType
		      and QPL.ID = ProductType
		      and P.ProductLine < 46000
		      group by P.OracleCode, QPL.ProductLineNumber, qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(P.OracleCode,
			 			CustomerOrderDetail.DistributionCenterID)

	insert into #pr_CreateTransactionsForOracleInventory
	select  P.OracleCode, 
		Sum(QuantityShipped) as QuantityShipped,
		count(*) as NumOrder, 
		QPL.ProductLineNumber,
		 qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(P.OracleCode,
			 CustomerOrderDetail.DistributionCenterID) as DistributionCenterCode 
		from    CustomerOrderDetail,
			CustomerOrderHeader,
			Batch, 
			QSPCanadaProduct..Pricing_Details as PD,
			QSPCanadaProduct..Product as P ,
		        ShipmentOrder,
			QSPCanadaCommon..QSPProductLine QPL
		where CustomerOrderHeaderInstance=Instance
		      and OrderBatchDate=Date
		      and OrderBatchID = Batch.id
	      	      and ShipmentOrder.OrderID = Batch.OrderID
		      and ShipmentOrder.ShipmentID = CustomerOrderDetail.ShipmentID
		      and ShipmentOrder.DistributionCenterID=CustomerOrderDetail.DistributionCenterID
		      and MagPrice_instance = PricingDetailsID
		      and P.product_instance=pd.product_instance
--		      and P.Product_Code=PD.Product_Code
		      and P.Product_Code=CustomerOrderDetail.ProductCode
--		      and Product_year = PD.Pricing_year
--		      and Product_Season = PD.Pricing_Season
		      and ShipmentOrder.IsShipmentBatchCreated=0
		      and Customerorderdetail.ProductType<>46001
		      and P.ProductLine=CustomerOrderDetail.ProductType
		      and QPL.ID = ProductType
		      group by P.OracleCode, QPL.ProductLineNumber, qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(P.OracleCode,
			 			CustomerOrderDetail.DistributionCenterID)

--select * from #pr_CreateTransactionsForOracleInventory

	Insert ShipmentBatch	
        (
		CountryCode,
		DateSentToOracle,
		SentToOracle
	)
	select 'CA', '1/1/95', 0

	if(@@error <> 0)
	begin
		return 1
	end
	select @shipbatchid = Scope_Identity()


	/* create the Oracle trans record */
	insert InventoryOracleTrans
	(
		ShipmentBatchID,
		QSPProductLine,
		OraLanguageCode,
		OracleCode,
		TransactionType,
		TransactionQty,
		Channel,
		NumberOfLine,
		DistributionCenterCode,
		ProductCondition
	)
	select @shipbatchid,ProductLineNumber,'09',OracleCode,'S', sum(QuantityShipped), '70', sum(NumOrder),
		DistributionCenterCode, '0' from #pr_CreateTransactionsForOracleInventory
		group by 	ProductLineNumber,OracleCode,	DistributionCenterCode

	/*
	select ProductLineNumber,'09',OracleCode,'S', sum(QuantityShipped), '70', sum(NumOrder),
		DistributionCenterCode, '0' from #pr_CreateTransactionsForOracleInventory
		group by 	ProductLineNumber,OracleCode,	DistributionCenterCode
	*/

	/* update the shipment order record */
	Update ShipmentOrder set ShipmentBatchID = @shipbatchid, IsShipmentBatchCreated=1
		where ShipmentOrder.IsShipmentBatchCreated=0
		
	drop table #pr_CreateTransactionsForOracleInventory
GO

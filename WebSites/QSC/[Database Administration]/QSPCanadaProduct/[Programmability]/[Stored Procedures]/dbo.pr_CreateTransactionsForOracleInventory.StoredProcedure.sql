USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateTransactionsForOracleInventory]    Script Date: 06/07/2017 09:17:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pr_CreateTransactionsForOracleInventory]
as
	/* Prep tables that will be used to seed oracle inventory interface tables */
	

	declare @shipbatchid int


	/* Get the records from shipment order that we haven't pushed up yet */
	select P.OracleCode, 
		Sum(QuantityShipped) as QuantityShipped,
		count(*) as NumOrder, 
		P.ProductLine,DistributionCenter.Name into #Temp
		from CustomerOrderDetail,CustomerOrderHeader,Batch, 
			QSPCanadaProduct..Pricing_Details as PD,
			QSPCanadaProduct..Product as P ,
		        ShipmentOrder,
			DistributionCenter
		where CustomerOrderHeaderInstance=Instance
		      and OrderBatchDate=Date
		      and OrderBatchID = Batch.id
	      		and ShipmentOrder.OrderID = Batch.OrderID
		      and ShipmentOrder.DistributionCenterID=CustomerOrderDetail.DistributionCenterID
		      and DistributionCenter.ID = CustomerOrderDetail.DistributionCenterID
		      and MagPrice_instance = PricingDetailsID
		      and P.OracleCode=PD.OracleCode
		      and Product_year = PD.Pricing_year
		      and Product_Season = PD.Pricing_Season
		      and ShipmentOrder.IsShipmentBatchCreated='N'
		      group by P.OracleCode, P.ProductLine,DistributionCenter.Name

--sp_columns 'ShipmentBatch'
--sp_columns 'InventoryOracleTrans'
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
	select @shipbatchid,ProductLine,'09',OracleCode,'S', QuantityShipped, '70', NumOrder,
		Name, '0' from #temp

	/* update the shipment order record */
	Update ShipmentOrder set ShipmentBatchID = @shipbatchid, IsShipmentBatchCreated=1
		where ShipmentOrder.IsShipmentBatchCreated='N'
		
	drop table #temp
GO

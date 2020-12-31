USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateInventoryForShippedBatch]    Script Date: 06/07/2017 09:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[pr_UpdateInventoryForShippedBatch]
	@batchdate datetime,
	@id int,           -- order batch
	@distCenterID int,      -- dist center id
	@shipmentID int
as
	-- draw down the inventory for this batch and dist center
	-- and then replaced stuff
--sp_columns 'CustomerOrderDetail'
	select sum(QuantityShipped) as QuantityShipped, 
		sum(QuantityReserved) as QuantityReserved,
		 P.OracleCode into #pr_UpdateInventoryForShippedBatch_temp
		from CustomerOrderDetail,CustomerOrderHeader,Batch, 
			QSPCanadaProduct..Pricing_Details PD,
			QSPCanadaProduct..Product P		
		where CustomerOrderHeaderInstance=Instance
		      and OrderBatchDate=Date
		      and OrderBatchID = id
		      and Date = @batchdate
		      and ID = @id
		      and CustomerOrderDetail.ShipmentID = @shipmentID
  	              and CustomerOrderDetail.DistributionCenterID= @distCenterID
		      and CustomerOrderDetail.StatusInstance = 508 -- Shipped
		      and ReplacedProductQty=0
		      and MagPrice_instance = PricingDetailsID
		     and PD.Product_instance=P.Product_Instance
/*
		      and P.Product_Code=PD.product_code
		      and P.Product_Year = PD.Pricing_Year
	              and P.Product_Season = PD.Pricing_Season
*/
		      group by P.OracleCode

	--  qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter ('SA00090024',2)
	-- on hand quantity gets drawn down as well as qty reserved

	update QSPCanadaProduct..ProductInventory set QtyOnHand=QtyOnHand-QuantityShipped,
		QtyReserved = QtyReserved - QuantityReserved
	        from QSPCanadaProduct..ProductInventory, #pr_UpdateInventoryForShippedBatch_temp
		where
		      QSPCanadaProduct..ProductInventory.OracleCode = #pr_UpdateInventoryForShippedBatch_temp.OracleCode		     
		      and DistributionCenterName =
			 qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(QSPCanadaProduct..ProductInventory.OracleCode ,@distCenterID)

/*
	update QSPCanadaProduct..ProductInventory set QtyOnHand=QtyOnHand-QuantityShipped,
		QtyReserved = QtyReserved - QuantityReserved
	        from QSPCanadaProduct..ProductInventory,DistributionCenter, #temp
		where
		      QSPCanadaProduct..ProductInventory.OracleCode = #temp.OracleCode
		      and DistributionCenter.ID = @distCenterID
		      and DistributionCenter.Name = DistributionCenterName
*/	
	drop table #pr_UpdateInventoryForShippedBatch_temp
	if(@@error<> 0)
	begin

		RAISERROR( 'Error updating inventory',16,1)
	
	end
GO

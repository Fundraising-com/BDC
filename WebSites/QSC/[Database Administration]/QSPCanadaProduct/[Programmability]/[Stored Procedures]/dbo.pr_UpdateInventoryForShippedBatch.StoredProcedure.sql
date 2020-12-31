USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateInventoryForShippedBatch]    Script Date: 06/07/2017 09:18:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pr_UpdateInventoryForShippedBatch]
	@batchdate datetime,
	@id int,           -- order batch
	@distCenterID int      -- dist center id
as
	-- draw down the inventory for this batch and dist center
	-- and then replaced stuff
--sp_columns 'CustomerOrderDetail'
	select sum(QuantityShipped) as QuantityShipped, 
		sum(QuantityReserved) as QuantityReserved,
		 OracleCode into #temp
		from CustomerOrderDetail,CustomerOrderHeader,Batch, 
			QSPCanadaProduct..Pricing_Details		
		where CustomerOrderHeaderInstance=Instance
		      and OrderBatchDate=Date
		      and OrderBatchID = id
		      and Date = @batchdate
		      and ID = @id
  	              and CustomerOrderDetail.DistributionCenterID= @distCenterID
		      and CustomerOrderDetail.StatusInstance = 508 -- Shipped
		      and ReplacedProductQty=0
		      and MagPrice_instance = PricingDetailsID
		      group by OracleCode


	-- on hand quantity gets drawn down as well as qty reserved
	update QSPCanadaProduct..ProductInventory set QtyOnHand=QtyOnHand-QuantityShipped,
		QtyReserved = QtyReserved - QuantityReserved
	        from QSPCanadaProduct..ProductInventory,DistributionCenter, #temp
		where
		      QSPCanadaProduct..ProductInventory.OracleCode = #temp.OracleCode
		      and DistributionCenter.ID = @distCenterID
		      and DistributionCenter.Name = DistributionCenterName

	drop table #temp
	if(@@error<> 0)
	begin

		RAISERROR( 'Error updating inventory',16,1)
	
	end
GO

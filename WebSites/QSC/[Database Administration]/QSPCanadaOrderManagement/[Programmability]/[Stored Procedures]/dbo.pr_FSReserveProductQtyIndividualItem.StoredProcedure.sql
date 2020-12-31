USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_FSReserveProductQtyIndividualItem]    Script Date: 06/07/2017 09:19:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE       procedure [dbo].[pr_FSReserveProductQtyIndividualItem]
	@coh int,
	@transid int,
	@distCenterID int,
	@prodCode varchar (20)
as 
BEGIN TRAN T1	

--SET NOCOUNT ON
	declare @Error int
	select @Error = 0

	update CustomerOrderDetail
		set QuantityReserved = CustomerOrderDetail.Quantity, DistributionCenterID=@distCenterID --, Status=TBD
		from CustomerOrderDetail, Batch, CustomerOrderHeader
		where CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
		and CustomerOrderDetail.CustomerOrderHeaderInstance=CustomerOrderHeader.Instance
		and CustomerOrderDetail.CustomerOrderHeaderInstance=@coh
		and CustomerOrderDetail.TransID = @transid
		and CustomerOrderDetail.ProductCode=@prodCode

	if(@@error <> 0)
	begin
		select @Error = 1
	end
-- Update all individual customer order detail records for desired product, setting QuantityReserved = Quantity
/*
	update CustomerOrderDetail
	set QuantityReserved = CustomerOrderDetail.Quantity, DistributionCenterID=@distCenterID --, Status=TBD
	from CustomerOrderDetail, Batch, CustomerOrderHeader, QSPCanadaProduct..PRICING_DETAILS, QSPCanadaProduct..Product, QSPCanadaProduct..ProductInventory, DistributionCenter
	where CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
	and CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
	and CustomerOrderDetail.CustomerOrderHeaderInstance=CustomerOrderHeader.Instance
	and QSPCanadaProduct..PRICING_DETAILS.MagPrice_Instance=CustomerOrderDetail.PricingDetailsID
	and QSPCanadaProduct..Product.Product_Code=QSPCanadaProduct..PRICING_DETAILS.Product_Code 
	and QSPCanadaProduct..Product.Product_Year=QSPCanadaProduct..PRICING_DETAILS.Pricing_Year 
	and QSPCanadaProduct..Product.Product_Season=QSPCanadaProduct..PRICING_DETAILS.Pricing_Season
	and QSPCanadaProduct..ProductInventory.OracleCode=QSPCanadaProduct..PRICING_DETAILS.OracleCode
--	and DistributionCenter.[Name]=QSPCanadaProduct..ProductInventory.DistributionCenterName
	and QSPCanadaProduct..ProductInventory.DistributionCenterName = 
		qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(QSPCanadaProduct..PRICING_DETAILS.OracleCode,
			 @distCenterID)
	and QSPCanadaProduct..Product.Type <> 46001
	and Batch.OrderId=@orderID
--	and DistributionCenter.[Id]=@distCenterID
	and QSPCanadaProduct..Product.Product_Code=@prodCode
*/
	-- Create temporary table with OracleCode and total quantity
	
	if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#tmpTotal]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[#tmpTotal]

	create table dbo.#tmpTotal
	(
		OracleCode varchar (50),
		DistName varchar (50),
		QtyReserved int
	)
	
	insert into #tmpTotal
	select QSPCanadaProduct..Product.OracleCode, 
		 qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(QSPCanadaProduct..Product.OracleCode,
					 @distCenterID), sum(CustomerOrderDetail.QuantityReserved) 
	from Batch
		join CustomerOrderHeader on CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
		join CustomerOrderDetail on CustomerOrderDetail.CustomerOrderHeaderInstance=CustomerOrderHeader.Instance
		join QSPCanadaProduct..PRICING_DETAILS on QSPCanadaProduct..PRICING_DETAILS.MagPrice_Instance=CustomerOrderDetail.PricingDetailsID
		join QSPCanadaProduct..Product on QSPCanadaProduct..Product.Product_Instance=QSPCanadaProduct..PRICING_DETAILS.Product_Instance
		join QSPCanadaProduct..ProductInventory on 
			QSPCanadaProduct..ProductInventory.OracleCode=QSPCanadaProduct..Product.OracleCode
			and qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(QSPCanadaProduct..Product.OracleCode,
					 @distCenterID) = QSPCanadaProduct..ProductInventory.DistributionCenterName
	--	join DistributionCenter on DistributionCenter.[Name]=QSPCanadaProduct..ProductInventory.DistributionCenterName
		where QSPCanadaProduct..Product.Type <> 46001
		and CustomerOrderDetail.CustomerOrderHeaderInstance=@coh
		and CustomerOrderDetail.TransID = @transid
	--	and DistributionCenter.[Id]=@distCenterID
--		and QSPCanadaProduct..Product.Product_Code=@prodCode
		group by QSPCanadaProduct..Product.OracleCode
	
	-- Update product inventory QtyReserved for each OracleCode
--select * from #tmpTotal
	
	update QSPCanadaProduct..ProductInventory
		set QSPCanadaProduct..ProductInventory.QtyReserved = QSPCanadaProduct..ProductInventory.QtyReserved + #tmpTotal.QtyReserved
		from QSPCanadaProduct..ProductInventory, #tmpTotal
		where QSPCanadaProduct..ProductInventory.OracleCode=#tmpTotal.OracleCode 
				and qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(QSPCanadaProduct..ProductInventory.OracleCode,
						 @distCenterID)=  QSPCanadaProduct..ProductInventory.DistributionCenterName
	if(@@error <> 0)
	begin
		select @Error = 1
	
	end
if( @Error = 0 )
begin
	COMMIT TRAN T1
end
else
begin

	/*
	**   Rollback the whole thing and set shipment to -1
	*/
	ROLLBACK tran T1
	declare @str varchar(500)
	select @str = 'Error reserving'+char(13)+cast( @coh as varchar(20) ) + char(13) + @prodCode
	exec QSPCanadaCommon..Send_EMail  'pr_FSReserveProductQtyIndividualItem.com',
				'qsp-qspfulfillment-dev@qsp.com',
				'pr_FSReserveProductQtyIndividualItem', @str


end
	drop table dbo.#tmpTotal
GO

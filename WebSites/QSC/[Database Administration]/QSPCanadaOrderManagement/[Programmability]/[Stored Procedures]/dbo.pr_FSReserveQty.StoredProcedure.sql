USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_FSReserveQty]    Script Date: 06/07/2017 09:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_FSReserveQty]
	@orderID int,
	@distCenterID int
as 

-- Update all individual customer order detail records, setting QuantityReserved = Quantity

update CustomerOrderDetail
set QuantityReserved = CustomerOrderDetail.Quantity, DistributionCenterID=@distCenterID --, Status=TBD
from CustomerOrderDetail, Batch, CustomerOrderHeader, QSPCanadaProduct..PRICING_DETAILS, QSPCanadaProduct..Product, QSPCanadaProduct..ProductInventory, DistributionCenter
where CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
and CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
and CustomerOrderDetail.CustomerOrderHeaderInstance=CustomerOrderHeader.Instance
and QSPCanadaProduct..PRICING_DETAILS.MagPrice_Instance=CustomerOrderDetail.PricingDetailsID
and QSPCanadaProduct..Product.Product_Code=QSPCanadaProduct..PRICING_DETAILS.Product_Code and QSPCanadaProduct..Product.Product_Year=QSPCanadaProduct..PRICING_DETAILS.Pricing_Year and QSPCanadaProduct..Product.Product_Season=QSPCanadaProduct..PRICING_DETAILS.Pricing_Season
and QSPCanadaProduct..ProductInventory.OracleCode=QSPCanadaProduct..PRICING_DETAILS.OracleCode
and DistributionCenter.[Name]=QSPCanadaProduct..ProductInventory.DistributionCenterName
and QSPCanadaProduct..Product.Type <> 46001
and Batch.OrderId=@orderID
and DistributionCenter.[Id]=@distCenterID

-- Create temporary table with OracleCode and total quantity

create table #tmpTotal
(
	OracleCode varchar (50),
	DistName varchar (50),
	QtyReserved int
)

insert into #tmpTotal
select QSPCanadaProduct..PRICING_DETAILS.OracleCode, @distCenterID, sum(CustomerOrderDetail.QuantityReserved) from Batch
join CustomerOrderHeader on CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
join CustomerOrderDetail on CustomerOrderDetail.CustomerOrderHeaderInstance=CustomerOrderHeader.Instance
join QSPCanadaProduct..PRICING_DETAILS on QSPCanadaProduct..PRICING_DETAILS.MagPrice_Instance=CustomerOrderDetail.PricingDetailsID
join QSPCanadaProduct..Product on QSPCanadaProduct..Product.Product_Code=QSPCanadaProduct..PRICING_DETAILS.Product_Code and QSPCanadaProduct..Product.Product_Year=QSPCanadaProduct..PRICING_DETAILS.Pricing_Year and QSPCanadaProduct..Product.Product_Season=QSPCanadaProduct..PRICING_DETAILS.Pricing_Season
join QSPCanadaProduct..ProductInventory on QSPCanadaProduct..ProductInventory.OracleCode=QSPCanadaProduct..PRICING_DETAILS.OracleCode
join DistributionCenter on DistributionCenter.[Name]=QSPCanadaProduct..ProductInventory.DistributionCenterName
where QSPCanadaProduct..Product.Type <> 46001
and Batch.OrderId=@orderID
and DistributionCenter.[Id]=@distCenterID
group by QSPCanadaProduct..PRICING_DETAILS.OracleCode

-- Update product inventory QtyReserved for each OracleCode

update QSPCanadaProduct..ProductInventory
set QtyReserved = #tmpTotal.QtyReserved
from QSPCanadaProduct..ProductInventory, #tmpTotal, DistributionCenter
where QSPCanadaProduct..ProductInventory.OracleCode=#tmpTotal.OracleCode
and DistributionCenter.[Id]=@distCenterID
and DistributionCenter.[Name]=QSPCanadaProduct..ProductInventory.DistributionCenterName

drop table #tmpTotal
GO

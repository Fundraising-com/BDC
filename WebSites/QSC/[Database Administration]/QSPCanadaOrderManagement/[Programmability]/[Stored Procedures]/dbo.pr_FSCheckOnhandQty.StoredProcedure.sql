USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_FSCheckOnhandQty]    Script Date: 06/07/2017 09:19:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     procedure [dbo].[pr_FSCheckOnhandQty]
	@orderID int,
	@distCenterID int,
	@retVal int output
as 
set nocount on
-- Test whether entire batch can be fulfilled at given distribution center
-- Return value: 0 -> NO; 1 -> YES

--insert into jfp_trace (c1, c2) values ('pr_FSCheckOnhandQty 01', getdate())

declare @tmpStr1 varchar (50)
declare @tmpStr2 varchar (50)

-- Initial query verifies every product in batch is stored at given center
-- Note: Ignore Product.Type = magazine (46001)

select @tmpStr1='' from Batch
join CustomerOrderHeader on CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
join CustomerOrderDetail on CustomerOrderDetail.CustomerOrderHeaderInstance=CustomerOrderHeader.Instance
join QSPCanadaProduct..PRICING_DETAILS on QSPCanadaProduct..PRICING_DETAILS.MagPrice_Instance=CustomerOrderDetail.PricingDetailsID
join QSPCanadaProduct..Product on QSPCanadaProduct..Product.Product_Code=QSPCanadaProduct..PRICING_DETAILS.Product_Code and QSPCanadaProduct..Product.Product_Year=QSPCanadaProduct..PRICING_DETAILS.Pricing_Year and QSPCanadaProduct..Product.Product_Season=QSPCanadaProduct..PRICING_DETAILS.Pricing_Season
Left join QSPCanadaProduct..ProductInventory on QSPCanadaProduct..ProductInventory.OracleCode=QSPCanadaProduct..Product.OracleCode
	and qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(QSPCanadaProduct..Product.OracleCode,
			 @distCenterID) = QSPCanadaProduct..ProductInventory.DistributionCenterName

--join DistributionCenter on DistributionCenter.[Name]=QSPCanadaProduct..ProductInventory.DistributionCenterName
where QSPCanadaProduct..Product.Type <> 46001
and Batch.OrderId=@orderID
--and DistributionCenter.[Id]=@distCenterID
and  QSPCanadaProduct..ProductInventory.OracleCode is null
group by QSPCanadaProduct..Product.OracleCode

--insert into jfp_trace (c1, c2) values ('pr_FSCheckOnhandQty 02', getdate())

-- Left join recordset indicates no
if @@rowcount = 1 
begin
	select @retVal = 0
end
else
begin


	-- Second query verifies every product in batch has enough inventory on hand,
	-- minus already reserved, to fulfill customer quantity request

	select @tmpStr1 = '' from Batch
	join CustomerOrderHeader on CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
	join CustomerOrderDetail on CustomerOrderDetail.CustomerOrderHeaderInstance=CustomerOrderHeader.Instance
	join QSPCanadaProduct..PRICING_DETAILS on QSPCanadaProduct..PRICING_DETAILS.MagPrice_Instance=CustomerOrderDetail.PricingDetailsID
	join QSPCanadaProduct..Product on QSPCanadaProduct..Product.Product_Code=QSPCanadaProduct..PRICING_DETAILS.Product_Code and QSPCanadaProduct..Product.Product_Year=QSPCanadaProduct..PRICING_DETAILS.Pricing_Year and QSPCanadaProduct..Product.Product_Season=QSPCanadaProduct..PRICING_DETAILS.Pricing_Season
	join QSPCanadaProduct..ProductInventory on QSPCanadaProduct..ProductInventory.OracleCode=QSPCanadaProduct..PRICING_DETAILS.OracleCode
		and qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(QSPCanadaProduct..ProductInventory.OracleCode,
			 @distCenterID) = QSPCanadaProduct..ProductInventory.DistributionCenterName
---	join DistributionCenter on DistributionCenter.[Name]=QSPCanadaProduct..ProductInventory.DistributionCenterName
	where QSPCanadaProduct..Product.Type <> 46001
	and Batch.OrderId=@orderID
--	and DistributionCenter.[Id]=@distCenterID
	group by QSPCanadaProduct..Product.OracleCode
	having sum(CustomerOrderDetail.Quantity) >= 
			sum(QSPCanadaProduct..ProductInventory.QtyOnHand) - sum(QSPCanadaProduct..ProductInventory.QtyReserved)

	-- A single recordset is enough to indicate one product had more requested quantitiy
	-- than available inventory

--insert into jfp_trace (c1, c2) values ('pr_FSCheckOnhandQty 03', getdate())

	if  @@rowcount= 0
	begin
		select @retVal = 1
	end
	else
	begin
		select @retVal = 0
	end

--insert into jfp_trace (c1, c2) values ('pr_FSCheckOnhandQty 04', getdate())

end
GO

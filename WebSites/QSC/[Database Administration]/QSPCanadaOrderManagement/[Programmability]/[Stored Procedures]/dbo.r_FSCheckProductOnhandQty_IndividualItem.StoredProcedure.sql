USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[r_FSCheckProductOnhandQty_IndividualItem]    Script Date: 06/07/2017 09:20:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    procedure [dbo].[r_FSCheckProductOnhandQty_IndividualItem]
	@orderID int,
	@coh int,
	@transid int,
	@distCenterID int,
	@prodCode varchar (20),
	@retVal int output
as 

-- Test whether entire batch of desired product code can be fulfilled at given distribution center
-- Return value: 0 -> NO; 1 -> YES

declare @tmpStr1 varchar (50)
declare @tmpStr2 varchar (50)

-- Initial query verifies specific product in batch is stored at given center
-- Note: Ignore Product.Type = magazine (46001)

select  QSPCanadaProduct..Product.OracleCode  from Batch
join CustomerOrderHeader on CustomerOrderHeader.OrderBatchDate=Batch.[Date] and CustomerOrderHeader.OrderBatchID=Batch.[ID]
join CustomerOrderDetail on CustomerOrderDetail.CustomerOrderHeaderInstance=CustomerOrderHeader.Instance
join QSPCanadaProduct..PRICING_DETAILS on QSPCanadaProduct..PRICING_DETAILS.MagPrice_Instance=CustomerOrderDetail.PricingDetailsID
join QSPCanadaProduct..Product on QSPCanadaProduct..Product.Product_Instance=QSPCanadaProduct..PRICING_DETAILS.Product_Instance 
join QSPCanadaProduct..ProductInventory on QSPCanadaProduct..ProductInventory.OracleCode=QSPCanadaProduct..Product.OracleCode
	and QSPCanadaProduct..ProductInventory.DistributionCenterName = qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(QSPCanadaProduct..Product.OracleCode,
			 			CustomerOrderDetail.DistributionCenterID)
where QSPCanadaProduct..Product.Type <> 46001
and Batch.OrderId=@orderID
and CustomerOrderHeader.instance=@coh
and TransID = @transid
and QSPCanadaProduct..Product.Product_Code=@prodCode
group by QSPCanadaProduct..Product.OracleCode
/*
 qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(P.OracleCode,
			 			CustomerOrderDetail.DistributionCenterID)
*/
-- Empty recordset indicates no

if @@rowcount =0
begin

	select @retVal=0
end
/*
if @tmpStr1 is null
begin
	select @retVal = 0
end
*/
else
begin
	declare @oraclecode varchar(200)

	declare @onhand int
	declare @qty int
	select @qty = Quantity from CustomerOrderDetail where CustomerOrderHeaderInstance=@coh and Transid = @transid

	
	select @oraclecode = QSPCanadaProduct..Product.OracleCode from CustomerOrderDetail,QSPCanadaProduct..Product,QSPCanadaProduct..PRICING_DETAILS
		where CustomerOrderHeaderInstance=@coh and Transid = @transid
			and  QSPCanadaProduct..PRICING_DETAILS.MagPrice_Instance=CustomerOrderDetail.PricingDetailsID
			and  QSPCanadaProduct..Product.Product_Instance=QSPCanadaProduct..PRICING_DETAILS.Product_Instance 

	-- Second query verifies every product in batch has enough inventory on hand,
	-- minus already reserved, to fulfill customer quantity request
select @qty as Ordered, * from  QSPCanadaProduct..ProductInventory where OracleCode=@oraclecode
	select @onhand = QtyOnHand -QtyReserved from  QSPCanadaProduct..ProductInventory where OracleCode=@oraclecode


	-- A single recordset is enough to indicate one product had more requested quantitiy
	-- than available inventory

	if @qty < @onhand
	begin
		
		select @retVal = 1
	end
	else
	begin
		-- We have enough FS item no BackOrder MS Sept 15th 2005 
		Select '1' from QSPCanadaOrderManagement..Batch
		Where OrderId=@OrderId And OrderQualifierId=39007 --FS

		if @@rowcount = 1
		begin
			select @retVal = 1	
		end
		else
			select @retVal = 0
	end


end
GO

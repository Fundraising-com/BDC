USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[Pr_FSUnReserveOrderItemsQty]    Script Date: 06/07/2017 09:19:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Pr_FSUnReserveOrderItemsQty] 
	@OrderId 	Int, 
	@RetVal	Int OutPut,
	@ErrorMessage Varchar(100) OutPut

AS

	Declare @OracleCode				Varchar(50),
			@DistributionCenterName	Varchar(50),
			@QuanityToUnreserve 	Int,
			@QtyCurrentlyReserved	Int,
			@coh 					Int,
			@TransId 				Int,
			@DistributionCenterId 	int,
			@OrderQualifierID		Int,
			@NoItemFound			Int

	Set @NoItemFound = 1

	Select	@DistributionCenterId = Case OrderQualifierId 
				When 39006 Then 1	--Kanata @QSP
				When 39007 Then 1	--FS 
				When 39017 Then 1	--Bonus
				When 39018 Then 1	--Kanata Problem Solver
				Else 2				--Unigistix
				End
	From	QSPcanadaOrderManagement..Batch 
	Where	OrderId = @OrderID

	Declare	AllItem Cursor For
	Select 	prd.oraclecode,
       		qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(p.OracleCode,@DistributionCenterId), 
	       	Sum(d.QuantityReserved),
			d.CustomerOrderHeaderInstance,
			d.TransId
	From 	QSPCanadaOrderManagement..Batch B, 
			QSPCanadaOrderManagement..CustomerOrderHeader h , 
			QSPCanadaOrderManagement..CustomerOrderDetail d,
			QSPCanadaProduct..Pricing_Details prd, 
			QSPCanadaProduct..Product p,
			QSPCanadaProduct..ProductInventory Pinv
	Where	h.OrderBatchDate = B.Date
	And		h.OrderBatchID=b.ID
	And		d.CustomerOrderHeaderInstance=h.Instance
	And		prd.MagPrice_Instance=d.PricingDetailsID
	And		p.Product_Instance=prd.Product_Instance 
	And		pinv.OracleCode=p.OracleCode
	And		p.Type <> 46001				--Magazine
	And		b.OrderId= @Orderid			
	--And	b.OrderTypeCode=41002 			--CAFS
	And		b.orderqualifierid in (39006,39007,39017,39018)
	And		b.StatusInstance not in (40014, 40013) 	--Partiall fulf or fulfilled
	And		d.delflag = 0				--Not deleted
	And		qspcanadaordermanagement.dbo.Udf_GetCatalystDistributionCenter(p.OracleCode, @DistributionCenterId) = pinv.DistributionCenterName
	Group By p.OracleCode,orderid,prd.oraclecode,pinv.DistributionCenterName,productType,D.customerorderHeaderInstance,d.transid

	Begin Transaction

	Open AllItem
	Fetch Next From AllItem Into
		 @OracleCode ,@DistributionCenterName,@QuanityToUnreserve,@coh,@TransId
					
	While(@@Fetch_Status = 0)
	Begin
	
		Set @NoItemFound = 0
		Set @QtyCurrentlyReserved = 0
		--Select @OracleCode+ ' - '+@DistributionCenterName + '  -  '+cast(isnull(@QuanityToUnreserve,0) as varchar)	+'  -  '+cast(@coh as varchar)

		Select @QtyCurrentlyReserved = QSPCanadaProduct..ProductInventory.QtyReserved 
		From QSPCanadaProduct..ProductInventory
		Where QSPCanadaProduct..ProductInventory.OracleCode=@OracleCode
		And QSPCanadaProduct..ProductInventory.DistributionCenterName=@DistributionCenterName

		--unreserve only If the item quantity is already reserved
		If @QuanityToUnreserve >  0
		Begin
					
			Update QSPCanadaProduct..ProductInventory
			Set QSPCanadaProduct..ProductInventory.QtyReserved = QSPCanadaProduct..ProductInventory.QtyReserved - IsNull(@QuanityToUnreserve,0)
			From QSPCanadaProduct..ProductInventory
			Where QSPCanadaProduct..ProductInventory.OracleCode=@OracleCode
			And QSPCanadaProduct..ProductInventory.DistributionCenterName=@DistributionCenterName
		
			--If Product has been unreserved update the qty reserved in COD 
			If @@Error =0
			Begin
					
				Update	CustomerOrderDetail 
				Set		QuantityReserved = QuantityReserved - IsNull(@QuanityToUnreserve,0),
						Comment = 'Quanity Unreserved by UnReserve proc'	
				Where	CustomerOrderHeaderInstance = @coh
				And		TransId = @TransID
			
				If @@Error =1 OR @@RowCount <> 1
				Begin
					Set @ErrorMessage = 'Error Updating record in COD'	
					Set @RetVal = 1
				End
			End
			Else
			Begin
				Set @ErrorMessage = 'Unable to Update Product Inventory'	
				Set @RetVal = 1
			End
		End

		Fetch Next From AllItem  Into  @OracleCode ,@DistributionCenterName,@QuanityToUnreserve,@coh,@TransId
	End

	--If no items found to unreserve qty
	If @NoItemFound = 1
	Begin
		Set @ErrorMessage = 'Unable to get order items to unreserve'	
		Set @RetVal=1
	End

	If IsNull(@RetVal,0)=0
	Begin
		--Select 'Commit'
		Set @RetVal=0
		Commit
	End
	Else
	Begin
		--Select 'RollBack'
		Set @RetVal=1
		RollBack
	End

Close AllItem
Deallocate AllItem
GO

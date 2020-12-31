USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ShipBatch]    Script Date: 06/07/2017 09:18:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pr_ShipBatch]
	@batchdate datetime,
	@id int,           -- order batch
	@distCenterID int,      -- dist center id
	@carrierID int,
	@shipDate datetime,
	@expectedDeliveryDate datetime,
	@numberOfBoxes int,
	@weight numeric(10,2),
	@numberskids int,
	@weightunitofmeasure numeric(10,2),
	@comment varchar(256),
	@userid int,
	@shipmentID int OUTPUT  -- use to add waybills
as

	/* double check the status 
	   Better be a record in batch distribution center and it should be either picked
	   or Sent to TPL   - raise an error- spew an email?
	*/
	declare @status int
	declare @orderid int
	select @status=StatusInstance from BatchDistributionCenter where
		BatchDate = @batchdate and BatchID = @ID and DistributionCenterID = @distCenterID

	if(@@rowcount <> 1)
	begin
		RAISERROR( 'Should only be one record for this batch',16,1)
		return 1
	end


	if(@status <> 40010 or @status <> 40012)
	begin

		RAISERROR( 'Incorrect status for this batch',16,1)
		return 1
	end

	--Fetch the order id
	select @orderid=orderid from Batch where Date= @batchdate and Id = @id


	/* create the shipment */


	insert Shipment
	( 	CarrierID,
		ShipmentDate,
		CountryCode,
		ExpectedDeliveryDate,
		NumberBoxes,
		Weight,
		DateModified,
		UserIDModified,
		OperatorName,
		NumberSkids,
		WeightUnitOfMeasure,
		Comment
	)
	select 
		@carrierID ,
		@shipDate ,
		'CA',
		@expectedDeliveryDate,
		@numberOfBoxes,
		@weight ,
		GetDate(),
		@userid ,
		'',
		@numberskids ,
		@weightunitofmeasure ,
		@comment 

	if(@@error <> 0)
	begin
		select @shipmentID = -1
		RAISERROR( 'Cannot create shipment record',16,1)
	        exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com','karen_tracy@readersdigest.com,Charles_scally@readersdigest.com',
					'ShipBatch', 'Error in inserting Shipment Record'
		return 1		
	end
	select @shipmentID = Scope_Identity()

	-- SET COD to shipped and update it's shipment id for those COD's that actually got
	-- picked
	update CustomerOrderDetail Set StatusInstance = 508, ShipmentID=@shipmentID, QuantityShipped=Quantity
		from CustomerOrderDetail,CustomerOrderHeader,Batch
		where CustomerOrderHeaderInstance=Instance
		      and OrderBatchDate=Date
		      and OrderBatchID = id
		      and Date = @batchdate
		      and ID = @id
  	              and CustomerOrderDetail.DistributionCenterID= @distCenterID
		      and CustomerOrderDetail.StatusInstance = 511 -- Picked

	if(@@error <> 0)
	begin
		select @shipmentID = -1
		RAISERROR( 'Updating CustomerOrderDetail failed in ShipBatch',16,1)
	        exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com',
					'karen_tracy@readersdigest.com,Charles_scally@readersdigest.com',
					'ShipBatch', 'Updating CustomerOrderDetail failed in ShipBatch'

		return 1		
	end

	-- BatchDistributionCenter record is now shipped 			   
	update BatchDistributionCenter Set StatusInstance = 40011  -- shipped
		where BatchDate=@batchdate and BatchID=@id
			and DistributionCenterID = @distCenterID

	-- Log the entire order into ShipmentOrder
--sp_columns 'ShipmentOrder'
	insert ShipmentOrder
	(
		ShipmentID,
		OrderID,
		DistributionCenterID,
		ShipmentBatchID,
		IsShipmentBatchCreated
	)
	select @shipmentID, @orderid, @distCenterID, 0,'N'
	

	if(@@error <> 0)
	begin
		select @shipmentID = -1
		RAISERROR( 'Updating BatchDistributionCenter failed in ShipBatch',16,1)
	        exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com',
					'karen_tracy@readersdigest.com,Charles_scally@readersdigest.com',
					'ShipBatch', 'Updating BatchDistributionCenter failed in ShipBatch'
		return 1		
	end
	

	-- See if there are any outstanding things to ship on this batch if not flip the Batch status to fulfilled
	select * from 
		BatchDistributionCenter where
			BatchDate = @batchdate and BatchID = @ID and StatusInstance <> 40011

	if(@@rowcount=0)
	begin
		update Batch set StatusInstance=40013 where Date = @batchdate and ID = @ID 
		if(@@error <> 0)
		begin
			select @shipmentID = -1
			RAISERROR( 'Updating Batch failed in ShipBatch',16,1)
		        exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com',
						'karen_tracy@readersdigest.com,Charles_scally@readersdigest.com',
						'ShipBatch', 'Updating Batch failed in ShipBatch'
			return 1		
		end

	end

	exec pr_UpdateInventoryForShippedBatch @batchdate ,@id,	@distCenterID
GO

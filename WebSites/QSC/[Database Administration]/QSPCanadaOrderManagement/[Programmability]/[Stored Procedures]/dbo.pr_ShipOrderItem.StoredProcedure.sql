USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ShipOrderItem]    Script Date: 06/07/2017 09:20:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
**   5/5/06 KET  aded the update to the batch distribution center
*/

CREATE            procedure [dbo].[pr_ShipOrderItem]
	--@batchdate datetime,
	--@id int,           -- order batch
	@customerOrderHeaderInstance int,  -- a string of orderids comma seperated
	@transID int,
	@distCenterID int,
	@carrierID int,
	@shipDate varchar(50),
	@expectedDeliveryDate varchar(50),
	@numberOfBoxes int,
	@weight numeric(10,2),
	@numberofskids int,
	@weightunitofmeasure varchar(100),
	@comment varchar(256),
	@userid int,
	@shipmentID int OUTPUT,  -- use to add waybills
	@SessionId varchar(100)

as

BEGIN TRAN T1	

SET NOCOUNT ON

	declare @Error int
	select @Error = 0	
	if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempProductLine]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[#TempProductLine]
	
	CREATE TABLE [dbo].[#TempProductLine] (
		[ProductLine] [int] NOT NULL
	) ON [PRIMARY]

	
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
		Comment,
		FMEmailNotificationSent
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
		@numberofskids ,
		@weightunitofmeasure ,
		@comment ,
		'1/1/95'

	if(@@error <> 0)
	begin
		select @shipmentID = -1
		RAISERROR( 'Cannot create shipment record',16,1)
	        exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com','qsp-qspfulfillment-dev@qsp.com',
					'ShipBatch', 'Error in inserting Shipment Record'
		select @Error = 1
		
	end
	select @shipmentID = Scope_Identity()


	DECLARE @batchdate datetime, @id  int
	declare @orderid int	

	--Fetch the order info
	insert into #TempProductLine 
	(
		ProductLine
	)

	select producttype from CustomerOrderDetail where 
		CustomerOrderHeaderInstance=@customerOrderHeaderInstance
		and TransID = @transID
		and DelFlag=0

	select @OrderID = OrderID, @batchdate = [OrderBatchDate] , @Id = [orderbatchid] 
			from CustomerOrderHeader,Batch where Instance=@customerOrderHeaderInstance
				and OrderBatchDate = Date
				and OrderBatchID = ID

	/* double check the status 
	   Better be a record in batch distribution center and it should be either picked
	   or Sent to TPL   - raise an error- spew an email?
	*/
	declare @status int
	select @status=StatusInstance from BatchDistributionCenter,#TempProductLine where
		BatchDate = @batchdate and BatchID = @ID and DistributionCenterID = @distCenterID
		and #TempProductLine.ProductLine = BatchDistributionCenter.QSPProductLine
		
			
	if(@status <> 40010 and @status <> 40012 and @status <> 40014)
	begin

		RAISERROR( 'Incorrect status for this batch',16,1)
		select @Error = 1
	end
			
			
					
	--------------------------  Process The COD
	---   First see if there is a partial shipment in the Shipment Variations table.
	---   If Yes,  deal with each line item individually else update all items
			
	Declare @VariationCount int

	SELECT @VariationCount =  Count(*) FROM ShipmentVariation WHERE SessionId LIKE @SessionId
		
	IF @VariationCount > 0 
		BEGIN
			-- UPDATE ONLY THOSE COD's THAT ARE IN THE VARIATIONS TABLE
			---- MUST ADD UPDATE TO COMMENT FIELD ONCE WE ADD THE COLUMN TO  COD.  -----
			UPDATE
				CustomerOrderDetail 
			SET 
				StatusInstance = 508
				, ShipmentID=@shipmentID
				, QuantityShipped=Quantity
				, ReplacedProductCode = D.ReplacementItemId
				, ReplacedProductQty = D.QuantityReplaced
				--, QuantityShipped = D.QuantityShipped
			FROM 
				CustomerOrderDetail A
				, ShipmentVariation D
			WHERE 
				D.CustomerOrderHeaderInstance = A.CustomerOrderHeaderInstance
				AND A.CustomerOrderHeaderInstance = @customerOrderHeaderInstance
				and A.TransID = @transID
				AND A.CustomerOrderHeaderInstance = @customerOrderHeaderInstance
				AND D.CustomerOrderHeaderInstance = A.CustomerOrderHeaderInstance
				AND D.TransId = A.TransId		
				AND D.SessionId LIKE @SessionId
				AND D.ShipTF = 1				
			
		END
	ELSE
		BEGIN

			-- SET COD to shipped and update it's shipment id for those COD's that actually got
			-- picked
			update CustomerOrderDetail Set StatusInstance = 508, ShipmentID=@shipmentID, 
				QuantityShipped=Quantity
				from CustomerOrderDetail A
				where CustomerOrderHeaderInstance=@customerOrderHeaderInstance
					AND A.TransID = @transID
/*
			update BatchDistributionCenter Set StatusInstance = 40011  -- partially shipped
				from BatchDistributionCenter,#TempProductLine
				where BatchDate=@batchdate and BatchID=@id
					and DistributionCenterID = @distCenterID
					and #TempProductLine.ProductLine = BatchDistributionCenter.QSPProductLine
*/
			if(@@error <> 0)
			begin
				select @shipmentID = -1
				RAISERROR( 'Updating CustomerOrderDetail failed in ShipBatch',16,1)
			        exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com',
							'qsp-qspfulfillment-dev@qsp.com',
							'ShipBatch', 'Updating CustomerOrderDetail failed in ShipBatch'
		
				select @Error = 1	
			end
		END													
---- CHECK THE ORDER TO SEE IF THIS MAKES A COMPLETE SHIPPED ORDER
			
	DECLARE @ShippedCount int
	
	SELECT 
		@ShippedCount  = COUNT(*)
	FROM
		CustomerOrderDetail A
		, CustomerOrderHeader
		, Batch							
	
	WHERE 
		A.CustomerOrderHeaderInstance=Instance
		      and OrderBatchDate=Date
		      and OrderBatchID = id
		      and Date = @batchdate
		      and ID = @id
		      and A.StatusInstance IN (511, 509) -- Picked, pending to tpl
				      
	IF @VariationCount > 0 and @ShippedCount > 0 
		BEGIN
			

		-- BatchDistributionCenter record is now partially shipped 			   
		update BatchDistributionCenter Set StatusInstance = 40014  -- partially shipped
			from BatchDistributionCenter,#TempProductLine
			where BatchDate=@batchdate and BatchID=@id
				and DistributionCenterID = @distCenterID
				and #TempProductLine.ProductLine = BatchDistributionCenter.QSPProductLine
		END
	ELSE if @ShippedCount = 0 
		BEGIN

			-- BatchDistributionCenter record is now shipped 			   
			update BatchDistributionCenter Set StatusInstance = 40011  -- shipped
				from BatchDistributionCenter
				where BatchDate=@batchdate and BatchID=@id
		END

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
	select @shipmentID, @orderid, @distCenterID, 0,0
				
			
	if(@@error <> 0)
	begin
		select @shipmentID = -1
		RAISERROR( 'Updating BatchDistributionCenter failed in ShipBatch',16,1)
	        exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com',
					'qsp-qspfulfillment-dev@qsp.com',
					'ShipBatch', 'Updating BatchDistributionCenter failed in ShipBatch'
		select @Error = 1	
	end
	

	-- See if there are any outstanding things to ship on this batch if not flip the Batch status to fulfilled
	select 1 from 
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
							'qsp-qspfulfillment-dev@qsp.com',
							'ShipBatch', 'Updating Batch failed in ShipBatch'
				select @Error = 1	
			end
	
		end
			
	-- See if there is any partially shipped because that will override other status
	select 1 from 
		BatchDistributionCenter where
			BatchDate = @batchdate and BatchID = @ID and StatusInstance = 40014
			
	if(@@rowcount > 0)
		begin
			update Batch set StatusInstance=40014 where Date = @batchdate and ID = @ID 
			if(@@error <> 0)
			begin
				select @shipmentID = -1
				RAISERROR( 'Updating Batch failed in ShipBatch',16,1)
			        exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com',
							'qsp-qspfulfillment-dev@qsp.com',
							'ShipBatch', 'Updating Batch failed in ShipBatch'
				return 1		
			end
	
		end
	
			
	exec pr_UpdateInventoryForShippedBatch @batchdate ,@id,	@distCenterID, @ShipmentID
			

		
		
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
	select @str = 'Rollback occurred'+char(13)
	exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com',
				'qsp-qspfulfillment-dev@qsp.com',
				'ShipBatch-Rollback', @str
	select @ShipmentID = -1

end

drop table #TempProductLine
SELECT @ShipmentID As 'ShipmentId'
GO

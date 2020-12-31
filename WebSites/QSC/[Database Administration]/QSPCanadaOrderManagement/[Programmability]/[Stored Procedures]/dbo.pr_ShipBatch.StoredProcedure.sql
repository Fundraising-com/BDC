USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ShipBatch]    Script Date: 06/07/2017 09:20:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ShipBatch]
	@OrderIds varchar(512), --a string of orderids comma seperated
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
	@shipmentID int OUTPUT,  --use to add waybills
	@SessionId varchar(100),
	@ProdLine varchar(512),
	@ShipmentGroupID int = null

as

BEGIN TRAN T1	

	SET NOCOUNT ON

	declare @Error int
	select @Error = 0

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
		FMEmailNotificationSent,
		ShipmentGroupID
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
		'1/1/95',
		@ShipmentGroupID

	if(@@error <> 0)
	begin
		select @shipmentID = -1
		RAISERROR( 'Cannot create shipment record',16,1)
	        exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com','qsp-qspfulfillment-dev@qsp.com',
					'ShipBatch', 'Error in inserting Shipment Record'
		select @Error = 1
	end
	select @shipmentID = Scope_Identity()
	
	CREATE TABLE [dbo].[#TempBatches] (
		[OrderId] [int] NOT NULL
	) ON [PRIMARY]

	EXEC('INSERT INTO #TempBatches SELECT OrderId FROM Batch WHERE OrderId IN (' + @OrderIds + ')')
	
	DECLARE @OrderID int

	DECLARE C1 CURSOR FOR
	SELECT	OrderID
	FROM	#TempBatches
		
	OPEN C1
	FETCH NEXT FROM C1 INTO	@OrderID
		
	WHILE @@Fetch_Status = 0
	BEGIN

		insert ShipmentOrder
		(
			ShipmentID,
			OrderID,
			DistributionCenterID,
			ShipmentBatchID,
			IsShipmentBatchCreated
		)
		select @shipmentID, @orderid, @distCenterID, 0, 0
					
		if(@@error <> 0)
		begin
			select @shipmentID = -1
			RAISERROR( 'Updating BatchDistributionCenter failed in ShipBatch',16,1)
				exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com',
						'qsp-qspfulfillment-dev@qsp.com',
						'ShipBatch', 'Updating BatchDistributionCenter failed in ShipBatch'
			select @Error = 1	
		end

		FETCH NEXT FROM C1 INTO @OrderID
	END
	CLOSE C1
	DEALLOCATE C1

if( @Error = 0 )
begin
	COMMIT TRAN T1
end
else
begin
	ROLLBACK tran T1
	declare @str varchar(500)
	select @str = 'Rollback occurred'+char(13)+@OrderIds
	exec QSPCanadaCommon..Send_EMail  'ShipBatch@qsp.com',
				'qsp-qspfulfillment-dev@qsp.com',
				'ShipBatch-Rollback', @str
	select @ShipmentID = -1
end

SELECT @ShipmentID As 'ShipmentId'
GO

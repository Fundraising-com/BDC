USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CloseOrder]    Script Date: 06/07/2017 09:19:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_CloseOrder]
 @orderid int
as

set nocount on

BEGIN TRAN

BEGIN TRY

declare @OrderType int
declare @OrderQualifier int

declare @batchStatus int
--declare @distCenter int
declare @RetVal int
declare @msg1 varchar(4000)

EXEC pr_ShippingFee_Insert @OrderID

print 'Update ProductType ' + cast(getdate() as varchar(50)) 
UPDATE	cod
SET		ProductType =	(SELECT		TOP 1 cod2.ProductType
						FROM		CustomerOrderDetail cod2
						JOIN		CustomerOrderHeader coh2 on coh2.Instance = cod2.CustomerOrderHeaderInstance
						JOIN		Batch b2 on b2.ID = coh2.OrderBatchID and b2.Date = coh2.OrderBatchDate
						WHERE		b2.OrderID = b.OrderID
						AND			ISNULL(cod2.PricingDetailsID, 0) <> 0
						AND			cod2.ProductType IN (46001, 46018, 46022, 46023)
						ORDER BY	cod2.ProductType)
FROM	CustomerOrderDetail cod
JOIN	CustomerOrderHeader coh on coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
WHERE	ISNULL(cod.PricingDetailsID, 0) = 0
AND		b.OrderID = @OrderID

--Gift Cards and Discount Cards on Mag Order Form need to be Customer Delivered
UPDATE	cod
SET		IsShippedToAccount = 0
FROM	CustomerOrderDetail cod
JOIN	CustomerOrderHeader coh on coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
WHERE	b.OrderID = @OrderID
AND		coh.FormCode = '0745'
AND		cod.ProductType IN (46007, 46024)
AND		cod.IsShippedToAccount = 1

--print 'Starting Proc ' + cast(getdate() as varchar(50))
--print 'Selecting from batch ' + cast(getdate() as varchar(50))
select  @OrderQualifier=OrderQualifierID,@OrderType=ordertypecode
	 from batch 
		where 	 orderid=@orderid

/*
	if(@OrderQualifier = 39006 or @OrderQualifier=39017 Or @OrderQualifier=39018 Or @OrderQualifier=39022)
	begin
		select @distCenter = 1
	end
	else
	begin
		select @distCenter = 2
	end

	if(@OrderType in (41007,41011))
	begin
		select @distCenter = 1

	end
*/

--if 

--select @distCenter = 2

	DECLARE  @OnHandReturnValue int
    --print 'executing pr_FSCheckOnhandQty ' + cast(getdate() as varchar(50))
	--exec pr_FSCheckOnhandQty @OrderId, @distCenter, @OnHandReturnValue output
--	print 'Distribution Center: ' + str(@distCenter)
--print @OnHandReturnValue
--select 	@OnHandReturnValue = 0		

	/*if (@OnHandReturnValue=0 ) 
	begin
		select @RetVal = 0
	end
	else
	begin
		-- perform the pre verification routine
		select @RetVal = 1
	end*/	
	-- Only verify campaign tyep orders
select @RetVal = 1
--print '@RetVal: ' + str(@RetVal)

	if( @OrderQualifier <> 39006 and  @OrderQualifier <> 39007 and  @OrderQualifier <> 39016 and  @OrderQualifier <> 39017 and @OrderQualifier <> 39022 and @OrderType <> 41007  and @OrderType <> 41011   ) 
	begin
        print 'executing spPreCloseVerification ' + cast(getdate() as varchar(50))
		exec spPreCloseVerification @orderid, @RetVal OUTPUT
	end
--print '@RetVal: ' + str(@RetVal)
--select @RetVal = 1
	if(@RetVal=1)
	begin

        print 'executing DoCloseOrder ' + cast(getdate() as varchar(50))
		exec DoCloseOrder @orderid,1
		if( @OrderQualifier <> 39006 and  @OrderQualifier <> 39007 and  @OrderQualifier <> 39016 and  @OrderQualifier <> 39017 and @OrderType <> 41011)
		begin

		print 'executing spPostCloseVerification ' + cast(getdate() as varchar(50))	
        exec spPostCloseVerification @orderid, @RetVal OUTPUT
print 'Post Close ' + cast(getdate() as varchar(50))	
--print @RetVal
select @RetVal = 1

		end
        --print 'selecting from batch ' + cast(getdate() as varchar(50))
		select @batchStatus=statusinstance from batch where orderid=@orderid
	end
	else
	begin
		-- send email order in error
		select @msg1 = 'Pre close failed: ' +str(@orderid) + char(13)
        
		exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','qsp-qspfulfillment-dev@qsp.com',	'Closing Campaign orders', @msg1

	end

	if(@RetVal = 1)
	begin
		/*
		**  Queue reports and do any warehousing
		**  Field Supplies and Kanata stay at QSP
		**  
		**  41006	FM
		    41007	FMBULK stay w/us
		    39006	Kanata
		    39007	Field Supplies
	
		    39016       FM gift samples
		    39017      WFC Orders
		*/
		
		-- What is the batch status  if 40004 then there is something to get from the warehouse
	
		if(@batchStatus=40004)
		begin
	        print 'executing pr_Insert_BatchDistributionCenter ' + cast(getdate() as varchar(50))
			exec pr_Insert_BatchDistributionCenter @OrderId

			print 'executing pr_ProcessReserveQuantities_ByOrderId_V2 ' + cast(getdate() as varchar(50))
			exec pr_ProcessReserveQuantities_ByOrderId_V2 @OrderId
            print 'executing pr_cleanprintqueue ' + cast(getdate() as varchar(50))
			exec pr_cleanprintqueue @OrderId

			print 'ShipmentGroup Select ' + cast(getdate() as varchar(50))
			DECLARE @ShipmentGroupID INT
			DECLARE	ShipmentGroup CURSOR FOR
			SELECT	DISTINCT pl.ShipmentGroupID
			FROM	BatchDistributionCenter bdc
			JOIN	Batch b ON b.ID = bdc.BatchID AND b.Date = bdc.BatchDate
			JOIN	QSPCanadaCommon..QSPProductLine pl ON pl.ID = bdc.QSPProductLine
			WHERE	b.OrderID = @OrderID
			
			OPEN ShipmentGroup
			FETCH NEXT FROM ShipmentGroup INTO @ShipmentGroupID
			WHILE @@FETCH_STATUS = 0
			begin
				print 'executing pr_Ins_Report_Parameters_V2 ' + cast(getdate() as varchar(50))
				exec pr_Ins_Report_Parameters_V2 @OrderId, -1, @ShipmentGroupID

				FETCH NEXT FROM ShipmentGroup INTO @ShipmentGroupID

			end
			CLOSE ShipmentGroup
			DEALLOCATE ShipmentGroup

			print 'executing pr_OrderStageTracking_InsertAtWarehouse ' + cast(getdate() as varchar(50))
			exec pr_OrderStageTracking_InsertAtWarehouse @OrderID
	
/*		else
			begin
				
				select @msg1 = 'Inventory issue: ' +str(@orderid) + char(13)

				exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','karen_tracy@readersdigest.com',
					'Closing  orders', @msg1			end
*/
		end
		else
		begin
            print 'executing pr_cleanprintqueue ' + cast(getdate() as varchar(50))
			exec pr_cleanprintqueue @OrderId
            print 'executing pr_Ins_Report_Parameters_V2 ' + cast(getdate() as varchar(50))
			exec pr_Ins_Report_Parameters_V2 @OrderId, -1, null
		end
				
	
	end
	else
	begin
		-- send email order in error
		
		select @msg1 = 'Post close failed: ' +str(@orderid) + char(13)
		exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','qsp-qspfulfillment-dev@qsp.com', 'Closing Campaign orders-Post Failed', @msg1
		
	end

COMMIT TRANSACTION

END TRY

BEGIN CATCH

  ROLLBACK TRANSACTION
  PRINT ERROR_MESSAGE()

END CATCH  
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[DoCloseOrder]    Script Date: 06/07/2017 09:19:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DoCloseOrder]
	@orderID int,	
	@forceIt int
as
set nocount on
	declare @remitStatus int
	declare @status int
	declare @id int
	declare @batchDate datetime
	declare @count int
	declare @type int
	declare @isIncentive int
	declare @orderType int
    
    print 'Starting proc DoCloseOrder ' + cast(getdate() as varchar(50))
	-- do not close an order wih any outstanding cc payments or refunds
	-- that are pending sent or pending coming back
	-- can't even force it in this situation
    print 'Select count(*) from customerorderheader,customerpaymentheader,creditcardpayment,batch... ' + cast(getdate() as varchar(50)) 
	select @count=count(*) from QSPCanadaOrderManagement..CustomerOrderHeader,
				    QSPCanadaOrderManagement..CustomerPaymentHeader,
				    QSPCanadaOrderManagement..CreditCardPayment,
					QSPCanadaOrderManagement..Batch
				where orderbatchdate=date
					and orderbatchid=id
					and customerorderheaderinstance=QSPCanadaOrderManagement..CustomerOrderHeader.instance
					and Customerpaymentheaderinstance = QSPCanadaOrderManagement..CustomerPaymentHeader.Instance
					and QSPCanadaOrderManagement..CreditCardPayment.StatusInstance in (19003, 19004)
					AND ORDERID=@orderid
    
    print 'Select... from batch ' + cast(getdate() as varchar(50))
	select @status = StatusInstance,@batchDate=date, @id=id,
			@isIncentive=IsIncentive,
			@type=OrderQualifierID,
			@orderType=OrderTypeCode from Batch where orderid = @orderID

	
	if(@count <> 0)
	begin

		select 114	-- 'CC is outstanding' 
		return 0
	end


	if(@status = 40006)  --u absolutely cannot close this don't try! CC Pending
	begin
		select 114	-- 'CC is outstanding' --
		return 0
	end
	

	if( @status = 40004 or @status = 40001 or @status = 40005 or @status=40013)
	begin
                select 111 -- 'Cannot close a batch with status of new, approved or cancelled'
		--111 -- code detail errors Cannot close a batch with status of new, approved or cancelled
		--return @@error
	end
	else
	begin
	    print 'update batch ' + cast(getdate() as varchar(50))
		update batch set statusinstance = 40004, DateBatchCompleted =GetDate() where orderid=@orderID

		DECLARE @CampaignID INT
		SELECT	@CampaignID = b.CampaignID
		FROM	Batch b
		WHERE	b.OrderID = @OrderID

		--Update Cookie Dough Group Profit as it depends on total quantity
		print 'Has CD Check ' + cast(getdate() as varchar(50)) 
		DECLARE @HasCD BIT
		SELECT	@HasCD = 1
		FROM	Batch b
		JOIN	CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
		JOIN	CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
		WHERE	b.CampaignID = @CampaignID
		AND		cod.ProductType = 46018
		AND		cod.StatusInstance IN (502, 509, 510)

		IF (@HasCD = 1)
		BEGIN
			print 'Cookie Dough Item Check ' + cast(getdate() as varchar(50)) 
			DECLARE @ItemCount INT
			SELECT	@ItemCount = SUM(cod.Quantity)
			FROM	Batch b
			JOIN	CustomerOrderHeader coh
						ON	coh.OrderBatchID = b.ID
						AND	coh.OrderBatchDate = b.Date
			JOIN	CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
			WHERE	b.CampaignID = @CampaignID
			AND		cod.ProductType = 46018
			AND		cod.StatusInstance IN (502, 509, 510)

			print 'Cookie Dough Get GP ' + cast(getdate() as varchar(50)) 
			DECLARE @ProfitPercentage NUMERIC(10,6)
			EXEC QSPCanadaFinance..GetGroupProfitPercentage @OrderID, @CampaignID, 46018, @ItemCount, @ProfitPercentage  OUTPUT

			print 'Cookie Dough Update GP ' + cast(getdate() as varchar(50)) 
			UPDATE	cod
			SET		GroupProfitAmount = cod.Price * @ProfitPercentage,
					Net = cod.Price - (cod.Price * @ProfitPercentage)
			FROM	Batch b
			JOIN	CustomerOrderHeader coh
						ON	coh.OrderBatchID = b.ID
						AND	coh.OrderBatchDate = b.Date
			JOIN	CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
			WHERE	b.CampaignID = @CampaignID
			AND		cod.ProductType = 46018
		END

		print 'Digital Item Email Address ' + cast(getdate() as varchar(50)) 
		exec pr_CustomerOrderDetail_DigitalItemMissingEmailAddress_SetInError @OrderID

		--exec pr_SetStatusOnStaffTimeTitles @orderID
		--exec pr_SetStatusOnLoonieTimeTitles @orderID
        print 'spPrepareRemitBatches ' + cast(getdate() as varchar(50))
		exec  spPrepareRemitBatches @batchDate, @id, @remitStatus OUTPUT
		

		/*
		** flip any unremittable magazines due to customer status instance issue
		*/
        print 'flip any unremittable magazines due to customer status instance issue ' + + cast(getdate() as varchar(50))
        print 'update customerorderdetail from ... orderdetail,orderheader,customer,batch ' + cast(getdate() as varchar(50))
		Update QSPCanadaOrderManagement..CustomerOrderDetail Set StatusInstance=512
				   from QSPCanadaOrderManagement..CustomerOrderDetail ,
					QSPCanadaOrderManagement..CustomerOrderHeader,
					QSPCanadaOrderManagement..Customer Customer,
					QSPCanadaOrderManagement..Batch
				   where  QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=
					QSPCanadaOrderManagement..CustomerOrderHeader.Instance
					and OrderBatchDate=Date
					and OrderBatchID=id
					and OrderID = @orderid
					and producttype = 46001
					and customerbilltoinstance=customer.instance
					and customer.statusinstance <> 300
		-- Set to unshippable
        print 'Set to unshippable ' + cast(getdate() as varchar(50))
        print 'update customerorderdetail,customerorderheader,customer,batch ' + cast(getdate() as varchar(50))
		Update QSPCanadaOrderManagement..CustomerOrderDetail Set StatusInstance=513
				   from QSPCanadaOrderManagement..CustomerOrderDetail ,
					QSPCanadaOrderManagement..CustomerOrderHeader,
					QSPCanadaOrderManagement..Customer Customer,
					QSPCanadaOrderManagement..Batch
				   where  QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=
					QSPCanadaOrderManagement..CustomerOrderHeader.Instance
					and OrderBatchDate=Date
					and OrderBatchID=id
					and OrderID = @orderid
					and producttype in (46006, 46007, 46023, 46024)
					and IsShippedToAccount = 0
					and customerbilltoinstance=customer.instance
					and customer.statusinstance <> 300

		if(@remitStatus = 0)
		begin

			select 112 -- 'Batch successfully approved'

			-- set any pickable orders


			declare @SuccessTF int
			if(@type <> 39009)
			begin
				-- Main CA order only
				if(@type =39001)-- and @orderType = 41001)
				begin
                    print 'exec pr_mergeOnline ' + cast(getdate() as varchar(50))
					exec pr_MergeOnline @orderid

					--Ensure Online Account Delivery is turned off
					UPDATE	QSPCanadaCommon..Campaign
					SET		IsshippingToAccountAllowed = 0, DateModified = GETDATE()
					WHERE	IsshippingToAccountAllowed = 1
					AND		ID = @CampaignID
				end
				if(@orderType <> 41009)
				begin
					if(@type =39001)
					begin
                        print 'exec GenerateIncentiveOrders_NonEnvelope_V2 ' + cast(getdate() as varchar(50)) 
						exec GenerateIncentiveOrders_NonEnvelope_V2 @id,@batchDate, 0,@SuccessTF OUTPUT, '1'
					end
					else if(@type = 39002)
					begin
                        print 'exec GenerateIncentiveOrders_NonEnvelope_V2 ' + cast(getdate() as varchar(50))
						exec GenerateIncentiveOrders_NonEnvelope_V2 @id,@batchDate, 1,@SuccessTF OUTPUT, '1'
					end
				end
			end
            
			-- batch stays approved
            print 'batch stays approved' + cast(getdate() as varchar(50))
            print 'update customerorderdetail,customerorderheader,batch ' + cast(getdate() as varchar(50))
			update QSPCanadaOrderManagement..CustomerOrderDetail set StatusInstance=510
				from QSPCanadaOrderManagement..CustomerOrderDetail ,
					QSPCanadaOrderManagement..CustomerOrderHeader,
					QSPCanadaOrderManagement..Batch
				   where  QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=
						QSPCanadaOrderManagement..CustomerOrderHeader.Instance
					and OrderBatchDate=Date
					and OrderBatchID=id
					and OrderID = @orderid
					and (producttype not in ( 46001, 46017, 46012, 46021, 46023, 46024, 46025) OR (ProductType = 46024 AND (FormCode IS NOT NULL OR OrderQualifierID NOT IN (39001, 39002))))
					and CustomerOrderDetail.StatusInstance  not in ( 513, 501, 508)
					and (CustomerOrderDetail.StatusInstance  not in (500) OR ProductType IN (46004, 46013, 46014))

			--Set Pretzel Rods orders to fulfilled
			print 'Pretzel Rods Update ' + cast(getdate() as varchar(50)) 
			update QSPCanadaOrderManagement..CustomerOrderDetail set StatusInstance=508
				from QSPCanadaOrderManagement..CustomerOrderDetail ,
					QSPCanadaOrderManagement..CustomerOrderHeader,
					QSPCanadaOrderManagement..Batch
				   where  QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=
						QSPCanadaOrderManagement..CustomerOrderHeader.Instance
					and OrderBatchDate=Date
					and OrderBatchID=id
					and OrderID = @orderid
					and producttype in (46025)
					and CustomerOrderDetail.StatusInstance  not in (500, 501, 508, 513)

			--Set Discount Card orders that were delivered by the FM to fulfilled
			print 'Discount Card Update ' + cast(getdate() as varchar(50)) 
			update	QSPCanadaOrderManagement..CustomerOrderDetail 
			set		StatusInstance=508,
					QuantityShipped = Quantity
			from	QSPCanadaOrderManagement..CustomerOrderDetail ,
					QSPCanadaOrderManagement..CustomerOrderHeader,
					QSPCanadaOrderManagement..Batch
			where	QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=QSPCanadaOrderManagement..CustomerOrderHeader.Instance
					and OrderBatchDate=Date
					and OrderBatchID=id
					and OrderID = @orderid
					and producttype in (46024)
					and FormCode IS NULL --ROE order
					and OrderQualifierID IN (39001, 39002)
					and CustomerOrderDetail.StatusInstance  not in (500, 501, 508, 513)

			--Set eBook orders to fulfilled
			print 'eBook Update ' + cast(getdate() as varchar(50)) 
			update	QSPCanadaOrderManagement..CustomerOrderDetail 
			set		StatusInstance=508,
					QuantityShipped = Quantity
			from	QSPCanadaOrderManagement..CustomerOrderDetail ,
					QSPCanadaOrderManagement..CustomerOrderHeader,
					QSPCanadaOrderManagement..Batch
			where	QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=QSPCanadaOrderManagement..CustomerOrderHeader.Instance
					and OrderBatchDate=Date
					and OrderBatchID=id
					and OrderID = @orderid
					and ProductType in (46001) AND LEFT(CustomerOrderDetail.ProductCode, 2) = 'DG'
					and CustomerOrderDetail.StatusInstance  not in (500, 501, 508, 512)

			--Set Donation orders to fulfilled
			print 'Donation Update ' + cast(getdate() as varchar(50)) 
			update	QSPCanadaOrderManagement..CustomerOrderDetail 
			set		StatusInstance=508
			from	QSPCanadaOrderManagement..CustomerOrderDetail ,
					QSPCanadaOrderManagement..CustomerOrderHeader,
					QSPCanadaOrderManagement..Batch
			where	QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=QSPCanadaOrderManagement..CustomerOrderHeader.Instance
					and OrderBatchDate=Date
					and OrderBatchID=id
					and OrderID = @orderid
					and ProductType in (46002) AND LEFT(CustomerOrderDetail.ProductCode, 2) IN ('DO', 'D1')
					and CustomerOrderDetail.StatusInstance  not in (500, 501, 508, 513)

			--Set Texture orders to fulfilled
			/*print 'Texture Update ' + cast(getdate() as varchar(50)) 
			update	QSPCanadaOrderManagement..CustomerOrderDetail 
			set		StatusInstance=508,
					QuantityShipped = Quantity
			from	QSPCanadaOrderManagement..CustomerOrderDetail ,
					QSPCanadaOrderManagement..CustomerOrderHeader,
					QSPCanadaOrderManagement..Batch
			where	QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=QSPCanadaOrderManagement..CustomerOrderHeader.Instance
					and OrderBatchDate=Date
					and OrderBatchID=id
					and OrderID = @orderid
					and ProductType in (46001) AND CustomerOrderDetail.ProductCode in ('D130', 'D131', 'D132')
					and CustomerOrderDetail.StatusInstance  not in (500, 501, 508, 512)*/

             print 'select count(*) from cusomerorderdetail,header,batch ' + cast(getdate() as varchar(50))
			select @count=count(*)
				from QSPCanadaOrderManagement..CustomerOrderDetail ,
						QSPCanadaOrderManagement..CustomerOrderHeader,
						QSPCanadaOrderManagement..Batch
					   where  QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=
							QSPCanadaOrderManagement..CustomerOrderHeader.Instance
						and OrderBatchDate=Date
						and OrderBatchID=id
						and (OrderID = @orderid
							 OR (OrderID IN (SELECT DISTINCT OnlineOrderID  
							 FROM OnlineOrderMappingTable  
							 WHERE LandedOrderID = @OrderID)
							 AND IsShippedToAccount = 1))
						and (producttype not in ( 46001, 46017, 46012, 46021, 46023, 46024, 46025) OR (ProductType = 46024 AND (FormCode IS NOT NULL OR OrderQualifierID NOT IN (39001, 39002))))
						and QSPCanadaOrderManagement..CustomerOrderDetail.StatusInstance not in (500, 508)
						
			if(@count = 0)
			begin
				-- then the batch is fulfilled
                print 'then the batch is fulfilled'
                print 'update batch set statusinstance = 40013... ' + cast(getdate() as varchar(50))
				update batch set statusinstance = 40013 where orderid=@orderID 

				if(@type = 39005 or @type = 39008 or @type = 39009)
				begin
					update batch set IsMagQueueDone = 1 where orderid=@orderID 
				end
			end
			if(@@error <> 0)
			begin
                print 'update batch set statusinstance = 40003 ...' + cast(getdate() as varchar(50))
				update batch set statusinstance = 40003 where orderid=@orderID

				declare @msg1 varchar(4000)
				select @msg1 = 'Could not update to pickable. OrderID:' +str(@orderid)
                print 'send_email ' + cast(getdate() as varchar(50))
				exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','qsp-qspfulfillment-dev@qsp.com',
					'Batch didnt close', @msg1

			end
		end
		else
		begin
            print 'update batch set statusinstinace = 40003 ' + cast(getdate() as varchar(50))
			update batch set statusinstance = 40003 where orderid=@orderID

			declare @msg varchar(4000)
			--select @msg = cast(@id as varchar(4000 ))
			-- something bad happened
			select 113 --'The batch did not close correctly.' -- 
            print 'send_email ' + cast(getdate() as varchar(50))
			exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','qsp-qspfulfillment-dev@qsp.com',
				'Batch didnt close', @msg
		end
	end
GO

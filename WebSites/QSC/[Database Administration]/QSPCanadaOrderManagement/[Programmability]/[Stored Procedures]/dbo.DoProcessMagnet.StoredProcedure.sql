USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[DoProcessMagnet]    Script Date: 06/07/2017 09:19:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- 
-- There will be one FulfillmentBatch record per Fulfillment house run
-- we are then able to track the file that gets generated as well as 
-- number of units, $'s remitted, # CHADDS and cancellations.
--#define CD_REGULARBATCH_ORDERSTATUS_NEW				40001
--#define CD_REGULARBATCH_ORDERSTATUS_INPROCESS		40002
--#define CD_REGULARBATCH_ORDERSTATUS_UNDERREVIEW		40003
--#define CD_REGULARBATCH_ORDERSTATUS_APPROVED		40004
--#define CD_REGULARBATCH_ORDERSTATUS_CANCELLED		40005
--#define CD_REGULARBATCH_ORDERSTATUS_CCPENDING		40006


CREATE                   procedure [dbo].[DoProcessMagnet]
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

	-- do not close an order wih any outstanding cc payments or refunds
	-- that are pending sent or pending coming back
	-- can't even force it in this situation
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
	
		update batch set statusinstance = 40004, DateBatchCompleted =GetDate() where orderid=@orderID

		exec  spPrepareRemitBatches @batchDate, @id, @remitStatus OUTPUT
		

		/*
		** flip any unremittable magazines due to customer status instance issue
		*/

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

		if(@remitStatus = 0)
		begin

			select 112 -- 'Batch successfully approved			
			update batch set statusinstance = 40015, DateBatchCompleted =GetDate() where orderid=@orderID
		end
		else
		begin
			update batch set statusinstance = 40003 where orderid=@orderID

			declare @msg varchar(4000)
			--select @msg = cast(@id as varchar(4000 ))
			-- something bad happened
			select 113 --'The batch did not close correctly.' -- 
			exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','karen_tracy@readersdigest.com',
				'Batch didnt close', @msg
		end
	end
GO

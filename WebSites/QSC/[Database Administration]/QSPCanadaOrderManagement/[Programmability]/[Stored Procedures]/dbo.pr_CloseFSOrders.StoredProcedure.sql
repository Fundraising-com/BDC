USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CloseFSOrders]    Script Date: 06/07/2017 09:19:47 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_CloseFSOrders]
as

set nocount on

declare @Main int
declare @Staff int
declare @Supp int
declare @Kanata int

declare @OrderType int
declare @OrderQualifier int

select @Main = 0
select @Staff = 0
Select @Supp = 0

declare @batchStatus int
declare @distCenter int
declare @RetVal int
declare @msg1 varchar(4000)

select  @Main=count(*) from batch 
	where 
	ke3filename is not null
	and statusinstance in (40002)
	and ordertypecode <> 41009
	and len(ke3filename) > 0
	and orderqualifierid=39001
		and date>='7/1/05'

select  @Staff=count(*) from batch 
	where   ke3filename is not null
	and statusinstance in (40002)
	and ordertypecode <> 41009
	and len(ke3filename) > 0
	and orderqualifierid=39003
		and date>='7/1/05'

select  @Supp=count(*) from batch 
	where  
	 ke3filename is not null
	and statusinstance in (40002)
	and ordertypecode <> 41009
	and len(ke3filename) > 0
	and orderqualifierid=39002
		and date>='7/1/05'

declare @orderid int
declare @ordercount int
select @ordercount = 0


declare aCloseThese cursor for 
select   orderid,OrderQualifierID,ordertypecode
	 from batch 
		where 		
		 statusinstance in (40002)
		and ordertypecode= 41002
		and date>='7/1/05'
--and OrderQualifierID=39017
--and orderid not  in (79184  )
/*
and Orderid  --between 80147 and 80147
 in
(
1020381,
1020383,
1020384,
1020385,
1020386,
1020387,
1020382,
1020390,
1020388


)
*/
order by batch.orderid 


--select campaignid,* from batch where orderid=77555

open aCloseThese
fetch next from aCloseThese  into @orderid,@OrderQualifier,@OrderType
while(@@fetch_status <> -1 )
begin

	if(@OrderQualifier = 39006 or @OrderQualifier = 39007 or @OrderQualifier=39017)
	begin
		select @distCenter = 2
	end
	else
	begin
		select @distCenter = 2
	end

	DECLARE  @OnHandReturnValue int
	exec pr_FSCheckOnhandQty @OrderId, @distCenter, @OnHandReturnValue output
--	print 'Distribution Center: ' + str(@distCenter)
--print @OnHandReturnValue
--select 	@OnHandReturnValue = 0		

	if (@OnHandReturnValue=0 ) 
	begin
		select @RetVal = 0
	end
	else
	begin
		-- perform the pre verification routine
		select @RetVal = 1
	end	
	-- Only verify campaign tyep orders
select @RetVal = 1
--print '@RetVal: ' + str(@RetVal)

	if( @OrderQualifier <> 39006 and  @OrderQualifier <> 39007 and  @OrderQualifier <> 39016 and  @OrderQualifier <> 39017)
	begin
		exec spPreCloseVerification @orderid, @RetVal OUTPUT
	end
--print '@RetVal: ' + str(@RetVal)

	if(@RetVal=1)
	begin

		exec DoCloseOrder @orderid,1
		if( @OrderQualifier <> 39006 and  @OrderQualifier <> 39007 and  @OrderQualifier <> 39016 and  @OrderQualifier <> 39017)
		begin
			exec spPostCloseVerification @orderid, @RetVal OUTPUT
		end
		select @batchStatus=statusinstance from batch where orderid=@orderid
	end
	else
	begin
		-- send email order in error
		select @msg1 = 'Pre close failed: ' +str(@orderid) + char(13)

		exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','qsp-qspfulfillment-dev@qsp.com',
			'Closing Campaign orders', @msg1

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
			
			exec pr_Insert_BatchDistributionCenter @distCenter, @OrderId

			-- check quantities
			
--			if(@OnHandReturnValue = 1)
			begin
				exec pr_ProcessReserveQuantities_ByOrderId_V2 @distCenter,@OrderId
				exec pr_Ins_Report_Parameters_V2 @OrderId, -1
			end
	
/*		else
			begin
				
				select @msg1 = 'Inventory issue: ' +str(@orderid) + char(13)

				exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','karen_tracy@readersdigest.com',
					'Closing  orders', @msg1
			end
*/
		end
		else
		begin
			exec pr_Ins_Report_Parameters_V2 @OrderId, -1

		end
		
		select @ordercount = @ordercount + 1
	
	end
	else
	begin
		-- send email order in error
		
		select @msg1 = 'Post close failed: ' +str(@orderid) + char(13)

		exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','qsp-qspfulfillment-dev@qsp.com',
			'Closing Campaign orders-Post Failed', @msg1

	end

	fetch next from aCloseThese  into @orderid,@OrderQualifier,@OrderType

end


close aCloseThese
deallocate aCloseThese



select @msg1 = 'Main Closed: ' +str(@Main) + char(13)
select @msg1 = @msg1 + 'Staff Closed: ' +str(@Staff) + char(13)	
select @msg1 = @msg1 + 'Supplementary Closed: ' +str(@Supp) + char(13)	
select @msg1 = @msg1 + 'Total Closed: ' +str(@ordercount) + char(13)	
/*exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','qsp-qspfulfillment-dev@qsp.com',
	'Closing FS orders', @msg1*/
GO

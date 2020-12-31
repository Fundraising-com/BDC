USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CloseInternetOrders]    Script Date: 06/07/2017 09:19:23 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[CloseInternetOrders]
 AS

	/*
	**    Loop through internet orders and close them out - KET
	**    4/7/05
	*/

	declare @orderid int
	declare @ordercount int
	
	select @ordercount = 0
	/*declare  aa cursor for select  orderid from batch
	 where statusinstance in (40002,40003) and orderqualifierid  in (39009) and ordertypecode not in(41009) and
	orderid not in(45601)
	 order by orderid*/

	declare  aa cursor for 
	select  orderid from batch
	where statusinstance in (40002,40003) 
	and orderqualifierid  in (39009) 
	and ordertypecode not in(41009) and
	orderid not in(45601)
	and not exists	(select  orderid from batch b , CustomerorderHeader h, Customerorderdetail d
		where b.id=orderbatchid
		and b.date=orderbatchdate
		and d.customerorderheaderInstance= h.instance
		and b.statusinstance in (40002,40003) 
		and b.orderqualifierid  in (39009) 
		and b.ordertypecode not in(41009) 
		and IsNull(d.pricingDetailsid,0)= 0
		and b.orderId=batch.orderid)
		
		--Temporary until TRT is ready
		/*and OrderID not in (SELECT	OrderID
							FROM	Batch b
							JOIN	CustomerOrderHeader coh
										ON	coh.OrderBatchID = b.ID
										AND	coh.OrderBatchDate = b.Date
							JOIN	CustomerOrderDetail cod
										ON	cod.CustomerOrderHeaderInstance = coh.Instance
							WHERE	cod.ProductType = 46023)*/

	order by orderid

	
	open aa
	
	fetch next from aa into @orderid
	
	while(@@fetch_status <> -1 )
	begin
	
		exec pr_CloseOrder @orderid
		select @ordercount = @ordercount + 1
	
		fetch next from aa into @orderid
	end
	
	
	close aa
	deallocate aa
	
	
	/*declare @msg1 varchar(4000)
	select @msg1 = 'Closed: ' +str(@ordercount)
	exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','qsp-qspfulfillment-dev@qsp.com,qsp-operations-canada@qsp.com',
		'Closing Internet orders', @msg1*/
GO

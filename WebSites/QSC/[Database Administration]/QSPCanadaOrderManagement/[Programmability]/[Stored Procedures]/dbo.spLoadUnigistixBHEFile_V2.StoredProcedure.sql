USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spLoadUnigistixBHEFile_V2]    Script Date: 06/07/2017 09:20:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE    procedure [dbo].[spLoadUnigistixBHEFile_V2]
	@fileName varchar(200)
as

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#temp_spLoadUnigistixBHEFile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#temp_spLoadUnigistixBHEFile]
	

create table #temp_spLoadUnigistixBHEFile
(
	BatchDate varchar(20),
	BatchID int,
	CustomerInstance int,
	WayBillNo varchar(50),
	OrderHeaderInstance Int, 
	TransID int,	
	QuantityOrder int,
	QuantityShipped int
)

set nocount on
exec ImportXMLFile @fileName,
		'#temp_spLoadUnigistixBHEFile',
			'/Batch/Customer/OrderDetail', 
			'BatchDate varchar(20) ''../../BatchDate'',
			BatchID int''../../BatchID'',
			CustomerInstance int ''../CustomerInstance'',	
			WayBillNo varchar(50) ''../WayBill'',	 	
			OrderHeaderInstance Int, 
			TransID int,
			QuantityOrder int,
			QuantityShipped int'

/*
** Make sure file has stuff
**
*/
select * into #Orders from #temp_spLoadUnigistixBHEFile
--select * from #Orders
if(@@rowcount =0)  -- pretty bad thing
begin
	
	RAISERROR( 'Nothing in Unigistix  BHE Batch  ',16,1)
        exec QSPCanadaCommon..Send_EMail  'Unigistix@qsp.com','qsp-qspfulfillment-dev@qsp.com',
				'ShipBatch', @fileName

end

/* 
** Make sure the items were pending
**
*/
declare @PendingCount int
declare @ActualCount Int
select @PendingCount=count(*) from #Orders,CustomerOrderDetail COD 
	where OrderHeaderInstance=COD.CustomerOrderHeaderInstance
	and #Orders.TransID = COD.Transid
	and COD.StatusInstance = 509
	and ProductType in (46006, 46007, 46012)	

select @ActualCount=count(*) from #Orders


if(@ActualCount <> @PendingCount)  -- pretty bad thing
begin
	
	RAISERROR( '@ActualCount <> @PendingCount  ',16,1)
        exec QSPCanadaCommon..Send_EMail  'Unigistix@qsp.com','qsp-qspfulfillment-dev@qsp.com',
				'ShipBatch', @fileName

end



declare @orderid int
declare @shipdate varchar(20)
declare @CarrierID int
declare @CarrierName varchar(50)
declare @NoOfBox int
declare @TotalWeight numeric(10,2)
declare @ShippingComment varchar(200)
declare @OrderHeaderInstance int
declare @TransID int
declare @Way varchar(50)
declare @shipmentid Int

select @orderid=OrderID from Batch,#Orders where Date=BatchDate and Id = Batchid

select * from Batch where OrderID = @orderid and StatusInstance = 40012
if(@@rowcount = 1)
begin
	/*
	**  Loop thru line items do and ship 
	*/
	declare @d datetime
	select @d = GetDate()

	declare aLoopThruOrder cursor for select OrderHeaderInstance,TransID,WayBillNo from #Orders
	open aLoopThruOrder

	fetch next from aLoopThruOrder into @OrderHeaderInstance, @TransID,@Way
	while( @@FETCH_Status = 0)
	begin
	
		exec dbo.pr_ShipOrderItem 
				  @OrderHeaderInstance,
				  @TransID,
				  2,  --Unigistix
				  53005, 
				  @d, 
				  @d, 
				  1,
				  1.0,
				  0,
				  '',
		 		  '',
				  1,		  			  		  
				  @shipmentid OUTPUT,
				  ''
		/*
		**  Insert waybills
		*/
	
		Insert QSPCanadaOrderManagement..ShipmentWayBill
		(
			ShipmentID,
			WayBillNumber
		)
		select @shipmentid,@Way
			
			fetch next from aLoopThruOrder into @OrderHeaderInstance, @TransID,@Way
	end
end
else
begin

	RAISERROR( 'Unigistix Batch already loaded ',16,1)
        exec QSPCanadaCommon.dbo.Send_EMail  
		'ShipBatch_test@qsp.com'
		,'qsp-qspfulfillment-dev@qsp.com','Unigistix Batch already loaded '
		,@fileName
		
end



drop table #temp_spLoadUnigistixBHEFile
drop table #Orders
GO

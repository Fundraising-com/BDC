USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spLoadUnigistixNonBHEFile_V2]    Script Date: 06/07/2017 09:20:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  procedure [dbo].[spLoadUnigistixNonBHEFile_V2]
	@fileName varchar(200)
as

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].#temp_spLoadUnigistixFile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#temp_spLoadUnigistixFile]


create table #temp_spLoadUnigistixFile
(
	BatchDate varchar(20),
	BatchID int,
	OrderID int,
	ShipDate varchar(20),
	CarrierID int,
	CarrierName varchar(50),
	NoOfBox int,
	TotalWeight numeric(10,2),
	ShippingComment varchar(200),
	OrderHeaderInstance Int, 
	TransID int,
	OracleCode varchar(50),
	QuantityOrder int,
	QuantityShipped int,
	ReplacedItemCode varchar(50),
	QuantityReplaced int
)
	
set nocount on
exec ImportXMLFile @fileName, '#temp_spLoadUnigistixFile','/Batch/OrderDetail', 
			'BatchDate varchar(20) ''../BatchDate'',
			BatchID int''../BatchID'',
			OrderID int ''../OrderId'',
			ShipDate varchar(20) ''../ShipDate'',
			CarrierID int ''../CarrierID'',
			CarrierName varchar(50) ''../CarrierName'',
			NoOfBox int ''../NoOfBox'',
			TotalWeight numeric(10,2) ''../TotalWeight'',
			ShippingComment varchar(200) ''../ShippingComment'',
			OrderHeaderInstance Int, 
			TransID int,
			OracleCode varchar(50),
			QuantityOrder int,
			QuantityShipped int,
			ReplacedItemCode varchar(50),
			QuantityReplaced int'


/*
** Make sure file has stuff
**
*/
select * into #Orders from #temp_spLoadUnigistixFile
--select * from #Orders
if(@@rowcount =0)  -- pretty bad thing
begin
	
	RAISERROR( 'Nothing in Unigistix Batch  ',16,1)
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
	and 	 ProductType not  in (46006, 46007, 46012)	
/*
	select COD.* from #Orders,CustomerOrderDetail COD 
	where OrderHeaderInstance=COD.CustomerOrderHeaderInstance
	and #Orders.TransID = COD.Transid
	and COD.StatusInstance = 509
*/
select @ActualCount=count(*) from #Orders


if(@ActualCount <> @PendingCount)  -- pretty bad thing
begin
	
	RAISERROR( '@ActualCount <> @PendingCount  ',16,1)
        exec QSPCanadaCommon..Send_EMail  'Unigistix@qsp.com','qsp-qspfulfillment-dev@qsp.com',
				'Count incorrect', @fileName

end
else
begin

	drop table #temp_spLoadUnigistixFile
	
	--select * from #orders
	create table #temp_spLoadUnigistixFile2
	(
		WayBillNo varchar(50)
	)
	
	exec ImportXMLFile @fileName, 
			'#temp_spLoadUnigistixFile2', '/Batch/WayBill/WayBillNo', 
				'WayBillNo varchar(50)''.'''
	
	
	
	declare @orderid int
	declare @shipdate varchar(20)
	declare @CarrierID int
	declare @CarrierName varchar(50)
	declare @NoOfBox int
	declare @TotalWeight numeric(10,2)
	declare @ShippingComment varchar(200)
	
	select @orderid=OrderID from #Orders
	
	select * from Batch where OrderID = @orderid and StatusInstance = 40012
	if(@@rowcount = 1)
	begin
	
		select @shipdate=ShipDate,
			 @CarrierID=CarrierID +53000,
			 @CarrierName =CarrierName,
			 @NoOfBox =NoOfBox,
			 @TotalWeight=TotalWeight,
			 @ShippingComment =ShippingComment
			  from #Orders
	/*
		print @orderid
	
		print @shipdate
		print @CarrierID
		print @CarrierName
		print @NoOfBox
		print @TotalWeight
	*/
	
		/*
		** Deal w/any variations that might have occured
		*/
		declare @newid varchar(100)
		declare @shipmentid int
		select @newid=newid()
	
		insert into ShipmentVariation 
		(
			SessionID,
			CustomerOrderHeaderInstance,
			TransID,
			QuantityShipped,
			QuantityReplaced
		)	
		select 'CA-13'+@newid,
			OrderHeaderInstance,
			TransID,
			QuantityShipped,
			QuantityReplaced
			from #Orders where orderid=@orderid
				and QuantityOrder <> QuantityShipped
	
		/*
		**  Call pr_ShipBatch it will do all status updates, draw down inventory
		**  and prep for Oracle
		*/
		declare @sdate datetime
		select @sdate =cast(@shipdate as datetime) 
		declare @orderstring varchar(512)
		select @orderstring = cast(@orderid as varchar(512))
	

		exec pr_ShipBatch @orderstring,
				  2,  --Unigistix
				  @CarrierID,
				  @sdate,
				  @sdate, 
				  @NoOfBox,
				  @TotalWeight,
				  0,
				  '',
		 		  @ShippingComment,
				  1,		  			  		  
				  @shipmentid OUTPUT,
				  @newid,
				  '46002,46003,46013,46014,46015,46008'  -- do waybills after
	
		/*
		**  Insert waybills
		*/
	
		Insert QSPCanadaOrderManagement..ShipmentWayBill
		(
			ShipmentID,
			WayBillNumber
		)
		select @shipmentid,WayBillNo
			from 
			#temp_spLoadUnigistixFile2

	
	end
	else
	begin
	
		RAISERROR( 'Unigistix Batch already loaded - in test',16,1)
	        exec QSPCanadaCommon.dbo.Send_EMail  
			'ShipBatch_test@qsp.com'
			,'qsp-qspfulfillment-dev@qsp.com'
			,@fileName
			
	end
	
	drop table #temp_spLoadUnigistixFile2

end
drop table #Orders
GO

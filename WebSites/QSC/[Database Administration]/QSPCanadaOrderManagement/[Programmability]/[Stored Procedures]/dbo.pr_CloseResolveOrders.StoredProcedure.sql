USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CloseResolveOrders]    Script Date: 06/07/2017 09:19:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[pr_CloseResolveOrders]
as

set nocount on

declare @Main int
declare @Staff int
declare @Supp int
declare @Kanata int

declare @OrderType int
declare @OrderQualifier int
declare @minOrderid int
declare @maxOrderid int

declare @batchStatus int
declare @distCenter int
declare @RetVal int
declare @msg1 varchar(4000)

update Batch
set StatusInstance = 40002
where StatusInstance = 40003

select @minOrderid = Long1Value, @maxOrderid=Long2Value from SystemOptions where  KeyValue='OrderIDRange'

declare @orderid int
declare @ordercount int
select @ordercount = 0

declare aCloseThese cursor for 
select  top 55 orderid,OrderQualifierID,ordertypecode
from batch 
where statusinstance in (40002)
and OrderQualifierID in (39001, 39002)
and OrderID not in (SELECT	OrderID
					FROM	Batch b
					JOIN	MatchJob mj ON mj.AccountCampaignID = b.CampaignID
					WHERE	mj.[Status] = 0)
/*and (OrderQualifierID <> 39001 OR OrderID in (SELECT	OrderID
												FROM	Batch b
												JOIN	MatchJob mj ON mj.AccountCampaignID = b.CampaignID
												WHERE	b.OrderQualifierID in (39001)
												AND		mj.[Status] > 0))*/
order by batch.orderid 

open aCloseThese
fetch next from aCloseThese  into @orderid,@OrderQualifier,@OrderType
while(@@fetch_status <> -1 )
begin

	exec pr_CloseOrder @orderid
	fetch next from aCloseThese  into @orderid,@OrderQualifier,@OrderType

end

close aCloseThese
deallocate aCloseThese

--exec pr_Order_KHKUpdate

/*select @msg1 = 'Main Closed: ' +str(@Main) + char(13)
select @msg1 = @msg1 + 'Staff Closed: ' +str(@Staff) + char(13)	
select @msg1 = @msg1 + 'Supplementary Closed: ' +str(@Supp) + char(13)	
select @msg1 = @msg1 + 'Total Closed: ' +str(@ordercount) + char(13)	
exec QSPCanadaCommon..Send_EMail  'docloseorder@qsp.com','qsp-qspfulfillment-dev@qsp.com',
	'Closing Campaign orders', @msg1
*/
GO

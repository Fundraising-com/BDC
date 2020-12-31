USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CloseOrders]    Script Date: 06/07/2017 09:19:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_CloseOrders]
as

set nocount on

declare @orderid int

declare aCloseThese cursor for 
select   orderid--,OrderQualifierID,ordertypecode
	 from batch 
		where 		
		 statusinstance in (40002)
		--and ordertypecode= 41002
		and OrderQualifierID NOT IN (39001, 39002, 39009)
order by batch.orderid 

open aCloseThese
fetch next from aCloseThese  into @orderid
while(@@fetch_status <> -1 )
begin

	exec pr_CloseOrder @orderid

	fetch next from aCloseThese  into @orderid

end

close aCloseThese
deallocate aCloseThese
GO

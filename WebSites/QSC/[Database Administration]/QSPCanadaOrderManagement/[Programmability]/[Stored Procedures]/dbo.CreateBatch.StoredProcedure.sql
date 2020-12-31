USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateBatch]    Script Date: 06/07/2017 09:19:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[CreateBatch]
	@batchdate datetime,
	@billtoacctid int,
	@shiptoacctid int,
	@campaignid int,
	@status int,
	@ordertypecode int, 
	@orderqualifierid int,
	@orderid int OUTPUT
as
	declare @maxid int
	declare @count int
	select @count = count(*) from qspcanadaordermanagement..batch where  date=@batchdate

	--MS March 13, 2008
	declare @IsStaffOrder int
	select @IsStaffOrder=IsStaffOrder From QSPCanadaCommon..campaign Where Id=@campaignid

	
	if ( @count = 0  ) 
	begin
		select @maxid = 20000
	end
	else
	begin
		select @maxid=max(id)+1 from qspcanadaordermanagement..batch where  date=@batchdate and id >= 20000 group by date
		set @maxid = coalesce(@maxid, 20000)
	end

	--SELECT  @maxid=isnull(max(id),1)+1 from Batch where date=@batchdate group by date
	--SELECT  @orderid=ISNULL(MAX(OrderID),1)+1 From Batch where OrderID < 85000
	SELECT @orderid=ISNULL(MAX(OrderID), 1000000) + 1 FROM Batch WHERE OrderID >= 1000000 AND OrderID < 5000000 -- Ben, 2005-09-14
--	PRINT 'maxid: ' + Convert(varchar, @maxid)

	insert batch (date,id,orderid ) values(@batchdate,@maxid,@orderid)


	

	update batch set accountid=@billtoacctid, OrderQualifierID=@orderqualifierid,
		datesent=GetDate(),datereceived=GetDate(),ShipToAccountID=@shiptoacctid,
		campaignid=@campaignid, ordertypecode=@ordertypecode,statusinstance=@status
		,IsstaffOrder=@IsStaffOrder --MS March 13, 2008
		where orderid=@orderid
	--insert batch (date,id,orderid,accountid,OrderQualifierID,datesent,datereceived,ShipToAccountID,campaignid,ordertypecode,statusinstance ) values(@batchdate,@maxid,@orderid,@billtoacctid,@orderqualifierid,getdate(),getdate(), @shiptoacctid,@campaignid,@ordertypecode,@status)
GO

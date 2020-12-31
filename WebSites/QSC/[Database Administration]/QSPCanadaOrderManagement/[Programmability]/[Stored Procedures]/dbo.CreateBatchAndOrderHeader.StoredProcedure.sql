USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateBatchAndOrderHeader]    Script Date: 06/07/2017 09:19:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CreateBatchAndOrderHeader]
	@batchdate datetime,
	@billtoacctid int,
	@shiptoacctid int,
	@campaignid int,
	@status int,
	@ordertypecode int, 
	@orderqualifierid int,
	@customerinstance int,
	@orderid int OUTPUT,
	@coh int OUTPUT,
	@ChangeUserId varchar(4)
as
	declare @maxinstance int
	declare @id int
	exec CreateBatch @batchdate ,
			@billtoacctid ,
			@shiptoacctid ,
			@campaignid ,
			@status ,
			@ordertypecode , 
			@orderqualifierid ,
			@orderid  OUTPUT

	select @id = id from batch where orderid=@orderid
	

	create table #temp
	(
		 NextInstance int
	)


	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 4
	select @maxinstance=nextinstance from #temp
	truncate table #temp

	insert customerorderheader (Instance, NextDetailTransID,AccountID, Orderbatchdate, orderbatchid, campaignid,CustomerBillToInstance,
			statusinstance,CreationDate,ChangeUserID,ChangeDate,Type, PaymentMethodInstance)
		values(@maxinstance, 0,@billtoacctid, @batchdate, @id,  @campaignid, @customerinstance,  400,GetDate(),@ChangeUserId,GetDate(),902, 50002) 

--sp_columns 'customerorderheader'	
	select @coh=@maxinstance
	drop table #temp
GO

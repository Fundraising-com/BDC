USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateOrderHeader]    Script Date: 06/07/2017 09:19:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  procedure [dbo].[CreateOrderHeader]
	@batchdate datetime,
	@batchid int,
	@billtoacctid int,
	@campaignid int,
	@customerinstance int,
	@coh int OUTPUT,
	@ChangeUserId varchar(4),
	@Type int = 902,
	@StudentInstance int = null,
	@OrderBatchSequence int = null,
	@FirstStatusInstance int = null,
	@StatusInstance int = 400,
	@PaymentMethodInstance int = null,
	@TRTGenerationCode varchar(4) = null,
	@ToteID int = null,
	@FormCode varchar(4) = null
as
	set nocount on
	declare @maxinstance int
	create table #temp
	(
		 NextInstance int
	)


	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 4
	select @maxinstance=nextinstance from #temp
	truncate table #temp

	insert customerorderheader (Instance, NextDetailTransID, AccountID, Orderbatchdate, orderbatchid, campaignid,CustomerBillToInstance,
			statusinstance,CreationDate,ChangeUserID,ChangeDate,Type, StudentInstance, OrderBatchSequence, FirstStatusInstance, PaymentMethodInstance,
			TRTGenerationCode, ToteID, FormCode)
		values(@maxinstance, 1, @billtoacctid, @batchdate, @batchid,  @campaignid, @customerinstance,  @StatusInstance, GetDate(),
			@ChangeUserId,GetDate(),@Type, @StudentInstance, @OrderBatchSequence, @FirstStatusInstance, @PaymentMethodInstance,
			@TRTGenerationCode, @ToteID, @FormCode) 

--sp_columns 'customerorderheader'	
	select @coh=@maxinstance
	drop table #temp
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ChangeOfAddressByCOD]    Script Date: 06/07/2017 09:19:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ChangeOfAddressByCOD] 



@iCustomerInstance			int 		= 0,
@iCustomerOrderHeaderInstance		int		= 0,--prerestore on 21 -- @iCustomerOrderHeaderInstance int = 6000114,
@iTransID 				int 		= 0,--prerestore on 21 -- @iTransID int	= 1,
@sFirstName 				nvarchar(50)	= '',
@sLastName 				nvarchar(50)	= '',
@sAddress1				nvarchar(50)	= '',
@sAddress2				nvarchar(50)	= '',
@sCity					nvarchar(50)	= '',
@sStateCode				nvarchar(5)	= '',
@sZip					nvarchar(20)	= '',
@sZipPlusFour				nvarchar(5)	= '',
@sOldAddress1				nvarchar(50)	= '',
@sOldAddress2				nvarchar(50)	= '',
@sOldCity				nvarchar(50)	= '',
@sOldStateCode			nvarchar(5)	= '',
@sOldZip				nvarchar(20)	= '',
@sUserID 				varchar(15) ,
--@dDate				datetime = '',
@iProblemCode				int = 0,
@iCommunicationChannelInstance	int = 0,
@iCommunicationSourceInstance	int = 0,
@iCustomerRemitHistoryInstance		int out

AS
declare @dDate datetime	
set @dDate = getdate()

DECLARE 	@sCODStatus  			nvarchar(50),
		@iInterfaceLayoutID		int,
		@iFormatSupportCHADD	int,
		@sUpdateStatus		nvarchar(50),
		@iRemitBatchID 		int,
	     	@iLastRemitBatchID 		int,
		@iFulfillmentHouseNbr 		int,
		@iInstance 			int,
		@sOldFirstName 		nvarchar(50),
		@sOldLastName 		nvarchar(50),
		@index				int,
		@index2			int,
		@recipient			nvarchar(50),
		@iProductStatus		int


--exec pr_RecordRecipientAddressHistory @iCustomerOrderHeaderInstance, @iTransID

--Set default value
SET @iFormatSupportCHADD = 0

--Get the interface layout
SELECT @iInterfaceLayoutID = fh.interfacelayoutid
  FROM RemitBatch rb,
       	QSPCanadaProduct.dbo.Fulfillment_House fh,
       	CustomerRemitHistory crh,
       	dbo.CustomerOrderDetailRemitHistory codrh
WHERE fh.Ful_Nbr = rb.FulfillmentHouseNbr
   	and crh.RemitBatchId = rb.Id
  	and codrh.RemitBatchId = crh.RemitBatchId
   	and codrh.CustomerRemitHistoryInstance = crh.Instance
	and codrh.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	and codrh.TransID = @iTransID
	and codrh.RemitBatchID = (SELECT TOP 1 RemitBatchID 
			    FROM CustomerOrderDetailRemitHistory
			 WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
			 	   TransID = @iTransID
			ORDER BY DateChanged DESC)

-- Depending on the interfacelayaout, set the update status : either chadd needs to be sent or chadd sent
IF(@iInterfaceLayoutID = '33004' OR @iInterfaceLayoutID = '33009' OR @iInterfaceLayoutID = '33014')
	SET @sUpdateStatus = '42006'
ELSE
	SET @sUpdateStatus = '42007'

-- Get the last status
SELECT @sCODStatus =  SubStatusInstance 
   FROM vw_GetSubInfo 
WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
	 TransID = @iTransID

IF @sCODStatus = '42000'
BEGIN

--	SELECT @iCustomerInstance = CustomerBillToInstance FROM CustomerOrderHeader WHERE Instance =@iCustomerOrderHeaderInstance 

	SELECT	@iInstance = CustomerRemitHistoryInstance
	FROM		CustomerOrderDetailRemitHistory
	WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		TransID = @iTransID
	AND		Status = 42000

	-- Update the customer remit history table
	UPDATE CustomerRemitHistory 
	        SET FirstName = @sFirstName,
		   LastName = @sLastName,
		   Address1 = @sAddress1,
		   Address2 = @sAddress2,
		   City = @sCity,
		   State = @sStateCode,
		   Zip = @sZip,
		   ZipPlusFour = @sZipPlusFour,
		   UserIDModified = @sUserID,
		   DateModified = @dDate
	 WHERE Instance = @iInstance


-- Update the customer table

	
SELECT @iCustomerInstance = CustomerShipToInstance FROM CustomerOrderDetail WHERE CustomerOrderHeaderInstance =@iCustomerOrderHeaderInstance AND TransID=@iTransID

if coalesce(@iCustomerInstance, 0) = 0
begin
	SELECT @iCustomerInstance = CustomerBillToInstance FROM CustomerOrderHeader WHERE Instance =@iCustomerOrderHeaderInstance 
	
	UPDATE Customer 
	        SET Address1 = @sAddress1,
		   Address2 = @sAddress2,
		   City = @sCity,
		   State = @sStateCode,
		   Zip = @sZip,
		   ZipPlusFour = @sZipPlusFour,
		   ChangeUserID = @sUserID,
		   ChangeDate    = @dDate
	 WHERE Instance = @iCustomerInstance

	UPDATE CustomerOrderDetail 
	        SET Recipient = @sFirstName + ' ' + @sLastName
	 WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		   AND TransID = @iTransID
end
else
begin

	UPDATE Customer 
	        SET firstname = @sFirstName,
		   lastname = @sLastName,
		   Address1 = @sAddress1,
		   Address2 = @sAddress2,
		   City = @sCity,
		   State = @sStateCode,
		   Zip = @sZip,
		   ZipPlusFour = @sZipPlusFour,
		   ChangeUserID = @sUserID,
		   ChangeDate    = @dDate
	 WHERE Instance = @iCustomerInstance

	--Jeff: 12/01/06: Added so when doing a CHADD, name gets changed correctly
	UPDATE CustomerOrderDetail 
	        SET Recipient = @sFirstName + ' ' + @sLastName
	 WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		   AND TransID = @iTransID

end


END
ELSE IF @sCODStatus <> '42002' AND @sCODStatus <> '42003' AND @sCODStatus <> '42004' 
BEGIN

SELECT	@iProductStatus = p.Status
FROM		QSPCanadaProduct..Product p,
		QSPCanadaProduct..Pricing_Details pd,
		CustomerOrderDetail cod
WHERE	pd.Product_Instance = p.Product_Instance
AND		cod.PricingDetailsID = pd.MagPrice_Instance
AND		cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		cod.TransID = @iTransID


SELECT @iCustomerInstance = CustomerShipToInstance FROM CustomerOrderDetail WHERE CustomerOrderHeaderInstance =@iCustomerOrderHeaderInstance AND TransID=@iTransID
	
if coalesce(@iCustomerInstance, 0) = 0
begin
	SELECT @iCustomerInstance = CustomerBillToInstance FROM CustomerOrderHeader WHERE Instance =@iCustomerOrderHeaderInstance 
end
	--Get the old address

	if @sOldAddress1 = ''
	begin
		SELECT  @sOldAddress1 = Address1,
			  @sOldAddress2=Address2,
			  @sOldCity=City,
			  @sOldStateCode=State,
			  @sOldZip=Zip
		FROM 	  Customer
		WHERE  Instance = @iCustomerInstance
	end

	-- FIX to remove double spaces which crash the procs
	select 	  @recipient = replace(recipient, '  ', ' ')
	from 	  customerorderdetail
	 WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		   AND TransID = @iTransID

	select @index = charindex(' ', @recipient,1)
		
	-- see if theres another blank
	select @index2 = charindex(' ', @recipient,@index+1)
	if(@index2 <> 0)
	begin
		select @sOldFirstName = left(@recipient, charindex(' ', @recipient,@index2)) 
		select @sOldLastName = right(@recipient, len(@recipient) - @index2)	
	end
	else
	begin
		select @sOldFirstName = left(@recipient, charindex(' ', @recipient,@index)) 
		select @sOldLastName = right(@recipient, len(@recipient) - @index)	
	end



	-- Find the remit batch id for which we want to insert the chadd in
	SET @iRemitBatchID = 0

	--get the lastest remit batch id from this cod
	SELECT @iLastRemitBatchID = MAX(RemitBatchID)
   	   FROM CustomerOrderDetailRemitHistory
	WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
 	 	 TransID = @iTransID

	--get the fulf house nbr for the latest remit batch
	SELECT @iFulfillmentHouseNbr=FulfillmentHouseNbr
	   FROM RemitBatch
	WHERE ID = @iLastRemitBatchID

	SELECT @iRemitBatchID=coalesce(MAX(ID),0)
	   FROM RemitBatch
	WHERE Status='42000' AND
		  FulfillmentHouseNbr = @iFulfillmentHouseNbr
		

	declare @maxinstance int
	create table #temp
	(
		NextInstance int
	)

	-- IF no Remit Batch is available, then create one
	IF @iRemitBatchID <= 0 
	BEGIN

		
		
		insert into #temp exec qspcanadaordermanagement..InsertNextInstance 17
		select @maxinstance=nextinstance from #temp
		truncate table #temp
			
		SELECT @iRemitBatchID = @maxinstance
	
		INSERT INTO RemitBatch (ID, Date, Status, FulfillmentHouseNbr,UserIDChanged) VALUES (@iRemitBatchID, GetDate(), '42000', @iFulfillmentHouseNbr,@sUserID)
	
	END 


	set @maxinstance = 1
	delete from #temp
	
	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 18
	select @maxinstance=nextinstance from #temp
	truncate table #temp
		
	drop table #temp
	SET @iInstance = @maxinstance

	insert into CustomerRemitHistory 
		(RemitBatchID,
			Instance,
			CustomerInstance,
			StatusInstance,
			LastName,
			FirstName,
			Address1,
			Address2,
			City,
			State,
			Zip,
			UserIDModified,
			DateModified)
	
		values (@iRemitBatchID,
			@iInstance,
			@iCustomerInstance,
			@sUpdateStatus,
			@sOldLastName,
			@sOldFirstName,
			@sOldAddress1,
			@sOldAddress2,
			@sOldCity,
			@sOldStateCode,
			@sOldZip,
			@sUserID,
			@dDate)
	
	-- Update the customer remit history table
	UPDATE CustomerRemitHistory 
	        SET FirstName = @sFirstName,
		   LastName = @sLastName,
		   Address1 = @sAddress1,
		   Address2 = @sAddress2,
		   City = @sCity,
		   State = @sStateCode,
		   Zip = @sZip,
		   ZipPlusFour = @sZipPlusFour,
		   UserIDModified = @sUserID,
		   DateModified = @dDate
	 WHERE Instance = @iInstance
	
	INSERT INTO CustomerOrderDetailRemitHistory        
		(CustomerOrderHeaderInstance,
		TransID,
		RemitBatchID,
		CountryCode,
		CustomerRemitHistoryInstance,
		Status,
		Quantity,
		RemitRate,
		BasePrice,
		CurrencyID,
		Lang,
		PremiumIndicator,
		PremiumCode,
		PremiumDescription,
		ABCCode,
		Renewal,
		TitleCode,
		MagazineTitle,
		CatalogPrice,
		ItemPriceTotal,
		NumberOfIssues,
		DefaultGrossValue,
		Comment,
		SwitchLetterBatchID,
		GiftOrderType,
		GiftOrderStatus,
		GiftCardDateGenerated,
		SupporterName,
		DateChanged,
		EffortKey,
		UserIdChanged)
		SELECT top 1 CustomerOrderHeaderInstance,
			TransID,
			@iRemitBatchID,
			CountryCode,
			@iInstance,
			@sUpdateStatus, --CASE @iProductStatus WHEN 30600 THEN @sUpdateStatus ELSE 42010 END,
			Quantity,
			RemitRate,
			BasePrice,
			CurrencyID,
			Lang,
			PremiumIndicator,
			PremiumCode,
			PremiumDescription,
			ABCCode,
			Renewal,
			TitleCode,
			MagazineTitle,
			CatalogPrice,
			ItemPriceTotal,
			NumberOfIssues,
			DefaultGrossValue,
			Comment,
			SwitchLetterBatchID,
			GiftOrderType,
			GiftOrderStatus,
			GiftCardDateGenerated,
			SupporterName,
			@dDate AS DateChanged,
			EffortKey,
			@sUserID as UserIDChanged
			   FROM CustomerOrderDetailRemitHistory
			WHERE  CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
		 		  TransID = @iTransID AND
				  RemitBatchID = @iLastRemitBatchID order by customerremithistoryinstance desc


SELECT @iCustomerInstance = CustomerShipToInstance FROM CustomerOrderDetail WHERE CustomerOrderHeaderInstance =@iCustomerOrderHeaderInstance AND TransID=@iTransID

if coalesce(@iCustomerInstance, 0) = 0
begin
	SELECT @iCustomerInstance = CustomerBillToInstance FROM CustomerOrderHeader WHERE Instance =@iCustomerOrderHeaderInstance 
	
	UPDATE Customer 
	        SET Address1 = @sAddress1,
		   Address2 = @sAddress2,
		   City = @sCity,
		   State = @sStateCode,
		   Zip = @sZip,
		   ZipPlusFour = @sZipPlusFour,
		   ChangeUserID = @sUserID,
		   ChangeDate    = @dDate,
		   statusinstance=300
	 WHERE Instance = @iCustomerInstance

	UPDATE CustomerOrderDetail 
	        SET Recipient = @sFirstName + ' ' + @sLastName
	 WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		   AND TransID = @iTransID
end
else
begin

	UPDATE Customer 
	        SET firstname = @sFirstName,
		   lastname = @sLastName,
		   Address1 = @sAddress1,
		   Address2 = @sAddress2,
		   City = @sCity,
		   State = @sStateCode,
		   Zip = @sZip,
		   ZipPlusFour = @sZipPlusFour,
		   ChangeUserID = @sUserID,
		   ChangeDate    = @dDate,
		   statusinstance=300
	 WHERE Instance = @iCustomerInstance

	--Jeff: 12/01/06: Added so when doing a CHADD name gets changed correctly
	UPDATE CustomerOrderDetail 
	        SET Recipient = @sFirstName + ' ' + @sLastName
	 WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		   AND TransID = @iTransID

end



END
ELSE
BEGIN
	SET @iInstance = 0
END

if @iProblemCode <> 0
begin 
	DECLARE @iInstancePC int,
		    @iIncidentInstance int

exec pr_Incident_Insert @iIncidentInstance out, @iProblemCode, @iCustomerOrderHeaderInstance, @iTransID, @iCommunicationChannelInstance, @iCommunicationSourceInstance,1,0,'Automated Chadd', @sUserID
exec pr_IncidentAction_Insert @iInstancePC out, @iIncidentInstance, 4, 'Automated Chadd', @sUserID


end

set @iCustomerRemitHistoryInstance=@iInstance
GO

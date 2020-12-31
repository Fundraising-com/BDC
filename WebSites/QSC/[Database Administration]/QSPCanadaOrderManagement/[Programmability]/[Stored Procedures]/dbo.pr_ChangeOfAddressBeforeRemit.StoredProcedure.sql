USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ChangeOfAddressBeforeRemit]    Script Date: 06/07/2017 09:19:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_ChangeOfAddressBeforeRemit] 

	@iCustomerOrderHeaderInstance		int		= 0,
	@iTransID				int		= 0,
	@sFirstName				varchar(40)	= '',
	@sLastName				varchar(40)	= '',
	@sAddress1				varchar(50)	= '',
	@sAddress2				varchar(50)	= '',
	@sCity					varchar(50)	= '',
	@sStateCode				varchar(10)	= '',
	@sZip					varchar(10)	= '',
	@sZipPlusFour				varchar(4)	= '',
	@iProblemCode				int		= 0,
	@iCommunicationChannelInstance	int		= 0,
	@iCommunicationSourceInstance	int		= 0,
	@sUserID				varchar(15)	= ''

AS
DECLARE	@iCustomerInstance	int,
		@status			varchar(50),
		@i			int

--exec pr_RecordRecipientAddressHistory @iCustomerOrderHeaderInstance, @iTransID

SELECT	@iCustomerInstance = CustomerShipToInstance
FROM		QSPCanadaOrderManagement..CustomerOrderDetail
WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		TransID = @iTransID

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
		   ChangeDate    = getdate(),
		   StatusInstance = 300
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
		   ChangeDate    = getdate(),
		   StatusInstance = 300
	 WHERE Instance = @iCustomerInstance

	--Jeff: 12/01/06: Added so when doing a CHADD before remit, name gets changed correctly
	UPDATE CustomerOrderDetail 
	        SET Recipient = @sFirstName + ' ' + @sLastName
	 WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		   AND TransID = @iTransID

end


Select @i=count(*)
 From 	QSPCanadaOrderManagement..CreditCardPayment 	as cp,
      	QSPCanadaOrderManagement..CustomerPaymentHeader as ph,
	QSPCanadaOrderManagement..CustomerOrderHeader 	as coh,
	QSPCanadaOrderManagement..CustomerOrderDetail 	as cod ,
	QSPCanadaOrderManagement..Customer 		as CustBill,
	QSPCanadaOrderManagement..Batch as b
 	
 Where cp.CustomerPaymentHeaderInstance = ph.Instance
   and ph.CustomerOrderHeaderInstance  = coh.Instance	
   and coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.OrderBatchID = b.ID
   and coh.OrderBatchDate = b.Date
 and coh.PaymentMethodInstance in (50003,50004) -- credit card payments
   and cp.StatusInstance  <> 19000
   and cod.ProductType = 46001
   and cod.CustomerOrderHeaderInstance=@iCustomerOrderHeaderInstance and  cod.transid=@iTransID
   and not (cod.StatusInstance = 501 and cod.ProductCode LIKE 'D%' AND ISNULL(CustBill.Email, '') = '')
   and (ISNULL(b.IsInvoiced, 0) = 0 OR ISNULL(cod.invoicenumber, 0) = 1)
   
if @i = 0
begin

	DECLARE	@ProductType INT,
			@ProductCode VARCHAR(20)
	SELECT	@ProductType = cod.ProductType,
			@ProductCode = cod.ProductCode
	FROM	CustomerOrderDetail cod
	WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		cod.TransID = @iTransID

	IF @ProductType = 46001 AND LEFT(@ProductCode, 2) <> 'DG'
	BEGIN
		exec spRemitIndividualItem @iCustomerOrderHeaderInstance, @iTransID, @status output
	END
end

if @iProblemCode <> 0
begin 
	DECLARE @iInstancePC int,
		    @iIncidentInstance int

	exec pr_Incident_Insert @iIncidentInstance out, @iProblemCode, @iCustomerOrderHeaderInstance, @iTransID, @iCommunicationChannelInstance, @iCommunicationSourceInstance,1,0,'Automated Chadd', @sUserID
	exec pr_IncidentAction_Insert @iInstancePC out, @iIncidentInstance, 4, 'Automated Chadd', @sUserID

end

--Jeff 09/jan/2007: Requeue OEFU report
DECLARE		@OrderID int
SELECT		@OrderID = b.OrderID
FROM		Batch b
LEFT JOIN	CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
WHERE		coh.Instance = @iCustomerOrderHeaderInstance

Update	ReportRequestBatch_OrderEntryFollowupReport
Set		createdate = getdate(), QUEUEDATE = NULL,RUNDATESTART= NULL,FILENAME =NULL 
where	reportrequestbatchid  IN (select id from ReportRequestBatch
								where batchorderid  = @OrderID)

GO

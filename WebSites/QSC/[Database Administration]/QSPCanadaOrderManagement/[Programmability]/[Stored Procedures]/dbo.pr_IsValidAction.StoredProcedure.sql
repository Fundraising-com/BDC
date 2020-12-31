USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IsValidAction]    Script Date: 06/07/2017 09:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_IsValidAction]

@iActionInstance 			int,
@iCustomerOrderHeaderInstance		int,
@iTransID 				int,
@iCreditCardAction			int = 0,
@isValid				int  output
AS
DECLARE	@iWasSent int,
		@iWasCancel int,
		@iIsTitleActive int,
		@sTitleCode varchar(50),
		@iHasSwitchLetter int,
		@iProductType int,
		@iApproved int,
		@iNonApproved int,
		@iOtherProduct int,
		@iHasCreditCard int,
		@iInactiveMagazine int,
		@iIsCCReprocessed int,				-- 02/09/2006 - Ben: Added new test to forbid multiple CC Reprocess
		@iIsAddressValid int,				-- 04/12/2006 - Ben: Added new test to forbid CC Reprocess for missing addresses
		@iIsStaffOrLoonieOrderForTime int,	-- 11/08/2006 - Jeff: Allow the refund action to be performed on staff and Loonie orders for Time titles 
		---@IsValid int
		@iPhoneExistsAndCCPay Int,
		@iCall1				 Int,
		@iCall2				 Int,
		@iCall3				 Int,
		@iCall4				 Int,
		@iCall5				 Int,
		@iCCPay				Int


SELECT	@iOtherProduct = count(*)
FROM		CustomerOrderDetail cod
WHERE	cod.ProductType <> 46001
AND		cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		cod.TransID = @iTransID

IF @iOtherProduct = 0
BEGIN
	SELECT	@iApproved = count(*)
	FROM		CustomerOrderDetailRemitHistory codrh
	WHERE	codrh.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		codrh.TransID = @iTransID
	
	IF @iApproved >= 1
		SET @iNonApproved = 0
	ELSE
		SET @iNonApproved = 1

	SELECT	@iInactiveMagazine = CASE p.Status WHEN 30601 THEN 1 ELSE 0 END
	FROM		QSPCanadaProduct..Product p,
			QSPCanadaProduct..Pricing_Details pd,
			CustomerOrderDetail cod
	WHERE	pd.Product_Instance = p.Product_Instance
	AND		cod.PricingDetailsID = pd.MagPrice_Instance
	AND		cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		cod.TransID = @iTransID

	-- 11/08/2006 - Jeff: Allow the refund action to be performed on staff and Loonie orders for Time titles 
	SELECT	@iIsStaffOrLoonieOrderForTime = CASE WHEN COUNT(*) >= 1 THEN 1 ELSE 0 END
	FROM	CustomerOrderDetail cod
	WHERE	cod.StatusInstance in (515, 516)
	AND		cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		cod.TransID = @iTransID

END
ELSE
BEGIN
	SET @iNonApproved = 0
	SET @iInactiveMagazine = 0
END

-- 02/09/2006 - Ben: Added new test to forbid multiple CC Reprocess
SELECT	@iIsCCReprocessed = CASE WHEN COUNT(*) >= 1 THEN 1 ELSE 0 END
FROM		Incident i,
		IncidentAction ia
WHERE	ia.IncidentInstance = i.IncidentInstance
AND		i.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		CASE @iCreditCardAction WHEN 0 THEN i.TransID ELSE 0 END = @iTransID
AND		ia.ActionInstance = 18

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  CANCEL SUBSCRIPTION
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @iActionInstance = 1
BEGIN
	IF @iNonApproved = 0 AND @iOtherProduct = 0 AND @iCreditCardAction = 0 --AND @iInactiveMagazine = 0
	BEGIN
		SELECT @iWasSent = count(*) 
		   FROM CustomerOrderDetailRemitHistory
		WHERE Status = '42001'
		      AND CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		      AND TransID = @iTransID
		
		SELECT @iWasCancel = count(*) 
		   FROM CustomerOrderDetailRemitHistory
		WHERE Status IN('42002', '42003', '42004')
		      AND CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		      AND TransID = @iTransID
	
		-- If it got remitted and is not already cancelled
		
		
		IF @iWasSent >= 1 AND @iWasCancel = 0
			SET @IsValid = 1
		ELSE
		 
			SET @IsValid = 0
	END
	ELSE IF @iNonApproved = 0 AND @iOtherProduct = 1 AND @iCreditCardAction = 0 
	BEGIN
		SET @IsValid = 1
		/*DECLARE @CODInvoiced BIT
		
		SELECT	@CODInvoiced = CASE ISNULL(cod.InvoiceNumber, 0) WHEN 0 THEN 0 ELSE 1 END
		FROM	CustomerOrderDetail cod
		WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		cod.TransID = @iTransID

		DECLARE @CODFulfilled BIT
		
		SELECT	@CODFulfilled = CASE cod.StatusInstance WHEN 508 THEN 1 ELSE 0 END
		FROM	CustomerOrderDetail cod
		WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		cod.TransID = @iTransID

		IF @CODInvoiced = 1 OR @CODFulfilled = 1
			SET @IsValid = 0
		ELSE
			SET @IsValid = 1*/
	END
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  NEW SUBSCRIPTION
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 2
BEGIN
	IF @iOtherProduct = 0 AND @iCreditCardAction = 0
		SET @IsValid = 1
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  ISSUE CUSTOMER REFUND
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 3
BEGIN
	--IF (@iNonApproved = 0 OR @iInactiveMagazine = 1 OR @iIsStaffOrLoonieOrderForTime = 1) AND @iCreditCardAction = 0
	IF @iCreditCardAction = 0
	BEGIN
		DECLARE	@TotalRefunded	NUMERIC(12, 2),
				@SubTotal		NUMERIC(12, 2)

		SELECT		@TotalRefunded = ISNULL(SUM(ref.Amount), 0.00)
		FROM		QSPCanadaFinance..Refund ref
		LEFT JOIN	QSPCanadaFinance..AP_Cheque apc
						ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
		WHERE		ref.Cancelled = 0
		AND			ISNULL(apc.AP_Cheque_Status_ID, 0) NOT IN (4, 5) --4: Voided, 5: Stopped
		AND			ref.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND			ref.TransID = @iTransID

		SELECT	@SubTotal = dbo.UDF_Refund_GetMaxCustomerRefundAmount(@iCustomerOrderHeaderInstance, @iTransID)
		/*SELECT	@SubTotal = SUM(cod.Price * ((100 -  ISNULL(camp.StaffOrderDiscount, 0)) / 100.00))
		FROM	CustomerOrderDetail cod
		JOIN	CustomerOrderHeader coh
					ON	coh.Instance = cod.CustomerOrderHeaderInstance
		JOIN	QSPCanadaCommon..Campaign camp
					ON	camp.ID = coh.CampaignID
		WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		cod.TransID = @iTransID
		AND		ISNULL(cod.InvoiceNumber, 0) <> 0*/
	
		IF @SubTotal > @TotalRefunded
			SET @IsValid = 1
		ELSE
			SET @IsValid = 0
	END
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  CHANGE OF ADDRESS
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance =4
BEGIN
	IF @iCreditCardAction = 0
	BEGIN
		IF @iOtherProduct = 0
			SET @iWasCancel = 0
		ELSE
		BEGIN
			SELECT @iWasCancel = count(*) 
			   FROM CustomerOrderDetailRemitHistory
			WHERE Status IN('42002', '42003', '42004')
				  AND CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
				  AND TransID = @iTransID
		END

		-- If never been cancelled
		IF @iWasCancel = 0
			SET @IsValid = 1
		ELSE
			SET @IsValid = 0
	END
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  GENERATE SWITCH LETTER
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 5
BEGIN
	IF @iNonApproved = 0 AND @iOtherProduct = 0 AND @iCreditCardAction = 0
	BEGIN
		-- title code must be unactive
		SET @IsValid = 1
	
		SELECT	@iHasSwitchLetter = count(*)
		FROM	LetterBatchCustomerOrderDetail lbcod
		JOIN	LetterBatch lb
					ON	lb.ID = lbcod.LetterBatchID
					AND	lb.DeletedTF = 0
		WHERE	lbcod.TransID = @iTransID 
		AND		lbcod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		lb.LetterTemplateID = 4
	
		SELECT @sTitleCode = ProductCode
		   FROM CustomerOrderDetail 
		WHERE TransID = @iTransID 
		      AND CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	
		SELECT @iIsTitleActive = CASE Status
					 	WHEN '30601' THEN 0
						ELSE 1 END
		   FROM QSPCanadaProduct..Product
		WHERE Product_Code = @sTitleCode
	
		-- Switch letter has not been generated and title is inactive
		IF @iHasSwitchLetter = 0 AND @iIsTitleActive = 0
			SET @IsValid = 1
		ELSE 
			SET @IsValid = 0
	END
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  NO ACTION REQUIRED
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 8
BEGIN --here @iNonApproved = 0 AND 
	IF @iCreditCardAction = 0
		SET @IsValid = 1
	ELSE 
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  CANCEL SUBSCRIPTION PRIOR TO REMIT
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 14
BEGIN
	IF @iNonApproved = 0 AND @iOtherProduct = 0 AND @iCreditCardAction = 0
	BEGIN
		DECLARE @iStatus int
		
		SELECT @iStatus = status FROM CustomerOrderDetailRemitHistory 
		WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
			  TransID = @iTransID order by datechanged desc
		IF @iStatus = '42000' or @iStatus='42010'
			SET @IsValid = 1
		ELSE 
			SET @IsValid = 0
	END
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  CANCEL SWITCH LETTER
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 16
BEGIN
	IF @iNonApproved = 0 AND @iOtherProduct = 0 AND @iCreditCardAction = 0
	BEGIN
		SELECT	@iHasSwitchLetter = count(*)
		FROM	LetterBatchCustomerOrderDetail lbcod
		JOIN	LetterBatch lb
					ON	lb.ID = lbcod.LetterBatchID
					AND	lb.DeletedTF = 0
		WHERE	lbcod.TransID = @iTransID 
		AND		lbcod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		lb.LetterTemplateID = 4

		IF @iHasSwitchLetter >= 1
			SET @IsValid = 1
		ELSE 
			SET @IsValid = 0
	END
	ELSE
		SET @IsValid = 0
 
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  REPRINT SWITCH LETTER
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 17
BEGIN
	IF @iNonApproved = 0 AND @iOtherProduct = 0 AND @iCreditCardAction = 0
	BEGIN
		SELECT	@iHasSwitchLetter = count(*)
		FROM	LetterBatchCustomerOrderDetail lbcod
		JOIN	LetterBatch lb
					ON	lb.ID = lbcod.LetterBatchID
					AND	lb.DeletedTF = 0
		WHERE	lbcod.TransID = @iTransID 
		AND		lbcod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		lb.LetterTemplateID = 4
	
		IF @iHasSwitchLetter >= 1
			SET @IsValid = 1
		ELSE 
			SET @IsValid = 0
	END
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  UPDATE CREDIT CARD INFO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 18
BEGIN
	IF @iIsCCReprocessed = 0
	BEGIN
		-- Check if address is missing
		SELECT	@iIsAddressValid = COUNT(*)
		FROM		Customer c,
				CustomerOrderDetail cod,
				CustomerOrderHeader coh
		WHERE	c.Instance = CASE cod.CustomerShipToInstance WHEN 0 THEN coh.CustomerBillToInstance ELSE cod.CustomerShipToInstance END
		AND		coh.Instance = cod.CustomerOrderHeaderInstance
		AND		coh.Instance = @iCustomerOrderHeaderInstance
		AND		COALESCE(c.Address1, '') <> ''
		AND		COALESCE(c.City, '') <> ''
		AND		COALESCE(c.State, '') <> ''
		AND		COALESCE(c.Zip, '') <> ''

		-- Ensure order is closed
		DECLARE @iIsClosed INT
		SELECT	@iIsClosed = COUNT(*)
		FROM	Batch b
		JOIN	CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
		WHERE	coh.Instance = @iCustomerOrderHeaderInstance
		AND		b.StatusInstance IN (40010, 40012, 40013, 40014)

		-- Ensure order has no line items not tied to offers
		DECLARE @iCODsWithoutOffer INT
		SELECT	@iCODsWithoutOffer = COUNT(*)
		FROM	CustomerOrderHeader coh
		JOIN	CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
		WHERE	coh.Instance = @iCustomerOrderHeaderInstance
		AND		ISNULL(cod.PricingDetailsID, 0) = 0

		IF(@iIsAddressValid >= 1 AND @iIsClosed >= 1 AND ISNULL(@iCODsWithoutOffer, 0) = 0)
		BEGIN
			IF (@iCreditCardAction = 1 AND (@iNonApproved = 1 OR @iOtherProduct = 1))
				SET @IsValid = 1
			ELSE IF (@iNonApproved = 1 OR @iOtherProduct = 1)
			BEGIN
				SELECT	@iHasCreditCard = count(*)
				FROM		CustomerOrderHeader coh
				WHERE	coh.Instance = @iCustomerOrderHeaderInstance
				AND		coh.PaymentMethodInstance IN (50003, 50004, 50005)
		
				IF @iHasCreditCard = 1
					SET @IsValid = 1
				ELSE
					SET @IsValid = 0
			END
			ELSE
				SET @IsValid = 0
		END
		ELSE
		BEGIN
			SET @IsValid = 0
		END
	END
	ELSE
	BEGIN
		SET @IsValid = 0
	END
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Credit card CALL Attempt 1
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 19
BEGIN
	
	--IF @iCreditCardAction = 1
	--BEGIN
		SELECT TOP 1 @iPhoneExistsAndCCPay = 1--CASE IsNull(c.Phone, '') WHEN '' THEN 0 ELSE 1 END 
		FROM   QSPcanadaOrdermanagement..CustomerOrderHeader h,
			   QSPcanadaOrdermanagement..Customer c,
			   QSPcanadaOrdermanagement..CustomerPaymentHeader cph,
			   QSPcanadaOrdermanagement..vw_CreditcardPayment ccp
		WHERE h.CustomerBilltoInstance=c.Instance
		AND	  h.PaymentMethodInstance IN (50003, 50004, 50005)
		AND   cph.Instance= ccp.CustomerPaymentHeaderInstance
		AND   cph.CustomerOrderHeaderInstance= h.Instance
		AND   ccp.StatusInstance <> 19000
		AND NOT EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
							    QSPCanadaOrderManagement.dbo.incident i
				WHERE  i.IncidentInstance = iA.IncidentInstance
				AND i.customerorderheaderinstance= h.instance
				AND ia.ActionInstance  in (18) )
		AND   h.Instance=	@iCustomerOrderHeaderInstance	

		-- If Valid 		
		IF IsNull(@iPhoneExistsAndCCPay,0)=1
		BEGIN
	
			SELECT @iCall1=count(*)
			FROM   QSPCanadaOrderManagement.dbo.incident inc,
				   QSPCanadaOrderManagement.dbo.incidentAction incA
			WHERE  inc.IncidentInstance = incA.IncidentInstance
			AND incA.ActionInstance  in (19) 
			AND inc.customerorderheaderinstance=@iCustomerOrderHeaderInstance
	
			IF IsNull(@iCall1,0) = 0 
				 SET @IsValid = 1
			ELSE
				SET @IsValid = 0 
		END
	    ELSE
				SET @IsValid = 0 
END
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Credit card CALL Attempt 2
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 20
BEGIN
	
	--IF @iCreditCardAction = 1
	--BEGIN
		SELECT TOP 1 @iPhoneExistsAndCCPay = 1--CASE IsNull(c.Phone, '') WHEN '' THEN 0 ELSE 1 END 
		FROM   QSPcanadaOrdermanagement..CustomerOrderHeader h,
			   QSPcanadaOrdermanagement..Customer c,
			   QSPcanadaOrdermanagement..CustomerPaymentHeader cph,
			   QSPcanadaOrdermanagement..vw_CreditcardPayment ccp
		WHERE h.CustomerBilltoInstance=c.Instance
		AND	  h.PaymentMethodInstance IN (50003, 50004, 50005)
		AND   cph.Instance= ccp.CustomerPaymentHeaderInstance
		AND   cph.CustomerOrderHeaderInstance= h.Instance
		AND   ccp.StatusInstance <> 19000
		AND NOT EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
							    QSPCanadaOrderManagement.dbo.incident i
				WHERE  i.IncidentInstance = iA.IncidentInstance
				AND i.customerorderheaderinstance= h.instance
				AND ia.ActionInstance  in (18) )
		AND   h.Instance=	@iCustomerOrderHeaderInstance	

		-- If Valid 		
		IF IsNull(@iPhoneExistsAndCCPay,0)=1
		BEGIN

			SELECT @iCall2=count(*)
			FROM   QSPCanadaOrderManagement.dbo.incident inc,
				   QSPCanadaOrderManagement.dbo.incidentAction incA
			WHERE  inc.IncidentInstance = incA.IncidentInstance
			AND incA.ActionInstance  in (19) 
			AND inc.customerorderheaderinstance=@iCustomerOrderHeaderInstance
			AND NOT EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
										    QSPCanadaOrderManagement.dbo.incident i
							WHERE  i.IncidentInstance = iA.IncidentInstance
							--AND incA.IncidentInstance = iA.IncidentInstance
							AND i.customerorderheaderinstance= inc.customerorderheaderinstance
							AND ia.ActionInstance  in (20) )
			AND NOT EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
										    QSPCanadaOrderManagement.dbo.incident i
							WHERE  i.IncidentInstance = iA.IncidentInstance
							--AND incA.IncidentInstance = iA.IncidentInstance
							AND i.customerorderheaderinstance= inc.customerorderheaderinstance
							AND ia.ActionInstance  in (21) )
	
			IF IsNull(@iCall2,0) >0
				 SET @IsValid = 1
			ELSE
				SET @IsValid = 0
		END
	    ELSE
	        SET @IsValid = 0
END
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Credit card CALL Attempt 3
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 21
BEGIN
	
	--IF @iCreditCardAction = 1
	--BEGIN
		SELECT TOP 1 @iPhoneExistsAndCCPay = 1--CASE IsNull(c.Phone, '') WHEN '' THEN 0 ELSE 1 END 
		FROM   QSPcanadaOrdermanagement..CustomerOrderHeader h,
			   QSPcanadaOrdermanagement..Customer c,
			   QSPcanadaOrdermanagement..CustomerPaymentHeader cph,
			   QSPcanadaOrdermanagement..vw_CreditcardPayment ccp
		WHERE h.CustomerBilltoInstance=c.Instance
		AND	  h.PaymentMethodInstance IN (50003, 50004, 50005)
		AND   cph.Instance= ccp.CustomerPaymentHeaderInstance
		AND   cph.CustomerOrderHeaderInstance= h.Instance
		AND   ccp.StatusInstance <> 19000
		AND NOT EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
							    QSPCanadaOrderManagement.dbo.incident i
				WHERE  i.IncidentInstance = iA.IncidentInstance
				AND i.customerorderheaderinstance= h.instance
				AND ia.ActionInstance  in (18) )
		AND   h.Instance=	@iCustomerOrderHeaderInstance	

		IF IsNull(@iPhoneExistsAndCCPay,0)=1
		BEGIN

			SELECT @iCall3=count(*)
			FROM   QSPCanadaOrderManagement.dbo.incident inc,
				   QSPCanadaOrderManagement.dbo.incidentAction incA
			WHERE  inc.IncidentInstance = incA.IncidentInstance
			AND incA.ActionInstance  in (19) 
			AND inc.customerorderheaderinstance=@iCustomerOrderHeaderInstance
			AND EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
										QSPCanadaOrderManagement.dbo.incident i
							WHERE  i.IncidentInstance = iA.IncidentInstance
							--AND incA.IncidentInstance = iA.IncidentInstance
							AND i.customerorderheaderinstance= inc.customerorderheaderinstance
							AND ia.ActionInstance  in (20) )
			AND  NOT EXISTS (SELECT 1 FROM  QSPCanadaOrderManagement.dbo.incidentAction ia,
										    QSPCanadaOrderManagement.dbo.incident i
							WHERE  i.IncidentInstance = iA.IncidentInstance
							--AND incA.IncidentInstance = iA.IncidentInstance
							AND i.customerorderheaderinstance= inc.customerorderheaderinstance
							AND ia.ActionInstance  in (21) )
	
			IF  IsNull(@iCall3,0)> 0
				 SET @IsValid = 1
			ELSE
				SET @IsValid = 0
		END
	    ELSE
	        SET @IsValid = 0
END
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Credit card CALL Attempt 4
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 26
BEGIN
	
	--IF @iCreditCardAction = 1
	--BEGIN
		SELECT TOP 1 @iPhoneExistsAndCCPay = 1--CASE IsNull(c.Phone, '') WHEN '' THEN 0 ELSE 1 END 
		FROM   QSPcanadaOrdermanagement..CustomerOrderHeader h,
			   QSPcanadaOrdermanagement..Customer c,
			   QSPcanadaOrdermanagement..CustomerPaymentHeader cph,
			   QSPcanadaOrdermanagement..vw_CreditcardPayment ccp
		WHERE h.CustomerBilltoInstance=c.Instance
		AND	  h.PaymentMethodInstance IN (50003, 50004, 50005)
		AND   cph.Instance= ccp.CustomerPaymentHeaderInstance
		AND   cph.CustomerOrderHeaderInstance= h.Instance
		AND   ccp.StatusInstance <> 19000
		AND NOT EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
							    QSPCanadaOrderManagement.dbo.incident i
				WHERE  i.IncidentInstance = iA.IncidentInstance
				AND i.customerorderheaderinstance= h.instance
				AND ia.ActionInstance  in (18) )
		AND   h.Instance=	@iCustomerOrderHeaderInstance	

		IF IsNull(@iPhoneExistsAndCCPay,0)=1
		BEGIN

			SELECT @iCall4=count(*)
			FROM   QSPCanadaOrderManagement.dbo.incident inc,
				   QSPCanadaOrderManagement.dbo.incidentAction incA
			WHERE  inc.IncidentInstance = incA.IncidentInstance
			AND incA.ActionInstance  in (19) 
			AND inc.customerorderheaderinstance=@iCustomerOrderHeaderInstance
			AND EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
										QSPCanadaOrderManagement.dbo.incident i
							WHERE  i.IncidentInstance = iA.IncidentInstance
							--AND incA.IncidentInstance = iA.IncidentInstance
							AND i.customerorderheaderinstance= inc.customerorderheaderinstance
							AND ia.ActionInstance  in (21) )
			AND  NOT EXISTS (SELECT 1 FROM  QSPCanadaOrderManagement.dbo.incidentAction ia,
										    QSPCanadaOrderManagement.dbo.incident i
							WHERE  i.IncidentInstance = iA.IncidentInstance
							--AND incA.IncidentInstance = iA.IncidentInstance
							AND i.customerorderheaderinstance= inc.customerorderheaderinstance
							AND ia.ActionInstance  in (26) )
	
			IF  IsNull(@iCall4,0)> 0
				 SET @IsValid = 1
			ELSE
				SET @IsValid = 0
		END
	    ELSE
	        SET @IsValid = 0
END
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Credit card CALL Attempt 5
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 27
BEGIN
	
	--IF @iCreditCardAction = 1
	--BEGIN
		SELECT TOP 1 @iPhoneExistsAndCCPay = 1--CASE IsNull(c.Phone, '') WHEN '' THEN 0 ELSE 1 END 
		FROM   QSPcanadaOrdermanagement..CustomerOrderHeader h,
			   QSPcanadaOrdermanagement..Customer c,
			   QSPcanadaOrdermanagement..CustomerPaymentHeader cph,
			   QSPcanadaOrdermanagement..vw_CreditcardPayment ccp
		WHERE h.CustomerBilltoInstance=c.Instance
		AND	  h.PaymentMethodInstance IN (50003, 50004, 50005)
		AND   cph.Instance= ccp.CustomerPaymentHeaderInstance
		AND   cph.CustomerOrderHeaderInstance= h.Instance
		AND   ccp.StatusInstance <> 19000
		AND NOT EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
							    QSPCanadaOrderManagement.dbo.incident i
				WHERE  i.IncidentInstance = iA.IncidentInstance
				AND i.customerorderheaderinstance= h.instance
				AND ia.ActionInstance  in (18) )
		AND   h.Instance=	@iCustomerOrderHeaderInstance	

		IF IsNull(@iPhoneExistsAndCCPay,0)=1
		BEGIN

			SELECT @iCall5=count(*)
			FROM   QSPCanadaOrderManagement.dbo.incident inc,
				   QSPCanadaOrderManagement.dbo.incidentAction incA
			WHERE  inc.IncidentInstance = incA.IncidentInstance
			AND incA.ActionInstance  in (19) 
			AND inc.customerorderheaderinstance=@iCustomerOrderHeaderInstance
			AND EXISTS (SELECT 1 FROM   QSPCanadaOrderManagement.dbo.incidentAction ia,
										QSPCanadaOrderManagement.dbo.incident i
							WHERE  i.IncidentInstance = iA.IncidentInstance
							--AND incA.IncidentInstance = iA.IncidentInstance
							AND i.customerorderheaderinstance= inc.customerorderheaderinstance
							AND ia.ActionInstance  in (26) )
			AND  NOT EXISTS (SELECT 1 FROM  QSPCanadaOrderManagement.dbo.incidentAction ia,
										    QSPCanadaOrderManagement.dbo.incident i
							WHERE  i.IncidentInstance = iA.IncidentInstance
							--AND incA.IncidentInstance = iA.IncidentInstance
							AND i.customerorderheaderinstance= inc.customerorderheaderinstance
							AND ia.ActionInstance  in (27) )
	
			IF  IsNull(@iCall5,0)> 0
				 SET @IsValid = 1
			ELSE
				SET @IsValid = 0
		END
	    ELSE
	        SET @IsValid = 0
END
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Remove From OEFU Report
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 22
BEGIN

	SELECT TOP 1 @iCCPay = CASE WHEN COUNT(*) >= 1 THEN 1 ELSE 0 END
	FROM		CustomerOrderHeader coh
	JOIN		CustomerPaymentHeader cph
					ON	coh.Instance = cph.CustomerOrderHeaderInstance
	JOIN		vw_CreditcardPayment ccp
					ON	ccp.CustomerPaymentHeaderInstance = cph.Instance
	WHERE		coh.PaymentMethodInstance IN (50003, 50004, 50005)
	AND			ccp.StatusInstance <> 19000
	AND			coh.Instance = @iCustomerOrderHeaderInstance	

	DECLARE @BadAddress INT
	SELECT	@BadAddress = CASE WHEN COUNT(*) >= 1 THEN 1 ELSE 0 END
	FROM	CustomerOrderHeader coh
	JOIN	Customer cust
				ON	cust.Instance = coh.CustomerBillToInstance
	WHERE	cust.StatusInstance = 301
	AND		coh.Instance = @iCustomerOrderHeaderInstance

	DECLARE @BadProduct INT
	SELECT	@BadProduct = CASE WHEN COUNT(*) >= 1 THEN 1 ELSE 0 END
	FROM	CustomerOrderDetail cod
	WHERE	(cod.ProductCode = 'NNNN' OR cod.StatusInstance = 501 OR ISNULL(cod.PricingDetailsID, 0) = 0)
	AND		cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		cod.TransID = @iTransID
	
	DECLARE @COHAction INT
	SELECT	@COHAction = CASE WHEN COUNT(*) = 0 THEN 1 ELSE 0 END
	FROM	incident inc
	JOIN	incidentAction incAct
				ON	incAct.IncidentInstance = inc.IncidentInstance
	WHERE	incAct.ActionInstance in (-1) --Was 22 Remove From OEFU Report, but moved to COD action
	AND		inc.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance

	DECLARE @CODAction INT
	SELECT	@CODAction = CASE WHEN COUNT(*) = 0 THEN 1 ELSE 0 END
	FROM	incident inc
	JOIN	incidentAction incAct
				ON	incAct.IncidentInstance = inc.IncidentInstance
	WHERE	incAct.ActionInstance in (18, 22, 150, 151) --CC Update, Remove From OEFU Report, New Sub to Invoice, New Item to Invoice)
	AND		inc.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		inc.TransID = @iTransID
	
	IF ((IsNull(@iCCPay,0) = 1 OR ISNULL(@BadAddress, 0) = 1 OR ISNULL(@BadProduct, 0) = 1) AND (ISNULL(@COHAction, 0) = 1 AND ISNULL(@CODAction, 0) = 1))
	BEGIN
		--Can only remove from OEFU if it exists in either school oefu or CC rejections report
		SELECT	@IsValid = 1
	END
    ELSE
		SET @IsValid = 0 
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  CANCEL CUSTOMER REFUND
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 23
BEGIN
	
	DECLARE	@RefundNotSent	BIT

	SELECT		@RefundNotSent = CONVERT(BIT, 1)
	FROM		QSPCanadaFinance..Refund ref
	LEFT JOIN	QSPCanadaFinance..AP_Cheque apc
					ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
	WHERE		ref.Cancelled = 0
	AND			(apc.AP_Cheque_Batch_ID IS NULL
	AND			ref.CreateDate > '2009-02-06 11:46') --Assume Old Customer Refunds have been sent
	AND			ref.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND			ref.TransID = @iTransID

	DECLARE	@RefundOutstanding	BIT

	SELECT	@RefundOutstanding = CONVERT(BIT, 1)
	FROM	QSPCanadaFinance..Refund ref
	JOIN	QSPCanadaFinance.dbo.AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
	WHERE	apc.AP_Cheque_Status_ID IN (2) --2: Outstanding
	AND		ref.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		ref.TransID = @iTransID

	--Refund exists and its cheque has either not yet been created or is still outstanding (not cashed/voided)
	IF @RefundOutstanding = 1 OR @RefundNotSent = 1
		SET @IsValid = 1
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  RESEND SUB
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 24
BEGIN

	SET @IsValid = 0

	IF @iNonApproved = 0 AND @iOtherProduct = 0 AND @iCreditCardAction = 0 AND @iInactiveMagazine = 0
	BEGIN
		SELECT	@iWasSent = COUNT(*) 
		FROM	CustomerOrderDetailRemitHistory codrh
		WHERE	codrh. CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		codrh.TransID = @iTransID
		AND		codrh.Status IN (42001) --42001: Sent
		
		DECLARE @iSendPending	INT
		SELECT	@iSendPending = COUNT(*) 
		FROM	CustomerOrderDetailRemitHistory codrh
		WHERE	codrh. CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		codrh.TransID = @iTransID
		AND		codrh.Status IN (42000) --42000: Needs to be sent

		DECLARE @iWasInactive	INT
		SELECT	@iWasInactive = COUNT(*) 
		FROM	CustomerOrderDetailRemitHistory codrh
		WHERE	codrh. CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		codrh.TransID = @iTransID
		AND		codrh.Status IN (42010) --42001: Inactive Magazine

		SELECT	@iWasCancel = COUNT(*) 
		FROM	CustomerOrderDetailRemitHistory codrh
		WHERE	codrh.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		codrh.TransID = @iTransID
		AND		codrh.Status IN (42002, 42003, 42004) --42002: Needs to be cancelled, 42003: Cancelled, 42004: Cancelled prior to remit

		DECLARE @iHasChadd	INT
		SELECT	@iHasChadd = COUNT(*)
		FROM	CustomerOrderDetailRemitHistory codrh
		WHERE	codrh.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		codrh.TransID = @iTransID
		AND		codrh.Status IN (42006, 42007) --42006: CHADD needs to be sent, 42007: CHADD Sent

		IF ((@iWasSent >= 1 AND @iSendPending = 0) OR @iWasInactive >= 1) AND @iWasCancel = 0 --AND @iHasChadd = 0
			SET @IsValid = 1
	END
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  UPDATE PRODUCT
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 25
BEGIN
	IF @iCreditCardAction = 0
	BEGIN
		/*DECLARE @IsOrderOpen BIT

		SELECT	@IsOrderOpen = 1
		FROM	CustomerOrderDetail cod
		JOIN	CustomerOrderHeader coh
					ON	coh.Instance = cod.CustomerOrderHeaderInstance
		JOIN	Batch b
					ON	b.ID = coh.OrderBatchID
					AND	b.Date = coh.OrderBatchDate
		WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		cod.TransID = @iTransID
		AND		(b.StatusInstance IN (40002, 40003) OR cod.StatusInstance IN (501, 512))*/
		
		DECLARE @IsCODInvoiced BIT
		
		SELECT	@IsCODInvoiced = CASE ISNULL(cod.InvoiceNumber, 0) WHEN 0 THEN 0 ELSE 1 END
		FROM	CustomerOrderDetail cod
		WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		cod.TransID = @iTransID

		IF @IsCODInvoiced = 1
			SET @IsValid = 0
		ELSE
			SET @IsValid = 1
	END
	ELSE
		SET @IsValid = 0
END


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  UPDATE EMAIL
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

ELSE IF @iActionInstance = 28
BEGIN
      IF @iCreditCardAction = 0
          SET @IsValid = 1
      ELSE
          SET @IsValid = 0
END


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  NEW ITEM
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 100
BEGIN
	IF @iNonApproved = 0 AND @iOtherProduct = 1 AND @iCreditCardAction = 0
	BEGIN
		SELECT @iProductType = cod.ProductType
		FROM CustomerOrderDetail cod
		WHERE cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND cod.TransID = @iTransID
	
		IF @iProductType = 46006 or @iProductType = 46007 or @iProductType = 46012
			SET @IsValid = 1
		ELSE
			SET @IsValid = 0
	END
	ELSE
		SET @IsValid = 0
END


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  NEW SUB TO INVOICE
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 150
BEGIN
	IF @iOtherProduct = 0 AND @iCreditCardAction = 0
	BEGIN
		SELECT @iProductType = cod.ProductType
		FROM CustomerOrderDetail cod
		WHERE cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND cod.TransID = @iTransID

		IF @iProductType = 46001
		BEGIN
			declare @iHasBeenRemitted int

			SELECT	@iHasBeenRemitted = count(*)
			FROM		CustomerOrderDetailRemitHistory
			WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
			AND		TransID = @iTransID

			IF @iHasBeenRemitted = 0
				SET @IsValid = 1
			ELSE
				SET @IsValid = 0
		END
		ELSE
		BEGIN
			SET @IsValid = 0
		END
	END
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  NEW ITEM TO INVOICE
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 151
BEGIN
	IF @iNonApproved = 0 AND @iOtherProduct = 1 AND @iCreditCardAction = 0
		SET @IsValid = 1
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  NEW SUB - TIME STAFF OR LOONIE
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 152
BEGIN
	IF @iIsStaffOrLoonieOrderForTime = 1 AND @iCreditCardAction = 0
	BEGIN
		DECLARE @iIsRemitted int

		SELECT	@iIsRemitted = count(*)
		FROM	CustomerOrderDetailRemitHistory
		WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		TransID = @iTransID

		IF @iIsRemitted = 0
			SET @IsValid = 1
		ELSE
			SET @IsValid = 0
	END
	ELSE
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  AR ACTION REQUIRED
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE IF @iActionInstance = 300
BEGIN
	IF @iNonApproved = 0 AND @iCreditCardAction = 0
		SET @IsValid = 1
	ELSE 
		SET @IsValid = 0
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  ALL OTHER ACTIONS
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ELSE
BEGIN
	IF @iNonApproved = 0 AND @iOtherProduct = 0 AND @iCreditCardAction = 0
		SET @IsValid = 1
	ELSE
		SET @IsValid = 0
END

print @IsValid
GO

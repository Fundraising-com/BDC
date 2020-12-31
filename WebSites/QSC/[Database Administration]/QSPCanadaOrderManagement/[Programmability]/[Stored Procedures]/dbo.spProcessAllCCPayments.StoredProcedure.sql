USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spProcessAllCCPayments]    Script Date: 06/07/2017 09:20:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[spProcessAllCCPayments]

AS

DECLARE	@CreditCardBatchID		INT,
		@RecordsProcessed		INT

--Landed + Customer Service To Invoice
DECLARE AllCCBatches CURSOR FOR
SELECT	DISTINCT
		CCB.ID
FROM	CreditCardbatch ccb
JOIN	CreditCardPayment ccp
			ON	ccb.ID = ccp.BatchID
WHERE	ISNULL(ccb.IsGLDone,0) = 0 
AND		ccp.StatusInstance = 19000

OPEN AllCCBatches
FETCH Next FROM AllCCBatches INTO @CreditCardBatchID 
		
WHILE @@FETCH_STATUS = 0
BEGIN
			
	EXEC spDoCreditCardGL @CreditCardBatchID, @RecordsProcessed OUTPUT

	FETCH Next FROM AllCCBatches INTO @CreditCardBatchID 

END
CLOSE AllCCBatches
DEALLOCATE AllCCBatches

--Online
INSERT INTO	NonBatchCreditCardPayment
SELECT		ccp.CustomerPaymentHeaderInstance,
			ccp.CreditCardNumber,
			ccp.ExpirationDate,
			ccp.ReasonCode,
			ccp.AuthorizationSource,
			ccp.AuthorizationCode,
			ccp.AuthorizationDate,
			ccp.AVSResponseCode,
			ccp.StatusInstance,
			ccp.DateCreated,
			ccp.UserIDCreated,
			ccp.DateChanged,
			ccp.UserIDChanged,
			ccp.VerisignID,
			0
FROM		CustomerPaymentHeader cph
JOIN		CreditCardPayment ccp
				ON ccp.CustomerPaymentHeaderInstance = cph.Instance
WHERE		cph.IsCreditCard = 1
AND			cph.StatusInstance = 600 --600: Good
AND			ccp.StatusInstance = 19000 --19000: Good
AND			ISNULL(ccp.BatchID, 0) = 0
AND			cph.Instance NOT IN (SELECT CustomerPaymentHeaderInstance FROM NonBatchCreditCardPayment)

IF @@ROWCOUNT > 0
BEGIN

	EXEC spDoCreditCardGLNonBatch @RecordsProcessed OUTPUT

END
GO

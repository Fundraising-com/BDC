USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Refund_DoCustomerRefund]    Script Date: 06/07/2017 09:17:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Refund_DoCustomerRefund]

	@CustomerOrderHeaderInstance	INT = 0,
	@TransID						INT = 0,
	@FirstName						NVARCHAR(50) = '',
	@LastName						NVARCHAR(50)= '',
	@Address1						NVARCHAR(50)= '',
	@Address2						NVARCHAR(50)= '',
	@City							VARCHAR(50) = '',
	@ProvinceCode					VARCHAR(2) = '',
	@PostalCode						VARCHAR(10) = '',
	@Amount							NUMERIC(10, 2) = 0,
	@Reason							NVARCHAR(255),
	@IncidentID						INT = 0,
	@iUserID						INT = 0,
	@ErrorMessage					VARCHAR(200) OUTPUT

AS

DECLARE
	@ProductCode					VARCHAR(4),
	@Title							VARCHAR(55),
	@CountryCode					VARCHAR(10),
	@RefundAmount					NUMERIC(10, 2),
	@SendEmailToIT					VARCHAR(1000),
	@RunDate						DATETIME,
	@Refund_ID						INT,
	@Refund_Type_ID					INT,
	@GLEntryID					INT,
	@GL_Debit_Transaction_ID		INT,
	@GL_Debit_Transaction_ID2		INT,
	@GL_Credit_Transaction_ID		INT,
	@GL_Credit_Transaction_ID2		INT

SET	@RunDate = GETDATE()
SET	@Refund_Type_ID = 1
SET @SendEmailToIT = 'qsp-IT-canada@qsp.com'
SET @CountryCode = 'CA'

SELECT	DISTINCT 
 		@ProductCode = cod.ProductCode, 
		@Title = cod.ProductName
		--@CountryCode = tp.Country_Code 
FROM 	QSPCanadaOrderManagement..CustomerOrderDetail cod
JOIN	QSPCanadaProduct..pricing_details pd
			ON	pd.MagPrice_Instance = cod.PricingDetailsID
--JOIN	QSPCanadaCommon..TaxRegionProvince trp
--			ON	trp.TaxRegionID = pd.TaxRegionID
--JOIN	QSPCanadaCommon..TaxProvince tp
--			ON	tp.province_code = trp.province
WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND		cod.TransID = @TransID

IF @@ROWCOUNT <> 1
BEGIN
	SET	@ErrorMessage = 'Error Obtaining subscription information for COH ' + CAST(@CustomerOrderHeaderInstance AS VARCHAR) + ' CustomerServiceRefund procedure'
	RETURN
END

IF LEN(@IncidentID) =  0 or @IncidentID = -1
BEGIN
	SET @ErrorMessage = 'Incident ID is missing for COH ' + CAST(@CustomerOrderHeaderInstance AS VARCHAR) + ' in CustomerServiceRefund procedure'
	RETURN
END

DECLARE @BusinessUnitID INT
SELECT	@BusinessUnitID = acc.BusinessUnitID
FROM	QSPCanadaOrderManagement..CustomerOrderHeader coh
JOIN	QSPCanadaCommon..Campaign camp
			ON	camp.ID = coh.CampaignID
JOIN	QSPCanadaCommon..CAccount acc
			ON	acc.ID = camp.BillToAccountID
WHERE	coh.Instance = @CustomerOrderHeaderInstance

BEGIN TRANSACTION

INSERT Refund
(
	Refund_Type_ID,
	Amount,
	Address1,
	Address2,
	City,
	Province,
	PostalCode,
	Country,
	CustomerOrderHeaderInstance,
	TransID,
	FirstName,
	LastName,
	Comment,
	CreateDate,
	CreateUserID
)
SELECT	@Refund_Type_ID,
		@Amount,
		@Address1,
		@Address2,
		@City,
		@ProvinceCode,
		@PostalCode,
		@CountryCode,
		@CustomerOrderHeaderInstance,
		@TransID,
		@FirstName,
		@LastName,
		@Reason,
		@RunDate,
		@iUserID
		
SET @Refund_ID = SCOPE_IDENTITY()

IF @@ERROR <> 0 OR ISNULL(@Refund_ID, 0) = 0
BEGIN
	ROLLBACK
	SELECT @ErrorMessage = 'Failed to insert refund record for COH ' + CAST(@CustomerOrderHeaderInstance AS VARCHAR)
	EXEC QSPCanadaCommon..Send_EMail  'CustomerRefunds@qsp.com', @SendEmailToIT, 'Error in Customer Refund', @ErrorMessage
	RETURN
END

DECLARE @GLDescription	VARCHAR(30)
SET @GLDescription = 'Customer Refund Cheque (Debit)'

EXEC	GL_Entry_Insert
		@Description = @GLDescription,
		@CountryCode = @CountryCode,
		@RefundID = @Refund_ID,
		@BusinessUnitID = @BusinessUnitID,
		@GLEntryID = @GLEntryID OUTPUT

IF @@ERROR <> 0 OR ISNULL(@GLEntryID, 0) = 0
BEGIN
	ROLLBACK
	SELECT @ErrorMessage = 'Failed to insert GL Entry record for COH ' + CAST(@CustomerOrderHeaderInstance AS VARCHAR)
	EXEC QSPCanadaCommon..Send_EMail  'CustomerRefunds@qsp.com', @SendEmailToIT, 'Error in Customer Refund', @ErrorMessage
	RETURN
END

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GLEntryID,
		glAccMap.GLAccountID,
		CASE glAccMap.Debit
			WHEN 0 THEN	'C'
			ELSE		'D'
		END,
		@Amount,
		2
FROM	GLAccountMap glAccMap
WHERE	glAccMap.GLEntryTypeID = 11 --11: Expense
AND		glAccMap.BusinessUnitID = @BusinessUnitID

SET @GL_Debit_Transaction_ID = SCOPE_IDENTITY()

IF @@ERROR <> 0 OR ISNULL(@GL_Debit_Transaction_ID, 0) = 0
BEGIN
	ROLLBACK
	SELECT @ErrorMessage = 'Failed to insert GL Debit Transaction record for COH ' + CAST(@CustomerOrderHeaderInstance AS VARCHAR)
	EXEC QSPCanadaCommon..Send_EMail  'CustomerRefunds@qsp.com', @SendEmailToIT, 'Error in Customer Refund', @ErrorMessage
	RETURN
END

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GLEntryID,
		glAccMap.GLAccountID,
		CASE glAccMap.Debit
			WHEN 0 THEN	'C'
			ELSE		'D'
		END,
		@Amount,
		2
FROM	GLAccountMap glAccMap
WHERE	glAccMap.GLEntryTypeID = 12 --12: Account Payable - Credit
AND		glAccMap.BusinessUnitID = @BusinessUnitID

SET @GL_Credit_Transaction_ID = SCOPE_IDENTITY()

IF @@ERROR <> 0 OR ISNULL(@GL_Credit_Transaction_ID, 0) = 0
BEGIN
	ROLLBACK
	SELECT @ErrorMessage = 'Failed to insert GL Credit Transaction #2 record for COH ' + CAST(@CustomerOrderHeaderInstance AS VARCHAR)
	EXEC QSPCanadaCommon..Send_EMail  'CustomerRefunds@qsp.com', @SendEmailToIT, 'Error in Customer Refund', @ErrorMessage
	RETURN
END

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GLEntryID,
		glAccMap.GLAccountID,
		CASE glAccMap.Debit
			WHEN 0 THEN	'C'
			ELSE		'D'
		END,
		@Amount,
		2
FROM	GLAccountMap glAccMap
WHERE	glAccMap.GLEntryTypeID = 14 --14: Acount Payable - Debit
AND		glAccMap.BusinessUnitID = @BusinessUnitID

SET @GL_Debit_Transaction_ID2 = SCOPE_IDENTITY()

IF @@ERROR <> 0 OR ISNULL(@GL_Debit_Transaction_ID2, 0) = 0
BEGIN
	ROLLBACK
	SELECT @ErrorMessage = 'Failed to insert GL Debit Transaction #2 record for COH ' + CAST(@CustomerOrderHeaderInstance AS VARCHAR)
	EXEC QSPCanadaCommon..Send_EMail  'CustomerRefunds@qsp.com', @SendEmailToIT, 'Error in Customer Refund', @ErrorMessage
	RETURN
END

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GLEntryID,
		glAccMap.GLAccountID,
		CASE glAccMap.Debit
			WHEN 0 THEN	'C'
			ELSE		'D'
		END,
		@Amount,
		2
FROM	GLAccountMap glAccMap
WHERE	glAccMap.GLEntryTypeID = 13 --13: Cash

SET @GL_Credit_Transaction_ID2 = SCOPE_IDENTITY()

IF @@ERROR <> 0 OR ISNULL(@GL_Credit_Transaction_ID2, 0) = 0
BEGIN
	ROLLBACK
	SELECT @ErrorMessage = 'Failed to insert GL Credit Transaction #2 record for COH ' + CAST(@CustomerOrderHeaderInstance AS VARCHAR)
	EXEC QSPCanadaCommon..Send_EMail  'CustomerRefunds@qsp.com', @SendEmailToIT, 'Error in Customer Refund', @ErrorMessage
	RETURN
END

COMMIT
GO

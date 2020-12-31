USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_Entry_Insert]    Script Date: 06/07/2017 09:17:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GL_Entry_Insert]

	@InvoiceID			INT = NULL,
	@PaymentID			INT = NULL,
	@AdjustmentID		INT = NULL,
	@Description		VARCHAR(50),
	@CountryCode		VARCHAR(10),
	@RefundID			INT = NULL,
	@APChequeRemitID	INT = NULL,
	@BusinessUnitID		INT,
	@GLEntryID			INT OUTPUT

AS

DECLARE	@AccountingYear		INT,
		@AccountingPeriod	INT

EXEC	AccountingPeriod_GetCurrent
		@AccountingYear = @AccountingYear OUTPUT,
		@AccountingPeriod = @AccountingPeriod OUTPUT

INSERT GL_Entry
(
	Invoice_ID,
	Payment_ID,
	Adjustment_ID,
	Accounting_Year,
	Accounting_Period,
	GL_Entry_Date,
	GL_Posting_Date,
	[Description],
	Is_Posted,
	Country_Code,
	Refund_ID,
	AP_Cheque_Remit_ID,
	BusinessUnitID
)
SELECT	@InvoiceID,
		@PaymentID,
		@AdjustmentID,
		@AccountingYear,
		@AccountingPeriod,
		GETDATE(),
		NULL,
		@Description,
		'N',
		@CountryCode,
		@RefundID,
		@APChequeRemitID,
		@BusinessUnitID

SET @GLEntryID = SCOPE_IDENTITY()
GO

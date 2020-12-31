USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_StatusReceipt_Process]    Script Date: 06/07/2017 09:17:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_StatusReceipt_Process]

	@ChequeStatusReceiptID		INT

AS

DECLARE	@RunDate				DATETIME,
		@Refund_ID				INT,
		@AP_Cheque_ID			INT,
		@BusinessUnitID			INT,
		@Amount					NUMERIC(12, 2),
		@Refund_Type_ID			INT,
		@AP_Cheque_Status_ID	INT,
		@AccountingYear			INT,
		@AccountingPeriod		INT,
		@GL_Entry_ID			INT,
		@GL_Account_AP			VARCHAR(MAX),
		@GL_Account_Cash		VARCHAR(MAX)

SET @RunDate = GETDATE()
SET @GL_Account_AP = '062.2007.0000.1007.00.00.000'
SET @GL_Account_Cash = '062.1000.0000.0000.00.00.000'

SELECT	@AccountingYear = Accounting_Year,
		@AccountingPeriod = Accounting_Period
FROM	Accounting_Period
WHERE	Is_Closed = 'N'
AND		Country_Code = 'CA'
AND		@RunDate >= [Start_Date]
AND		@RunDate < End_Date

SELECT	@Refund_ID = ref.Refund_ID,
		@AP_Cheque_ID = apc.AP_Cheque_ID,
		@AP_Cheque_Status_ID = CASE apcsr.AP_Cheque_StatusReceipt_Type_ID
								WHEN 1 THEN 3 --3: Paid
							END,
		@Amount = ref.Amount,
		@Refund_Type_ID = ref.Refund_Type_ID
FROM	AP_Cheque_StatusReceipt apcsr
JOIN	AP_Cheque apc
			ON	apc.AP_Cheque_ID = apcsr.AP_Cheque_ID
JOIN	Refund ref
			ON	ref.AP_Cheque_ID = apc.AP_Cheque_ID
WHERE	apcsr.AP_Cheque_StatusReceipt_ID = @ChequeStatusReceiptID

IF @Refund_Type_ID = 1 --1: Customer
BEGIN
	SELECT	@BusinessUnitID = acc.BusinessUnitID
	FROM	Refund ref
	JOIN	QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.Instance = ref.CustomerOrderHeaderInstance
	JOIN	QSPCanadaCommon..Campaign camp
				ON	camp.ID = coh.CampaignID
	JOIN	QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
	WHERE	ref.Refund_ID = @Refund_ID
END
ELSE IF @Refund_Type_ID = 2 --2: Group
BEGIN
	SELECT	@BusinessUnitID = acc.BusinessUnitID
	FROM	Refund ref
	JOIN	QSPCanadaCommon..Campaign camp
				ON	camp.ID = ref.Campaign_ID
	JOIN	QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
	WHERE	ref.Refund_ID = @Refund_ID
END

BEGIN TRANSACTION

UPDATE	AP_Cheque
SET		AP_Cheque_Status_ID = 3 --3: Paid
WHERE	AP_Cheque_ID = @AP_Cheque_ID

/*Enable once moved to Scotia -- remove this from AddGLEntriesForAdjustment
INSERT GL_ENTRY
(
	Accounting_Year,
	Accounting_Period,
	GL_Entry_Date,
	[Description],
	Country_Code,
	Refund_ID,
	BusinessUnitID
)
SELECT
	@AccountingYear,
	@AccountingPeriod,
	@RunDate,
	'Refund Cheque (Debit) Cashed',
	'CA',
	@Refund_ID,
	@BusinessUnitID

SELECT @GL_Entry_ID = SCOPE_IDENTITY()

INSERT GL_TRANSACTION
(
	GL_Entry_ID,
	GL_Account_Number,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GL_Entry_ID,
		@GL_Account_AP,
		'D',
		@Amount,
		2

INSERT GL_TRANSACTION
(
	GL_Entry_ID,
	GL_Account_Number,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GL_Entry_ID,
		@GL_Account_Cash,
		'C',
		@Amount,
		2
*/

UPDATE	AP_Cheque_StatusReceipt
SET		AP_Cheque_StatusReceipt_Status_ID = 2 --2: Processed
WHERE	AP_Cheque_StatusReceipt_ID = @ChequeStatusReceiptID

COMMIT TRANSACTION
GO

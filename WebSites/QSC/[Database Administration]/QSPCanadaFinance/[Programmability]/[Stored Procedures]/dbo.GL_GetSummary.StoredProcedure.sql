USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_GetSummary]    Script Date: 06/07/2017 09:17:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GL_GetSummary]

	@Accounting_Year	INT,
	@Accounting_Period	INT,
	@ShowDetail			BIT,
	@ShowTimeGL			BIT = 1

AS

IF ISNULL(@ShowDetail, 0) = 0
BEGIN
	SELECT		CASE glAcc.BusinessUnitID
					WHEN 1 THEN	'00172'
					WHEN 2 THEN '00173'
					WHEN 3 THEN	'00172'
					WHEN 4 THEN '00173'
					ELSE		NULL
				END AS BusinessUnitID,
				ROW_NUMBER() OVER(ORDER BY glAcc.BusinessUnitID, e.Country_Code, glAcc.Account, glAcc.Division, glAcc.Product, glAcc.Department) AS 'JournalLineID',
				glAcc.Account,
				glAcc.Division,
				glAcc.Product,
				glAcc.Department,
				glAcc.Project,
				glAcc.Source,
				glAcc.Geographic,
				glAcc.Other,
				glAcc.Affiliate,
				CASE e.Country_Code
					WHEN 'CA' THEN	'CAD'
					ELSE			'USD'
				END Currency,
				SUM(CASE t.Debit_Credit
						WHEN 'C' THEN	t.Amount * -1
						ELSE			t.Amount
					END) AS Amount
	FROM		GL_Entry e
	JOIN		GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
	LEFT JOIN	GLAccount glAcc
					ON	glAcc.GLAccountID = t.GLAccountID
	WHERE		e.Accounting_Year = @Accounting_Year
	AND			e.Accounting_Period = @Accounting_Period
--	AND			(@ShowTimeGL = 1 AND e.BusinessUnitID IN (1, 2) OR @ShowTimeGL = 0 AND e.BusinessUnitID IN (3, 4))
	AND			(@ShowTimeGL = 1 AND e.BusinessUnitID IN (1, 2) OR @ShowTimeGL = 0)
	GROUP BY	glAcc.BusinessUnitID,
				glAcc.Account,
				glAcc.Division,
				glAcc.Product,
				glAcc.Department,
				glAcc.Project,
				glAcc.Source,
				glAcc.Geographic,
				glAcc.Other,
				glAcc.Affiliate,
				e.Country_Code
	HAVING		SUM(CASE t.Debit_Credit
						WHEN 'C' THEN	t.Amount * -1
						ELSE			t.Amount
					END) <> 0.00
	ORDER BY	glAcc.BusinessUnitID,
				e.Country_Code,
				glAcc.Account,
				glAcc.Division,
				glAcc.Product,
				glAcc.Department
END
ELSE
BEGIN
	SELECT		CASE buEntry.BusinessUnitID
					WHEN 1 THEN	'00172'
					WHEN 2 THEN '00173'
					WHEN 3 THEN	'00172'
					WHEN 4 THEN '00173'
					ELSE		NULL
				END AS ClientBusinessUnitID,
				buEntry.BusinessUnitDescription AS ClientBusinessUnit,
				dbo.UDF_GLAccount_GetAccountNumber(glAcc.GLAccountID) AS GLAccountID,
				glAcc.Description AS GLAccountDescription,
				CASE e.Country_Code
					WHEN 'CA' THEN	'CAD'
					ELSE			'USD'
				END Currency,
				CASE t.Debit_Credit
					WHEN 'C' THEN	t.Amount * -1
					ELSE			t.Amount
				END AS Amount,
				e.Invoice_ID,
				e.Payment_ID,
				e.Adjustment_ID,
				e.GL_Entry_Date,
				e.GL_Posting_Date,
				e.Description AS GLEntryDescription,
				apcRemit.ChequeNumber AS RemitQSPChequeNumber,
				pmt.Cheque_Number AS PaymentChequeNumber,
				pmt.Cheque_Date AS PaymentChequeDate,
				pmt.Cheque_Payer AS PaymentChequePayer,
				cdPaymentMethod.Description AS GroupPaymentMethod,
				accPmt.ID AS PaymentGroupID,
				accPmt.Name AS PaymentGroupName,
				pmt.PAYMENT_EFFECTIVE_DATE PaymentEffectiveDate,
				accAdj.ID AS AdjustmentGroupID,
				accAdj.Name AS AdjustmentGroupName,
				accRef.ID AS RefundGroupID,
				accRef.Name AS RefundGroupName,
				apcRef.ChequeNumber AS RefundChequeNumber,
				accInv.ID AS InvoiceGroupID,
				accInv.Name AS InvoiceGroupName,
				cdOrderQualifier.Description OrderQualifier
	FROM		GL_Entry e (NOLOCK)
	JOIN		GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
	LEFT JOIN	GLAccount glAcc
					ON	glAcc.GLAccountID = t.GLAccountID
	LEFT JOIN	QSPCanadaCommon..BusinessUnit buEntry
					ON	buEntry.BusinessUnitID = e.BusinessUnitID
	LEFT JOIN	QSPCanadaCommon..BusinessUnit buGLAcc
					ON	buGLAcc.BusinessUnitID = glAcc.BusinessUnitID
	LEFT JOIN	AP_Cheque_Remit apcr
					ON	apcr.AP_Cheque_Remit_ID = e.AP_Cheque_Remit_ID
	LEFT JOIN	AP_Cheque apcRemit
					ON	apcRemit.AP_Cheque_ID = apcr.AP_Cheque_ID
	LEFT JOIN	Payment pmt
					ON	pmt.Payment_ID = e.Payment_ID
	LEFT JOIN	QSPCanadaCommon..Codedetail cdPaymentMethod
					ON	cdPaymentMethod.Instance = pmt.Payment_Method_ID
	LEFT JOIN	QSPCanadaCommon..CAccount accPmt
					ON	accPmt.ID = pmt.Account_ID
	LEFT JOIN	Refund ref
					ON	ref.Refund_ID = e.Refund_ID
	LEFT JOIN	QSPCanadaCommon..Campaign refCamp
					ON	refCamp.ID = ref.Campaign_ID
	LEFT JOIN	QSPCanadaCommon..CAccount accRef
					ON	accRef.ID = refCamp.BillToAccountID
	LEFT JOIN	AP_Cheque apcRef
					ON	apcRef.AP_Cheque_ID = ref.AP_Cheque_ID
	LEFT JOIN	Adjustment adj
					ON	adj.Adjustment_ID = e.Adjustment_ID
	LEFT JOIN	QSPCanadaCommon..CAccount accAdj
					ON	accAdj.ID = adj.Account_ID
	LEFT JOIN	Invoice inv
					ON	inv.Invoice_ID = e.Invoice_ID
	LEFT JOIN	QSPCanadaCommon..CAccount accInv
					ON	accInv.ID = inv.Account_ID
	LEFT JOIN	QSPCanadaOrderManagement..Batch b
					ON	b.OrderID = COALESCE(inv.ORDER_ID, pmt.Order_ID, adj.Order_ID)
	LEFT JOIN	QSPCanadaCommon..Codedetail cdOrderQualifier
					ON	cdOrderQualifier.Instance = b.OrderQualifierID
	WHERE		e.Accounting_Year = @Accounting_Year
	AND			e.Accounting_Period = @Accounting_Period
--	AND			(@ShowTimeGL = 1 AND e.BusinessUnitID IN (1, 2) OR @ShowTimeGL = 0 AND e.BusinessUnitID IN (3, 4))
	AND			(@ShowTimeGL = 1 AND e.BusinessUnitID IN (1, 2) OR @ShowTimeGL = 0)
	ORDER BY	buGLAcc.BusinessUnitID,
				e.Country_Code,
				glAcc.Account,
				glAcc.Division,
				glAcc.Product,
				glAcc.Department,
				e.GL_Entry_Date
END
GO

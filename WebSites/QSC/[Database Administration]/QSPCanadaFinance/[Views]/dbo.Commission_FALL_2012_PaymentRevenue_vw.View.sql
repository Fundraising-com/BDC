USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[Commission_FALL_2012_PaymentRevenue_vw]    Script Date: 06/07/2017 09:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Commission_FALL_2012_PaymentRevenue_vw]
AS 
SELECT e.INVOICE_ID,e.PAYMENT_ID, e.ACCOUNTING_YEAR, e.ACCOUNTING_PERIOD, t.debit_credit,acc.glaccountid, acc.DESCRIPTION  
,  SUM(ISNULL((CASE t.DEBIT_CREDIT 
	WHEN 'D' THEN t.AMOUNT
	ELSE t.AMOUNT * -1
	END),0))  AS Amount
FROM GL_Entry e WITH (NOLOCK)
JOIN GL_Transaction t WITH (NOLOCK) ON	t.GL_Entry_ID = e.GL_Entry_ID
JOIN QSPCanadaCommon..BusinessUnit bu WITH (NOLOCK) ON	bu.BusinessUnitID = e.BusinessUnitID
JOIN GLAccount acc  WITH (NOLOCK) ON	acc.GLAccountID = t.GLAccountID
WHERE (Accounting_year = 2012 AND ACCOUNTING_PERIOD < 8)
AND acc.GLAccountID IN (142)
GROUP BY e.INVOICE_ID,e.PAYMENT_ID,e.ACCOUNTING_YEAR, e.ACCOUNTING_PERIOD, t.debit_credit,acc.glaccountid, acc.DESCRIPTION
GO

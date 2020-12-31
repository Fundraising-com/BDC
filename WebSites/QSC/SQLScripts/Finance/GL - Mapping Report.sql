USE QSPCanadaFinance

SELECT		tranType.Description AS TransactionType,
			NULL AS TransactionSubType,
			NULL AS TransactionSubTypeFrench,
			CASE tranType.TransactionTypeID
				WHEN 1 THEN 'C'
				ELSE		'D'
			END AS FTS_Debit_Credit,
			ISNULL(bu.BusinessUnitDescription, 'ALL') AS BusinessUnit,
			glEntryType.Description AS GLEntryType,
			prodLine.Description AS ProductLine,
			tax.Tax_Desc AS TaxType,
			cdCur.Description AS Currency,
			cdPmt.Description AS PaymentMethod,
			CASE glAccMap.Debit
				WHEN 0 THEN 'C'
				ELSE		'D'
			END AS GL_Debit_Credit,
			dbo.UDF_GLAccount_GetAccountNumber(glAcc.GLAccountID) AS GLAccountNumber,
			glAcc.Description AS GLAccountDescription
FROM		GLAccount glAcc
JOIN		GLAccountMap glAccMap
				ON	glAccMap.GLAccountID = glAcc.GLAccountID
JOIN		GLEntryType glEntryType
				ON	glEntryType.GLEntryTypeID = glAccMap.GLEntryTypeID
JOIN		TransactionType tranType
				ON	tranType.TransactionTypeID = glEntryType.TransactionTypeID
LEFT JOIN	QSPCanadaCommon..Tax tax
				ON	tax.Tax_ID = glAccMap.TaxID
LEFT JOIN	QSPCanadaCommon..QSPProductLine prodLine
				ON	prodLine.ID = glAccMap.ProductLineID
LEFT JOIN	QSPCanadaCommon..CodeDetail cdCur
				ON	cdCur.Instance = glAccMap.CurrencyID
LEFT JOIN	QSPCanadaCommon..CodeDetail cdPmt
				ON	cdPmt.Instance = glAccMap.PaymentMethodID
LEFT JOIN	QSPCanadaCommon..BusinessUnit bu
				ON	bu.BusinessUnitID = glAccMap.BusinessUnitID

UNION ALL

SELECT	'Adjustment' AS TransactionType,
		adjType.Name AS TransactionSubType,
		ISNULL(adjType.French_Name, adjType.Name) AS TransactionSubTypeFrench,
		adjType.Debit_Credit AS FTS_Debit_Credit,
		'QSP' AS BusinessUnitDescription,
		NULL AS GLEntryType,
		NULL AS ProductLine,
		NULL AS TaxType,
		NULL AS Currency,
		NULL AS PaymentMethod,
		CASE glAccAdjType.Debit
			WHEN 0 THEN 'C'
			ELSE		'D'
		END AS GL_Debit_Credit,
		dbo.UDF_GLAccount_GetAccountNumber(glAcc.GLAccountID) AS GLAccountNumber,
		glAcc.Description AS GLAccountDescription
FROM	GLAccount glAcc
JOIN	GLAccountAdjustmentType glAccAdjType
			ON	glAccAdjType.GLAccountID = glAcc.GLAccountID
JOIN	Adjustment_Type adjType
			ON	adjType.Adjustment_Type_ID = glAccAdjType.AdjustmentTypeID
ORDER BY	TransactionType,
			TransactionSubType,
			FTS_Debit_Credit,
			ISNULL(bu.BusinessUnitDescription, 'ALL'),
			GLEntryType,
			GLAccountNumber
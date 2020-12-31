SELECT		camp.FMID, fm.FirstName + ' ' + fm.LastName FMName, camp.ID CampaignID, camp.StartDate, camp.EndDate, acc.Id AccountID, acc.Name AccountName,
			CASE ISNULL(ref.Refund_ID, 0) WHEN 0 THEN 'No' ELSE 'Yes' END WasProfitChequeForced, ISNULL(SUM(sdCamp.TransactionAmount) * -1, 0) CampaignCreditBalance
FROM		Campaign camp
JOIN		FieldManager fm ON fm.FMID = camp.FMID
JOIN		CAccount acc ON acc.Id = camp.BillToAccountID
LEFT JOIN	QSPCanadaOrderManagement..Batch bMain ON bMain.CampaignID = camp.ID AND bMain.OrderQualifierID IN (39001)
--LEFT JOIN	QSPCanadaOrderManagement..Batch bOnline ON bOnline.CampaignID = camp.ID AND bOnline.OrderQualifierID IN (39009)
LEFT JOIN	QSPCanadaFinance.dbo.UDF_Statement_GetDetails_WithBusLogic('2099-12-31') sdCamp ON sdCamp.CampaignID = camp.ID
LEFT JOIN	QSPCanadaFinance..Refund ref ON ref.Campaign_ID = camp.ID
WHERE		ISNULL(camp.OnlineOnlyPrograms, 0) = 0
AND			camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31'
AND			camp.Status = 37002
AND			camp.FMID NOT IN ('0508', '0506')
AND			bMain.OrderID IS NULL
--AND			sdCamp
--AND			bOnline.OrderID IS NOT NULL
GROUP BY	camp.FMID, fm.FirstName + ' ' + fm.LastName, camp.ID, camp.StartDate, camp.EndDate, acc.Id, acc.Name, CASE ISNULL(ref.Refund_ID, 0) WHEN 0 THEN 'No' ELSE 'Yes' END
HAVING		ISNULL(SUM(sdCamp.TransactionAmount) * -1, 0) > 5.00
ORDER BY	ISNULL(SUM(sdCamp.TransactionAmount), 0)


/*
SELECT		DISTINCT camp.FMID, fm.FirstName + ' ' + fm.LastName FMName, camp.ID CampaignID, camp.StartDate, camp.EndDate, acc.Id AccountID, acc.Name AccountName,
			CASE ISNULL(ref.Refund_ID, 0) WHEN 0 THEN 'No' ELSE 'Yes' END WasProfitChequeForced, QSPCanadaFinance.dbo.UDF_Account_GetBalance(camp.ID, acc.ID, '2099-12-31') CampaignBalanceOwing
FROM		Campaign camp
JOIN		FieldManager fm ON fm.FMID = camp.FMID
JOIN		CAccount acc ON acc.Id = camp.BillToAccountID
LEFT JOIN	QSPCanadaOrderManagement..Batch bMain ON bMain.CampaignID = camp.ID AND bMain.OrderQualifierID IN (39001)
--LEFT JOIN	QSPCanadaOrderManagement..Batch bOnline ON bOnline.CampaignID = camp.ID AND bOnline.OrderQualifierID IN (39009)
LEFT JOIN	QSPCanadaFinance..Refund ref ON ref.Campaign_ID = camp.ID
WHERE		ISNULL(camp.OnlineOnlyPrograms, 0) = 0
AND			camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31'
AND			camp.Status = 37002
AND			camp.FMID NOT IN ('0508', '0506')
AND			bMain.OrderID IS NULL
AND			QSPCanadaFinance.dbo.UDF_Account_GetBalance(camp.ID, acc.ID, '2099-12-31') > 5.00
--AND			bOnline.OrderID IS NOT NULL
ORDER BY	camp.StartDate

SELECT		*
FROM		Campaign camp
JOIN		QSPCanadaOrderManagement..Batch b ON b.CampaignID = camp.ID AND b.OrderQualifierID IN (39001) and b.Date >= DATEADD(dd, 100, camp.StartDate)
WHERE		ISNULL(camp.OnlineOnlyPrograms, 0) = 0
AND			camp.StartDate >= '2014-07-01'
AND			b.OrderID IS NOT NULL
*/
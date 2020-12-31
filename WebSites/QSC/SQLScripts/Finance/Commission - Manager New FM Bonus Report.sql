SELECT		dm.FirstName DMFirstName, dm.LastName DMLastName, fm.FirstName FMFirstName, fm.LastName FMLastName,
			acc.ID AccountID, acc.Name, campCurrent.ID CampaignID, campCurrent.StartDate CampaignStartDate, campCurrent.EndDate CampaignEndDate, SUM(v.NetSale) NetSale, SUM(v.NetSale) * 0.04 ManagerPayment
FROM		Campaign campCurrent
JOIN		CAccount acc ON acc.ID = campCurrent.BillToAccountID
JOIN		FieldManager fm ON fm.FMID = campCurrent.FMID
JOIN		FieldManager dm ON dm.FMID = fm.DMID
JOIN		QSPCanadaFinance..vw_GetNetForReporting v ON v.CampaignID = campCurrent.ID
WHERE		v.invoice_date BETWEEN '2017-07-01' AND '2018-06-30'
AND			campCurrent.BillToAccountID NOT IN
(
	SELECT		campLastYear.BillToAccountID
	FROM		Campaign campLastYear 
	WHERE		campLastYear.StartDate BETWEEN '2016-07-01' AND '2017-06-30'
	AND			campLastYear.Status = 37002
)
AND			fm.FMID IN ('1571','1572','1573')
GROUP BY	dm.FirstName,
			dm.LastName,
			fm.FirstName,
			fm.LastName,
			acc.ID,
			acc.Name,
			campCurrent.ID,
			campCurrent.StartDate,
			campCurrent.EndDate

UNION ALL

SELECT		dm.FirstName DMFirstName, dm.LastName DMLastName, fm.FirstName FMFirstName, fm.LastName FMLastName,
			acc.ID AccountID, acc.Name, campCurrent.ID CampaignID, campCurrent.StartDate CampaignStartDate, campCurrent.EndDate CampaignEndDate, SUM(v.NetSale) NetSale, SUM(v.NetSale) * 0.04 ManagerPayment
FROM		Campaign campCurrent
JOIN		CAccount acc ON acc.ID = campCurrent.BillToAccountID
JOIN		FieldManager fm ON fm.FMID = campCurrent.FMID
JOIN		FieldManager dm ON dm.FMID = fm.DMID
JOIN		QSPCanadaFinance..vw_GetNetForReporting v ON v.CampaignID = campCurrent.ID
WHERE		v.invoice_date BETWEEN '2017-07-01' AND '2018-12-31'
AND			campCurrent.BillToAccountID NOT IN
(
	SELECT		campLastYear.BillToAccountID
	FROM		Campaign campLastYear 
	WHERE		campLastYear.StartDate BETWEEN '2016-07-01' AND '2017-06-30'
	AND			campLastYear.Status = 37002
)
AND			fm.FMID IN ('1569','1570')
GROUP BY	dm.FirstName,
			dm.LastName,
			fm.FirstName,
			fm.LastName,
			acc.ID,
			acc.Name,
			campCurrent.ID,
			campCurrent.StartDate,
			campCurrent.EndDate
ORDER BY	dm.FirstName,
			fm.FirstName,
			fm.LastName,
			acc.ID,
			campCurrent.ID

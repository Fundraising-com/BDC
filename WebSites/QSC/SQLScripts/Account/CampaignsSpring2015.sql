SELECT		campCurrent.ID CampaignID, campCurrent.BillToAccountID AccountID, acc.Name AccountName, fm.FirstName + ' ' + fm.LastName FMName,
			campCurrent.StartDate CampaignStartDate, campCurrent.EndDate CampaignEndDate, campCurrent.DateSubmitted CampaignCreationDate
FROM		Campaign campCurrent WITH (NOLOCK)
JOIN		FieldManager fm WITH (NOLOCK) ON fm.FMID = campCurrent.FMID
JOIN		CAccount acc ON acc.ID = campCurrent.BillToAccountID
WHERE		campCurrent.StartDate BETWEEN '2015-01-01' AND '2015-06-30'
AND			campCurrent.Status = 37002
AND			campCurrent.ID IN (SELECT CampaignID FROM QSPCanadaOrderManagement..Batch WHERE OrderQualifierID IN (39001, 39002, 39009))
ORDER BY	fm.FirstName + ' ' + fm.LastName,
			campCurrent.DateSubmitted
			
			
SELECT		fm.FirstName + ' ' + fm.LastName FMName,
			COUNT(campCurrent.ID) NumberOfSpring2015Campaigns,
			SUM(CASE WHEN campCurrent.DateSubmitted < '2015-01-01' THEN 1 ELSE 0 END) NumberOfSpring2015CampaignsEnteredInFFSIn2014
FROM		Campaign campCurrent WITH (NOLOCK)
JOIN		FieldManager fm WITH (NOLOCK) ON fm.FMID = campCurrent.FMID
JOIN		CAccount acc ON acc.ID = campCurrent.BillToAccountID
WHERE		campCurrent.StartDate BETWEEN '2015-01-01' AND '2015-06-30'
AND			campCurrent.Status = 37002
AND			campCurrent.ID IN (SELECT CampaignID FROM QSPCanadaOrderManagement..Batch WHERE OrderQualifierID IN (39001, 39002, 39009))
GROUP BY	fm.FirstName + ' ' + fm.LastName
ORDER BY	fm.FirstName + ' ' + fm.LastName


SELECT		fm.FirstName + ' ' + fm.LastName FMName,
			COUNT(campCurrent.ID) NumberOfSpring2016Campaigns
FROM		Campaign campCurrent WITH (NOLOCK)
JOIN		FieldManager fm WITH (NOLOCK) ON fm.FMID = campCurrent.FMID
JOIN		CAccount acc ON acc.ID = campCurrent.BillToAccountID
WHERE		campCurrent.StartDate BETWEEN '2016-01-01' AND '2016-06-30'
AND			campCurrent.Status = 37002
GROUP BY	fm.FirstName + ' ' + fm.LastName
ORDER BY	fm.FirstName + ' ' + fm.LastName
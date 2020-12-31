USE QSPCanadaCommon
GO

SELECT		fm.Firstname + ' ' + fm.LastName FM, camp.ID CampaignID, camp.BillToAccountID AccountID,  COUNT(*) NumberGiftPrograms
FROM		Campaign camp
JOIN		CampaignProgram cp ON cp.CampaignID = camp.ID
JOIN		FieldManager fm ON fm.FMID = camp.FMID
WHERE		camp.StartDate >= '2017-07-01'
AND			camp.Status = 37002
AND			cp.ProgramID IN (53, 55, 59)
AND			cp.DeletedTF = 0
AND			cp.OnlineOnly = 0
AND			camp.OnlineOnlyPrograms = 0
AND			ISNULL(camp.Notes,'') NOT LIKE '%EXCEPTION%'
GROUP BY	fm.Firstname + ' ' + fm.LastName,
			camp.ID,
			camp.BillToAccountID
HAVING		COUNT(*) > 2
ORDER BY	fm.Firstname + ' ' + fm.LastName,
			camp.BillToAccountID,
			camp.ID

SELECT		acc.ID AccountID, COUNT(*) NumberGiftPrograms
FROM		Campaign camp
JOIN		CampaignProgram cp ON cp.CampaignID = camp.ID
JOIN		CAccount acc ON acc.ID = camp.BillToAccountID
WHERE		camp.StartDate >= '2017-07-01'
AND			camp.Status = 37002
AND			cp.ProgramID IN (53, 55, 59)
AND			cp.DeletedTF = 0
AND			cp.OnlineOnly = 0
AND			camp.OnlineOnlyPrograms = 0
AND			ISNULL(camp.Notes,'') NOT LIKE '%EXCEPTION%'
GROUP BY	acc.ID
HAVING		COUNT(*) > 2
ORDER BY	acc.ID
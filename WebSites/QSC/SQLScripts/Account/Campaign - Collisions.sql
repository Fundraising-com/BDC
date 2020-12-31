USE [QSPCanadaCommon]
GO

SELECT		fm.FirstName + ' ' + fm.LastName FM,
			camp1.BillToAccountID AccountID,
			camp1.ID Campaign1ID,
			camp2.ID Campaign2ID,
			camp1.StartDate Campaign1StartDate,
			camp1.EndDate Campaign1EndDate,
			camp2.StartDate Campaign2StartDate,
			camp2.EndDate Campaign2EndDate
FROM		Campaign camp1
JOIN		Campaign camp2 ON camp2.BillToAccountID = camp1.BillToAccountID
JOIN		Season seasCurrent ON GETDATE() BETWEEN seasCurrent.StartDate AND seasCurrent.EndDate AND seasCurrent.Season IN ('S', 'F')
JOIN		FieldManager fm ON fm.FMID = camp1.FMID
WHERE		camp2.ID > camp1.ID
AND			camp1.Status = 37002
AND			camp2.Status = 37002
AND			camp1.ID IN (SELECT CampaignID FROM CampaignProgram WHERE ProgramID IN (1, 2, 50, 52, 54, 54))
AND			camp2.ID IN (SELECT CampaignID FROM CampaignProgram WHERE ProgramID IN (1, 2, 50, 52, 54, 54))
AND			camp1.StartDate BETWEEN seasCurrent.StartDate AND seasCurrent.EndDate
AND			camp2.StartDate BETWEEN seasCurrent.StartDate AND seasCurrent.EndDate
ORDER BY	camp1.BillToAccountID
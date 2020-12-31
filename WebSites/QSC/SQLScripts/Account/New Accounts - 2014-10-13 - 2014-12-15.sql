SELECT		DISTINCT campCurrent.ID CampaignID, campCurrent.BillToAccountID AccountID, fm.FirstName + ' ' + fm.LastName FMName, campCurrent.StartDate NewCampaignStartDate, campCurrent.EndDate NewCampaignEndDate, campCurrent.DateSubmitted NewCampaignCreationDate
FROM		Campaign campCurrent
JOIN		FieldManager fm ON fm.FMID = campCurrent.FMID
WHERE		campCurrent.DateSubmitted BETWEEN '2014-10-13' AND '2014-12-15'
AND			campCurrent.StartDate BETWEEN '2014-07-01' AND '2014-12-31'
AND			campCurrent.Status = 37002
AND			campCurrent.BillToAccountID NOT IN
(
	SELECT		campCurrent.BillToAccountID
	FROM		Campaign campCurrent
	--JOIN		CampaignProgram cpCurrent ON cpCurrent.CampaignID = campCurrent.ID AND cpCurrent.DeletedTF = 0
	JOIN		FieldManager fm ON fm.FMID = campCurrent.FMID
	JOIN		Campaign campLastYear ON campLastYear.BillToAccountID = campCurrent.BillToAccountID AND campLastYear.Status = 37002 AND campLastYear.StartDate BETWEEN '2013-07-01' AND '2013-12-31'
	--JOIN		CampaignProgram cpLastYear ON cpLastYear.CampaignID = campLastYear.ID AND cpLastYear.DeletedTF = 0 AND (cpLastYear.ProgramID = cpCurrent.ProgramID OR (cpLastYear.ProgramID=1 and cpCurrent.ProgramID=2 or cpLastYear.ProgramID=2 and cpCurrent.ProgramID=1)) AND cpLastYear.ProgramID IN (1, 2, 44, 50, 52, 53, 54)
	WHERE		campCurrent.DateSubmitted BETWEEN '2014-10-13' AND '2014-12-15'
	AND			campCurrent.StartDate BETWEEN '2014-07-01' AND '2014-12-31'
	AND			campCurrent.Status = 37002
)
ORDER BY	FMName

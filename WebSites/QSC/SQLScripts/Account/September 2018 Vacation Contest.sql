USE QSPCanadaCommon
GO

SELECT		fm.FirstName FMFirstName, fm.LastName FMLastName, COUNT(*) NumberOfContracts, SUM(CASE WHEN cpSav.CampaignID IS NOT NULL OR cpLab.CampaignID IS NOT NULL THEN 1 ELSE 0 END) NumberOfSavingsContracts
INTO		#c
FROM		Campaign camp
JOIN        CAccount acc ON acc.Id = camp.BillToAccountID
JOIN		FieldManager fm ON fm.FMID = camp.FMID
LEFT JOIN	CampaignProgram cpSav ON cpSav.CampaignID = camp.ID AND cpSav.DeletedTF = 0 AND cpSav.ProgramID IN (64) AND cpSav.OnlineOnly = 0
LEFT JOIN	CampaignProgram cpLab ON cpLab.CampaignID = camp.ID AND cpLab.DeletedTF = 0 AND cpLab.ProgramID IN (72) AND cpSav.OnlineOnly = 0
WHERE		camp.Status = 37002
AND			camp.StartDate BETWEEN '2018-07-01' AND '2018-12-31'
AND			camp.ApprovedStatusDate >= '2018-08-27'
AND			acc.CAccountCodeClass NOT IN ('FM')
GROUP BY	fm.FirstName, fm.LastName

SELECT		*, CASE WHEN NumberOfContracts < 10 THEN 0
					WHEN NumberOfContracts BETWEEN 10 AND 19 THEN 1 
					WHEN NumberOfContracts >= 20 THEN (NumberOfContracts - 8)
				END 
				+
				CASE WHEN NumberOfContracts >= 10 THEN NumberOfSavingsContracts ELSE 0 END
				NumberOfEntries
FROM		#c
ORDER BY	FMLastName

DROP TABLE #c
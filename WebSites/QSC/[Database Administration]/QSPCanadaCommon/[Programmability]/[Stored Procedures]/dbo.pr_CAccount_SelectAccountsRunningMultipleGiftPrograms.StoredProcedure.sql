USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CAccount_SelectAccountsRunningMultipleGiftPrograms]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CAccount_SelectAccountsRunningMultipleGiftPrograms]

AS

SELECT		fm.Firstname + ' ' + fm.LastName FM, acc.ID AccountID, seas.Name Season, COUNT(*) NumberGiftPrograms
FROM		Campaign camp
JOIN		CampaignProgram cp ON cp.CampaignID = camp.ID
JOIN		FieldManager fm ON fm.FMID = camp.FMID
JOIN		CAccount acc ON acc.ID = camp.BillToAccountID
JOIN		Season seas ON camp.StartDate BETWEEN seas.StartDate AND seas.EndDate AND seas.Season IN ('F','S')
WHERE		camp.StartDate >= '2017-07-01'
AND			camp.Status = 37002
AND			cp.ProgramID IN (53, 55, 59, 67, 68, 69)
AND			cp.DeletedTF = 0
AND			cp.OnlineOnly = 0
AND			camp.OnlineOnlyPrograms = 0
AND			ISNULL(camp.Notes,'') NOT LIKE '%EXCEPTION%'
GROUP BY	fm.Firstname + ' ' + fm.LastName,
			acc.ID,
			seas.Name
HAVING		COUNT(*) > 2
ORDER BY	fm.Firstname + ' ' + fm.LastName,
			acc.ID
GO

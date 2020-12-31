USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectGift]    Script Date: 06/07/2017 09:33:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Campaign_SelectGift]

AS

SELECT		fm.FirstName + ' ' + fm.LastName FMLastName,
			acc.ID AccountID,
			acc.Name AccountName,
			c.ID CampaignID,
			p.Name Program,
			c.StartDate CampaignStartDate,
			c.EndDate CampaignEndDate,
			CASE WHEN c.OnlineOnlyPrograms = 1 OR cp.OnlineOnly = 1 THEN 'Yes' ELSE 'No' End OnlineOnly,
			c.NumberOfParticipants,
			isnull((select top 1 'Yes' from qspcanadaordermanagement..Batch b where orderqualifierid in (39001,39002) and b.CampaignID = c.ID), 'No') LandedOrderInFFS
FROM		Campaign c
JOIN		CAccount acc on acc.id = c.billtoaccountid
JOIN		CampaignProgram cp on cp.CampaignID = c.ID and cp.DeletedTF = 0
JOIN		FieldManager fm on fm.fmid = c.fmid
JOIN		Program p on p.id = cp.programid
JOIN		Season s on c.StartDate BETWEEN s.StartDate AND s.EndDate and s.Season IN ('S','F')
WHERE		c.Status = 37002
AND			cp.programID in (1, 2, 9, 23, 39, 42, 44, 53, 54, 55, 56, 58, 59, 60, 61, 62, 65, 66, 67, 69, 70)
AND			cp.deletedtf = 0
AND			GETDATE() BETWEEN s.StartDate AND s.EndDate
ORDER BY	cp.ProgramID,
			isnull((select top 1 'Yes' from qspcanadaordermanagement..Batch b where orderqualifierid in (39001,39002) and b.CampaignID = c.ID), 'No')
GO

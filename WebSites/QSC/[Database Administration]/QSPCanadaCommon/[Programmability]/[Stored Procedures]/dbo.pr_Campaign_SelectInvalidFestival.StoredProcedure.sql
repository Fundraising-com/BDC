USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectInvalidFestival]    Script Date: 06/07/2017 09:33:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Campaign_SelectInvalidFestival]

AS

SELECT		campCurrent.ID NewCampaignID, campLastYear.ID OldCampaignID, campCurrent.billtoaccountid AccountID, fm.FirstName + ' ' + fm.LastName FMName, campCurrent.StartDate NewCampaignStartDate, campCurrent.EndDate NewCampaignEndDate, campLastYear.StartDate OldCampaignStartDate, campLastYear.EndDate OldCampaignEndDate
FROM		Campaign campCurrent
JOIN		CampaignProgram cpMag ON cpMag.CampaignID = campCurrent.ID AND cpMag.DeletedTF = 0 AND cpMag.ProgramID IN (1, 2)
JOIN		CampaignProgram cpFestival ON cpFestival.CampaignID = campCurrent.ID AND cpFestival.DeletedTF = 0 AND cpFestival.ProgramID IN (54)
JOIN		Campaign campLastYear ON campLastYear.BillToAccountID = campCurrent.BillToAccountID AND campLastYear.Status = 37002 AND campLastYear.StartDate < campCurrent.StartDate AND campLastYear.EndDate > DATEADD(yy, -1, campCurrent.StartDate)
JOIN		FieldManager fm ON fm.FMID = campCurrent.FMID
WHERE		campCurrent.ID NOT IN (100697)
GO

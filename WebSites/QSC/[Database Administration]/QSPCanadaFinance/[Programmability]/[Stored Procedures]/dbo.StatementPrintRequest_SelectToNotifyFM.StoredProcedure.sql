USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementPrintRequest_SelectToNotifyFM]    Script Date: 06/07/2017 09:17:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementPrintRequest_SelectToNotifyFM]

AS

SELECT		fm.FMID,
			fm.Email
FROM		StatementPrintRequest spr
JOIN		[Statement] stat
				ON	stat.StatementID = spr.StatementID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = stat.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
WHERE		spr.FMNotificationDate IS NULL
AND			ISNULL(fm.Email, '') <> ''
GROUP BY	fm.FMID,
			fm.Email
GO

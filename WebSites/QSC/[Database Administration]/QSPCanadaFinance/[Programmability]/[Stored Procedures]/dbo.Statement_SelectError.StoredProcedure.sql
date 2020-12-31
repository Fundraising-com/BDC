USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Statement_SelectError]    Script Date: 06/07/2017 09:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Statement_SelectError]

AS

SELECT		se.StatementErrorID,
			se.CreationDate AS Date,
			se.CampaignID,
			acc.ID AS AccountID,
			acc.Name AS AccountName,
			fm.FMID,
			fm.Firstname,
			fm.Lastname,
			[set].Error
FROM		StatementError se
JOIN		StatementErrorType [set]
				ON	[set].StatementErrorTypeID = se.StatementErrorTypeID
LEFT JOIN	QSPCanadaCommon..Campaign camp
				ON	camp.ID = se.CampaignID
LEFT JOIN	QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
LEFT JOIN	QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
WHERE		se.IsFixed = 0
AND			se.IsReviewed = 0
AND			NOT EXISTS (SELECT	TOP 1
								se2.StatementErrorID
						FROM	StatementError se2
						WHERE	se2.CampaignID = se.CampaignID
						AND		se2.StatementErrorID > se.StatementErrorID)
GO

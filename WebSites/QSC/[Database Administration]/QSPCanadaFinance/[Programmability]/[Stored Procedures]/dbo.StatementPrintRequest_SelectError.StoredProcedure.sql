USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementPrintRequest_SelectError]    Script Date: 06/07/2017 09:17:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementPrintRequest_SelectError]

AS

SELECT		spre.StatementPrintRequestErrorID,
			spre.CreationDate AS Date,
			spre.StatementID,
			stat.CampaignID,
			[spret].Error
FROM		StatementPrintRequestError spre
JOIN		StatementPrintRequestErrorType [spret]
				ON	[spret].StatementPrintRequestErrorTypeID = spre.StatementPrintRequestErrorTypeID
JOIN		[Statement] stat
				ON	stat.StatementID = spre.StatementID
WHERE		spre.IsFixed = 0
AND			spre.IsReviewed = 0
GO

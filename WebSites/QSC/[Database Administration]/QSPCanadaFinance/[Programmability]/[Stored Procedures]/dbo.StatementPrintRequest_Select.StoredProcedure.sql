USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementPrintRequest_Select]    Script Date: 06/07/2017 09:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementPrintRequest_Select]

	@StatementPrintRequestBatchID INT

AS

SELECT		stat.CampaignID,
			RIGHT('00000000' + CONVERT(VARCHAR(MAX), 1000000000 - CONVERT(INT, stat.Balance * 100)), 9) + '-' + CONVERT(VARCHAR(MAX), stat.CampaignID) + '.pdf' AS [Filename]
FROM		[Statement] stat
JOIN		StatementPrintRequest spr
			ON	spr.StatementID = stat.StatementID
JOIN		StatementPrintRequestBatch sprb
			ON	sprb.StatementPrintRequestBatchID = spr.StatementPrintRequestBatchID
WHERE		sprb.StatementPrintRequestBatchID = @StatementPrintRequestBatchID
ORDER BY	stat.Balance
GO

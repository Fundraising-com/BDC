USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatch_SelectNonprinted]    Script Date: 06/07/2017 09:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_LetterBatch_SelectNonprinted]

AS

DECLARE	@CurrentFYStartDate	DATETIME
SELECT	@CurrentFYStartDate = seas.StartDate
FROM	QSPCanadaCommon..Season seas
WHERE	GETDATE() BETWEEN seas.StartDate AND seas.EndDate
AND		seas.Season IN ('Y')

DECLARE	@ToDate		DATETIME
SET @ToDate = DATEADD(DAY, -7, GETDATE())
PRINT @ToDate

SELECT		lt.Name AS LetterTemplate,
			imlb.ProductCode,
			cdlbt.Description AS LetterTemplateType,
			lb.DateFrom,
			lb.DateTo,
			lb.RunID,
			lb.DateCreated
FROM		LetterBatch lb
JOIN		LetterTemplate lt
				ON	lt.ID = lb.LetterTemplateID
JOIN		QSPCanadaCommon..CodeDetail cdlbt
				ON	cdlbt.Instance = lb.LetterBatchType
LEFT JOIN	InactiveMagazineLetterBatch imlb
				ON	imlb.LetterBatchID = lb.ID
WHERE		lb.IsPrinted = 0
AND			lb.DeletedTF <> 1
AND			lb.DateCreated >= @CurrentFYStartDate
AND			lb.DateCreated <= @ToDate
GO

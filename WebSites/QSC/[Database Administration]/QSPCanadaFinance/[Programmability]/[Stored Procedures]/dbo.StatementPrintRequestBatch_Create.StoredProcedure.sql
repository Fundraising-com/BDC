USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementPrintRequestBatch_Create]    Script Date: 06/07/2017 09:17:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementPrintRequestBatch_Create]

	@StatementPrintRequestBatchID	INT OUTPUT

AS

DECLARE @StatementPrintRequestsAvailable BIT

SELECT	@StatementPrintRequestsAvailable = CONVERT(BIT, COUNT(*))
FROM	StatementPrintRequest
WHERE	ISNULL(StatementPrintRequestBatchID, 0) = 0

--If no StatementPrintRequests available then don't create a batch
IF @StatementPrintRequestsAvailable = CONVERT(BIT, 0)
BEGIN
	SET @StatementPrintRequestBatchID = 0
	RETURN
END

BEGIN TRANSACTION

INSERT INTO StatementPrintRequestBatch
(
	CreationDate
)
SELECT	GETDATE()

SET @StatementPrintRequestBatchID = SCOPE_IDENTITY()

UPDATE	StatementPrintRequest
SET		StatementPrintRequestBatchID = @StatementPrintRequestBatchID
WHERE	ISNULL(StatementPrintRequestBatchID, 0) = 0

COMMIT TRANSACTION
GO

USE QSPCanadaOrderManagement

/*SELECT	Product_Code,
		RemitCode,*
FROM	QSPCanadaProduct..Product
WHERE	Product_Sort_Name LIKE 'rolling%'
AND		Product_Year = 2019
AND		Product_Season = 'F'*/

DECLARE @ProductCode	VARCHAR(15),
		@RunID			INT
SET @ProductCode = '1487'
SET @RunID = 1632

SELECT	codrh.TitleCode, *
FROM	CustomerOrderDetailRemitHistory codrh
JOIN	RemitBatch rb
			ON	rb.ID = codrh.RemitBatchID
WHERE	codrh.RemitCode = @ProductCode
AND		rb.RunID = @RunID

EXEC	[dbo].[pr_Remit_ReRemitSubsByProductandRemitBatchID]
		@Product_Code = @ProductCode,
		@RunIDFrom = @RunID,
		@RunIDTo = @RunID,
		@AlreadyRemittedToPublisher = 1,
		@ReRemitSubs = 1,
		@RemitInactiveMagSubs = 1,
		@ReRemitCancels = 1,
		@ReRemitChadds = 1

SELECT	codrh.TitleCode, *
FROM	CustomerOrderDetailRemitHistory codrh
JOIN	RemitBatch rb
			ON	rb.ID = codrh.RemitBatchID
WHERE	codrh.RemitCode = @ProductCode
AND		rb.RunID = @RunID

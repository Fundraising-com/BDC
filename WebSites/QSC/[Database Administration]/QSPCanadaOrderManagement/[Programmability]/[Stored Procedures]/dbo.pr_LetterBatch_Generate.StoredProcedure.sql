USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatch_Generate]    Script Date: 06/07/2017 09:20:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benoit Nadon
-- Create date: 10/05/2006
-- Description:	Generates a letter batch
-- =============================================
CREATE PROCEDURE [dbo].[pr_LetterBatch_Generate]

	@iLetterTemplateID				int,
	@iLetterBatchType				int,
	@iRunID							int,
	@dDateFrom						datetime,
	@dDateTo						datetime,
	@iCustomerOrderHeaderInstance	int,
	@iTransID						int,
	@bIsLocked						bit,
	@iUserID						int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE		@iLetterBatchID		int,
				@sqlStatement		nvarchar(4000)

	INSERT INTO	LetterBatch
				(LetterTemplateID,
				LetterBatchType,
				DateFrom,
				DateTo,
				RunID,
				IsPrinted,
				DatePrinted,
				IsLocked,
				UserIDCreated,
				DateCreated,
				DeletedTF)
	VALUES		(@iLetterTemplateID,
				@iLetterBatchType,
				@dDateFrom,
				@dDateTo,
				@iRunID,
				0,
				NULL,
				@bIsLocked,
				@iUserID,
				getdate(),
				0)

	SELECT		@iLetterBatchID = SCOPE_IDENTITY()

	EXEC dbo.pr_LetterBatch_GetUnprocessedQuery @iLetterTemplateID, @iRunID, @dDateFrom, @dDateTo, @iCustomerOrderHeaderInstance, @iTransID, @sqlStatement OUTPUT

	SET @sqlStatement =
	'SELECT		' + CONVERT(nvarchar, @iLetterBatchID) + ' AS LetterBatchID,
				vw.CustomerOrderHeaderInstance,
				vw.TransID
	' + @sqlStatement

	INSERT INTO	LetterBatchCustomerOrderDetail
	EXEC		sp_executesql @sqlStatement

	SELECT		@iLetterBatchID

	SET NOCOUNT OFF;
END
GO

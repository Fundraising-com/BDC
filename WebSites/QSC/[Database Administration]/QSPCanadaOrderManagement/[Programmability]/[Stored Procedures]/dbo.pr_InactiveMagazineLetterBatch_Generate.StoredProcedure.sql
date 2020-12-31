USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_InactiveMagazineLetterBatch_Generate]    Script Date: 06/07/2017 09:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jeff Miles
-- Create date: 02/23/2007
-- Description:	Generates an Inactive Magazine letter batch
-- =============================================
CREATE PROCEDURE [dbo].[pr_InactiveMagazineLetterBatch_Generate]

	@iLetterTemplateID				int,
	@iLetterBatchType				int,
	@iRunID							int,
	@dDateFrom						datetime,
	@dDateTo						datetime,
	@iCustomerOrderHeaderInstance	int,
	@iTransID						int,
	@bIsLocked						bit,
	@iUserID						int,
	@sProductCode					varchar(50),
	@iReason						int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE		@iLetterBatchID		int,
				@ExistingLetterBatchID int, -- Don't create new batch if type is cust. service, and an unlocked nondeleted one already exits for the same product
				@sqlParameterDefinition		nvarchar(4000),
				@sqlStatement		nvarchar(4000)

	SET @ExistingLetterBatchID = 0 
	
	SELECT TOP 1	@ExistingLetterBatchID = ISNULL(MAX(lb.ID),0)
	FROM			LetterBatch lb
	JOIN			InactiveMagazineLetterBatch imlb
						ON	imlb.LetterBatchID = lb.ID
	JOIN			LetterTemplate lt
						ON	lt.ID = lb.LetterTemplateID
	WHERE			lb.IsPrinted = 0--lb.isLocked = 0
	AND				lb.DeletedTF = 0
	AND				imlb.ProductCode  = @sProductCode
	AND				lt.ID = @iLetterTemplateID

	IF @ExistingLetterBatchID > 0 AND @iLetterBatchType = 67003
	BEGIN
		SET	@iLetterBatchID = @ExistingLetterBatchID
	END

	IF @ExistingLetterBatchID = 0 OR @iLetterBatchType <> 67003
	BEGIN
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

		INSERT INTO	InactiveMagazineLetterBatch
					(LetterBatchID,
					ProductCode,
					Reason)
		VALUES		(@iLetterBatchID,
					@sProductCode,
					@iReason)
	END

	EXEC dbo.pr_InactiveMagazineLetterBatch_GetUnprocessedQuery @iLetterTemplateID, @iRunID, @dDateFrom, @dDateTo, @iCustomerOrderHeaderInstance, @iTransID, @sProductCode,	@iReason, @sqlStatement OUTPUT, @sqlParameterDefinition OUTPUT

	SET @sqlStatement =
	'SELECT		' + CONVERT(nvarchar, @iLetterBatchID) + ' AS LetterBatchID,
				vw.CustomerOrderHeaderInstance,
				vw.TransID
	' + @sqlStatement

	INSERT INTO	LetterBatchCustomerOrderDetail
	EXEC		sp_executesql @sqlStatement, @sqlParameterDefinition,
	  @iLetterTemplateID = @iLetterTemplateID,
	  @iRunID = @iRunID,
	  @dDateFrom = @dDateFrom,
	  @dDateTo = @dDateTo,
	  @iCustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance,
	  @iTransID = @iTransID,
	  @sProductCode = @sProductCode,
	  @iReason = @iReason

	SELECT		@iLetterBatchID

	SET NOCOUNT OFF;
END
GO

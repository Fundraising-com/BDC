USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatch_SelectAll]    Script Date: 06/07/2017 09:20:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benoit Nadon
-- Create date: 10/09/2006
-- Description:	Gets all Letter Batches
-- =============================================
CREATE PROCEDURE [dbo].[pr_LetterBatch_SelectAll]

	@iLetterTemplateID		int			= null,
	@dDateCreatedFrom		datetime	= null,
	@dDateCreatedTo			datetime	= null,
	@iLetterBatchType		int			= null,
	@dDateFrom				datetime	= null,
	@dDateTo				datetime	= null,
	@iRunIDFrom				int			= null,
	@iRunIDTo				int			= null,
	@bIsPrinted				bit			= null,
	@bIsLocked				bit			= null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE		@sqlParameterDefinition		nvarchar(4000),
				@sqlStatement				nvarchar(4000)

	SELECT		@sqlParameterDefinition = '@iLetterTemplateID		int,
				@dDateCreatedFrom		datetime,
				@dDateCreatedTo			datetime,
				@iLetterBatchType		int,
				@dDateFrom				datetime,
				@dDateTo				datetime,
				@iRunIDFrom				int,
				@iRunIDTo				int,
				@bIsPrinted				bit,
				@bIsLocked				bit',

				@sqlStatement = 
	'SELECT		lb.ID,
				lb.LetterTemplateID,
				lt.Name AS LetterTemplateName,
				lb.LetterBatchType,
				cdlbt.Description AS LetterBatchTypeDescription,
				lb.DateFrom,
				lb.DateTo,
				lb.RunID,
				lb.IsPrinted,
				lb.DatePrinted,
				lb.IsLocked,
				lb.UserIDCreated,
				cup.FirstName + '' '' + cup.LastName AS UserNameCreated,
				lb.DateCreated,
				lb.DeletedTF,
				lt.ReportName,
				lbcod.lbcodCount AS SubscriptionCount
	FROM		LetterBatch lb
	JOIN		LetterTemplate lt
					ON	lt.ID = lb.LetterTemplateID '

	IF(ISNULL(@iLetterTemplateID, -1) <> -1)
	BEGIN
		SET		@sqlStatement = @sqlStatement + ' AND	lt.ID = @iLetterTemplateID '
	END

	SET			@sqlStatement = @sqlStatement + '
	JOIN		QSPCanadaCommon..CodeDetail cdlbt
					ON	cdlbt.Instance = lb.LetterBatchType
	JOIN		QSPCanadaCommon..CUserProfile cup
					ON	cup.Instance = lb.UserIDCreated
	JOIN		(SELECT		lbcod.LetterBatchID,
							COUNT(*) AS lbcodCount
				FROM		LetterBatchCustomerOrderDetail lbcod
				GROUP BY	lbcod.LetterBatchID) lbcod
					ON	lbcod.LetterBatchID = lb.ID
	WHERE		lb.DeletedTF = 0 '

	IF(ISNULL(@dDateCreatedFrom, '1995-01-01') <> '1995-01-01' AND ISNULL(@dDateCreatedTo, '1995-01-01') <> '1995-01-01')
	BEGIN
		SET		@sqlStatement = @sqlStatement + ' AND lb.DateCreated BETWEEN @dDateCreatedFrom AND @dDateCreatedTo '
	END

	IF(ISNULL(@iLetterBatchType, -1) <> -1)
	BEGIN
		SET		@sqlStatement = @sqlStatement + ' AND lb.LetterBatchType = @iLetterBatchType '
	END

	IF(ISNULL(@dDateFrom, '1995-01-01') <> '1995-01-01' AND ISNULL(@dDateTo, '1995-01-01') <> '1995-01-01')
	BEGIN
		SET		@sqlStatement = @sqlStatement + ' AND (lb.DateFrom BETWEEN @dDateFrom AND @dDateTo OR lb.DateTo BETWEEN @dDateFrom AND @dDateTo) '
	END

	IF(ISNULL(@iRunIDFrom, -1) <> -1 AND ISNULL(@iRunIDTo, -1) <> -1)
	BEGIN
		SET		@sqlStatement = @sqlStatement + ' AND lb.RunID BETWEEN @iRunIDFrom AND @iRunIDTo '
	END

	IF(@bIsPrinted IS NOT NULL)
	BEGIN
		SET		@sqlStatement = @sqlStatement + ' AND lb.IsPrinted = @bIsPrinted '
	END

	IF(@bIsLocked IS NOT NULL)
	BEGIN
		SET		@sqlStatement = @sqlStatement + ' AND lb.IsLocked = @bIsLocked '
	END

	SET @sqlStatement = @sqlStatement + ' ORDER BY	lb.DateCreated DESC '

	EXEC sp_executesql @sqlStatement, @sqlParameterDefinition,
		@iLetterTemplateID = @iLetterTemplateID,
		@dDateCreatedFrom = @dDateCreatedFrom,
		@dDateCreatedTo = @dDateCreatedTo,
		@iLetterBatchType = @iLetterBatchType,
		@dDateFrom = @dDateFrom,
		@dDateTo = @dDateTo,
		@iRunIDFrom = @iRunIDFrom,
		@iRunIDTo = @iRunIDTo,
		@bIsPrinted = @bIsPrinted,
		@bIsLocked = @bIsLocked

END
GO

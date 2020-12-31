USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatch_GetUnprocessedQuery]    Script Date: 06/07/2017 09:20:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benoit Nadon
-- Create date: 09/19/2006
-- Description:	Gets the FROM clause for an unprocessed LetterBatch query
-- =============================================
CREATE PROCEDURE [dbo].[pr_LetterBatch_GetUnprocessedQuery]

	@iLetterTemplateID				int,
	@iRunID							int,
	@dDateFrom						datetime,
	@dDateTo						datetime,
	@iCustomerOrderHeaderInstance	int,
	@iTransID						int,
	@sqlStatement					nvarchar(4000) OUTPUT

AS
BEGIN
    DECLARE	@zViewName		varchar(100)

	SELECT		@zViewName = lt.ViewName
	FROM		LetterTemplate lt
	WHERE		lt.ID = @iLetterTemplateID

	SET @sqlStatement = 
	'FROM		' + @zViewName + ' vw
	LEFT JOIN	(LetterBatchCustomerOrderDetail lbcod
	JOIN		LetterBatch lb
					ON	lb.ID = lbcod.LetterBatchID
					AND	lb.DeletedTF = 0
					AND	lb.LetterTemplateID = ' + CONVERT(nvarchar, @iLetterTemplateID) + ')
					ON	lbcod.CustomerOrderHeaderInstance = vw.CustomerOrderHeaderInstance
					AND	lbcod.TransID = vw.TransID
	WHERE		lbcod.LetterBatchID IS NULL '

	IF(COALESCE(@iRunID, -1) <> -1)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND vw.RunID = ' + CONVERT(nvarchar, @iRunID)
	END

	IF(COALESCE(@dDateFrom, '1995-01-01') <> '1995-01-01' AND COALESCE(@dDateTo, '1995-01-01') <> '1995-01-01')
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND vw.CreationDate BETWEEN ''' + CONVERT(nvarchar, @dDateFrom) + ''' AND ''' + CONVERT(nvarchar, @dDateTo) + ''' '
	END

	IF(COALESCE(@iCustomerOrderHeaderInstance, -1) <> -1 AND COALESCE(@iTransID, -1) <> -1)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND vw.CustomerOrderHeaderInstance = ' + CONVERT(nvarchar, @iCustomerOrderHeaderInstance) + ' AND vw.TransID = ' + CONVERT(nvarchar, @iTransID)
	END
END
GO

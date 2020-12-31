USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_Adjustment_SelectSearch]    Script Date: 06/07/2017 09:17:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Adjustment_SelectSearch]

	@iAdjustmentBatchID		int = 0,
	@iAdjustmentTypeID		int = 0,
	@iAdjustmentBatchStatus	int = 0,
	@dDateFrom			datetime = '1995-01-01',
	@dDateTo			datetime = '1995-01-01'

AS
DECLARE	@sqlStatement		nvarchar(4000)

SET @sqlStatement = 'SELECT	adj.ADJUSTMENT_ID,
		adj.ACCOUNT_ID,
		a.Name AS AccountName,
		c.StartDate AS CampaignStartDate,
		c.EndDate AS CampaignEndDate,
		adj.ACCOUNT_TYPE_ID,
		adj.ADJUSTMENT_TYPE_ID,
		adj.ADJUSTMENT_EFFECTIVE_DATE,
		adj.ADJUSTMENT_AMOUNT,
		COALESCE(adj.NOTE_TO_PRINT, '''') AS NOTE_TO_PRINT,
		adj.DATE_CREATED,
		COALESCE(adj.DATETIME_MODIFIED, ''1995-01-01'') AS DATETIME_MODIFIED,
		COALESCE(adj.LAST_UPDATED_BY, '''') AS LAST_UPDATED_BY,
		adj.COUNTRY_CODE,
		COALESCE(adj.INTERNAL_COMMENT, '''') AS INTERNAL_COMMENT,
		COALESCE(adj.ORDER_ID, 0) AS ORDER_ID,
		adj.CAMPAIGN_ID,
		adj.ADJUSTMENT_BATCH_ID

FROM		Adjustment adj
INNER JOIN	AdjustmentBatch adjb
			ON	adjb.ID = adj.ADJUSTMENT_BATCH_ID
INNER JOIN	QSPCanadaCommon..CAccount a
			ON	a.ID = adj.ACCOUNT_ID
INNER JOIN	QSPCanadaCommon..Campaign c
			ON	c.ID = adj.CAMPAIGN_ID

WHERE	1 = 1 '

IF(@iAdjustmentBatchID <> 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND adjb.ID = ' + CONVERT(nvarchar, @iAdjustmentBatchID)
END

IF(@iAdjustmentTypeID <> 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND adjb.AdjustmentTypeID = ' + CONVERT(nvarchar, @iAdjustmentTypeID)
END

IF(@iAdjustmentBatchStatus <> 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND adjb.Status = ' + CONVERT(nvarchar, @iAdjustmentBatchStatus)
END

IF(@dDateFrom <> '1995-01-01' AND @dDateTo <> '1995-01-01')
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND adjb.DateFrom <= ''' + CONVERT(nvarchar, @dDateTo, 101) + ''' '
	SET	@sqlStatement = @sqlStatement + ' AND adjb.DateTo > ''' + CONVERT(nvarchar, @dDateFrom, 101) + ''' '
END

SET	@sqlStatement = @sqlStatement + ' ORDER BY	adj.ADJUSTMENT_ID '

EXEC(@sqlStatement)
GO

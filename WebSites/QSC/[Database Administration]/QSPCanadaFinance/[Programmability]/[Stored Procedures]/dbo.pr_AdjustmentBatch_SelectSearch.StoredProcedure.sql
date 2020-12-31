USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_AdjustmentBatch_SelectSearch]    Script Date: 06/07/2017 09:17:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_AdjustmentBatch_SelectSearch]

	@iID			int = 0,
	@iAdjustmentTypeID	int = 0,
	@iStatus		int = 0,
	@dDateFrom		datetime = '1995-01-01',
	@dDateTo		datetime = '1995-01-01'

AS
DECLARE @sqlStatement	nvarchar(4000)

SET @sqlStatement = 'SELECT	adjb.ID,
		adjb.AdjustmentTypeID,
		adjt.Name AS AdjustmentTypeName,
		adjb.Status,
		cdStatus.Description AS StatusDescription,
		adjb.DateFrom,
		adjb.DateTo,
		adjb.CreateUserID,
		cupCreate.FirstName + '' '' + cupCreate.LastName AS CreateUserName,
		adjb.CreateDate,
		adjb.ChangeUserID,
		COALESCE(cupChange.FirstName, '''') + CASE COALESCE(cupChange.FirstName, '''') WHEN '''' THEN '''' ELSE '' '' END + COALESCE(cupChange.LastName, '''') AS ChangeUserName,
		adjb.ChangeDate

FROM		AdjustmentBatch adjb
INNER JOIN	Adjustment_Type adjt
			ON	adjt.Adjustment_Type_ID = adjb.AdjustmentTypeID
INNER JOIN	QSPCanadaCommon..CodeDetail cdStatus
			ON	cdStatus.Instance = adjb.Status
INNER JOIN	QSPCanadaCommon..CUserProfile cupCreate
			ON	cupCreate.Instance = adjb.CreateUserID
LEFT JOIN	QSPCanadaCommon..CUserProfile cupChange
			ON	cupChange.Instance = adjb.ChangeUserID

WHERE	1 = 1 '

IF(@iID <> 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND adjb.ID = ' + CONVERT(nvarchar, @iID)
END

IF(@iAdjustmentTypeID <> 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND adjb.AdjustmentTypeID = ' + CONVERT(nvarchar, @iAdjustmentTypeID)
END

IF(@iStatus <> 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND adjb.Status = ' + CONVERT(nvarchar, @iStatus)
END

IF(@dDateFrom <> '1995-01-01' AND @dDateTo <> '1995-01-01')
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND adjb.DateFrom <= ''' + CONVERT(nvarchar, @dDateTo, 101) + ''' '
	SET	@sqlStatement = @sqlStatement + ' AND adjb.DateTo > ''' + CONVERT(nvarchar, @dDateFrom, 101) + ''' '
END

SET	@sqlStatement = @sqlStatement + ' ORDER BY	adjb.CreateDate DESC '

EXEC(@sqlStatement)
GO

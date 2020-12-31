USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_AccountList_old]    Script Date: 06/07/2017 09:33:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_get_AccountList_old]
  @fmid varchar(4) = null,
  @sAccountID int = null,
  @sAccountName varchar(50) = null,
  @sCity varchar(50) = null,
  @sState varchar(10) = null,
  @sFMFirstName varchar(50) = null,
  @sFMLastName varchar(50) = null,
  @orderby int = null
AS

SET NOCOUNT ON

SELECT @sAccountID = NULL WHERE @sAccountID = 0 -- a null accountid becomes 0 in the C# code
SELECT @sAccountName = NULL WHERE ltrim(rtrim(@sAccountName)) = ''
SELECT @sCity = NULL WHERE ltrim(rtrim(@sCity)) = ''
SELECT @sState = NULL WHERE ltrim(rtrim(@sState)) = ''
SELECT @sFMFirstName = NULL WHERE ltrim(rtrim(@sFMFirstName)) = ''
SELECT @sFMLastName = NULL WHERE ltrim(rtrim(@sFMLastName)) = ''

CREATE TABLE #Campaigns
(
	AccountId int NOT NULL,
	StartDate datetime NULL,
	EndDate datetime NULL,
	FMID varchar(4) NULL
)

DECLARE @AccountID int 
DECLARE DistinctAccounts_cursor CURSOR FOR 
SELECT DISTINCT BillToAccountID 
FROM Campaign
	LEFT JOIN FieldManager on Campaign.FMID = FieldManager.FMID
	LEFT JOIN CAccount on Campaign.BillToAccountID = CAccount.ID
where 
	(((@sFMFirstName IS NOT NULL) AND (FieldManager.FirstName LIKE @sFMFirstName)) OR @sFMFirstName IS NULL ) --(ltrim(rtrim(@sFMFirstName)) <> '')
	AND (((@sFMLastName IS NOT NULL) AND (FieldManager.LastName LIKE @sFMLastName)) OR @sFMLastName IS NULL )
	AND (((@fmid IS NOT NULL) AND (FieldManager.FMID LIKE @fmid)) OR (@fmid IS NULL or @fmid = '9999') )
	AND (((@sAccountID IS NOT NULL) AND (BillToAccountID = @sAccountID)) OR @sAccountID IS NULL )
	AND (
		(
			(@sAccountID IS NOT NULL)
			AND( ((@sCity IS NOT NULL) AND (CAccount.City LIKE @sCity)) OR @sCity IS NULL )
		)
		OR (@sAccountID IS NULL)
	)
	AND (
		(
			(@sAccountID IS NOT NULL)
			AND( ((@sState IS NOT NULL) AND (CAccount.State LIKE @sState)) OR @sState IS NULL )
		)
		OR (@sAccountID IS NULL)
	)

	AND (
		(
			(@sAccountID IS NOT NULL)
			AND( ((@sAccountName IS NOT NULL) AND (CAccount.[Name] LIKE @sAccountName)) OR @sAccountName IS NULL )
		)
		OR (@sAccountID IS NULL)
	)


OPEN DistinctAccounts_cursor
FETCH NEXT FROM DistinctAccounts_cursor INTO @AccountID

WHILE @@FETCH_STATUS = 0
BEGIN
	
	INSERT INTO #Campaigns
	SELECT TOP 1 BillToAccountID, StartDate, EndDate, FMID
	FROM Campaign 
	WHERE BillToAccountID = @AccountID
	ORDER BY StartDate DESC

	-- Get the next campaign.
	FETCH NEXT FROM DistinctAccounts_cursor INTO @AccountID
END

CLOSE DistinctAccounts_cursor
DEALLOCATE DistinctAccounts_cursor

SELECT
	CAccount.[Id],
	CAccount.[Name], 
	CAccount.City, 
	CAccount.State, 
	CAccount.Sponsor, 
	ActPhone.PhoneNumber		 AS Phone,
	#Campaigns.StartDate,
	#Campaigns.EndDate,
	#Campaigns.FMID,
	isnull(FieldManager.Firstname, '')	 AS FMFirstName,
	isnull(FieldManager.LastName, '') 	 AS FMLastName
INTO
	#AccountList
FROM
	CAccount INNER JOIN #Campaigns ON CAccount.Id = #Campaigns.AccountID
	LEFT JOIN Phone ActPhone ON CAccount.PhoneListID = ActPhone.PhoneListID AND ActPhone.Type = 1
	LEFT JOIN FieldManager ON #Campaigns.FMID = FieldManager.FMID

DROP TABLE #Campaigns;


--GET the order to use
DECLARE 	@order varchar(100)
SELECT	@order = CASE @orderby
				WHEN  1 THEN '[Id]'
				WHEN  2 THEN '[Name]'
				WHEN  3 THEN '[City]'
				WHEN  4 THEN '[State]'
				WHEN  5 THEN 'Sponsor'
				WHEN  6 THEN 'Phone'
				WHEN  7 THEN 'DATEPART(yyyy, StartDate), DATEPART(mm, StartDate), DATEPART(dd, StartDate)' --strlen is 75 here
				WHEN  8 THEN 'DATEPART(yyyy, EndDate), DATEPART(mm, EndDate), DATEPART(dd, EndDate)' 
				WHEN  9 THEN 'FMID'
				ELSE '[Name]' --default
			END

--get the date format to use
DECLARE	@DateFormat int
exec 		@DateFormat = pr_DateFormat @fmid

--construct SQL to select from the temp table
DECLARE	@SQL varchar(1300)
SELECT @SQL = 'SELECT [Id], [Name], City, State, Sponsor, Phone, ';
SELECT @SQL = @SQL + ' CONVERT (varchar(10), StartDate, ' + CAST(@DateFormat as varchar) + ') AS StartDate,'; 
SELECT @SQL = @SQL + ' CONVERT (varchar(10), EndDate,' +   CAST(@DateFormat as varchar) + ') AS EndDate, FMID';
SELECT @SQL = @SQL + ' FROM #AccountList'

IF @order IS NOT NULL AND ltrim(rtrim(@order)) <> ''
begin
	SELECT @SQL = @SQL + ' ORDER BY ' + @order;	
end

SET NOCOUNT OFF
EXEC (@SQL)
drop table #AccountList
GO

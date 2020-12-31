USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_AccountList]    Script Date: 06/07/2017 09:33:09 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_AccountList]
  @mode int,
  @fmid varchar(4) = null,
  @orderby int = null,
  @search varchar(22) = null
AS

/*
<param name="mode">
	<value type="2">Search by Account Id, no other filter</value>
	<value type="3">Search by Account Name, no other filter</value>
	<value type="4">Search by City, no other filter</value>
	<value type="5">Search by State, no other filter</value>
	<value type="21">Search by FMID, no other filter</value>
	<value type="22">Search by Account Id, fmid</value>
	<value type="23">Search by Account Name, fmid</value>
	<value type="24">Search by City, fmid</value>
	<value type="25">Search by State, fmid</value>
</param>
*/


SET NOCOUNT ON

DECLARE @AccountID int 
--DECLARE @rows int
--SELECT @rows = 0

CREATE TABLE #Campaigns
(
	AccountId int NOT NULL,
	StartDate datetime NULL,
	EndDate datetime NULL
)

if (@mode > 20)
begin
	--print 'FM Mode'
	--modes that are for FM views
	--keeping the WHERE clause up here vs. in the @SQL for performance reasons.

	DECLARE DistinctAccounts_cursor CURSOR FOR 
	SELECT DISTINCT BillToAccountID FROM Campaign WHERE FMID = @FMID
	OPEN DistinctAccounts_cursor

	FETCH NEXT FROM DistinctAccounts_cursor INTO @AccountID

	WHILE @@FETCH_STATUS = 0
	BEGIN
	  
		INSERT INTO #Campaigns
		SELECT TOP 1 BillToAccountID, StartDate, EndDate
		FROM Campaign 
		WHERE FMID = @FMID and BillToAccountID = @AccountID
		ORDER BY StartDate DESC

		--SELECT @rows = @rows + 1

		-- Get the next campaign.
		FETCH NEXT FROM DistinctAccounts_cursor INTO @AccountID
	END

	CLOSE DistinctAccounts_cursor
	DEALLOCATE DistinctAccounts_cursor

	--print '(' + CAST(@rows as varchar) + ' row(s) affected (Distinct Accounts in Campaign Table) )'

	SELECT
		CAccount.[Id],
		CAccount.[Name], 
		CAccount.City, 
		CAccount.State, 
		CAccount.Sponsor, 
		ActPhone.PhoneNumber	 AS Phone,
		#Campaigns.StartDate,
		#Campaigns.EndDate,
		@FMID AS FMID
	INTO
		#AccountList
	FROM
		CAccount INNER  JOIN #Campaigns ON CAccount.Id = #Campaigns.AccountID
		LEFT JOIN Phone ActPhone ON CAccount.PhoneListID = ActPhone.PhoneListID AND ActPhone.Type = 1
end
else
begin
	--home office type modes

	DECLARE DistinctAccounts_cursor CURSOR FOR 
	SELECT DISTINCT BillToAccountID FROM Campaign WHERE FMID = @FMID
	OPEN DistinctAccounts_cursor

	FETCH NEXT FROM DistinctAccounts_cursor INTO @AccountID

	WHILE @@FETCH_STATUS = 0
	BEGIN
	  
		INSERT INTO #Campaigns
		SELECT TOP 1 BillToAccountID, StartDate, EndDate
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
		ActPhone.PhoneNumber	 AS Phone,
		#Campaigns.StartDate,
		#Campaigns.EndDate
	INTO
		#AccountList2
	FROM
		CAccount INNER JOIN #Campaigns ON CAccount.Id = #Campaigns.AccountID
		LEFT JOIN Phone ActPhone ON CAccount.PhoneListID = ActPhone.PhoneListID AND ActPhone.Type = 1

end
DROP TABLE #Campaigns


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

DECLARE	@searchField varchar(20)
SELECT	@searchField = CASE @mode
				WHEN   2 THEN '[Id]'
				WHEN   3 THEN '[Name]'
				WHEN   4 THEN '[City]'
				WHEN   5 THEN '[State]'
				WHEN  21 THEN 'FMID'
				WHEN  22 THEN '[Id]'
				WHEN  23 THEN '[Name]'
				WHEN  24 THEN '[City]'
				WHEN  25 THEN '[State]'
			END


--get the date format to use
DECLARE	@DateFormat int
exec 		@DateFormat = pr_DateFormat @fmid

--construct SQL to select from the temp table
DECLARE	@SQL varchar(300)
SELECT @SQL = 'SELECT [Id], [Name], City, State, Sponsor, Phone, ';
SELECT @SQL = @SQL + ' CONVERT (varchar(10), StartDate, ' + CAST(@DateFormat as varchar) + ') AS StartDate,'; 
SELECT @SQL = @SQL + ' CONVERT (varchar(10), EndDate,' +   CAST(@DateFormat as varchar) + ') AS EndDate, FMID';

if (@mode > 20)
begin
	SELECT @SQL = @SQL + ' FROM #AccountList'
end
else
begin
	SELECT @SQL = @SQL + ' FROM #AccountList2'
end

IF((@mode = 3)OR(@mode = 23))
begin
	--setup this search to use wildcards
	SELECT @search = '%' + @search + '%'
end

IF( @mode <> 21)
begin
	--21 is use FMID only., the rest have a search field
	SELECT @SQL = @SQL + ' WHERE ' + @searchField + ' LIKE ''' + @search + ''''
end
	
SELECT @SQL = @SQL + ' ORDER BY ' + @order;	
SET NOCOUNT OFF

EXEC (@SQL)
drop table #AccountList
GO

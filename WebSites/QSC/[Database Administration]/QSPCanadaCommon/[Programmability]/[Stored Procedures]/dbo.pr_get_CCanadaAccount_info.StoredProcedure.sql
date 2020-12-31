USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_CCanadaAccount_info]    Script Date: 06/07/2017 09:33:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE    PROCEDURE [dbo].[pr_get_CCanadaAccount_info]
  @fmid varchar(4) = '9999',
  @sAccountID int = null,
  @sAccountName varchar(50)  = null,
  @sCity varchar(50)  = null,
  @sState varchar(10)  = null,
  @sFMFirstName varchar(50)  = null,
  @sFMLastName varchar(50)  = null,
  @orderby int  = null
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
	FMID varchar(4) NULL,
	NumberOfParticipants int NULL,
	NumberOfClassroooms int NULL
)

DECLARE @AccountID int 
DECLARE DistinctAccounts_cursor CURSOR FOR 
SELECT DISTINCT BillToAccountID 
FROM QSPCanadaCommon.dbo.Campaign CA
	LEFT JOIN QSPCanadaCommon.dbo.FieldManager FM
	on CA.FMID = FM.FMID
	LEFT JOIN QSPCanadaCommon.dbo.CAccount Acct
	on CA.BillToAccountID = Acct.[ID]
where 
	    ( ((@sFMFirstName IS NOT NULL) AND(FM.FirstName LIKE @sFMFirstName)) OR @sFMFirstName IS NULL )
	AND ( ((@sFMLastName IS NOT NULL)  AND(FM.LastName LIKE @sFMLastName))   OR @sFMLastName IS NULL )
	AND ( ((@fmid IS NOT NULL)         AND(FM.FMID LIKE @fmid))              OR (@fmid IS NULL or @fmid = '9999') )
	AND ( ((@sAccountID IS NOT NULL)   AND(BillToAccountID = @sAccountID))             OR @sAccountID IS NULL )
	AND ( ((@sCity IS NOT NULL)        AND(Acct.City LIKE @sCity))                 OR @sCity IS NULL )
	AND ( ((@sState IS NOT NULL)       AND(Acct.State LIKE @sState))               OR @sState IS NULL )
	AND ( ((@sAccountName IS NOT NULL) AND(Acct.[Name] LIKE @sAccountName))        OR @sAccountName IS NULL )


OPEN DistinctAccounts_cursor
FETCH NEXT FROM DistinctAccounts_cursor INTO @AccountID

WHILE @@FETCH_STATUS = 0
BEGIN
	
	INSERT INTO #Campaigns
	SELECT TOP 1 BillToAccountID, StartDate, EndDate, FMID, NumberOfParticipants, NumberOfClassroooms
	FROM QSPCanadaCommon.dbo.Campaign 
	WHERE 
		BillToAccountID = @AccountID
		AND Status IN (37002)
	ORDER BY 
		StartDate DESC

	-- Get the next campaign.
	FETCH NEXT FROM DistinctAccounts_cursor INTO @AccountID
END

CLOSE DistinctAccounts_cursor
DEALLOCATE DistinctAccounts_cursor

SELECT
	Acct.[Id],
	Acct.[Name],
	isnull(Addr.[street1], '')		 AS Address1,
	isnull(Addr.[street2], '')		 AS Address2,
	isnull(Addr.[city], '')			 AS City,
	isnull(Addr.[stateProvince], '')	 AS State,
	isnull(replace(Addr.[postal_code], ' ',''), '')		 AS Zip,
	isnull(Addr.[zip4], '')			 AS Zip4,
	Acct.Sponsor, 
	ActPhone.PhoneNumber		 AS Phone,
	#Campaigns.StartDate,
	#Campaigns.EndDate,
	#Campaigns.FMID,
	#Campaigns.NumberOfParticipants,
	#Campaigns.NumberOfClassroooms, 
	isnull(FM.Firstname, '')	 AS FMFirstName,
	isnull(FM.LastName, '') 	 AS FMLastName,
	isnull(Addr.[street1], '')		 AS OriginalAddress1,
	isnull(Addr.[street2], '')		 AS OriginalAddress2,
	isnull(Addr.[city], '')			 AS OriginalCity,
	isnull(Addr.[stateProvince], '')	 AS OriginalState,
	isnull(replace(Addr.[postal_code], ' ',''), '')		 AS OriginalZip,
	isnull(Addr.[zip4], '')			 AS OriginalZip4,
	Acct.StatusID,
	Acct.Comment,
	Acct.[County],
	Acct.[SalesRegionID],
	Acct.CAccountCodeClass,
	Acct.CAccountCodeGroup
INTO
	#AccountList
FROM
	QSPCanadaCommon.dbo.CAccount Acct 
	INNER JOIN #Campaigns 
		ON Acct.Id = #Campaigns.AccountID
	LEFT JOIN QSPCanadaCommon.dbo.Phone ActPhone 
		ON Acct.PhoneListID = ActPhone.PhoneListID 
		AND ActPhone.Type = 1
	LEFT JOIN QSPCanadaCommon.dbo.FieldManager FM
		ON #Campaigns.FMID = FM.FMID
	LEFT JOIN QSPCanadaCommon.dbo.Address Addr 
		ON Acct.[AddressListID] = Addr.[AddressListID] 
		AND Addr.[address_type] = 54001 -- shipto

DROP TABLE #Campaigns;
set nocount off

/*
IF @sAccountID IS NOT NULL
BEGIN
	DELETE FROM QSPCanadaCommon.dbo.CCanadaAccount WHERE [Id] = @sAccountID;
END
ELSE
BEGIN
	TRUNCATE TABLE QSPCanadaCommon.dbo.CCanadaAccount;
END
*/

INSERT QSPCanadaCommon.dbo.CCanadaAccount([Id], [Name], [Address1], [Address2], [City], [State], [Zip], [Zip4], [Sponsor], [FMID], [FMRegion], [ProgramStartDate], [ProgramEndDate], [SchoolType], [NumberOfStudents], [NumberOfClassRooms], [ProgramType], [IsNational], [Status], [FlagpoleInstance], [Comment], [CreateDate], [CreateUser], [LastUpdateDate], [UpdatedBy], [County], [ATrackSchoolType], [TaxExemptNumber], [Commission], [ATrackDateCreated], [ATrackUserIDCreated], [ATrackDateChanged], [ATrackUserIDChanged], [InvoiceCalculationMethod], [ATrackStatus], [InvoicePercentage], [ShowSalesFiguresOnInvoice], [ATrackProgramType], [ATrackType], [TaxRate], [QMSStatus], [OriginalAddress1], [OriginalAddress2], [OriginalCity], [OriginalState], [OriginalZip], [OriginalZip4], [MagNetAcctID], [UnitsComm])

SELECT
	[Id],
	cast ([Name] as varchar(50)) as [Name],
	[Address1],
	[Address2],
	[City],
	[State],
	case [Id]
		when 3854 then cast(' ' as varchar(6))
		else [Zip]
	end AS [Zip],
	[Zip4],
	[Sponsor],
	[FMID],

	--DEBUG is this correct ?? will it conflict with us regions ? :	
	case SalesRegionID
		when 38001 then cast('01' as char(2))
		when 38002 then cast('02' as char(2))
		when 38003 then cast('03' as char(2))
		when 38004 then cast('04' as char(2))
		when null then null
	end as [FMRegion],
	--cast('00' as varchar) as FMRegion,
	-- from 20: SELECT distinct  
	-- from 20: from QSPCanadaCommon.dbo.CAccount
	-- from 20: 38002,38001,38004,NULL,38003
	-- from 20: (5 row(s) affected)
	-- from 20: SELECT Instance,CodeHeaderInstance,Description FROM QSPCanadaCommon.dbo.CodeDetail
	-- from 20: WHERE (CodeHeaderInstance = 38000)
	-- from 20: Instance CodeHeaderInstance Description                                                      
	-- from 20: -------- ------------------ ---------------------------------------------------------------- 
	-- from 20: 38001    38000              sales region - atlantic canada
	-- from 20: 38002    38000              Sales region - Quebec
	-- from 20: 38003    38000              Sales Region Ontario East
	-- from 20: 38004    38000              Sales Reqion - Western Canada
	-- from 20: (4 row(s) affected)


	--convert(varchar(30), StartDate, 121) AS ProgramStartDate,
	--convert(varchar(30), EndDate,   121) AS ProgramEndDate,
	cast( StartDate AS smalldatetime) AS ProgramStartDate,
	cast( EndDate AS smalldatetime) AS ProgramEndDate,
		
	--DEBUG:
	case CAccountCodeGroup
		when 'Sc1' then cast('EL' as varchar(2))
		when 'Sc2' then cast('HS' as varchar(2))
		when 'Sc3' then cast('JH' as varchar(2))
		when 'Sc4' then cast('M' as varchar(2))
		else cast(' ' AS varchar(2))
	end as [SchoolType],	
	--need to know how to determine this
	--from 12: SELECT distinct SchoolType from caccount
	--from 12: (NULL,1A,1C,1D,1F,1G,CO,CS,EL,F,G,HS,JH,M,SO)
	--from 12: (15 row(s) affected)
	
	[NumberOfParticipants] AS [NumberOfStudents],
	cast([NumberOfClassroooms] as smallint) AS [NumberOfClassRooms],
	
	--DEBUG:
	cast(0 as tinyint) as [ProgramType],
	--from 12: SELECT distinct ProgramType from caccount
	--from 12: NULL,1,2,3,4,5,6,7,9,11
	--from 12: (10 row(s) affected)
	--from 12: SELECT * FROM CAccount_ProgramType--(12 row(s) affected)
	--from 12: Id   ProductId Description                                        
	--from 12: ---- --------- ----------------------
	--from 12: 1    1         Regular MMB
	--from 12: 2    1         Direct Mail
	--from 12: 3    1         Family Reading Program
	--from 12: 4    1         PayLater
	--from 12: 5    1         Internet
	--from 12: 6    1         Misc.
	--from 12: 7    2         Food
	--from 12: 8    2         Misc.
	--from 12: 9    3         Gift
	--from 12: 10   3         Misc.
	--from 12: 11   4         Chocolate
	--from 12: 12   4         Misc.
	--There are multiple programs to a campaign in QSP CA. Which one do I choose ?
	
	--DEBUG:
	cast(0 as bit) as [IsNational], 
	--Are any of them National Accounts ??
	
	--DEBUG:
	case StatusID
		when 35001 then cast(0 as tinyint)
		when 35002 then cast(0 /*6*/ as tinyint)
		when 35003 then cast(0 as tinyint)
		else cast(0 as tinyint)
	end as [Status],
	--how do I translate these status codes ? 
	--from 12: select distinct Status from CAccount --(NULL, 1, 2, 6, 7, 9) --(6 row(s) affected)
	--from 12: select * from CAccount_Status	
	-- Status_Code Status_Description                                 
	-- ----------- ------------------
	-- 1           New
	-- 2           Updateable
	-- 3           As Is
	-- 4           Deleted
	-- 5           Research
	-- 6           Inactive
	-- 7           MDR Closed
	-- from 20: select distinct StatusID from QSPCanadaCommon.dbo.CAccount
	-- from 20: (35001,35002,35003)
	-- from 20:
	-- from 20: SELECT Instance,CodeHeaderInstance,Description FROM QSPCanadaCommon.dbo.CodeDetail
	-- from 20: WHERE     (CodeHeaderInstance = 35000)
	-- from 20: Instance    CodeHeaderInstance Description                                                      
	-- from 20: ----------- ------------------ ---------------------------------------------------------------- 
	-- from 20: 35001       35000              Account  status - active
	-- from 20: 35002       35000              Account  status - inactive
	-- from 20: 35003       35000              Account  status - pending
	-- from 20: 
	-- from 20: select distinct status from campaign order by status asc
	-- from 20: (37001,37002,37004,37005,37007) --(5 row(s) affected)
	-- from 20: 
	-- from 20: SELECT Instance,CodeHeaderInstance,Description 
	-- from 20: FROM QSPCanadaCommon.dbo.CodeDetail
	-- from 20: WHERE (CodeHeaderInstance = 37000) --(7 row(s) affected)
	-- from 20: Instance    CodeHeaderInstance Description
	-- from 20: ----------- ------------------ ------------------------------------
	-- from 20: 37001       37000              campaign status - pending incomplete
	-- from 20: 37002       37000              campaign status - approved
	-- from 20: 37003       37000              campaign status - pending fs
	-- from 20: 37004       37000              campaign status - oh hold
	-- from 20: 37005       37000              campaign status - cancel
	-- from 20: 37006       37000              campaign status - inactive
	-- from 20: 37007       37000              campaign status - Order Logged
	
	--DEBUG:
	cast (0 as int) as [FlagpoleInstance],
	--do we have canadian flagpoles ? 

	cast ([Comment] as varchar(500)) AS Comment,
	cast( getdate() AS smalldatetime) AS [CreateDate],
	cast( 'JLC-DTS' AS varchar(50) ) AS [CreateUser],
	cast( getdate() AS smalldatetime) AS [LastUpdateDate],
	cast( 'JLC-DTS' AS varchar(50) ) AS [UpdatedBy],
	[County],

	--DEBUG:
	cast(NULL as varchar(10)) as [ATrackSchoolType],
	--from 12: SELECT distinct ATrackSchoolType from caccount
	--from 12: NULL,,111,112,121,131,141,142,151,211,212,241,242,411,412,441,451
	--from 12: EL,HS,JH,M
	
	--DEBUG:
	cast(NULL as varchar(23)) as [TaxExemptNumber],
	--where is this coming from?

	--DEBUG:
	--grab commission off of the campaign program record? 
	--which program do i link up to ? 
	cast(0.0 as float ) as [Commission], 
	
	cast( '1995-01-01' AS datetime) AS [ATrackDateCreated],
	cast(1223 as int ) as [ATrackUserIDCreated],
	cast( '1995-01-01' AS datetime) AS [ATrackDateChanged],
	cast(1223 as int ) as [ATrackUserIDChanged],
	
	--DEBUG:
	cast(0 AS int) as [InvoiceCalculationMethod],
	-- from 20: SELECT InvoiceCalculationMethod as data, 
	-- from 20: 	count(InvoiceCalculationMethod) as amount
	-- from 20: from caccount
	-- from 20: group by InvoiceCalculationMethod
	-- from 20: order by count(InvoiceCalculationMethod) desc
	-- from 20: data amount      
	-- from 20: ---- ----------- 
	-- from 20: 0    258331
	-- from 20: 1    1749
	-- from 20: 3    597
	-- from 20: 4    566
	-- from 20: 6    93
	-- from 20: 2    70
	-- from 20: 5    49
	-- from 20: 11   13
	-- from 20: 7    4
	-- from 20: 8    4
	-- from 20: (10 row(s) affected)
	
	--DEBUG:
	cast(0 AS int) as [ATrackStatus],
	--from 12: SELECT distinct ATrackStatus from caccount
	--from 12: (1400,1401,1402,1403,1405)
	--from 12: (5 row(s) affected)


	--DEBUG:
	cast(0.0 as numeric(10,2)) as [InvoicePercentage],

	--DEBUG:
	cast('N' as char(1)) AS [ShowSalesFiguresOnInvoice],
	-- from 20: SELECT 
 	-- from 20:	'|' + ShowSalesFiguresOnInvoice + '|' as data, 
	-- from 20: 	count(ShowSalesFiguresOnInvoice) as amount
	-- from 20: from caccount
	-- from 20: group by ShowSalesFiguresOnInvoice
	-- from 20: order by count(ShowSalesFiguresOnInvoice) desc
	-- from 20: data amount      
	-- from 20: ---- ----------- 
	-- from 20: |N|  256916
	-- from 20: | |  4560
	-- from 20: 
	-- from 20: (2 row(s) affected)

	--DEBUG:
	cast(0 as int) as [ATrackProgramType],
	--from 12: SELECT distinct ATrackProgramType from caccount
	--from 12: (0)
	--from 12: (1 row(s) affected)

	--DEBUG:
	cast(NULL as int) as [ATrackType],
	--from 12: SELECT distinct ATrackType from caccount
	--from 12: (NULL,1600,1601,1602,1603)
	--from 12: (5 row(s) affected)

	--DEBUG:
	cast(NULL as float) as [TaxRate],

	--DEBUG:
	cast(0 as int) as [QMSStatus],
	--cast(NULL as int) as [QMSStatus],
	--from 12: SELECT distinct QMSStatus from caccount
	--from 12: (NULL,1,2,5,6)
	--from 12: (5 row(s) affected)
	
	[Address1] 	 AS [OriginalAddress1],
	[Address2] 	 AS [OriginalAddress2],
	[City] 		 AS [OriginalCity],
	[State]		 AS [OriginalState],
	case [Id]
		when 3854 then cast('' as varchar(6))
		else [Zip]
	end AS [OriginalZip],
	[Zip4]		 AS [OriginalZip4],
	
	--DEBUG:
	cast(0 as int) as [MagNetAcctID],

	--DEBUG:
	cast(0.0 as numeric(10,2)) as [UnitsComm]
--INTO	
--	QSPCanadaCommon.dbo.CCanadaAccount3
FROM
	#AccountList
Where
	Id NOT IN(1)

drop table #AccountList

/*
DELETE FROM QSPCanadaCommon.dbo.CCanadaAccount WHERE fakeid in (1526,1527)
ALTER TABLE QSPCanadaCommon.dbo.CCanadaAccount 
	DROP CONSTRAINT [PK_CCanadaAccount]
ALTER TABLE QSPCanadaCommon.dbo.CCanadaAccount 
	DROP COLUMN fakeid
ALTER TABLE QSPCanadaCommon.dbo.CCanadaAccount 
ADD CONSTRAINT [PK_CCanadaAccount] PRIMARY KEY([Id])
*/


/*
SELECT
	[Id],
	[Name],
	[Address1],
	[Address2],
	[City],
	[State],
	[Zip],
	[Zip4],
	[Sponsor],
	[FMID],
	[FMRegion],
	[ProgramStartDate],
	[ProgramEndDate],
	[SchoolType],
	--[NumberOfStudents],
	--[NumberOfClassRooms],
	--[ProgramType],
	--[IsNational],
	--[Status],
	--[FlagpoleInstance],
	[Comment],
	--[CreateDate],
	--[CreateUser],
	--[LastUpdateDate],
	--[UpdatedBy],
	[County],
	--[ATrackSchoolType],
	--[TaxExemptNumber],
	--[Commission],
	--[ATrackDateCreated],
	--[ATrackUserIDCreated],
	--[ATrackDateChanged],
	--[ATrackUserIDChanged],
	--[InvoiceCalculationMethod],
	--[ATrackStatus],
	--[InvoicePercentage],
	--[ShowSalesFiguresOnInvoice],
	--[ATrackProgramType],
	--[ATrackType],
	--[TaxRate],
	--[QMSStatus],
	[OriginalAddress1],
	[OriginalAddress2],
	[OriginalCity],
	[OriginalState],
	[OriginalZip],
	[OriginalZip4]--,
	--[MagNetAcctID],
	--[UnitsComm]
FROM 
	QSPCanadaCommon.dbo.CAccount
*/

--Are Candian Provinces needed in 2k12->QSPCommon.dbo.CAccount_States??
--data cleanup

/*

SELECT * FROM QSPCanadaCommon.dbo.CCanadaAccount2
where len(zip) > 6 --id:3854, zip:'V2X24V5'
--12140 203 ST,MAPLE RIDGE,	BC	V2X24V5


SELECT * FROM QSPCanadaCommon.dbo.CCanadaAccount2
where len(zip4) > 4--nada

SELECT * FROM QSPCanadaCommon.dbo.CCanadaAccount2
where len(County) > 15--nada

*/

/*

select id, count(id)
from QSPCanadaCommon.dbo.CCanadaAccount3
group by id
order by count(id) desc

SELECT * FROM QSPCanadaCommon.dbo.CCanadaAccount WHERE ID = 5204
*/


--(2452 row(s) affected)
GO

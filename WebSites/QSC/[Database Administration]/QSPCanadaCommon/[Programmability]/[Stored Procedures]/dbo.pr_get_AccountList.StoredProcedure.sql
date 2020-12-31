USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_AccountList]    Script Date: 06/07/2017 09:33:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_AccountList]
  @sFMID varchar(4) = '',
  @iId int = 0,
  @iCampaignID int = 0,
  @sName varchar(50) = '',
  @sCity varchar(50) = '',
  @sState varchar(10) = '',
  @sZip varchar(20) = '', 
  @sFMFirstName varchar(50) = '',
  @sFMLastName varchar(50) = '',
  @iFiscalYear int = 0,
  @iOrderBy int = 2

AS

/*
declare @JC varchar(2000)
declare @endl varchar(2)
select @endl = char(13) + char(10)
select @JC = 'account list debug info:' + @endl
select @JC = @JC + 'fmid: ' + isnull(@fmid, 'null') + @endl
select @JC = @JC + 'sAccountID: ' + cast(isnull(@sAccountID, -19791979) as varchar) + @endl
select @JC = @JC + 'sAccountName: ' + isnull(@sAccountName, 'null') + @endl
select @JC = @JC + 'sCity: ' + isnull(@sCity, 'null') + @endl
select @JC = @JC + 'sState: ' + isnull(@sState, 'null') + @endl
select @JC = @JC + 'sPostal: ' + isnull(@sPostal, 'null') + @endl
select @JC = @JC + 'sFMFirstName: ' + isnull(@sFMFirstName, 'null') + @endl
select @JC = @JC + 'sFMLastName: ' + isnull(@sFMLastName, 'null') + @endl
select @JC = @JC + 'fiscalYear: ' + cast(isnull(@fiscalYear, -19791979) as varchar) + @endl
select @JC = @JC + 'orderby: ' + cast(isnull(@orderby, -19791979) as varchar) + @endl


EXEC QSPCanadaCommon.dbo.Send_EMAIL 
  @From   = 'QSP.CA@rd.com'
 ,@To     = 'Joshua.Caesar@rd.com'
 ,@Subject= 'QSP_FULF 20-dev Account List Debug'
 ,@Body   = @JC
 ,@HTML   = 0 ;
*/

DECLARE @sql VARCHAR(8000)
DECLARE @first_where int
DECLARE @order varchar(100)
DECLARE @StartDate varchar(20)
DECLARE @EndDate varchar(20)


SET @first_where = 1

SET @sql = '

SELECT DISTINCT
	a.[ID],
	coalesce(a.[Name], '''') AS Name,
	coalesce(AddrBill.City, '''') AS City,
	coalesce(AddrBill.StateProvince, '''') AS State,
	coalesce(a.Sponsor, '''') AS Sponsor,
	coalesce(dbo.FormatPhoneUS_dash(ActPhone.PhoneNumber),'''') AS Phone--,
	--coalesce(Campaign.StartDate, ''1995-01-01'') AS StartDate,
	--coalesce(Campaign.EndDate, ''1995-01-01'') AS EndDate,
	--Campaign.FMID
FROM
	CAccount a LEFT OUTER JOIN Campaign on Campaign.billtoaccountid = a.id
	LEFT JOIN QSPCanadaCommon..PhoneList pl on a.PhoneListID = PL.ID
	LEFT OUTER JOIN Phone ActPhone ON ActPhone.PhoneListID = PL.ID AND ActPhone.Type = 30505 AND ActPhone.ID = (SELECT max(ID) FROM Phone WHERE Phone.PhoneListID = PL.ID AND Phone.Type = 30505)
	LEFT JOIN FieldManager ON Campaign.FMID = FieldManager.FMID
	LEFT JOIN QSPCanadaCommon..AddressList AL on a.AddressListID = AL.ID
	LEFT OUTER JOIN QSPCanadaCommon..Address AddrBill ON AddrBill.AddressListID = AL.ID AND AddrBill.Address_Type = 54002 AND AddrBill.address_id = (SELECT max(address_id) FROM Address WHERE Address.AddressListID = AL.ID AND Address.address_type = 54002)
'

IF @sFMID <> '' AND @sFMID <> '9999'
BEGIN
	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' Campaign.FMID = ''' + @sFMID + ''' '
END

IF @iId <> 0
BEGIN
	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' a.id = '  + convert(nvarchar, @iId)
END

IF @iCampaignID <> 0
BEGIN
	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' Campaign.ID = ' + convert(nvarchar, @iCampaignID)
END

IF @sName <> ''
BEGIN
	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' a.Name LIKE ''%' + @sName + '%'' '
END

IF @sCity <> ''
BEGIN
	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' AddrBill.City = ''' + @sCity + ''' '
END

IF @sState <> ''
BEGIN
	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' AddrBill.StateProvince = ''' + @sState + ''' '
END

IF @sZip <> ''
BEGIN
	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' AddrBill.postal_code = ''' + @sZip + ''' '
END

IF @sFMFirstName <> ''
BEGIN
	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' FieldManager.FirstName = ''' + @sFMFirstName + ''' '
END

IF @sFMLastName <> ''
BEGIN
	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' FieldManager.LastName = ''' + @sFMLastName + ''' '
END

IF @iFiscalYear <> 0
begin
	SELECT @StartDate = '07/01/'+str(@iFiscalYear - 1)
	SELECT @EndDate = '06/30/'+str(@iFiscalYear)

	IF @first_where = 1
	BEGIN
		SET @sql = @sql + ' WHERE '
		SET @first_where = 0
	END
	ELSE
	BEGIN
		SET @sql = @sql + ' AND '
	END
	SET @sql = @sql + ' Campaign.StartDate >= ''' + @StartDate + ''' AND Campaign.EndDate < ''' + @EndDate + ''' '
end


SELECT	@order = CASE @iOrderBy
				WHEN  1 THEN 'a.[Id]'
				WHEN  2 THEN '[Name]'
				WHEN  3 THEN '[City]'
				WHEN  4 THEN '[State]'
				WHEN  5 THEN 'Sponsor'
				WHEN  6 THEN 'Phone'
				WHEN  7 THEN 'StartDate'
				WHEN  8 THEN 'EndDate' 
				WHEN  9 THEN 'FMID'
				ELSE '[Name]' --default
			END

SET @sql = @sql + ' ORDER BY ' + @order
EXEC (@SQL)
GO

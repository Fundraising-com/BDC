USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_CampaignList_V2]    Script Date: 06/07/2017 09:33:21 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE        PROCEDURE [dbo].[pr_get_CampaignList_V2]
  @sFMID varchar(4) = '',
  @iAccountID	int		= 0,
  @iCampaignID int		= 0,
  @sName varchar(50) = '',
  @sCity varchar(50) = '',
  @sState varchar(10) = '',
  @sZip varchar(20) = '', 
  @sFMFirstName varchar(50) = '',
  @sFMLastName varchar(50) = '',
  @iFiscalYear int = 0,
  @sFromSupplyShipment varchar(20)='',
  @sToSupplyShipment varchar(20)='',
  @iSupplyGenerated int =0

AS

DECLARE @sqlStatement nvarchar(4000)
DECLARE @StartDate varchar(20)
DECLARE @EndDate varchar(20)

SET @sqlStatement =
'
Set NoCount on
SELECT	DISTINCT
		c.[ID] 			AS [CampaignID],
		coalesce(c.StartDate, ''01/01/1995'')	AS [StartDate],
		coalesce(c.EndDate, ''01/01/1995'') 	AS [EndDate],
		c.FMID,
		FieldManager.FirstName + '' '' + FieldManager.LastName AS FMName,
		c.Status,
		cd.Description				AS StatusName,
		c.Lang,
		cast('' '' as varchar(200)) 			as Programs ,
		coalesce(c.IsStaffOrder, 0)		AS [IsStaffOrder],
		coalesce(c.OnlineOnlyPrograms, 0) AS [IsOnlineOnly],
		coalesce(b.Date, ''1995-01-01'')		AS [MainOrderFulfilled],
		coalesce(SuppliesDeliveryDate, ''1995-01-01'')	AS [SupplyFulfilled],
		c.BillToAccountID Into #List
FROM		Campaign c
JOIN		CAccount a
	ON	a.ID = c.ShipToAccountID
LEFT OUTER JOIN	PhoneList pl
	ON	a.PhoneListID = PL.ID
LEFT OUTER JOIN	(SELECT top 1 ID, Type, PhoneListID, PhoneNumber, BestTimeToCall FROM Phone WHERE Phone.Type = 30505 ORDER BY ID DESC) ActPhone
	ON	PL.ID = ActPhone.PhoneListID
LEFT OUTER JOIN	FieldManager
	ON	c.FMID = FieldManager.FMID
LEFT OUTER JOIN	AddressList AL
	ON	a.AddressListID = AL.ID
LEFT OUTER JOIN	Address AddrBill
	ON	AL.ID = AddrBill.AddressListID
	AND	AddrBill.Address_Type = 54002
LEFT OUTER JOIN QSPCanadaOrderManagement..Batch b
	ON	b.CampaignID = c.ID
	AND	b.OrderQualifierID = 39001
	AND	b.StatusInstance = 40013
	AND	b.Date =
		(SELECT	MIN(bMinDate.Date)
		FROM		QSPCanadaOrderManagement..Batch bMinDate
		WHERE	bMinDate.CampaignID = c.ID
		AND		bMinDate.OrderQualifierID = 39001
		AND		bMinDate.StatusInstance = 40013)
LEFT OUTER JOIN
		CodeDetail cd
	ON	cd.Instance = c.Status

WHERE	1 = 1 '

if(@sFMID <> '')
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND (c.FMID = ''' + @sFMID + ''' OR FieldManager.DMID = ''' + @sFMID + ''') '
END

IF(@iAccountID <> 0)
BEGIN
	SET @sqlStatement = @sqlStatement + /*' AND c.ShipToAccountId = ' + convert(nvarchar, @iAccountID) +*/ ' AND c.BillToAccountId = ' + convert(nvarchar, @iAccountID)
END

if(@iCampaignID <> 0)
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND c.ID = ' + convert(nvarchar, @iCampaignID)
END

if(@sName <> '')
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND a.Name LIKE ''' + @sName + '%'' '
END

if(@sCity <> '')
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND AddrBill.City = ''' + @sCity + ''' '
END

if(@sState <> '')
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND AddrBill.StateProvince = ''' + @sState + ''' '
END

if(@sZip <> '')
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND AddrBill.postal_code = ''' + @sZip + ''' '
END

if(@sFMFirstName <> '')
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND FieldManager.FirstName = ''' + @sFMFirstName + ''' '
END

if(@sFMLastName <> '')
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND FieldManager.LastName = ''' + @sFMLastName + ''' '
END

if(@iFiscalYear <> 0)
BEGIN
	SELECT @StartDate = '07/01/'+str(@iFiscalYear - 1)
	SELECT @EndDate = '06/30/'+str(@iFiscalYear)

	SET @sqlStatement = @sqlStatement + ' AND c.StartDate >= ''' + @StartDate + ''' AND c.EndDate < ''' + @EndDate + ''' '
END

if (@sFromSupplyShipment <> '')
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND SuppliesDeliveryDate >= ''' + @sFromSupplyShipment + ''' AND SuppliesDeliveryDate <= ''' + @sToSupplyShipment + ''' '

END

if(@iSupplyGenerated <> 0)
begin
		
	if(@iSupplyGenerated =1)
	begin
		SET @sqlStatement = @sqlStatement + ' AND FSOrderRecCreated = 1 '

	end
	else
	begin
		SET @sqlStatement = @sqlStatement + ' AND c.Status = 37002 AND c.FSRequired = 1 AND FSOrderRecCreated = 0 '
	end
end

set @sqlStatement = @sqlStatement + 
'



declare @id int
declare @progid int
declare aSetProg  cursor for select #List.campaignid, programid from campaignprogram,#List where campaignprogram.campaignid =#List.CampaignID and DeletedTF=0 AND OnlineOnly = 0
			
open aSetProg
fetch next from aSetProg into @id, @progid
WHILE(@@fetch_status <> -1)
begin
		
	update #List  set Programs = Programs + '' '' + Abr from #List,CampaignProgram,Program where 
			#List.CampaignID = @id and CampaignProgram.ProgramID=@ProgID and Program.ID = @ProgID
			and campaignprogram.campaignid=@id

	fetch next from aSetProg into @id, @progid

end
close aSetProg
deallocate aSetProg


select * from #list order by StartDate Desc
drop table #list
'

-------------------------------
--- Update the programs run ---
-------------------------------
/*DECLARE @CampaignIDtoUpdate int
DECLARE @c_IsStaffOrder bit
DECLARE @CurrentProgram varchar(255)

DECLARE CampaignsCursor CURSOR FOR 
SELECT CampaignID, IsStaffOrder FROM #Campaigns
OPEN CampaignsCursor
FETCH NEXT FROM CampaignsCursor INTO @CampaignIDtoUpdate, @c_IsStaffOrder

--loop through #Campaigns
WHILE(@@fetch_status <> -1)
BEGIN
	DECLARE ProgramsCursor CURSOR FOR 
	select 	c.[Name] + ';' AS ProgramName
	from 	CampaignProgram a left join Program c on a.ProgramID = c.ID
	WHERE	a.CampaignID = @CampaignIDtoUpdate AND a.DeletedTF <> 1
	ORDER BY a.ProgramID ASC

	OPEN ProgramsCursor
	FETCH NEXT FROM ProgramsCursor INTO @CurrentProgram
	
	--loop through programs for this @CampaignIDtoUpdate
	WHILE(@@fetch_status <> -1)
	BEGIN
		UPDATE #Campaigns
		SET [Programs] = [Programs] + @CurrentProgram
		WHERE CampaignID = @CampaignIDtoUpdate
		
		--get the next program to update
		--this campaign with
		FETCH NEXT FROM ProgramsCursor INTO @CurrentProgram
	END
	--all done with this campaign
	CLOSE ProgramsCursor
	DEALLOCATE ProgramsCursor

	--pretend that "IsStaffOrder" is a program 
	UPDATE #Campaigns
	SET [Programs] = 'Staff Order;' + [Programs]
	WHERE CampaignID = @CampaignIDtoUpdate AND @c_IsStaffOrder = 1

	--GET THE NEXT campaign to update
	FETCH NEXT FROM CampaignsCursor INTO @CampaignIDtoUpdate, @c_IsStaffOrder
END
--all done updating the campaigns
CLOSE CampaignsCursor
DEALLOCATE CampaignsCursor

--final select, then toss the temp table
SET NOCOUNT OFF

SELECT 
    [CampaignID]
  , CONVERT (varchar(10), StartDate, @DateFormat) 		AS [StartDate]
  , CONVERT (varchar(10), EndDate, @DateFormat) 		AS [EndDate]
  , [FMID]   , [Status]
  , cd.Description AS StatusName
  , [Lang] 
--  [Type], 
--  case [IsStaffOrder]
--    when 0 then 'N'
--    when 1 then 'Y'
--   end as [IsStaffOrder],
  , [Programs]
  , CONVERT (varchar(10), OrderReceivedDate, @DateFormat) 	AS [OrderReceivedDate]
  , CONVERT (varchar(10), MainOrderFulfilled, @DateFormat) 	AS [MainOrderFulfilledDate]
  , CONVERT (varchar(10), SupplyFulfilled, @DateFormat) 	AS [SupplyFulfilledDate]
FROM			#Campaigns
LEFT OUTER JOIN	CodeDetail cd
	ON	cd.Instance = #Campaigns.Status
ORDER BY  
  DATEPART(yyyy, StartDate) DESC, 
  DATEPART(mm, StartDate) DESC, 
  DATEPART(dd, StartDate) DESC, 
  --DATEPART(yyyy, EndDate) DESC, 
  --DATEPART(mm, EndDate) DESC,
  --DATEPART(dd, EndDate) DESC, 
  CampaignID DESC

DROP TABLE #Campaigns*/
--print @sqlStatement
EXEC(@sqlStatement)
GO

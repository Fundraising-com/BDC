USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectSearchShipment]    Script Date: 06/07/2017 09:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 11/08/06 Madina : added Fiscal Year 
CREATE    PROCEDURE [dbo].[pr_SelectSearchShipment]

@OrderID int= 0,
@FMID varchar(4)  = '',
@FMFirstName nvarchar(50) = '',
@FMLastName nvarchar(50) = '',
@CampaignID int = 0,
@ShipmentID int =0,
@AccountID int =0,
@GroupName nvarchar(50) ='',
@ShipmentDateFrom datetime ='',
@ShipmentDateTo datetime ='',
@OrderDateFrom datetime ='',
@OrderDateTo datetime ='',
@FiscalYear int = 0,
@Query varchar(4000) output


AS
DECLARE @sqlStatement  nvarchar(2048),
	    @firstCondition bit

SET @firstCondition = 1

SET @sqlStatement = 	'select  orderid, ' +
			'orderdate, ' +
			'orderstatus, ' +
			'ordertype, ' +
			'orderqualifier, ' +
			'campaignid, ' +
			'groupname, ' +
			'shipmentid, ' +
			'shipmentdate, ' +
			'expecteddeliverydate ' +
			'from vw_GetShipmentInfo  '
	
if(@OrderID <> 0)
begin
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' orderId =  ' + convert(nvarchar,@OrderID)
	set @firstCondition = 0
end

if(@FMID <> '')
begin
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' fmId =  ' + convert(nvarchar,@FMID)
	set @firstCondition = 0
end

if(@AccountID <> 0)
begin
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' accountId =  ' + convert(nvarchar,@AccountID)
	set @firstCondition = 0
end

if(@CampaignID <> 0)
begin
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' campaignId =  ' + convert(nvarchar,@CampaignID)
	set @firstCondition = 0
end

if(@ShipmentID <> 0)
begin
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' shipmentId =  ' + convert(nvarchar,@ShipmentID)
	set @firstCondition = 0
end

if(@FMFirstName <> '')
begin
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' fmfirstname LIKE '''+ @FMFirstName + '%'''
	set @firstCondition = 0
end

if(@FMLastName <> '')
begin
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' fmlastname LIKE '''+ @FMLastName + '%'''
	set @firstCondition = 0
end

if(@GroupName <> '')
begin
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' groupname LIKE '''+ @GroupName + '%'''
	set @firstCondition = 0
end


if(@ShipmentDateFrom <> '' and @ShipmentDateTo <>'' )
BEGIN
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' ShipmentDate between '''+  convert(nvarchar,@ShipmentDateFrom,101) + ''''   
	set @sqlStatement = @sqlStatement + ' and ' 
	set @sqlStatement = @sqlStatement + ''''+ convert(nvarchar, @ShipmentDateTo,101)  +''''
	set @firstCondition = 0
END

if(@OrderDateFrom <> '' and @OrderDateTo <>'' )
BEGIN
	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + ' OrderDate between '''+  convert(nvarchar,@OrderDateFrom,101) + ''''   
	set @sqlStatement = @sqlStatement + ' and ' 
	set @sqlStatement = @sqlStatement + ''''+ convert(nvarchar, @OrderDateTo,101)  +''''
	set @firstCondition = 0
END

if(@FiscalYear <> 0)
BEGIN
	DECLARE @StartDate varchar(10)
	DECLARE @EndDate varchar(10)

	SET @StartDate = '07/01/' + CAST(@FiscalYear - 1 AS varchar)
	SET @EndDate = '06/30/' + CAST(@FiscalYear AS varchar)

	if @firstCondition = 0
		set @sqlStatement = @sqlStatement + ' and '
	else
		set @sqlStatement = @sqlStatement + ' where '

	set @sqlStatement = @sqlStatement + 
	' OrderDate BETWEEN ''' +  @StartDate + ''' AND ''' + @EndDate + ''' '
	set @firstCondition = 0
END

set @Query = @sqlStatement
exec (@sqlStatement)
GO

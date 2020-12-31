USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectSearchOrder]    Script Date: 06/07/2017 09:20:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[pr_SelectSearchOrder] 

--FM
@FMID varchar(4) = '',
@FMFirstName nvarchar(50) ='',
@FMLastName nvarchar(50) ='',
-- Fiscal Year
@FiscalYear int = 0,
--ShipTo
@ShipToGroupID int =0,
@ShipToGroupName nvarchar(50) ='',
@ShipToAddress nvarchar(50) ='',
@ShipToCity nvarchar(50) ='',
@ShipToProvince nvarchar(50) ='',
@ShipToPostalCode nvarchar(50)='',
--BillTo
@BillToGroupID int=0,
@BillToGroupName nvarchar(50)='',
@BillToAddress nvarchar(50)='',
@BillToCity  nvarchar(50)='',
@BillToProvince nvarchar(50)='',
@BillToPostalCode nvarchar(50)='',
--Campaign
@CampaignID int =0,
@OrderID int=0,
@QualifierID int= 0,
@Query  nvarchar(4000) output,
@OrderTypeID int= 0,
@OrderStatusID int = 0,
@StaffOrder int =-1,
--Date
@FromDate datetime = '', -- TODO date in the query
@ToDate datetime = ''



AS
declare @sqlStatement nvarchar(4000)
 --'select * --OrderId,Status,OrderDate,OrderType,ProblemSolver,ShipGroupID,ShipGroupName,ShipCity,PostalCode,CampaignID,InvoiceDate,InvoiceID,GroupID,GroupName,BillCity,PostalCode 
set @sqlStatement =  'select distinct  OrderID,'+
	'cdStatus.description as status,'+
	'accBillTo.name as BillToGroupName,'+
	'addrBillTo.street1 as BillToGroupAddress1,'+
	'addrBillTo.city as BillToGroupCity,'+
	'addrBillTo.postal_code as BillToGroupPostalCode,'+
	'addrBillTo.stateProvince  as BillToGroupProvince,'+
	'accShipTo.name as ShipToGroupName,'+
	'addrShipTo.street1 as ShipToGroupAddress1,'+
	'addrShipTo.city as ShipToGroupCity,'+
	'addrShipTo.stateProvince  as ShipToGroupProvince,'+
	'addrShipTo.postal_code as ShipToGroupPostalCode,'+
	'[Date],'+
	'cdOrderType.description as ordertype,'+
	'cdQualifierName.description as QualifierName,'+
	'cp.BillToAccountID,'+
	'cp.ShipToAccountID,'+
	'cp.ID as CampaignID'+


' from '+
	'batch bc,qspcanadacommon..Campaign cp,'+
 	'Codedetail cdStatus,'+
	'qspcanadacommon..Caccount accBillTo,'+
	'qspcanadacommon..Caccount accShipTo,'+
	'Codedetail cdOrderType,'+
	'Codedetail cdQualifierName,'+
	'qspcanadacommon..address addrBillTo,'+
	'qspcanadacommon..address addrShipTo,'+
	'qspcanadacommon..FieldManager fm'+
	
' where '+
	'bc.campaignid = cp.id and '+
 	'bc.statusinstance = cdStatus.instance and '+
	'accBillTo.id = cp.BillToAccountID and '+
	'accShipTo.id = cp.ShipToAccountID and '+
	'addrBillTo.AddressListID = accBillTo.AddressListID and '+
	'addrShipTo.AddressListID = accShipTo.AddressListID and '+
	'cdOrderType.instance = bc.ordertypecode and '+
	'cdQualifierName.instance = bc.OrderQualifierID '+
	'and cp.FMID = fm.FMID '+
	'and addrShipTo.address_type = 54001 '+
	'and addrBillTo.address_type = 54002 '


if(@FMID <> '')
BEGIN

	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + 'fm.FMID  =  '+ convert(nvarchar, @FMID)
	
END


if(@FMFirstName  <>'')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + 'fm.FirstName  like  '''+ @FMFirstName + '%'''
	
END

if(@FMLastName  <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + 'fm.LastName  like  '''+ @FMLastName  + '%'''

END



if(@ShipToGroupID  <>0)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + 'cp.ShipToAccountID =  '+ convert(nvarchar, @ShipToGroupID)
	
END

if(@ShipToGroupName  <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + ' accShipTo.Name like  '''+ @ShipToGroupName  +  '%'''
	
END

if(@ShipToAddress   <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + ' addrShipTo.Street1 like  ''%'+ @ShipToAddress  +  '%'''
	
END

if(@ShipToCity <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + 'addrShipTo.City  like  '''+ @ShipToCity  +  '%'''
	
END

if(@ShipToProvince  <>'')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + 'addrShipTo.stateProvince like  '''+ @ShipToProvince   +  '%'''
	
END

if(@ShipToPostalCode  <>'')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + 'addrShipTo.postal_code  =  '''+ @ShipToPostalCode   + ''''
	

END
--BillTo

if(@BillToGroupID <> 0)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + 'cp.BillToAccountID  =  '+ convert(nvarchar, @BillToGroupID)
	
END

if(@BillToGroupName  <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + ' accBillTo.Name like  '''+ @BillToGroupName   +  '%'''
	
END

if(@BillToAddress <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + 'addrBillTo.Street1  like  ''%'+  @BillToAddress   +  '%'''
	

END

if(@BillToCity   <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + ' addrBillTo.City like  '''+ @BillToCity  +  '%'''
	
END

if(@BillToProvince  <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + ' addrBillTo.stateProvince =  '''+  @BillToProvince + ''''
	
END

if(@BillToPostalCode  <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement + 'addrBillTo.postal_code  =  '''+@BillToPostalCode   + ''''
	
END

--Campaign
if(@CampaignID  <> 0)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + ' bc.CampaignID =  '+ convert(nvarchar, @CampaignID)
	
END

if(@OrderID <> 0)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + ' bc.OrderID =  '+ convert(nvarchar, @OrderID)
	
END

if(@QualifierID  <> 0)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + 'bc.OrderQualifierID  =  ' +convert(nvarchar,  @QualifierID)  

END

if(@FromDate  <> ''  and  @ToDate <> '')
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + ' bc.DateCreated  between  ''' + convert(nvarchar, @FromDate,101) + ''''   
	set @sqlStatement = @sqlStatement + ' and ' 
	set @sqlStatement = @sqlStatement + ''''+ convert(nvarchar, @ToDate,101)  +''''

END

if(@OrderTypeID  <> 0)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + 'bc.ordertypecode  =  ' +convert(nvarchar,  @OrderTypeID)  

END

/*

if(@OrderTypeID  <> 0)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + 'bc.ordertypecode  =  ' +convert(nvarchar,  @OrderTypeID)  

END
*/
if(@OrderStatusID  <> 0)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + 'bc.OriginalStatusInstance  =  ' +convert(nvarchar,  @OrderStatusID)  

END

if(@StaffOrder  <> -1)
BEGIN
	
	set @sqlStatement = @sqlStatement + ' and '
	set @sqlStatement = @sqlStatement  + 'bc.IsStaffOrder  =  ' +convert(nvarchar,  @StaffOrder)  

END

IF (@FiscalYear <> 0)
BEGIN
		DECLARE @StartDate varchar(10)
	DECLARE @EndDate varchar(10)

	SET @StartDate = '07/01/' + CAST(@FiscalYear - 1 AS varchar)
	SET @EndDate = '06/30/' + CAST(@FiscalYear AS varchar)

	set @sqlStatement = @sqlStatement + 
	'AND bc.Date BETWEEN ''' +  @StartDate + ''' AND ''' + @EndDate + ''' '
END

set @Query = @sqlStatement
exec (@sqlStatement)
GO

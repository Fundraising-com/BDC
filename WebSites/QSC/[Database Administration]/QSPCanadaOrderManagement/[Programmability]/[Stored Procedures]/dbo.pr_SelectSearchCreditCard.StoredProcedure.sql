USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectSearchCreditCard]    Script Date: 06/07/2017 09:20:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[pr_SelectSearchCreditCard]

@iOrderID int= 0,
@iCustomerOrderHeaderInstance int =0,
@sFirstName nvarchar(50) = '',
@sLastName nvarchar(50) = '',
@iCreditCardNumber varchar(16) =  '',
@sAuthorizationCode nvarchar(125) = '',
@sReturnCode nvarchar(50) ='',
@FiscalYear int = 0,
@Query varchar(4000) output


AS
DECLARE @sqlStatement  nvarchar(2048)

SET @sqlStatement = 	' select * from vw_GetCreditCardInfo where 1 = 1   ' 
	
if(@iOrderID <> 0)
	set @sqlStatement = @sqlStatement + ' and orderId =  ' + convert(nvarchar,@iOrderID)

if(@FiscalYear <> 0)
BEGIN
	DECLARE @StartDate varchar(10)
	DECLARE @EndDate varchar(10)

	SET @StartDate = '07/01/' + CAST(@FiscalYear - 1 AS varchar)
	SET @EndDate = '06/30/' + CAST(@FiscalYear AS varchar)

	set @sqlStatement = @sqlStatement + 
	'AND OrderDate BETWEEN ''' +  @StartDate + ''' AND ''' + @EndDate + ''' '
END

if(@iCustomerOrderHeaderInstance <> 0)
	set @sqlStatement = @sqlStatement + ' and customerorderheaderinstance =  ' + convert(nvarchar,@iCustomerOrderHeaderInstance)

if(@sFirstName <> '')
	set @sqlStatement = @sqlStatement + ' and FirstName  like  '''+ @sFirstName + '%'''


if( @sLastName <> '')
	set @sqlStatement = @sqlStatement + ' and LastName  like  '''+ @sLastName + '%'''

if(@iCreditCardNumber <> '')
	set @sqlStatement = @sqlStatement + ' and creditcardnumber =  '''+ convert(nvarchar,@iCreditCardNumber)  + ''''

if(@sAuthorizationCode <> '')
	set @sqlStatement = @sqlStatement + ' and AuthorizationCode  like  '''+ @sAuthorizationCode + '%'''

if(@sReturnCode <> '')
	set @sqlStatement = @sqlStatement + ' and ReturnCode  like  '''+ @sReturnCode + '%'''

set @sqlStatement = @sqlStatement + ' ORDER BY OrderID'


set @Query = @sqlStatement
exec (@sqlStatement)
GO

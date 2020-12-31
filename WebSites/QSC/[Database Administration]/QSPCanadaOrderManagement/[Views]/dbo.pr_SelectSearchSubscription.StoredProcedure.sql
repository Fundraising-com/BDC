USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectSearchSubscription]    Script Date: 06/07/2017 09:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectSearchSubscription] 

-- PARAMETERS
@CustomerOrderHeaderInstance int =0,
@RecipientFirstName nvarchar(50) ='',
@RecipientLastName nvarchar(50) ='',
@City nvarchar(50) ='',
@Province nvarchar(50) ='',
@PostalCode varchar(20)='',--@PostalCode nvarchar(50)='',
@ProductCode varchar(10)='',
@Title nvarchar(50)='',
@InternetOrderID int = 0,
@FromDateSub datetime = '',
@ToDateSub datetime =  '' ,
@RemitID int = 0,
@RemitDate datetime = '',
@ParticipantLastName nvarchar(50)='',
@ParticipantFirstName nvarchar(50)='',
@TransID int= 0,
@SearchCategory int = 0,
@FiscalYear int = 0,

@List nvarchar(4000) = '',
@Query nvarchar(4000) output
AS
DECLARE @sqlStatement nvarchar(4000)

set @sqlStatement =   'SELECT OrderID, ' +
			'CustomerInstance, ' + 
			'CustomerOrderHeaderInstance, ' +
			'TransID, ' +
			'StudentLastName, ' +
			'StudentFirstName, ' +
			'RecipientLastName, ' +
			'RecipientFirstName, ' +
			'CustomerLastName, ' +
			'CustomerFirstName, ' +
			'CustomerCity, ' +
			'CustomerState, ' +
			'CustomerZip, ' +
			'TitleCode, ' +
			'Title, ' +
			'IssuesSent, ' +
			'CatalogPrice, ' +
			'OverrideProduct, ' +	
			'Status, ' +
			'RemitBatchID, ' +
			'RemitBatchDate, ' +
			'CampaignID, '+
			'OrderStatus, '+
			' DateSub, '+
			' QualifierName, '+
			' AccountID, '+
			'ProductType, '+
			'SubscriptionDate '

if(@SearchCategory = 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' FROM  vw_GetSubInfo WHERE 1=1 '
END
else
BEGIN
	set @sqlStatement = @sqlStatement + ' FROM  vw_GetAllProductsInfo WHERE '
	if(@SearchCategory = 1)
	BEGIN
		set @sqlStatement = @sqlStatement + ' ProductTypeInstance = 46001 '
	END
	else
	BEGIN
		set @sqlStatement = @sqlStatement + ' (ISNULL(ProductTypeInstance, 0) <> 46001 OR (ProductTypeInstance = 46001 AND LEFT(TitleCode, 2) = ''DG'')) '
	END
END


if(@List <> '')
BEGIN

	set @sqlStatement = @sqlStatement + ' and ' + @List

	if(@ProductCode <> '')
		BEGIN
			set @sqlStatement = @sqlStatement +  ' and TitleCode = '''+ convert(nvarchar,@ProductCode) + ''''
		END

		if(@Title<> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and Title LIKE '''+ @Title + '%'''
		END

END
else
BEGIN
		-- 06/08/2006 - Ben : Tweak to use the index instead of searching on the view's computed columns
		if(@SearchCategory <> 0 AND @RecipientFirstName <> '')
		BEGIN
			if(charindex('%', @RecipientFirstName) = 0)
			BEGIN
				set @sqlStatement = @sqlStatement + ' and RecipientName LIKE ''' + @RecipientFirstName + '%'' '
			END
			else
			BEGIN
				set @sqlStatement = @sqlStatement + ' and RecipientName LIKE ''' + @RecipientFirstName + ''' '
			END
		END

		if (@RecipientFirstName <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and RecipientFirstName LIKE  '''+ @RecipientFirstName + ''''
		END

		if(@RecipientLastName <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and RecipientLastName LIKE  '''+ @RecipientLastName + ''''
		END

		if(@City <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and CustomerCity LIKE  '''+ @City + '%'''
		END

		if(@Province <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and CustomerState =  '''+ @Province + ''''
		END

		if(@PostalCode <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and CustomerZip =  ''' + @PostalCode + ''''
		END

		if(@ProductCode <> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and TitleCode =  '''+ convert(nvarchar, @ProductCode) + ''''
		END

		if(@Title<> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and Title LIKE '''+ @Title + '%'''
		END

		if(@InternetOrderID <> 0)
		BEGIN
			set @sqlStatement = @sqlStatement + ' and (InternetOrderID =  '''+ convert(nvarchar, @InternetOrderID) + ''' OR  InternetOrderID =  ''' + convert(nvarchar, FLOOR(@InternetOrderID / 10)) + ''') '
		END

		if(@FromDateSub<> '' and @ToDateSub<>'' )
		BEGIN
			set @sqlStatement = @sqlStatement + ' and DateSub between '''+  convert(nvarchar,@FromDateSub,101) + ''''   
			set @sqlStatement = @sqlStatement + ' and ' 
			set @sqlStatement = @sqlStatement + ''''+ convert(nvarchar, @ToDateSub,101)  +''''
		END

		if(@RemitID <> 0)
		BEGIN
			set @sqlStatement = @sqlStatement + ' and RunID =  '''+ convert(nvarchar, @RemitID) + ''''
		END

		if(@RemitDate<> ' ')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and RemitBatchDate =  '''+ convert(nvarchar, @RemitDate) + ''''
		END

		if(@ParticipantLastName<> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and StudentLastName LIKE  '''+ @ParticipantLastName + '%'''
		END

		if(@ParticipantFirstName<> '')
		BEGIN
			set @sqlStatement = @sqlStatement + ' and StudentFirstName LIKE  '''+ @ParticipantFirstName + '%'''
		END
		if(@CustomerOrderHeaderInstance <>0)
		BEGIN
			set @sqlStatement = @sqlStatement + ' and CustomerOrderHeaderInstance = '+ convert(nvarchar, @CustomerOrderHeaderInstance)
		END

		if(@TransID <> 0)
		BEGIN
			set @sqlStatement = @sqlStatement + ' and TransID = '+ convert(nvarchar,@TransID)
		END

		--JM - 02/02/07 - Don't return soft deleted COD's
		set @sqlStatement = @sqlStatement + ' and DelFlag = 0 '

		if(@FiscalYear <> 0)
		BEGIN
			DECLARE @StartDate varchar(10)
			DECLARE @EndDate varchar(10)
		
			SET @StartDate = '07/01/' + CAST(@FiscalYear - 1 AS varchar)
			SET @EndDate = '06/30/' + CAST(@FiscalYear AS varchar)
		
			set @sqlStatement = @sqlStatement + 
			'AND SubscriptionDate BETWEEN ''' +  @StartDate + ''' AND ''' + @EndDate + ''' '
		END
END

set @sqlStatement = @sqlStatement + ' ORDER BY RecipientLastName, RecipientFirstName, CustomerOrderHeaderInstance, TransID' 

set @Query = @sqlStatement
exec (@sqlStatement)
GO

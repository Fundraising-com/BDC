USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectSearchProduct]    Script Date: 06/07/2017 09:20:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectSearchProduct] 

-- PARAMATERS
@iProductType int ,
@List nvarchar(4000),
@Query nvarchar(4000) output

AS

DECLARE @sqlStatement nvarchar(4000)


set @sqlStatement =   'SELECT OrderID,'+
	'ProductType,'+
	'CatalogCode,'+
	' ItemDescription,'+
	'QuantityOrdered,'+
	'PriceEntered,'+
	'CatalogPrice,'+
	'OverrideCode,'+
	'OrderItemStatus,'+
	'ShipDate,'+
	'PurchasedBy,'+
	'StudentFirstName,'+
	'StudentLastName'+
   		' FROM  vw_GetProductInfo ' +
	          ' WHERE ProductType= ' + convert(nvarchar,@iProductType) + ' '



if(@List <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' and ' +@List
END


set @Query = @sqlStatement
exec (@sqlStatement)
GO

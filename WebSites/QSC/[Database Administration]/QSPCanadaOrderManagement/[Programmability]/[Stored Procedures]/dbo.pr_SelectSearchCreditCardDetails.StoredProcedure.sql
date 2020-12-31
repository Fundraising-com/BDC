USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectSearchCreditCardDetails]    Script Date: 06/07/2017 09:20:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectSearchCreditCardDetails] 

@List varchar(4000),
@Query varchar(4000) output

AS
declare @sqlStatement varchar(4000)

set @sqlStatement = ' SELECT  cod.CustomerOrderHeaderInstance, '+
	' cod.TransID, '+
	'  cdp.Description AS ProductType, '+
	'  cod.ProductCode, '+
	'  cod.ProductName, '+
	'  cod.Quantity, '+			       --MS March 25, 2007
	'  convert(numeric(10,2),CASE ISNULL(cod.CatalogPrice, 0.00) WHEN 0.00 THEN cod.Price ELSE cod.CatalogPrice END) as Price, '+ --convert(numeric(10,2),cod.Price) as Price, '+ --' --'  convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as Price, '+
	--'  convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) AS Price,  '+
	'  cod.Recipient AS RecipientName, '+
	'  cds.Description AS Status,  '+
	' cod.CreationDate As SubscriptionDate '+
	  
 '  FROM  CustomerOrderDetail cod, '+
	'  CodeDetail cdp, '+
	'  CodeDetail cds, '+
	'  CustomerOrderHeader coh, '+
	'  QSPCanadaCommon..Campaign ca '+
' WHERE  coh.Instance = cod.CustomerOrderHeaderInstance AND '+
' ca.ID = coh.CampaignID AND '+
' cod.ProductType = cdp.Instance AND '+
' cod.DelFlag = 0 AND '+
' cod.StatusInstance = cds.Instance AND '+ @List

set @Query = @sqlStatement
exec(@sqlStatement)
GO

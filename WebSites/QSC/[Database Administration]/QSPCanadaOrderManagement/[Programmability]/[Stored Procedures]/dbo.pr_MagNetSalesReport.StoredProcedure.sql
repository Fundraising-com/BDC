USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_MagNetSalesReport]    Script Date: 06/07/2017 09:20:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_MagNetSalesReport] 

@DateFrom	datetime	= '1995-01-01',
@DateTo	datetime	= '2010-01-01',
@zFMID	varchar(4)	= ''

AS

DECLARE @sqlStatement nvarchar(4000)

SET @sqlStatement = 
'SELECT 	a.ID as GroupID,
		a.Name as GroupName,
		c.Id as CampaignID,
		c.FMID as FMID, 
		fm.FirstName + '' '' + fm.LastName as FMName,
		count(cod.Price) as SubsOrdered,
		coalesce(sum(cod.Price),0) as GrossSales,
		case addr.stateProvince when ''NB'' then coalesce(sum(cod.Price/ 1.14),0) when ''NS'' then coalesce(sum(cod.Price/ 1.14),0) when ''NL'' then coalesce(sum(cod.Price/ 1.14),0) when ''QC'' then coalesce(sum(cod.Price/ 1.1395),0) else coalesce(sum(cod.Price/ 1.06),0) end as NetSales,
		case addr.stateProvince when ''NB'' then cast(coalesce(sum(cod.Price/ 1.14),0)* 0.37 as numeric(10, 2)) when ''NS'' then cast(coalesce(sum(cod.Price/ 1.14),0)* 0.37 as numeric(10, 2)) when ''NL'' then cast(coalesce(sum(cod.Price/ 1.14),0)* 0.37 as numeric(10, 2)) when ''QC'' then cast(coalesce(sum(cod.Price/ 1.1395),0)* 0.37 as numeric(10, 2)) else cast(coalesce(sum(cod.Price/ 1.06),0)* 0.37 as numeric(10, 2)) end as CampaignProfit,
		case addr.stateProvince when ''NB'' then coalesce(sum(cod.Price/ 1.14),0) - cast(coalesce(sum(cod.Price/ 1.14),0)* 0.37 as numeric(10, 2)) 
					when ''NS'' then coalesce(sum(cod.Price/ 1.14),0) - cast(coalesce(sum(cod.Price/ 1.14),0)* 0.37 as numeric(10, 2)) 
					when ''NL'' then coalesce(sum(cod.Price/ 1.14),0) - cast(coalesce(sum(cod.Price/ 1.14),0)* 0.37 as numeric(10, 2)) 
					when ''QC'' then coalesce(sum(cod.Price/ 1.1395),0) - cast(coalesce(sum(cod.Price/ 1.1395),0)* 0.37 as numeric(10, 2)) 
					else coalesce(sum(cod.Price/ 1.06),0) - cast(coalesce(sum(cod.Price/ 1.06),0)* 0.37 as numeric(10, 2)) end as QSPRevenue
		
	
FROM		QSPCanadaCommon..CAccount a,
		QSPCanadaCommon..Campaign c,
		QSPCanadaCommon..FieldManager fm,
		QSPCanadaOrderManagement..Batch b,
		QSPCanadaOrderManagement..CustomerOrderHeader coh,
		QSPCanadaOrderManagement..CustomerOrderDetail cod,
		QSPCanadaCommon..Address addr
		--QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh

WHERE	a.id = c.BillToAccountID
		and c.FMID = fm.FMID
		and b.CampaignID = c.ID
		and b.ID = coh.OrderBatchID
		and b.Date = coh.OrderBatchDate
		and coh.Instance = cod.CustomerOrderHeaderInstance
		and b.ordertypecode = 41009
		and a.addresslistid = addr.addresslistid
		and addr.address_type=54001
		--and cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
		--and cod.TransID = codrh.TransID
		--and codrh.DateChanged between ''' + convert(nvarchar, @DateFrom,101) + '''   and ''' + convert(nvarchar, @DateTo,101)  + '''
		--and codrh.status in(''42000'',''42001'')
		and exists(select CustomerOrderHeaderInstance, TransID from customerorderdetailremithistory x where x.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and x.TransID = cod.TransID and x.status in(42000, 42001) and DateChanged between ''' + convert(nvarchar, @DateFrom,101) + '''   and  + ''' + convert(nvarchar, @DateTo,101) + '''  ) '

IF(@zFMID <> '')
BEGIN
	SET @sqlStatement = @sqlStatement + ' AND fm.FMID = ''' + @zFMID + ''' '
END

SET @sqlStatement = @sqlStatement +
'GROUP BY 	a.Id,
		a.Name,
		c.Id,
		c.FMID, 
		fm.FirstName,
		fm.LastName,
		addr.stateProvince
--having cast((coalesce(sum(cod.Price),0) - coalesce(sum(cod.Tax),0)) * 0.37 as numeric(10, 2))  > 100
ORDER BY FM.lastname, fm.firstname, a.id '

EXEC(@sqlStatement)
GO

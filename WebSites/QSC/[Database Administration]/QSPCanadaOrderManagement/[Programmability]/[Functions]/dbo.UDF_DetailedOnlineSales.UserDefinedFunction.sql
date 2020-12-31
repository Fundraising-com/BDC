USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_DetailedOnlineSales]    Script Date: 06/07/2017 09:21:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_DetailedOnlineSales] (@dFrom datetime, @dTo datetime)  
RETURNS TABLE AS  
RETURN(
SELECT b.campaignid,
	s.LastName + ', ' + s.FirstName as ParticipantName,
	cod.ProductCode as TitleCode,
	cod.ProductName as MagazineTitleName,
	count(cod.Price) as TotalNumberOfSubs,
	coalesce(sum(cod.Price),0) as TotalSalesAmount,
	--cast((coalesce(sum(cod.Net),0)) * 0.37 as numeric(10, 2)) as TotalProfitEarned
	--New Tax rates July 01, 2006 MS July 04, 2006
	case addr.stateProvince when 'NB' then cast(coalesce(sum(cod.Price/ 1.14),0)* 0.37 as numeric(10, 2)) when 'NS' then cast(coalesce(sum(cod.Price/ 1.14),0)* 0.37 as numeric(10, 2)) when 'NL' then cast(coalesce(sum(cod.Price/ 1.14),0)* 0.37 as numeric(10, 2)) when 'QC' then cast(coalesce(sum(cod.Price/ 1.1395),0)* 0.37 as numeric(10, 2)) else cast(coalesce(sum(cod.Price/ 1.06),0)* 0.37 as numeric(10, 2)) end as TotalProfitEarned

FROM
	QSPCanadaOrderManagement..Student s,
--	QSPCanadaOrderManagement..Teacher t,
--	QSPCanadaCommon..CAccount ca,
--	QSPCanadaCommon..Campaign c,
	QSPCanadaOrderManagement..Batch b,
	QSPCanadaOrderManagement..CustomerOrderHeader coh,
	QSPCanadaOrderManagement..CustomerOrderDetail cod,
	qspcanadacommon..campaign c,
	qspcanadacommon..caccount a,
	qspcanadacommon..address addr  
	--QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh

WHERE
--	s.TeacherInstance = t.Instance
--	AND	ca.ID = t.AccountID
--	AND	c.BillToAccountID = ca.ID
--	AND	b.CampaignID = c.ID
--	AND	
	b.Date = coh.OrderBatchDate
	AND 	b.id = coh.OrderBatchID
	AND	coh.StudentInstance = s.Instance
	AND	cod.CustomerOrderHeaderInstance = coh.Instance
	AND	b.OrderQualifierID = 39009
	and	b.campaignid=c.id
	and 	c.billtoaccountid= a.id
	and 	a.addresslistid = addr.addresslistid
	and 	addr.address_type=54001
	--AND	c.ID = @iCampaignID
		--and cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
		--and cod.TransID = codrh.TransID
		--and codrh.DateChanged between convert(nvarchar, '2004-08-01',101)   and convert(nvarchar, '2004-11-19',101)  
		--and codrh.status in('42000','42001')
		--and codrh.DateChanged between @dFrom and @dTo
		and exists(select CustomerOrderHeaderInstance, TransID from customerorderdetailremithistory x where x.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and x.TransID = cod.TransID and status in(42000, 42001) and DateChanged between convert(nvarchar, @dFrom,101)   and convert(nvarchar, @dTo,101)  )
	

GROUP BY 
	b.campaignid,
	s.LastName, 
	s.FirstName, 
	cod.ProductCode, 
	cod.ProductName,
	addr.stateProvince
)
GO

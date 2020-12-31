USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_OnlineSalesByCampaign]    Script Date: 06/07/2017 09:21:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_OnlineSalesByCampaign]
(@dFrom datetime, @dTo datetime, @iOver100 int)  
RETURNS TABLE AS
RETURN
(
	select  	b.campaignid, 
		count(cod.Price) as totalunits,  
		sum(cod.Price) as totalamount, 
		--New tax rate Aug 01, 2006 MS July 04, 2006
		case addr.stateProvince when 'NB' then coalesce(sum(round(cod.Price/ 1.14,2)),0) when 'NS' then coalesce(sum(round(cod.Price/ 1.14,2)),0) when 'NL' then coalesce(sum(round(cod.Price/ 1.14,2)),0) when 'QC' then coalesce(sum(round(cod.Price/ 1.1395,2)),0) else coalesce(sum(round(cod.Price/ 1.06,2)),0) end as totalnet

	from 	CustomerOrderDetail cod, 
		CustomerOrderHeader coh, 
		batch b,
		qspcanadacommon..campaign c,
		qspcanadacommon..caccount a,
		qspcanadacommon..address addr
		--CustomerOrderDetailRemitHistory codrh
	where 	coh.Instance = cod.CustomerOrderHeaderInstance
		and coh.orderbatchid = b.id
		and coh.orderbatchdate = b.date
		and b.orderqualifierid='39009'
		and b.campaignid=c.id
		and c.billtoaccountid= a.id
		and a.addresslistid = addr.addresslistid
		and addr.address_type=54001
		--and cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
		--and cod.TransID = codrh.TransID
		--and codrh.DateChanged between convert(nvarchar, '2004-08-01',101)   and convert(nvarchar, '2004-11-19',101)  
		--and codrh.status in('42000','42001')
		--and codrh.DateChanged between @dFrom and @dTo
		and exists(select CustomerOrderHeaderInstance, TransID from customerorderdetailremithistory x where x.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and x.TransID = cod.TransID and status in(42000, 42001) and DateChanged between convert(nvarchar, @dFrom,101)   and convert(nvarchar, @dTo,101)  )
	group by b.campaignid, addr.stateProvince
	having	cast((sum(cod.Price) - sum(cod.Tax)) *.37 as numeric (10,2)) >= @iOver100 * 100
)
GO

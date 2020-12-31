USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_OnlineSalesByCampaign]    Script Date: 06/07/2017 09:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_OnlineSalesByCampaign] as
	select  b.campaignid, count(cod.Price) as totalunits,  sum(cod.Price) as totalamount, sum(cod.tax) as totaltaxes
	from 	CustomerOrderDetail cod, 
		CustomerOrderHeader coh, 
		batch b,
		CustomerOrderDetailRemitHistory codrh
	where 	coh.Instance = cod.CustomerOrderHeaderInstance
		and coh.orderbatchid = b.id
		and coh.orderbatchdate = b.date
		and b.orderqualifierid='39009'
		and cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
		and cod.TransID = codrh.TransID
		and codrh.DateChanged between convert(nvarchar, '2004-08-01',101)   and convert(nvarchar, '2004-11-19',101)  
		and codrh.status in('42000','42001')

	group by b.campaignid
GO

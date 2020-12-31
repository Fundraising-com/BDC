USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_MagSummary]    Script Date: 06/07/2017 09:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_MagSummary] as 
select  b.orderid, b.date, count(cod.Price) as units,  coalesce(sum(cod.Price), 0) as amount
	from CustomerOrderDetail cod, CustomerOrderHeader coh, batch b
	where coh.Instance = cod.CustomerOrderHeaderInstance
	AND cod.ProductType =46001
--	and cod.statusinstance in(507,508)
	and coh.orderbatchid = b.id
	and coh.orderbatchdate = b.date
group by b.orderid, b.date
GO

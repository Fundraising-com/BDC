USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_MagAndGift]    Script Date: 06/07/2017 09:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_MagAndGift] as 
/*
select  b.orderid, b.date, count(cod.Price) as units,  coalesce(sum(cod.Price), 0) as amount, count(distinct studentinstance) as nbrstudents --
	from CustomerOrderDetail cod, CustomerOrderHeader coh, batch b
	where coh.Instance = cod.CustomerOrderHeaderInstance
	AND cod.ProductType in (46001,46002)
	and coh.orderbatchid = b.id
	and coh.orderbatchdate = b.date
group by b.orderid, b.date 

*/

select orderid, date, sum(units) as units, sum(amount) as amount from (
select  b.orderid, b.date, count(cod.Price) as units,  coalesce(sum(cod.Price), 0) as amount --, count(distinct studentinstance) as nbrstudents --
	from CustomerOrderDetail cod, CustomerOrderHeader coh, batch b
	where coh.Instance = cod.CustomerOrderHeaderInstance
	AND cod.ProductType =46001
	--AND cod.statusinstance in(507,508)
	and coh.orderbatchid = b.id
	and coh.orderbatchdate = b.date
group by b.orderid, b.date
union all
select  b.orderid, b.date, count(cod.Price) as units,  coalesce(sum(cod.Price), 0) as amount --, count(distinct studentinstance) as nbrstudents --
	from CustomerOrderDetail cod, CustomerOrderHeader coh, batch b
	where coh.Instance = cod.CustomerOrderHeaderInstance
	AND cod.ProductType =46002
	and coh.orderbatchid = b.id
	and coh.orderbatchdate = b.date
group by b.orderid, b.date 
) x 
group by orderid, date
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[v1]    Script Date: 06/07/2017 09:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[v1] (Id, Date, OrderId, CampaignId, EnterredAmount, instance, customerorderheaderinstance, transid, ProductType)
with schemabinding as
select a.Id, a.Date, a.OrderId, a.CampaignId, a.EnterredAmount, b.instance, c.customerorderheaderinstance, c.transid, c.ProductType
  from dbo.Batch a
       inner join dbo.CustomerOrderHeader b on a.[Date] = b.OrderBatchDate and a.ID = b.OrderBatchId
       inner join dbo.CustomerOrderDetail c ON b.Instance = c.CustomerOrderHeaderInstance
GO

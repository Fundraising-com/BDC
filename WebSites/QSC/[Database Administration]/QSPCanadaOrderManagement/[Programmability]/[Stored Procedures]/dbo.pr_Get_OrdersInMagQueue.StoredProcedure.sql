USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_OrdersInMagQueue]    Script Date: 06/07/2017 09:19:59 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_OrdersInMagQueue] AS

select a.OrderId,
       convert(varchar, a.Date,101) as 'DateOrdered',
       a.CampaignId,
       b.Description as 'OrderType',
       a.EnterredAmount as 'EnteredAmount',
       1 as 'IsPickable',
       c.Description as 'OrderQualifier'
  into #TempMagQueue
  from QSPCanadaOrderManagement.dbo.Batch a
       inner join QSPCanadaOrderManagement.dbo.CodeDetail b on a.OrderTypeCode = b.Instance
       inner join QSPCanadaOrderManagement.dbo.CodeDetail c on a.OrderQualifierId = c.Instance
 where a.StatusInstance = 40013
   and a.PickDate is null
   and a.IsMagQueueDone is null
   and a.OrderQualifierID not in (39009,39011,39012,39013,39014) --(Internet,Internet Fix, Order Correction)
   and a.OrderTypeCode <> 41009


select a.OrderId, 
       case c.producttype when 46001 then 1 else 0 end as ProductType,
       count (c.Quantity) as ItemQuantity,
       sum (c.Price) as ItemTotalCost
  into #TempMagTotals
  from Batch a
       inner join dbo.CustomerOrderHeader b on a.[Date] = b.OrderBatchDate and a.ID = b.OrderBatchId
       inner join dbo.CustomerOrderDetail c ON b.Instance = c.CustomerOrderHeaderInstance
 where a.StatusInstance = 40013 
   and a.PickDate is null
   and a.IsMagQueueDone is null
   and a.OrderQualifierID not in (39009,39011,39012,39013,39014) --(Internet,Internet Fix, Order Correction)
   and a.OrderTypeCode <> 41009
 group by a.OrderID, case c.producttype when 46001 then 1 else 0 end 


update #TempMagQueue
   set IsPickable = 0
  from #TempMagQueue
       inner join #TempMagTotals on #TempMagQueue.OrderId = #TempMagTotals.OrderId
 where #TempMagTotals.ProductType = 0

select q.OrderId,
       q.DateOrdered,
       q.CampaignId,
       q.OrderType,
       q.EnteredAmount,
       t.ItemQuantity,
       t.ItemTotalCost,
       q.IsPickable,
       q.OrderQualifier,
       1 as 'OnHandOK'
  from #TempMagQueue q
       inner join #TempMagTotals t on q.orderid = t.orderid
 where q.IsPickable = 1
   and t.ItemTotalCost > 0
   and t.ItemQuantity > 0
 order by q.OrderQualifier, q.OrderId
GO

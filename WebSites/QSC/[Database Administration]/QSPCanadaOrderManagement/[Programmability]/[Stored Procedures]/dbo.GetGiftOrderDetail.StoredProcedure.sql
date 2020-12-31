USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetGiftOrderDetail]    Script Date: 06/07/2017 09:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetGiftOrderDetail] @orderid int  AS

SELECT    od.ProductCode , od.ProductName , od.Quantity , od.Price ,
 od.CatalogPrice ,  QSPCanadaCommon.dbo.CodeDetail.Description, QSPCanadaCommon.dbo.CodeDetail.Instance priceOverride, dbo.Batch.OrderID
FROM         dbo.Batch INNER JOIN
                      dbo.CustomerOrderHeader oh ON dbo.Batch.ID = oh.OrderBatchID AND dbo.Batch.[Date] = oh.OrderBatchDate INNER JOIN
                      dbo.CustomerOrderDetail od ON oh.Instance = od.CustomerOrderHeaderInstance INNER JOIN
                      QSPCanadaCommon.dbo.CodeDetail ON od.PriceOverrideID = QSPCanadaCommon.dbo.CodeDetail.Instance
WHERE  producttype in (46002) and od.statusinstance not in (501,506)
And (dbo.Batch.OrderID = @orderid)
GO

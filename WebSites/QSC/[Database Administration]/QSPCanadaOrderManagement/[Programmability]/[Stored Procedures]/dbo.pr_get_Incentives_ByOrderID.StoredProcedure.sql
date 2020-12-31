USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Incentives_ByOrderID]    Script Date: 06/07/2017 09:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_Incentives_ByOrderID] 
 @OrderID int
AS
	
SELECT 
	COD.*
  FROM 
	QSPCanadaOrderManagement.dbo.CustomerOrderDetail COD
	INNER JOIN QSPCanadaOrderManagement.dbo.CustomerOrderHeader COH
	ON COD.[CustomerOrderHeaderInstance] = COH.[Instance] 
	INNER JOIN QSPCanadaOrderManagement.dbo.Batch 	BT
	ON (COH.[orderbatchdate] = BT.Date AND COH.[orderbatchid] = BT.Id)
	and BT.OrderID = @OrderID 
	AND COD.ProductType IN (46008, 46013, 46014, 46015)
GO

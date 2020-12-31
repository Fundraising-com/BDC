USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_del_Incentives_ByOrderID]    Script Date: 06/07/2017 09:19:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_del_Incentives_ByOrderID] 
 @OrderID int
AS
	
DELETE FROM QSPCanadaOrderManagement.dbo.CustomerOrderDetail
 WHERE 
	ProductType IN (46008, 46013, 46014, 46015)
	AND [CustomerOrderHeaderInstance] IN 
	(
		SELECT COH.[Instance] 
		  FROM 
			QSPCanadaOrderManagement.dbo.CustomerOrderHeader COH
			INNER JOIN QSPCanadaOrderManagement.dbo.Batch 	BT
			ON (COH.[orderbatchdate] = BT.Date AND COH.[orderbatchid] = BT.Id)
		 WHERE
			(COH.[orderbatchdate] = BT.Date AND COH.[orderbatchid] = BT.Id)
			and BT.OrderID = @OrderID 
	)
GO

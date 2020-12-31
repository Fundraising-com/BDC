USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectOrderTotals]    Script Date: 06/07/2017 09:20:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectOrderTotals] 

@iOrderID int = 0

AS

SELECT OrderID,
	  ProductType,
	  Count(*) AS TotalItems,
	  sum(ISNULL(Price, 0.00)) AS TotalAmount
   FROM  vw_GetSubAndProductsInfo
WHERE  OrderID = @iOrderID 
GROUP BY OrderID, ProductType
GO

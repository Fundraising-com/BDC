USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Adj_Tot_Sales]    Script Date: 02/14/2014 13:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  VIEW [dbo].[vw_CR_Adj_Tot_Sales]
AS
SELECT     dbo.Adjustment.Sales_ID, SUM(CONVERT(money, ISNULL(dbo.Adjustment.Adjustment_On_Shipping, 0)) + CONVERT(money, 
                      ISNULL(dbo.Adjustment.Adjustment_On_Sale_Amount, 0)) + CONVERT(money, ISNULL(dbo.vw_CR_Adj_Tax_Sales.GST, 0)) + CONVERT(money, 
                      ISNULL(dbo.vw_CR_Adj_Tax_Sales.QST, 0))) AS Total_Adj, SUM(CASE WHEN ([Reason_ID] = 7 OR
                      [Reason_ID] = 8 OR
                      [Reason_ID] = 9 OR
                      [Reason_ID] = 10 OR
                      [Reason_ID] = 13 OR
		 [Reason_ID] = 12 OR
		 [Reason_ID] = 14) THEN CONVERT(money, ISNULL([Adjustment_On_Shipping], 0)) + CONVERT(money, ISNULL([Adjustment_On_Sale_Amount], 0)) 
                      + CONVERT(money, ISNULL([GST], 0)) + CONVERT(money, ISNULL([QST], 0)) ELSE 0 END) AS Total_Adj_Cond
FROM         dbo.Adjustment LEFT OUTER JOIN
                      dbo.vw_CR_Adj_Tax_Sales ON dbo.Adjustment.Adjustment_No = dbo.vw_CR_Adj_Tax_Sales.Adjustement_No AND 
                      dbo.Adjustment.Sales_ID = dbo.vw_CR_Adj_Tax_Sales.Sales_Id
GROUP BY dbo.Adjustment.Sales_ID
GO

USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Shipped_Div_Month]    Script Date: 02/14/2014 13:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Shipped_Div_Month]
AS
SELECT     Currency_Code, Sales_Year, Sales_Month, Division_ID, SUM(CONVERT(money, ISNULL(Total_Product, 0)) + CONVERT(money, ISNULL(Total_Shipping, 
                      0)) + CONVERT(money, ISNULL(Total_GST, 0)) + CONVERT(money, ISNULL(Total_QST, 0))) AS Tot_Shipped, SUM(0) AS Tot_Box_Return, SUM(0) 
                      AS Tot_Box_Reship, SUM(0) AS Tot_Adj, SUM(0) AS Tot_Adj_Cond, SUM(0) AS Tot_Deposit, SUM(0) AS Tot_Paid
FROM         dbo.vw_CR_Shipped_Div_Month_Union
GROUP BY Currency_Code, Sales_Year, Sales_Month, Division_ID
GO

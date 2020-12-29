USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Deposit_Div_Month]    Script Date: 02/14/2014 13:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Deposit_Div_Month]
AS
SELECT     dbo.vw_CR_Prod_Class_Perc.Currency_Code, dbo.Product_Class.Division_ID, dbo.vw_CR_Prod_Class_Perc.Sales_Year, 
                      dbo.vw_CR_Prod_Class_Perc.Sales_Month, SUM(0) AS Tot_Shipped, SUM(0) AS Tot_Box_Return, SUM(0) AS Tot_Box_Reship, SUM(0) AS Tot_Adj, 
                      SUM(0) AS Tot_Adj_Cond, SUM(0) AS Tot_Paid, 
                      SUM(dbo.vw_CR_Deposit_Tot_Sales.Total_Deposit * dbo.vw_CR_Prod_Class_Perc.Sales_Product_Class_Percentage) AS Tot_Deposit
FROM         dbo.Product_Class INNER JOIN
                      dbo.vw_CR_Prod_Class_Perc ON dbo.Product_Class.Product_Class_ID = dbo.vw_CR_Prod_Class_Perc.Product_Class_ID INNER JOIN
                      dbo.vw_CR_Deposit_Tot_Sales ON dbo.vw_CR_Prod_Class_Perc.Sales_ID = dbo.vw_CR_Deposit_Tot_Sales.Sales_ID
GROUP BY dbo.vw_CR_Prod_Class_Perc.Currency_Code, dbo.Product_Class.Division_ID, dbo.vw_CR_Prod_Class_Perc.Sales_Year, 
                      dbo.vw_CR_Prod_Class_Perc.Sales_Month
GO

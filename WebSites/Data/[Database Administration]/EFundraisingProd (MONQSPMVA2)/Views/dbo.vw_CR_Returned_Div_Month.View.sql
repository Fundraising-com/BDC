USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Returned_Div_Month]    Script Date: 02/14/2014 13:02:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Returned_Div_Month]
AS
SELECT     Currency_Code, Division_ID, Sales_Year, Sales_Month, 0 AS Tot_Shipped, CONVERT(money, ISNULL(Total_Product, 0)) + CONVERT(money, 
                      ISNULL(Total_Shipping, 0)) + CONVERT(money, ISNULL(Total_GST, 0)) + CONVERT(money, ISNULL(Total_QST, 0)) AS Tot_Box_Return, 
                      0 AS Tot_Box_Reship, 0 AS Tot_Adj, 0 AS Tot_Adj_Cond, 0 AS Tot_Paid, 0 AS Tot_Deposit
FROM         dbo.vw_CR_Returned_Div_Month_Union
GO

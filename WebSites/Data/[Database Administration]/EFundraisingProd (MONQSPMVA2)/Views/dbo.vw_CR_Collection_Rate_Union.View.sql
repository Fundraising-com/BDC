USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Collection_Rate_Union]    Script Date: 02/14/2014 13:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_CR_Collection_Rate_Union]
AS
SELECT     Currency_Code, Sales_Year, Sales_Month, Division_ID, Tot_Shipped, Tot_Box_Return, Tot_Box_Reship, Tot_Paid, Tot_Adj, Tot_Adj_Cond, 
                      Tot_Deposit
FROM         vw_CR_Shipped_Div_Month
UNION ALL
SELECT     Currency_Code, Sales_Year, Sales_Month, Division_ID, Tot_Shipped, Tot_Box_Return, Tot_Box_Reship, Tot_Paid, Tot_Adj, Tot_Adj_Cond, 
                      Tot_Deposit
FROM         vw_CR_Reshipped_Div_Month
UNION ALL
SELECT     Currency_Code, Sales_Year, Sales_Month, Division_ID, Tot_Shipped, Tot_Box_Return, Tot_Box_Reship, Tot_Paid, Tot_Adj, Tot_Adj_Cond, 
                      Tot_Deposit
FROM         vw_CR_Returned_Div_Month
UNION ALL
SELECT     Currency_Code, Sales_Year, Sales_Month, Division_ID, Tot_Shipped, Tot_Box_Return, Tot_Box_Reship, Tot_Paid, Tot_Adj, Tot_Adj_Cond, 
                      Tot_Deposit
FROM         vw_CR_Adj_Div_Month
UNION ALL
SELECT     Currency_Code, Sales_Year, Sales_Month, Division_ID, Tot_Shipped, Tot_Box_Return, Tot_Box_Reship, Tot_Paid, Tot_Adj, Tot_Adj_Cond, 
                      Tot_Deposit
FROM         vw_CR_Paid_Div_Month
UNION ALL
SELECT     Currency_Code, Sales_Year, Sales_Month, Division_ID, Tot_Shipped, Tot_Box_Return, Tot_Box_Reship, Tot_Paid, Tot_Adj, Tot_Adj_Cond, 
                      Tot_Deposit
FROM         vw_CR_Deposit_Div_Month
GO

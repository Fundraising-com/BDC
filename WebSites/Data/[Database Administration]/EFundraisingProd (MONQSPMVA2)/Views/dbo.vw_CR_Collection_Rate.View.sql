USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Collection_Rate]    Script Date: 02/14/2014 13:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Collection_Rate]
AS
SELECT     dbo.vw_CR_Collection_Rate_Union.Currency_Code, dbo.vw_CR_Collection_Rate_Union.Sales_Year, dbo.vw_CR_Collection_Rate_Union.Sales_Month, 
                      dbo.Division.Division_Name, dbo.vw_CR_Collection_Rate_Union.Division_ID, SUM(dbo.vw_CR_Collection_Rate_Union.Tot_Shipped) AS Tot_Shipped, 
                      SUM(dbo.vw_CR_Collection_Rate_Union.Tot_Box_Return) AS Tot_Box_Return, SUM(dbo.vw_CR_Collection_Rate_Union.Tot_Box_Reship) 
                      AS Tot_Reshipped, SUM(dbo.vw_CR_Collection_Rate_Union.Tot_Paid) AS Tot_Paid, SUM(dbo.vw_CR_Collection_Rate_Union.Tot_Adj) AS Tot_Adj, 
                      SUM(dbo.vw_CR_Collection_Rate_Union.Tot_Adj_Cond) AS Tot_Adj_Cond, SUM(dbo.vw_CR_Collection_Rate_Union.Tot_Deposit) AS Tot_Deposit
FROM         dbo.vw_CR_Collection_Rate_Union INNER JOIN
                      dbo.Division ON dbo.vw_CR_Collection_Rate_Union.Division_ID = dbo.Division.Division_ID
GROUP BY dbo.vw_CR_Collection_Rate_Union.Currency_Code, dbo.vw_CR_Collection_Rate_Union.Sales_Year, 
                      dbo.vw_CR_Collection_Rate_Union.Sales_Month, dbo.Division.Division_Name, dbo.vw_CR_Collection_Rate_Union.Division_ID
GO

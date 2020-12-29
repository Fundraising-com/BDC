USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_total_receivable_by_Ageing]    Script Date: 02/14/2014 13:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_total_receivable_by_Ageing]
AS
SELECT     SUM(dbo.Sale.Total_Amount - COALESCE (dbo.Total_Deposit_By_Sales.Total_Deposit, 0) - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0)) 
                      AS Total_Receivable, SUM(dbo.Sale.Total_Amount) AS Expr1, SUM(dbo.Total_Deposit_By_Sales.Total_Deposit) AS Total_Deposit_On_Sale, 
                      SUM(dbo.Total_Adjustment.Adjustment_Amount) AS Total_Adjustments_On_Sale, dbo.Country.Currency_Code, 
                      dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, GETDATE()) AS Ageing
FROM         dbo.Sale INNER JOIN
                      dbo.Client_Address ON dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code AND 
                      dbo.Sale.Client_ID = dbo.Client_Address.Client_ID INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code LEFT OUTER JOIN
                      dbo.Total_Adjustment ON dbo.Sale.Sales_ID = dbo.Total_Adjustment.Sales_ID LEFT OUTER JOIN
                      dbo.Total_Deposit_By_Sales ON dbo.Sale.Sales_ID = dbo.Total_Deposit_By_Sales.Sales_ID
WHERE     (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NULL) AND (dbo.Sale.Reship_Date IS NULL) AND 
                      (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Total_Amount - COALESCE (dbo.Total_Deposit_By_Sales.Total_Deposit, 0) 
                      - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0) > 0.01) OR
                      (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NOT NULL) AND (dbo.Sale.Reship_Date IS NOT NULL) AND 
                      (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Total_Amount - COALESCE (dbo.Total_Deposit_By_Sales.Total_Deposit, 0) 
                      - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0) > 0.01)
GROUP BY dbo.Country.Currency_Code, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, GETDATE())
HAVING      (SUM(dbo.Sale.Total_Amount - COALESCE (dbo.Total_Deposit_By_Sales.Total_Deposit, 0) - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0)) 
                      > 0.01) OR
                      (SUM(dbo.Sale.Total_Amount - COALESCE (dbo.Total_Deposit_By_Sales.Total_Deposit, 0) - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0)) 
                      > 0.01)
GO

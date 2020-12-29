USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[temp]    Script Date: 02/14/2014 13:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[temp]
AS
SELECT     TOP 100 PERCENT SUM(dbo.Sale.Total_Amount - COALESCE (dbo.tmp_total_deposit.Total_Deposit, 0) 
                      - COALESCE (dbo.tmp_total_adjustment.Adjustment_Amount, 0)) AS Total_Receivable, dbo.Country.Currency_Code, 
                      dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, '2003 - 06 - 13') AS Ageing, SUM(dbo.tmp_total_adjustment.Adjustment_Amount) AS adjust, 
                      SUM(dbo.tmp_total_deposit.Total_Deposit) AS depo, SUM(dbo.Sale.Total_Amount) AS tot
FROM         dbo.Sale INNER JOIN
                      dbo.Client_Address ON dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code AND 
                      dbo.Sale.Client_ID = dbo.Client_Address.Client_ID INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code LEFT OUTER JOIN
                      dbo.tmp_total_adjustment ON dbo.Sale.Sales_ID = dbo.tmp_total_adjustment.Sales_ID LEFT OUTER JOIN
                      dbo.tmp_total_deposit ON dbo.Sale.Sales_ID = dbo.tmp_total_deposit.Sales_ID
WHERE     (dbo.Sale.Total_Amount - COALESCE (dbo.tmp_total_deposit.Total_Deposit, 0) - COALESCE (dbo.tmp_total_adjustment.Adjustment_Amount, 0) > 0.01) 
                      AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NULL) AND (dbo.Sale.Reship_Date IS NULL) AND 
                      (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= CONVERT(DATETIME, '2003-06-13 18:00:00', 102)) OR
                      (dbo.Sale.Total_Amount - COALESCE (dbo.tmp_total_deposit.Total_Deposit, 0) - COALESCE (dbo.tmp_total_adjustment.Adjustment_Amount, 0) > 0.01) 
                      AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NOT NULL) AND (dbo.Sale.Reship_Date IS NOT NULL) AND 
                      (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= CONVERT(DATETIME, '2003-06-13 18:00:00', 102))
GROUP BY dbo.Country.Currency_Code, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, '2003 - 06 - 13')
ORDER BY dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, '2003 - 06 - 13')
GO

USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_getReceivablePerAgeing2]    Script Date: 02/14/2014 13:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_getReceivablePerAgeing2]
AS
SELECT     TOP 100 PERCENT SUM(dbo.Sale.Total_Amount - COALESCE (dbo.tmp_total_deposit.Total_Deposit, 0) 
                      - COALESCE (dbo.tmp_total_adjustment.Adjustment_Amount, 0)) AS Total_Receivable, dbo.Country.Currency_Code, 
                      dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, '2003-11-30') AS Ageing
FROM         dbo.Sale INNER JOIN
                      dbo.Client_Address ON dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code AND 
                      dbo.Sale.Client_ID = dbo.Client_Address.Client_ID INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code INNER JOIN
                      dbo.Consultant ON dbo.Sale.Consultant_ID = dbo.Consultant.Consultant_ID LEFT OUTER JOIN
                      dbo.tmp_total_adjustment ON dbo.Sale.Sales_ID = dbo.tmp_total_adjustment.Sales_ID LEFT OUTER JOIN
                      dbo.tmp_total_deposit ON dbo.Sale.Sales_ID = dbo.tmp_total_deposit.Sales_ID
WHERE     (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= '2003-11-30') AND 
                      (dbo.Sale.Total_Amount - COALESCE (dbo.tmp_total_deposit.Total_Deposit, 0) - COALESCE (dbo.tmp_total_adjustment.Adjustment_Amount, 0) > 0.01) 
                      AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NULL) AND (dbo.Sale.Reship_Date IS NULL) AND 
                      (dbo.Consultant.Is_Fm = 0) OR
                      (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= '2003-11-30') AND 
                      (dbo.Sale.Total_Amount - COALESCE (dbo.tmp_total_deposit.Total_Deposit, 0) - COALESCE (dbo.tmp_total_adjustment.Adjustment_Amount, 0) > 0.01) 
                      AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NOT NULL) AND (dbo.Sale.Reship_Date IS NOT NULL) AND 
                      (dbo.Consultant.Is_Fm = 0)
GROUP BY dbo.Country.Currency_Code, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, '2003-11-30')
ORDER BY dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, '2003-11-30')
GO

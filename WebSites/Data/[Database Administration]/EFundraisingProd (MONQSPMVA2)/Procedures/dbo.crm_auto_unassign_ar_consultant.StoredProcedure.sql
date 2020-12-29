USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_auto_unassign_ar_consultant]    Script Date: 02/14/2014 13:03:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[crm_auto_unassign_ar_consultant]
AS
  UPDATE sale
  SET    ar_consultant_id = NULL
  WHERE  sales_id IN (SELECT sales_id
                      FROM   (SELECT dbo.Sale.Sales_ID,
                                     dbo.Sale.ar_consultant_id,
                                     dbo.Sale.Total_Amount - Coalesce(dbo.Total_Deposit_By_Sales.Total_Deposit,
                                                                      0) - Coalesce(dbo.Total_Adjustment.Adjustment_Amount,0) AS Total_Receivable,
                                     dbo.Sale.Total_Amount,
                                     Coalesce(dbo.Total_Deposit_By_Sales.Total_Deposit,
                                              0) AS Total_Deposit_On_Sale,
                                     Coalesce(dbo.Total_Adjustment.Adjustment_Amount,0) AS Total_Adjustments_On_Sale,
                                     dbo.Sale.Client_Sequence_Code,
                                     dbo.Sale.Client_ID,
                                     dbo.Country.Currency_Code,
                                     dbo.Fn_return_ageing_by_sale(dbo.Sale.Sales_ID,Getdate()) AS Ageing,
                                     c.name
                              FROM   dbo.Sale
                                     INNER JOIN dbo.Client_Address
                                       ON dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code
                                          AND dbo.Sale.Client_ID = dbo.Client_Address.Client_ID
                                     INNER JOIN dbo.Country
                                       ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code
                                     LEFT OUTER JOIN dbo.Total_Adjustment
                                       ON dbo.Sale.Sales_ID = dbo.Total_Adjustment.Sales_ID
                                     LEFT OUTER JOIN dbo.Total_Deposit_By_Sales
                                       ON dbo.Sale.Sales_ID = dbo.Total_Deposit_By_Sales.Sales_ID
                                     LEFT JOIN dbo.consultant c
                                       ON dbo.Sale.ar_consultant_id = c.consultant_id
                              WHERE  (dbo.Sale.Total_Amount - Coalesce(dbo.Total_Deposit_By_Sales.Total_Deposit,
                                                                       0) - Coalesce(dbo.Total_Adjustment.Adjustment_Amount,0) > 0.01)
                                     AND (dbo.Sale.Actual_Ship_Date IS NOT NULL )
                                     AND (dbo.Sale.Box_Return_Date IS NULL )
                                     AND (dbo.Sale.Reship_Date IS NULL )
                                     AND (dbo.Client_Address.Address_Type = 'bt')
                                      OR (dbo.Sale.Total_Amount - Coalesce(dbo.Total_Deposit_By_Sales.Total_Deposit,
                                                                           0) - Coalesce(dbo.Total_Adjustment.Adjustment_Amount,0) > 0.01)
                                     AND (dbo.Sale.Actual_Ship_Date IS NOT NULL )
                                     AND (dbo.Sale.Box_Return_Date IS NOT NULL )
                                     AND (dbo.Sale.Reship_Date IS NOT NULL )
                                     AND (dbo.Client_Address.Address_Type = 'bt')) a
                      WHERE  ar_consultant_id IS NOT NULL 
                             AND ar_consultant_id <> 1774
                             AND ar_consultant_id <> 1775
			     AND ar_consultant_id <> 3501
                             AND total_receivable > 0
                             AND (ageing = '90-120 Days'
                                   OR ageing = '120+ Days'))
GO

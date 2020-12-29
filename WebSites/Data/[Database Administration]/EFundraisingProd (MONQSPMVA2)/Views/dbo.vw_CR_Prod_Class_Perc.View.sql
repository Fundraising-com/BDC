USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Prod_Class_Perc]    Script Date: 02/14/2014 13:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_CR_Prod_Class_Perc]
AS
SELECT     dbo.vw_CR_Prod_Class_Regroup.Currency_Code, dbo.vw_CR_Prod_Class_Regroup.Sales_Year, dbo.vw_CR_Prod_Class_Regroup.Sales_Month, 
                      dbo.vw_CR_Prod_Class_Regroup.Sales_ID, dbo.vw_CR_Prod_Class_Regroup.Total_Product_Class, 
                      dbo.vw_CR_Prod_Class_Regroup.Product_Class_ID, 
                      CASE WHEN [Total_Product_Class] = 0 THEN [Total_Product_Class] ELSE ([Total_Product_Class] / CASE WHEN [sale_amount] = 0 THEN [Total_Product_Class]
                       ELSE [sale_amount] END) END AS Sales_Product_Class_Percentage, 
                      CASE WHEN [Total_Product_Class] = 0 THEN [Net_Sales_Shipping] ELSE ([Total_Product_Class] / CASE WHEN [sale_amount] = 0 THEN [Total_Product_Class]
                       ELSE [sale_amount] END * [Net_Sales_Shipping]) END AS Shipping_On_Product_Class
FROM         dbo.vw_CR_Prod_Class_Regroup INNER JOIN
                      dbo.total_by_sale ON dbo.vw_CR_Prod_Class_Regroup.Sales_ID = dbo.total_by_sale.Sales_ID
GO

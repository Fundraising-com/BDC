USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Shipped_Div_Month_Sales]    Script Date: 02/14/2014 13:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Shipped_Div_Month_Sales]
AS
SELECT     dbo.Country.Currency_Code, YEAR(dbo.Sale.Actual_Ship_Date) AS Sales_Year, MONTH(dbo.Sale.Actual_Ship_Date) AS Sales_Month, 
                      dbo.Product_Class.Division_ID, SUM(dbo.Sales_Item.Sales_Amount) AS Total_Product, 
                      SUM((dbo.Sale.Shipping_Fees - dbo.Sale.Shipping_Fees_Discount) * dbo.Sales_Item.Sales_Amount / dbo.total_by_sale.sale_amount) 
                      AS Total_Shipping, SUM(CASE WHEN [GST] = 0 THEN 0 ELSE ([GST] * [Sales_Item].[sales_amount]) / [total_by_sale].[sale_amount] END) AS Total_GST,
                       SUM(CASE WHEN [QST] = 0 THEN 0 ELSE [QST] * [Sales_Item].[sales_amount] / [total_by_sale].[sale_amount] END) AS Total_QST
FROM         dbo.Product_Class INNER JOIN
                      dbo.Country INNER JOIN
                      dbo.Client_Address INNER JOIN
                      dbo.Sale ON dbo.Client_Address.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client_Address.Client_ID = dbo.Sale.Client_ID ON 
                      dbo.Country.Country_Code = dbo.Client_Address.Country_Code INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID ON 
                      dbo.Product_Class.Product_Class_ID = dbo.Scratch_Book.Product_Class_ID INNER JOIN
                      dbo.total_by_sale ON dbo.Sale.Sales_ID = dbo.total_by_sale.Sales_ID LEFT OUTER JOIN
                      dbo.vw_Total_Tax_By_Sale ON dbo.Sale.Sales_ID = dbo.vw_Total_Tax_By_Sale.Sales_ID
WHERE     (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sales_Item.Sales_Amount <> 0)
GROUP BY dbo.Country.Currency_Code, YEAR(dbo.Sale.Actual_Ship_Date), MONTH(dbo.Sale.Actual_Ship_Date), dbo.Product_Class.Division_ID
HAVING      (YEAR(dbo.Sale.Actual_Ship_Date) IS NOT NULL) AND (MONTH(dbo.Sale.Actual_Ship_Date) IS NOT NULL)
GO

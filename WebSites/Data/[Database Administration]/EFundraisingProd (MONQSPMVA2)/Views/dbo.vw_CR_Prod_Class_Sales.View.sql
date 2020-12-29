USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Prod_Class_Sales]    Script Date: 02/14/2014 13:02:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Prod_Class_Sales]
AS
SELECT     dbo.Country.Currency_Code, YEAR(dbo.Sale.Actual_Ship_Date) AS Sales_Year, MONTH(dbo.Sale.Actual_Ship_Date) AS Sales_Month, 
                      dbo.Sale.Sales_ID, SUM(dbo.Sales_Item.Sales_Amount) AS Total_Product_Class, dbo.Product_Class.Product_Class_ID, 
                      dbo.Sale.Shipping_Fees - dbo.Sale.Shipping_Fees_Discount AS Net_Sales_Shipping
FROM         dbo.Sale INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Scratch_Book INNER JOIN
                      dbo.Product_Class ON dbo.Scratch_Book.Product_Class_ID = dbo.Product_Class.Product_Class_ID ON 
                      dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID INNER JOIN
                      dbo.Client_Address ON dbo.Sale.Client_ID = dbo.Client_Address.Client_ID AND 
                      dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code
WHERE     (dbo.Client_Address.Address_Type = 'bt')
GROUP BY dbo.Country.Currency_Code, YEAR(dbo.Sale.Actual_Ship_Date), MONTH(dbo.Sale.Actual_Ship_Date), dbo.Sale.Sales_ID, 
                      dbo.Product_Class.Product_Class_ID, dbo.Sale.Shipping_Fees - dbo.Sale.Shipping_Fees_Discount
HAVING      (YEAR(dbo.Sale.Actual_Ship_Date) IS NOT NULL) AND (MONTH(dbo.Sale.Actual_Ship_Date) IS NOT NULL)
GO

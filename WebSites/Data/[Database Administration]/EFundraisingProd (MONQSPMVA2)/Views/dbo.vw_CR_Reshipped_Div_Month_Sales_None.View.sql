USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Reshipped_Div_Month_Sales_None]    Script Date: 02/14/2014 13:02:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Reshipped_Div_Month_Sales_None]
AS
SELECT     dbo.Country.Currency_Code, YEAR(dbo.Sale.Actual_Ship_Date) AS Sales_Year, MONTH(dbo.Sale.Actual_Ship_Date) AS Sales_Month, 
                      dbo.Division.Division_ID, SUM(0) AS Total_Product, SUM(dbo.Sale.Shipping_Fees - dbo.Sale.Shipping_Fees_Discount) AS Total_Shipping, SUM(0) 
                      AS Total_GST, SUM(0) AS Total_QST
FROM         dbo.Division CROSS JOIN
                      dbo.Country INNER JOIN
                      dbo.Client_Address INNER JOIN
                      dbo.Sale ON dbo.Client_Address.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client_Address.Client_ID = dbo.Sale.Client_ID ON 
                      dbo.Country.Country_Code = dbo.Client_Address.Country_Code
WHERE     (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Total_Amount - dbo.Sale.Shipping_Fees + dbo.Sale.Shipping_Fees_Discount = 0) AND 
                      (dbo.Division.Division_ID = 1) AND (dbo.Sale.Shipping_Fees - dbo.Sale.Shipping_Fees_Discount <> 0) AND (dbo.Sale.Reship_Date IS NOT NULL)
GROUP BY dbo.Country.Currency_Code, YEAR(dbo.Sale.Actual_Ship_Date), MONTH(dbo.Sale.Actual_Ship_Date), dbo.Division.Division_ID
HAVING      (YEAR(dbo.Sale.Actual_Ship_Date) IS NOT NULL) AND (MONTH(dbo.Sale.Actual_Ship_Date) IS NOT NULL)
GO

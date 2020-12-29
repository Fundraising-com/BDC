USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Prod_Class_Regroup]    Script Date: 02/14/2014 13:02:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Prod_Class_Regroup]
AS
SELECT     TOP 100 PERCENT Currency_Code, Sales_Year, Sales_Month, Sales_ID, Product_Class_ID, SUM(Total_Product_Class) AS Total_Product_Class, 
                      SUM(Net_Sales_Shipping) AS Net_Sales_Shipping
FROM         dbo.vw_CR_Prod_Class_Union
GROUP BY Currency_Code, Sales_Year, Sales_Month, Sales_ID, Product_Class_ID
ORDER BY Sales_ID
GO

USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Prod_Class_Union]    Script Date: 02/14/2014 13:02:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Prod_Class_Union]
AS
SELECT     Currency_Code, Sales_Year, Sales_Month, Sales_ID, Total_Product_Class, Product_Class_ID, Net_Sales_Shipping
FROM         vw_CR_Prod_Class_Sales
UNION
SELECT     Currency_Code, Sales_Year, Sales_Month, Sales_ID, Total_Product_Class, Product_Class_ID, Net_Sales_Shipping
FROM         vw_CR_Prod_Class_Sales_None
GO

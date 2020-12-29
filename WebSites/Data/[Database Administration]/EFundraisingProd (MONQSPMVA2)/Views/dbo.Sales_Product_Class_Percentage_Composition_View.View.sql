USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Sales_Product_Class_Percentage_Composition_View]    Script Date: 02/14/2014 13:02:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Sales_Product_Class_Percentage_Composition_View    Script Date: 2003-02-22 20:34:18 ******/
create view [dbo].[Sales_Product_Class_Percentage_Composition_View]
  as select Sales_Product_Class_Composition_View.Sales_ID,Sales_Product_Class_Composition_View.Total_Product_Class,Sales_Product_Class_Composition_View.Product_Class_ID,(Total_Product_Class/sale_amount) as Sales_Product_Class_Percentage,(Total_Product_Class/sale_amount)*Net_Sales_Shipping as Shipping_On_Product_Class from total_by_Sale join Sales_Product_Class_Composition_View on total_by_Sale.Sales_ID = Sales_Product_Class_Composition_View.Sales_ID
GO

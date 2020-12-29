USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Sales_Product_Class_Composition_View]    Script Date: 02/14/2014 13:02:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Sales_Product_Class_Composition_View    Script Date: 2003-02-22 20:34:18 ******/


/****** Object:  View dbo.Sales_Product_Class_Composition_View    Script Date: 2/11/2003 12:27:44 PM ******/

create view [dbo].[Sales_Product_Class_Composition_View]
--fA-41,B-15
  as select Sale.Sales_ID,Sum(Sales_Item.Sales_Amount) as Total_Product_Class,Product_Class.Product_Class_ID,Sum(Shipping_Fees-Shipping_Fees_Discount) as Net_Sales_Shipping from(Sale join Sales_Item on Sale.Sales_ID = Sales_Item.Sales_ID) join(Scratch_Book join Product_Class on Scratch_Book.Product_Class_ID = Product_Class.Product_Class_ID) on Sales_Item.Scratch_Book_ID = Scratch_Book.Scratch_Book_ID where Sale.Total_Amount <> 0 group by Sale.Sales_ID,Product_Class.Product_Class_ID
GO

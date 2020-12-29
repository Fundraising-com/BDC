USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Item_Percentage_Per_Sale_View]    Script Date: 02/14/2014 13:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Item_Percentage_Per_Sale_View    Script Date: 2003-02-22 20:34:17 ******/
CREATE VIEW [dbo].[Item_Percentage_Per_Sale_View]
AS
SELECT     dbo.Sales_Item.Sales_ID, dbo.Sales_Item.Scratch_Book_ID, dbo.Sales_Item.Sales_Amount / dbo.total_by_sale.sale_amount AS Item_Percentage, 
                      dbo.Sale.Shipping_Fees - dbo.Sale.Shipping_Fees_Discount AS Net_Shipping_Fees
FROM         dbo.total_by_sale INNER JOIN
                      dbo.Sales_Item ON dbo.total_by_sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID INNER JOIN
                      dbo.Sale ON dbo.Sales_Item.Sales_ID = dbo.Sale.Sales_ID
GO

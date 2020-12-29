USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_client_sales_products]    Script Date: 02/14/2014 13:02:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_client_sales_products]
AS
SELECT DISTINCT 
                      dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, dbo.Client.Client_ID, dbo.Product_Class.Description, 
                      dbo.Product_Class.Product_Class_ID
FROM         dbo.Client INNER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Scratch_Book.Product_Class_ID = dbo.Product_Class.Product_Class_ID
WHERE     (dbo.Product_Class.Product_Class_ID = 8 OR
                      dbo.Product_Class.Product_Class_ID = 4 OR
                      dbo.Product_Class.Product_Class_ID = 5)
GO

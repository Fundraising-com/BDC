USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_active_product_list]    Script Date: 02/14/2014 13:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_active_product_list]
AS
SELECT     dbo.Product_Class.Description AS product_class_desc, dbo.Scratch_Book.Description AS product_desc, dbo.Scratch_Book.Product_Code, 
                      dbo.Scratch_Book.Current_Description, dbo.Scratch_Book_Price_Info.Effective_Date, dbo.Scratch_Book_Price_Info.Unit_Price
FROM         dbo.Scratch_Book INNER JOIN
                      dbo.Scratch_Book_Price_Info ON dbo.Scratch_Book.Scratch_Book_ID = dbo.Scratch_Book_Price_Info.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Scratch_Book.Product_Class_ID = dbo.Product_Class.Product_Class_ID AND 
                      dbo.Scratch_Book_Price_Info.Product_Class_Id = dbo.Product_Class.Product_Class_ID
WHERE     (dbo.Scratch_Book_Price_Info.Country_Code = 'US') AND (dbo.Scratch_Book.Is_Active = 1)
GO

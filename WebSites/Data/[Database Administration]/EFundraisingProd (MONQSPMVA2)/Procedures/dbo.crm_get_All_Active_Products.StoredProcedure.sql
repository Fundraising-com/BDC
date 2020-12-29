USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_All_Active_Products]    Script Date: 02/14/2014 13:03:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--
--JF Lavigne

CREATE       procedure [dbo].[crm_get_All_Active_Products]
as
SELECT     dbo.scratch_book.product_code AS Product_Code, dbo.scratch_book.description AS Product, dbo.product_class.description AS Product_Class, 
                      dbo.Package.Description AS Package, dbo.scratch_book.raising_potential, dbo.Package.profit_min, dbo.Package.profit_max, 
                      dbo.Package.profit_default, dbo.supplier.supplier_name, dbo.scratch_book_price_info.unit_price
FROM         dbo.scratch_book INNER JOIN
                      dbo.product_class ON dbo.scratch_book.product_class_id = dbo.product_class.product_class_id INNER JOIN
                      dbo.Package ON dbo.scratch_book.package_id = dbo.Package.Package_Id INNER JOIN
                      dbo.scratch_book_price_info ON dbo.scratch_book.scratch_book_id = dbo.scratch_book_price_info.scratch_book_id LEFT OUTER JOIN
                      dbo.supplier ON dbo.scratch_book.supplier_id = dbo.supplier.supplier_id
WHERE     (dbo.scratch_book.is_active <> 0)
ORDER BY dbo.product_class.description
GO

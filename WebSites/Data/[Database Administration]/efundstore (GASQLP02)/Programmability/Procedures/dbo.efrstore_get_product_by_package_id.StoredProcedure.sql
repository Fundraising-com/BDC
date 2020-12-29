USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_by_package_id]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product
CREATE PROCEDURE [dbo].[efrstore_get_product_by_package_id] @Package_id int AS
begin

select p.Product_id, p.Parent_product_id, p.Description, p.Raising_potential, p.Product_code, p.Current_description, p.Product_class_id, p.Supplier_id, p.Active, p.Scratch_book_id, p.Small_image, p.Display, p.Total_qty, p.Configuration
from Product p INNER JOIN Product_Package ON p.product_id = Product_Package.product_id
where Product_Package.Package_id=@Package_id

end
GO

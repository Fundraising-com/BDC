USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_products_root]    Script Date: 02/14/2014 13:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product
CREATE PROCEDURE [dbo].[efrstore_get_products_root] 
    @culture_code nvarchar(5)
AS
begin

select p.Product_id
, p.Parent_product_id
--, pd.Description
, p.Raising_potential
, p.Product_code
--, Current_description
--, p.Product_class_id
--, p.Supplier_id
, p.Enabled
, p.Scratch_Book_id
, pd.image_name
, pd.Display_order
--, Total_qty
, pd.Configuration
from Product p
    INNER JOIN product_desc pd
        ON pd.product_id = p.product_id
where parent_product_id is null
  and pd.culture_code = @culture_code

end
GO

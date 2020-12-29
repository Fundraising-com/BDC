USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_products_by_scratch_book_id]    Script Date: 02/14/2014 13:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product
CREATE    PROCEDURE [dbo].[efrstore_get_products_by_scratch_book_id] 
                 @scratch_book_id int,
                 @package_root_id int
AS
begin

select p.Product_id, p.Parent_product_id, p.Scratch_book_id, p.[name], p.Raising_potential
       , p.Product_code, p.Enabled, p.Is_inner, p.Create_date 
from Product p inner join

       --all efund crm packages 2 level max
       --all efund crm products 1 level max
      (select product_id from product_package where package_id in (
      	select package_id from package where parent_package_id in 
    	(
           select package_id from package where parent_package_id in 
              (select parent_package_id from package where parent_package_id = @package_root_id)
           )
       )
       UNION
       (select product_id from product_package where package_id in (
           select package_id from package where parent_package_id in 
              (select parent_package_id from package where parent_package_id = @package_root_id)
           )
       ))efr on p.product_id = efr.product_id 


where p.scratch_book_id = @scratch_book_id

end
GO

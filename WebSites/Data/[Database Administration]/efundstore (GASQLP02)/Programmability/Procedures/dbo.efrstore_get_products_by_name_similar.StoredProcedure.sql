USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_products_by_name_similar]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product
CREATE     PROCEDURE [dbo].[efrstore_get_products_by_name_similar] --'general'
    @name varchar(100),
    @package_root_id int
    
AS
begin



SELECT     p.product_id, p.name, p.create_date, p.is_inner, p.enabled, p.product_code, 
           p.raising_potential, p.scratch_book_id, p.parent_product_id
FROM         dbo.product p inner join

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

WHERE  [name] like '%' + @name + '%' and p.enabled <> 0

end
GO

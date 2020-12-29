USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_products_by_package_id]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product
CREATE   PROCEDURE [dbo].[efrstore_get_products_by_package_id] 
    @Package_id int 
AS
begin

select 	p.product_id
	, p.[name]
	, p.create_date
	, p.is_inner
	, p.enabled
	, p.product_code
	, p.raising_potential
	, p.scratch_book_id
	, p.parent_product_id
from product p
inner join product_package pp on p.product_id = pp.product_id 
inner join package pa on pp.package_id = pa.package_id
where pa.package_id = @package_id
order by display_order

end
GO

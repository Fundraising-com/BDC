USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_product_type_by_id]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product_type
CREATE PROCEDURE [dbo].[es_get_product_type_by_id] @Product_type_id int AS
begin

select Product_type_id, Product_line_id, Product_type_name, Administration_supply, Fulfillment_charge, Erp_product_type_id from Product_type WITH (NOLOCK) where Product_type_id=@Product_type_id

end
GO

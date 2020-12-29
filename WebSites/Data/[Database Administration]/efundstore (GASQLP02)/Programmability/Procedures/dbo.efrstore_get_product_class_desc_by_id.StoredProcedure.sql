USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_class_desc_by_id]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product_class_desc
CREATE PROCEDURE [dbo].[efrstore_get_product_class_desc_by_id] @Product_class_id int AS
begin

select Product_class_id, Language_id, Product_class_desc, Min_requirement from Product_class_desc where Product_class_id=@Product_class_id

end
GO

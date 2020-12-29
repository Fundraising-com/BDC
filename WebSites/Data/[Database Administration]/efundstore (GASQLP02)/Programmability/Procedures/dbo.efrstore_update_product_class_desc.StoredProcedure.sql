USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_product_class_desc]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Product_class_desc
CREATE PROCEDURE [dbo].[efrstore_update_product_class_desc] @Product_class_id int, @Language_id tinyint, @Product_class_desc varchar(50), @Min_requirement varchar(100) AS
begin

update Product_class_desc set Language_id=@Language_id, Product_class_desc=@Product_class_desc, Min_requirement=@Min_requirement where Product_class_id=@Product_class_id

end
GO

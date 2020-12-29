USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_product_class_desc]    Script Date: 02/14/2014 13:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Product_class_desc
CREATE PROCEDURE [dbo].[efrstore_insert_product_class_desc] @Product_class_id int OUTPUT, @Language_id tinyint, @Product_class_desc varchar(50), @Min_requirement varchar(100) AS
begin

insert into Product_class_desc(Language_id, Product_class_desc, Min_requirement) values(@Language_id, @Product_class_desc, @Min_requirement)

select @Product_class_id = SCOPE_IDENTITY()

end
GO

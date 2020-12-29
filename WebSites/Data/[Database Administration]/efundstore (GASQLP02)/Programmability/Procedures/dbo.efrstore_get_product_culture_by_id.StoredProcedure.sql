USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_culture_by_id]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product_culture
CREATE PROCEDURE [dbo].[efrstore_get_product_culture_by_id] @Product_id int AS
begin

select Product_id, Culture_code from Product_culture where Product_id=@Product_id

end
GO

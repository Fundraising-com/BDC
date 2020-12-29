USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_product_package]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Product_package
CREATE  PROCEDURE [dbo].[efrstore_update_product_package] @Product_id int, @Package_id int, @Display_order int, @Display int, @Create_date datetime AS
begin

update Product_package set Package_id=@Package_id, Display_order=@Display_order, Display=@Display, Create_date=@Create_date where Product_id=@Product_id

end
GO

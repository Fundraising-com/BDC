USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_product_desc]    Script Date: 02/14/2014 13:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Product_desc
CREATE PROCEDURE [dbo].[efrcrm_update_product_desc] @Product_desc_id smallint, @Product_id int, @Language_id tinyint, @Product_name varchar(100), @Product_short_desc varchar(300), @Product_long_desc varchar(1000), @Product_small_img varchar(25), @Product_large_img varchar(25), @Available_online bit AS
begin

update Product_desc set Product_id=@Product_id, Language_id=@Language_id, Product_name=@Product_name, Product_short_desc=@Product_short_desc, Product_long_desc=@Product_long_desc, Product_small_img=@Product_small_img, Product_large_img=@Product_large_img, Available_online=@Available_online where Product_desc_id=@Product_desc_id

end
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_product_descs]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product_desc
CREATE PROCEDURE [dbo].[efrcrm_get_product_descs] AS
begin

select Product_desc_id, Product_id, Language_id, Product_name, Product_short_desc, Product_long_desc, Product_small_img, Product_large_img, Available_online from Product_desc

end
GO

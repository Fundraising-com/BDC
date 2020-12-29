USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_products_packages]    Script Date: 02/14/2014 13:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Products_packages
CREATE PROCEDURE [dbo].[efrcrm_update_products_packages] @Product_id int, @Package_id tinyint, @Display_order tinyint, @Displayable bit AS
begin

update Products_packages set Package_id=@Package_id, Display_order=@Display_order, Displayable=@Displayable where Product_id=@Product_id

end
GO

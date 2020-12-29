USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_products_packages_by_id]    Script Date: 02/14/2014 13:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Products_packages
CREATE PROCEDURE [dbo].[efrcrm_get_products_packages_by_id] @Product_id int AS
begin

select Product_id, Package_id, Display_order, Displayable from Products_packages where Product_id=@Product_id

end
GO

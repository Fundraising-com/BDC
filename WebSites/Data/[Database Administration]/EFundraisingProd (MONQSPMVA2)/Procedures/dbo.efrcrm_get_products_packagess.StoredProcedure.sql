USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_products_packagess]    Script Date: 02/14/2014 13:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Products_packages
CREATE PROCEDURE [dbo].[efrcrm_get_products_packagess] AS
begin

select Product_id, Package_id, Display_order, Displayable from Products_packages

end
GO

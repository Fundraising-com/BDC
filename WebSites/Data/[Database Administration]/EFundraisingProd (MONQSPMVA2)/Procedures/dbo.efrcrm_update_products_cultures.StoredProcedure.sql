USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_products_cultures]    Script Date: 02/14/2014 13:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Products_cultures
CREATE PROCEDURE [dbo].[efrcrm_update_products_cultures] @Product_id int, @Culture_id tinyint AS
begin

update Products_cultures set Culture_id=@Culture_id where Product_id=@Product_id

end
GO

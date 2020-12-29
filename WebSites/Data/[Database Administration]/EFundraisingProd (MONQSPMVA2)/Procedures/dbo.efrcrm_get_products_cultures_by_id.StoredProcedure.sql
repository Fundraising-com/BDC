USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_products_cultures_by_id]    Script Date: 02/14/2014 13:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Products_cultures
CREATE PROCEDURE [dbo].[efrcrm_get_products_cultures_by_id] @Product_id int AS
begin

select Product_id, Culture_id from Products_cultures where Product_id=@Product_id

end
GO

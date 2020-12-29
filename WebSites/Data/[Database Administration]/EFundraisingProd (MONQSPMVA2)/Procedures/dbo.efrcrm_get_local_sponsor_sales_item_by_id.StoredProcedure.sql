USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_local_sponsor_sales_item_by_id]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Local_Sponsor_Sales_Item
CREATE PROCEDURE [dbo].[efrcrm_get_local_sponsor_sales_item_by_id] @Brand_ID int AS
begin

select Brand_ID, Local_Sponsor_ID, Sales_ID, Sales_Item_No from Local_Sponsor_Sales_Item where Brand_ID=@Brand_ID

end
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_local_sponsor_sales_item]    Script Date: 02/14/2014 13:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Local_Sponsor_Sales_Item
CREATE PROCEDURE [dbo].[efrcrm_update_local_sponsor_sales_item] @Brand_ID int, @Local_Sponsor_ID int, @Sales_ID int, @Sales_Item_No smallint AS
begin

update Local_Sponsor_Sales_Item set Local_Sponsor_ID=@Local_Sponsor_ID, Sales_ID=@Sales_ID, Sales_Item_No=@Sales_Item_No where Brand_ID=@Brand_ID

end
GO

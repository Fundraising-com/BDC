USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_inventory_adjustment_type]    Script Date: 02/14/2014 13:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Inventory_Adjustment_Type
CREATE PROCEDURE [dbo].[efrcrm_update_inventory_adjustment_type] @Inventory_Adjustment_Type_ID int, @Inventory_Adjustment_Type_Desc varchar(255) AS
begin

update Inventory_Adjustment_Type set Inventory_Adjustment_Type_Desc=@Inventory_Adjustment_Type_Desc where Inventory_Adjustment_Type_ID=@Inventory_Adjustment_Type_ID

end
GO

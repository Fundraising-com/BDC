USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_inventory_adjustment_type_by_id]    Script Date: 02/14/2014 13:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Inventory_Adjustment_Type
CREATE PROCEDURE [dbo].[efrcrm_get_inventory_adjustment_type_by_id] @Inventory_Adjustment_Type_ID int AS
begin

select Inventory_Adjustment_Type_ID, Inventory_Adjustment_Type_Desc from Inventory_Adjustment_Type where Inventory_Adjustment_Type_ID=@Inventory_Adjustment_Type_ID

end
GO

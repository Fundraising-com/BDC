USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_inventory_adjustment_by_id]    Script Date: 02/14/2014 13:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Inventory_Adjustment
CREATE PROCEDURE [dbo].[efrcrm_get_inventory_adjustment_by_id] @Inventory_Adjustment_ID int AS
begin

select Inventory_Adjustment_ID, Inventory_Adjustment_Type_ID, Scratch_Book_Id, Adjustment_Date, Quantity from Inventory_Adjustment where Inventory_Adjustment_ID=@Inventory_Adjustment_ID

end
GO

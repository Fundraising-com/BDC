USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_inventory_adjustment]    Script Date: 02/14/2014 13:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Inventory_Adjustment
CREATE PROCEDURE [dbo].[efrcrm_update_inventory_adjustment] @Inventory_Adjustment_ID int, @Inventory_Adjustment_Type_ID int, @Scratch_Book_Id int, @Adjustment_Date smalldatetime, @Quantity int AS
begin

update Inventory_Adjustment set Inventory_Adjustment_Type_ID=@Inventory_Adjustment_Type_ID, Scratch_Book_Id=@Scratch_Book_Id, Adjustment_Date=@Adjustment_Date, Quantity=@Quantity where Inventory_Adjustment_ID=@Inventory_Adjustment_ID

end
GO

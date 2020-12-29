USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_inventory_adjustment]    Script Date: 02/14/2014 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Inventory_Adjustment
CREATE PROCEDURE [dbo].[efrcrm_insert_inventory_adjustment] @Inventory_Adjustment_ID int OUTPUT, @Inventory_Adjustment_Type_ID int, @Scratch_Book_Id int, @Adjustment_Date smalldatetime, @Quantity int AS
begin

insert into Inventory_Adjustment(Inventory_Adjustment_Type_ID, Scratch_Book_Id, Adjustment_Date, Quantity) values(@Inventory_Adjustment_Type_ID, @Scratch_Book_Id, @Adjustment_Date, @Quantity)

select @Inventory_Adjustment_ID = SCOPE_IDENTITY()

end
GO

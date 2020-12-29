USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_inventory_adjustments]    Script Date: 02/14/2014 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Inventory_Adjustment
CREATE PROCEDURE [dbo].[efrcrm_get_inventory_adjustments] AS
begin

select Inventory_Adjustment_ID, Inventory_Adjustment_Type_ID, Scratch_Book_Id, Adjustment_Date, Quantity from Inventory_Adjustment

end
GO

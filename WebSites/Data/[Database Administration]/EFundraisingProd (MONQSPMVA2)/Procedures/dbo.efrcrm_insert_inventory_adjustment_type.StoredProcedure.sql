USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_inventory_adjustment_type]    Script Date: 02/14/2014 13:06:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Inventory_Adjustment_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_inventory_adjustment_type] @Inventory_Adjustment_Type_ID int OUTPUT, @Inventory_Adjustment_Type_Desc varchar(255) AS
begin

insert into Inventory_Adjustment_Type(Inventory_Adjustment_Type_Desc) values(@Inventory_Adjustment_Type_Desc)

select @Inventory_Adjustment_Type_ID = SCOPE_IDENTITY()

end
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sales_warnings_temp]    Script Date: 02/14/2014 13:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sales_Warnings_Temp
CREATE PROCEDURE [dbo].[efrcrm_update_sales_warnings_temp] @Sale_To_Add_ID int, @Sales_Item_No int, @Sales_Constraint_ID int AS
begin

update Sales_Warnings_Temp set Sales_Item_No=@Sales_Item_No, Sales_Constraint_ID=@Sales_Constraint_ID where Sale_To_Add_ID=@Sale_To_Add_ID

end
GO

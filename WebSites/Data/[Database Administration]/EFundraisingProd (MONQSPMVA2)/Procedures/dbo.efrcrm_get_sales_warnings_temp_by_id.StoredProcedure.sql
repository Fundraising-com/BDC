USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_warnings_temp_by_id]    Script Date: 02/14/2014 13:06:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Sales_Warnings_Temp
CREATE PROCEDURE [dbo].[efrcrm_get_sales_warnings_temp_by_id] @Sale_To_Add_ID int AS
begin

select Sale_To_Add_ID, Sales_Item_No, Sales_Constraint_ID from Sales_Warnings_Temp where Sale_To_Add_ID=@Sale_To_Add_ID

end
GO

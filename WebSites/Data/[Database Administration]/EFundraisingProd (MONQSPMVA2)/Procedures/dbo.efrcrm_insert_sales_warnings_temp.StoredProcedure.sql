USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sales_warnings_temp]    Script Date: 02/14/2014 13:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sales_Warnings_Temp
CREATE PROCEDURE [dbo].[efrcrm_insert_sales_warnings_temp] @Sale_To_Add_ID int OUTPUT, @Sales_Item_No int, @Sales_Constraint_ID int AS
begin

insert into Sales_Warnings_Temp(Sales_Item_No, Sales_Constraint_ID) values(@Sales_Item_No, @Sales_Constraint_ID)

select @Sale_To_Add_ID = SCOPE_IDENTITY()

end
GO

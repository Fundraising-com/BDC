USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sales_warnings]    Script Date: 02/14/2014 13:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sales_Warnings
CREATE PROCEDURE [dbo].[efrcrm_insert_sales_warnings] @Sales_ID int OUTPUT, @Sales_Item_No int, @Sales_Constraint_Id int AS
begin

insert into Sales_Warnings(Sales_Item_No, Sales_Constraint_Id) values(@Sales_Item_No, @Sales_Constraint_Id)

select @Sales_ID = SCOPE_IDENTITY()

end
GO

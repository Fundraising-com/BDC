USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sales_warnings]    Script Date: 02/14/2014 13:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sales_Warnings
CREATE PROCEDURE [dbo].[efrcrm_update_sales_warnings] @Sales_ID int, @Sales_Item_No int, @Sales_Constraint_Id int AS
begin

update Sales_Warnings set Sales_Item_No=@Sales_Item_No, Sales_Constraint_Id=@Sales_Constraint_Id where Sales_ID=@Sales_ID

end
GO

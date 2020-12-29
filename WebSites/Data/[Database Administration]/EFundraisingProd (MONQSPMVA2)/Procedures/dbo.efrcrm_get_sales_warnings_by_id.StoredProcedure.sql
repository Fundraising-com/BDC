USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_warnings_by_id]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Sales_Warnings
CREATE PROCEDURE [dbo].[efrcrm_get_sales_warnings_by_id] @Sales_ID int AS
begin

select Sales_ID, Sales_Item_No, Sales_Constraint_Id from Sales_Warnings where Sales_ID=@Sales_ID

end
GO

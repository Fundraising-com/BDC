USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_warnings_temps]    Script Date: 02/14/2014 13:06:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sales_Warnings_Temp
CREATE PROCEDURE [dbo].[efrcrm_get_sales_warnings_temps] AS
begin

select Sale_To_Add_ID, Sales_Item_No, Sales_Constraint_ID from Sales_Warnings_Temp

end
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_warningss]    Script Date: 02/14/2014 13:06:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sales_Warnings
CREATE PROCEDURE [dbo].[efrcrm_get_sales_warningss] AS
begin

select Sales_ID, Sales_Item_No, Sales_Constraint_Id from Sales_Warnings

end
GO

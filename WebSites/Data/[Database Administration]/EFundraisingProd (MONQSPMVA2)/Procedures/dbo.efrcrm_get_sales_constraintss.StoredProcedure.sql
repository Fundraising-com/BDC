USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_constraintss]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sales_constraints
CREATE PROCEDURE [dbo].[efrcrm_get_sales_constraintss] AS
begin

select Sales_constraint_id, Product_class_id, Description, High_level from Sales_constraints

end
GO

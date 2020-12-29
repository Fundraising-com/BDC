USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_product_classs]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product_class
CREATE PROCEDURE [dbo].[efrcrm_get_product_classs] AS
begin

select Product_class_id, Division_id, Accounting_class_id, Description, Product_code, Display_name, Is_displayable, Minimum_order_qty from Product_class

end
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_product_class_by_name]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[efrcrm_get_product_class_by_name] @description varchar(50)  AS
begin

select Product_class_id, Division_id, Accounting_class_id, Description, Product_code, Display_name, Is_displayable, Minimum_order_qty from Product_class
where description = @description

end
GO

USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_classs]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product_class
CREATE PROCEDURE [dbo].[efrstore_get_product_classs] AS
begin

select Product_class_id, Division_id, Accounting_class_id, Description, Display, Minimum_order_qty from Product_class

end
GO

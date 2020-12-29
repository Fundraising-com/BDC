USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_product_business_rules_by_product_id]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Client_address
CREATE  PROCEDURE [dbo].[efrcrm_get_product_business_rules_by_product_id]
                  @product_id int 
                       
AS
begin


select product_business_rule_id, product_class_id, product_id, min_order, free,average_delivery_time
from product_business_rule
where product_id = @product_id

end
GO

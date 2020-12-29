USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_profit_range_by_product_business_rule_id]    Script Date: 02/14/2014 13:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Client_address
CREATE  PROCEDURE [dbo].[efrcrm_get_profit_range_by_product_business_rule_id]
                  @product_business_rule_id int 
                       
AS
begin


select pr.profit_range_id, item_nbr_min, item_nbr_max, profit_percentage
from profit_range pr inner join product_business_rule_profit_range br 
                     on pr.profit_range_id = br.profit_range_id
where br.product_business_rule_id = @product_business_rule_id

end
GO

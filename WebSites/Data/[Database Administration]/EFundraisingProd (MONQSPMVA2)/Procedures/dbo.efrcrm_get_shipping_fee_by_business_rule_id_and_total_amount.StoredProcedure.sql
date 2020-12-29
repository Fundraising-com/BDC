USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_shipping_fee_by_business_rule_id_and_total_amount]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product_class
CREATE PROCEDURE [dbo].[efrcrm_get_shipping_fee_by_business_rule_id_and_total_amount] --1,1000
                 @product_business_rule_id int,
                 @total_amount decimal 
                   
AS
begin


select sf.shipping_fee_id, sale_amt_min, sale_amt_max, shipping_fee
from shipping_fee sf inner join product_business_rule_shipping_fee br on
           sf.shipping_fee_id = br.shipping_fee_id 
where br.product_business_rule_id = @product_business_rule_id
      and @total_amount >= sale_amt_min 
      and (@total_amount <= sale_amt_max or sale_amt_max is null)

end
GO

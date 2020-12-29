USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sale_potential_commissions]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[efrcrm_get_sale_potential_commissions] 
	  @sale_id int
	
AS
BEGIN 




select * 
      ,(total_amount * (sales_amount / total_amount) * commission_rate_no_free) * conversion_rate as converted_commission_on_item
      , total_amount * (sales_amount / total_amount) * commission_rate_no_free  as commission_on_item_new

from 
(

select  s.sales_id
        , ca.country_code 
	, si.sales_item_no
	 , sb.scratch_book_id
	, sb.description
	, sb.is_active
        , sb.raising_potential
        , si.unit_price_sold
        , ( case 
                --sample
                 when sb.scratch_book_id in (SELECT sb.scratch_book_id FROM   dbo.scratch_book sb inner join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id where (sb.description LIKE 'Sample%')) then 0.0
           
                 --agent sale
                 when sb.scratch_book_id = 43 then 0.3
                 when (sb.description like '%agent%') then 0.05

                 --scratchcards prepaid 
                 when sb.product_class_id = 1 and payment_term_id in (2,8,12) and ca.country_code = 'CA'  then 0.1
                 when sb.product_class_id = 1 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.09  --was 0.085

                   --scratchcards not prepaid 
                 when sb.product_class_id = 1 and ca.country_code = 'CA' then 0.07
                 when sb.product_class_id = 1 then 0.055 --was 0.065

				 --Herbert/lamontagne - upfront payment
                when sb.product_class_id = 27 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then .015
				 
                --Herbert/lamontagne - base
                when sb.product_class_id = 27 then 0.01 --was 0.015

                --frozen food us --Pine Valley
                when sb.product_class_id = 29 or sb.product_class_id = 37 then 0.065  --was 0.075 

				 --T-Shirt - upfront payment
                when sb.product_class_id = 36 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.07
				
                --T-Shirt - base
                when sb.product_class_id = 36 then 0.06

                --frozen food ca chippery     
                when sb.product_class_id = 33 then 0.06

                --lollipops us
                     --stand
                     when sb.scratch_book_id = 3339 then 0.1
                     --others               
                     when sb.product_class_id = 28 or sb.product_class_id = 7 then 0.02

                --lollipops ca
                when sb.product_class_id = 32 then 0.03

                --beef jerky us
                      --x-stick
                      when sb.package_id = 36 and si.quantity_sold <= 2 then .035
                      --original beef stick
                      when sb.package_id = 129 then .035
                      --other 
                      when sb.product_class_id = 30 then 0.02
                
                --beef jerky ca
                when sb.product_class_id = 34 then 0.02
                
				--Restaurant.com cards - upfront payment
                when sb.product_class_id = 10 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.07
				
                --Restaurant.com cards - base
                when sb.product_class_id = 10 then 0.065

				--Hershey m&m -- upfront payment
                when sb.product_class_id = 5 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.03
				
                --Hershey m&m -- base
                when sb.product_class_id = 5 then 0.02 --was 0.025
               
                --wfc - all - upfront payment
                when sb.product_class_id in (25,26) and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.015
				
				--wfc - all - base
				when sb.product_class_id in (25,26) then 0.01 -- was .015
				
				--wfc CA - upfront payment
                when sb.product_class_id = 35 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.03
				
                --wfc CA - base
                when sb.product_class_id = 35 then 0.02 --was 0.025

                --caramilk
                when sb.product_class_id = 31 then 0.02
				
				--Emblems upfront payment
                when sb.product_class_id = 38 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.06
				
               --Emblems base 
                when sb.product_class_id = 38 then 0.05
             
                --pop corn
                when sb.product_class_id = 16 then 0.03

                --efund cards
                when sb.product_class_id = 39 then 0.06

                --van wyk
						
					 --one dollar bar - upfront payment
                      when sb.package_id = 125 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.02
                      --one dollar bar - base
                      when sb.package_id = 125 then 0.01
					  --chocolatiers -upfront payment
                      when sb.package_id = 124 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.06
                      --chocolatiers - base
                      when sb.package_id = 124 then 0.045 
                      --2$ Ultimate
                      when sb.package_id = 149 then 0.035 
                      --free cards
                      when sb.product_class_id = 72 then 0
 
				 --pucks - upfront payment
                when sb.product_class_id = 14 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.03
 
                 --pucks - base
                when sb.product_class_id = 14 then 0.02
                      --case when (sb.raising_potential - si.unit_price_sold) / sb.raising_potential = 0.45 then 0.035 
                       --    when (sb.raising_potential - si.unit_price_sold) / sb.raising_potential = 0.5 then 0.025
                       --    else 0 end 
               
               --Niagara naturals
                when sb.product_class_id = 40 then 0.05
				
				--Mediator Earings - base
                when sb.product_class_id = 41 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) then 0.07
				
                --Mediator Earings - base
                when sb.product_class_id = 41 then 0.06
               
               --gift
                when sb.product_class_id = 23 then 0.06
                
                -- Otis Spunkmeyer FF 
				when sb.product_class_id = 43 then 0.055

                when ca.country_code = 'CA' then isnull(sbc.commission_rate_ca,0)

                 else isnull(sbc.commission_rate,0) end) as commission_rate_no_free 
	, si.sales_amount
	, s.total_amount
        , s.sales_date
	, (case when si.quantity_free <> 0 then 1 else 0 end) as free_cases

	, s.actual_ship_date
	, (case when scon.consultant_id in (2258, 3450, 3518)
		then lcon.name
		else scon.name end) as sale_consultant
	, (case when scon.consultant_id in (2258, 3450, 3518)
		then lcon.consultant_id
		else scon.consultant_id end) as sale_consultant_id
	, l.lead_id
        , l.organization
	, lcon.name as lead_consultant
	
	, sb.Product_Class_ID
        , (case when ca.country_code = 'CA' then 1.00
           else (select top 1 conversion_rate from conversion_rate_table where conversion_date <= s.sales_date order by conversion_date desc)end) as conversion_rate
from lead l
inner join consultant lcon on l.consultant_id = lcon.consultant_id
inner join client c on l.lead_id = c.lead_id
inner join client_address ca on c.client_id = ca.client_id and c.client_sequence_code = ca.client_sequence_code and ca.address_type = 'bt'
inner join sale s on c.client_id = s.client_id and c.client_sequence_code = s.client_sequence_code
inner join sales_item si on si.sales_id = s.sales_id 
inner join scratch_book sb on si.scratch_book_id = sb.scratch_book_id
left outer join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id
left outer join package pa on sb.package_id = pa.package_Id 
inner join consultant scon on s.consultant_id = scon.consultant_id
where s.sales_id = @sale_id
and   sb.product_class_id not in (25,26)


)  comm
where total_amount<> 0 
END
GO

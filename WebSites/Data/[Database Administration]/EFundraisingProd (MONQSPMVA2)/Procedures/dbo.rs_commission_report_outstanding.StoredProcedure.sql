USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_commission_report_outstanding]    Script Date: 02/14/2014 13:08:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[rs_commission_report_outstanding]
	 @consultant_id int = null


AS
BEGIN 

if @consultant_id is not null 
begin

select * 
       , (total_receivable * (sales_amount / total_amount) * commission_rate_no_free) * conversion_rate as converted_commission_on_item
       , (case when free_cases > 0 and product_class_id in (4,5,14,15,17)
		then total_receivable * (sales_amount / total_amount) * (commission_rate_no_free * 0.5)
		else total_receivable * (sales_amount / total_amount) * commission_rate_no_free end) as commission_on_item_new

from 
(


select  s.sales_id
        , ca.country_code 
	, si.sales_item_no
      	, sb.scratch_book_id
        , pt.description as payment_term
	, sb.description
	, sb.is_active
           --if qsp referreal, give different rate
	   --if not sc, normal commission, If sc then check if upfront payment and adjust
        , (  case
                --sample
                 when sb.scratch_book_id in (SELECT sb.scratch_book_id FROM   dbo.scratch_book sb inner join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id where (sb.description LIKE 'Sample%')) then 0.0
                  
                 --scratchcards not prepaid 
                 --when sb.product_class_id = 1 and s.payment_term_id not in (2,8,12) and ca.country_code = 'CA' then 0.07
                 --when sb.product_class_id = 1 and s.payment_term_id not in (2,8,12) then 0.065

                 --scratchcards prepaid 
                 --when sb.product_class_id = 1 and ca.country_code = 'CA' then 0.1
                 when sb.product_class_id = 1 then 0.17

                 --agent sale
                 when (c.client_sequence_code = 'CA' or c.client_sequence_code = 'UA') and sb.scratch_book_id != 43 then 0.17
                             
              
                --Herbert/lamontagne
                when sb.product_class_id = 27 then 0.03

                --frozen food us
                when sb.product_class_id = 29 then 0.07

               --PINE Valley
                --when sb.product_class_id = 37 then 0.075

                --T-Shirt
                when sb.product_class_id = 36 then 0.07

                --frozen food ca chippery
                when sb.product_class_id = 33 then 0.07

                --lollipops us
                when sb.product_class_id = 28 then 0.02

                --lollipops ca
                when sb.product_class_id = 32 then 0.03

                --beef jerky us
                when sb.product_class_id = 30 then 0.02

                --beef jerky ca
                when sb.product_class_id = 34 then 0.03

                --discount cards (tb renamed Restaurant.com)
                when sb.product_class_id = 10 then 0.07


                --Hershey m&m
                when sb.product_class_id = 5 then 0.02
               
                --wfc - all
                --when sb.product_class_id in (25,26) then 0.015

                --wfc CA
                --when sb.product_class_id in (35) then 0.025

                --caramilk
                when sb.product_class_id in (31) then 0.02

                --1$ van wyk  (to do select by package)
                when sb.scratch_book_id in (SELECT scratch_book_id FROM dbo.scratch_book where description like '$1 Van Wyk%') then 0.02
                --2$ van wyk  (to do select by package)
                when sb.scratch_book_id in (SELECT scratch_book_id FROM dbo.scratch_book where description like '$2 Van Wyk%') then 0.03

                 --pucks
                when sb.product_class_id = 14 then
                      case when (sb.raising_potential - si.unit_price_sold) / sb.raising_potential = 0.45 then 0.035
                           when (sb.raising_potential - si.unit_price_sold) / sb.raising_potential = 0.5 then 0.025
                           else 0 end 
						   
				 --Niagara naturals
                when sb.product_class_id = 40 then 0.07
         
				--Mediator Earings - upfront payment
                --when sb.product_class_id = 41 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) and datediff(dd, coalesce(s.actual_ship_date,getdate()), p.payment_entry_date ) < = 5 then 0.07
				
                --Mediator Earings - base
                when sb.product_class_id = 41 then 0.03

                --gift
                when sb.product_class_id = 23 then 0.06

				-- Otis Spunkmeyer FF 
				when sb.product_class_id = 43 then 0.055
				
				-- Entertainment books / National Discounts cards 
				when sb.product_class_id in ( 46, 47)  then 0.07
				-- Kathryn Beich 
				when (sb.product_class_id = 45 and ca.country_code = 'CA')   or sb.product_class_id = 48 then 0.04
				
				-- Kathryn Beich
				when sb.product_class_id = 45  then 0.02
				
				-- Nestle
				when sb.product_class_id = 44  then 0.03
				
				-- TB Frozen Food
				when sb.product_class_id = 49  then 0.07
				
				-- Nestle Big pack
				when sb.product_class_id = 50  then 0.03
				
				-- Nestle Big pack
				when sb.product_class_id = 51  then 0.07

				-- Kathryn Beich/Nestle
				when sb.product_class_id = 55  then 0.03
				
                 when ca.country_code = 'CA' then sbc.commission_rate_ca
                 else sbc.commission_rate end) as commission_rate_no_free 
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
           else (select top 1 conversion_rate from conversion_rate_table order by conversion_date desc)end) as conversion_rate
       , p.total_receivable
from lead l
inner join consultant lcon on l.consultant_id = lcon.consultant_id
inner join client c on l.lead_id = c.lead_id
inner join client_address ca on c.client_id = ca.client_id and c.client_sequence_code = ca.client_sequence_code and ca.address_type = 'bt'
inner join sale s on c.client_id = s.client_id and c.client_sequence_code = s.client_sequence_code
inner join payment_term pt on s.payment_term_id = pt.payment_term_id
inner join sales_item si on si.sales_id = s.sales_id 
inner join scratch_book sb on si.scratch_book_id = sb.scratch_book_id
left outer join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id
left outer join package pa on sb.package_id = pa.package_Id 
inner join consultant scon on s.consultant_id = scon.consultant_id
inner join (SELECT     dbo.Sale.Sales_ID, dbo.Sale.Total_Amount - COALESCE (dbo.Total_Payment.Payment_Amount, 0) 
                      - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0) AS Total_Receivable
           FROM         dbo.Sale LEFT OUTER JOIN
                      dbo.Total_Adjustment ON dbo.Sale.Sales_ID = dbo.Total_Adjustment.Sales_ID LEFT OUTER JOIN
                      dbo.Total_Payment ON dbo.Sale.Sales_ID = dbo.Total_Payment.Sales_ID
           WHERE      (dbo.Sale.Total_Amount - COALESCE (dbo.Total_Payment.Payment_Amount, 0) - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0) > 0.01) 
                      and dbo.sale.box_return_date IS NULL 
           ) p on s.sales_id = p.sales_id
and ((lcon.consultant_id = @consultant_id and scon.consultant_id in (2258, 3450, 3518)) or scon.consultant_id = @consultant_id)
and s.ar_status_id = 21
--and datediff(dd, coalesce(s.actual_ship_date,getdate()), getdate()) <= 90
and s.sales_date > '2007-1-1'
)  comm
where total_amount <> 0 

end
else
begin


select * 
     , (total_receivable * (sales_amount / total_amount) * commission_rate_no_free) * conversion_rate as converted_commission_on_item
     , (case when free_cases > 0 and product_class_id in (4,5,14,15,17)
		then total_receivable * (sales_amount / total_amount) * (commission_rate_no_free * 0.5)
		else total_receivable * (sales_amount / total_amount) * commission_rate_no_free end) as commission_on_item_new

from 
(


select  s.sales_id
        , ca.country_code 
	, si.sales_item_no
	, sb.scratch_book_id
        , pt.description as payment_term
	, sb.description
	, sb.is_active
           --if qsp referreal, give different rate
	   --if not sc, normal commission, If sc then check if upfront payment and adjust
        , ( case   
                --sample
                 when sb.scratch_book_id in (SELECT sb.scratch_book_id FROM   dbo.scratch_book sb inner join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id where (sb.description LIKE 'Sample%')) then 0.0
               
                    --scratchcards not prepaid 
                 --when sb.product_class_id = 1 and s.payment_term_id not in (2,8,12) and ca.country_code = 'CA' then 0.07
                 --when sb.product_class_id = 1 and s.payment_term_id not in (2,8,12) then 0.065

                 --scratchcards prepaid 
                 --when sb.product_class_id = 1 and ca.country_code = 'CA' then 0.1
                 when sb.product_class_id = 1 then 0.17
                 --agent sale
                 when (c.client_sequence_code = 'CA' or c.client_sequence_code = 'UA') and sb.scratch_book_id != 43 then 0.17
                
               
              
                --Herbert/lamontagne
                when sb.product_class_id = 27 then 0.03

                --frozen food us
                when sb.product_class_id = 29 then 0.07

               --PINE Valley
                --when sb.product_class_id = 37 then 0.075

                --T-Shirt
                when sb.product_class_id = 36 then 0.07
               
               --frozen food ca chippery
                when sb.product_class_id = 33 then 0.07

                --lollipops us
                when sb.product_class_id = 28 then 0.02

                --lollipops ca
                when sb.product_class_id = 32 then 0.03

                --beef jerky us
                when sb.product_class_id = 30 then 0.02

                --beef jerky ca
                when sb.product_class_id = 34 then 0.03

                --discount cards (tb renamed Restaurant.com)
                when sb.product_class_id = 10 then 0.07


                --Hershey m&m
                when sb.product_class_id = 5 then 0.02
               
                --wfc - all
                --when sb.product_class_id in (25,26) then 0.015

                --wfc CA
                --when sb.product_class_id in (35) then 0.025

                --caramilk
                when sb.product_class_id in (31) then 0.02

                --1$ van wyk  (to do select by package)
                when sb.scratch_book_id in (SELECT scratch_book_id FROM dbo.scratch_book where description like '$1 Van Wyk%') then 0.02
                --2$ van wyk  (to do select by package)
                when sb.scratch_book_id in (SELECT scratch_book_id FROM dbo.scratch_book where description like '$2 Van Wyk%') then 0.03

                 --pucks
                when sb.product_class_id = 14 then
                      case when (sb.raising_potential - si.unit_price_sold) / sb.raising_potential = 0.45 then 0.035
                           when (sb.raising_potential - si.unit_price_sold) / sb.raising_potential = 0.5 then 0.025
                           else 0 end 
				
				  --Niagara naturals
                when sb.product_class_id = 40 then 0.07
         
				--Mediator Earings - upfront payment
                --when sb.product_class_id = 41 and (s.po_consultant_commission=1 or payment_term_id in (2,8,12)) and datediff(dd, coalesce(s.actual_ship_date,getdate()), p.payment_entry_date ) < = 5 then 0.07
				
                --Mediator Earings - base
                when sb.product_class_id = 41 then 0.03

                --gift
                when sb.product_class_id = 23 then 0.06

				-- Otis Spunkmeyer FF 
				when sb.product_class_id = 43 then 0.055
				
				-- Entertainment books / National Discounts cards 
				when sb.product_class_id in ( 46, 47)  then 0.07
				
				-- Kathryn Beich 
				when (sb.product_class_id = 45 and ca.country_code = 'CA')   or sb.product_class_id = 48 then 0.04
				
				-- Kathryn Beich
				when sb.product_class_id = 45  then 0.02
				
				-- Nestle
				when sb.product_class_id = 44  then 0.03
				
				-- TB Frozen Food
				when sb.product_class_id = 49  then 0.07
				
				-- Nestle Big pack
				when sb.product_class_id = 50  then 0.03
				
				-- Nestle Big pack
				when sb.product_class_id = 51  then 0.07
				
				-- Kathryn Beich/Nestle
				when sb.product_class_id = 55  then 0.03
				
                 when ca.country_code = 'CA' then sbc.commission_rate_ca 
                 else sbc.commission_rate end) as commission_rate_no_free 
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
           else (select top 1 conversion_rate from conversion_rate_table order by conversion_date desc)end) as conversion_rate
       , p.total_receivable
from lead l
inner join consultant lcon on l.consultant_id = lcon.consultant_id
inner join client c on l.lead_id = c.lead_id
inner join client_address ca on c.client_id = ca.client_id and c.client_sequence_code = ca.client_sequence_code and ca.address_type = 'bt'
inner join sale s on c.client_id = s.client_id and c.client_sequence_code = s.client_sequence_code
inner join payment_term pt on s.payment_term_id = pt.payment_term_id
inner join sales_item si on si.sales_id = s.sales_id 
inner join scratch_book sb on si.scratch_book_id = sb.scratch_book_id
left outer join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id
left outer join package pa on sb.package_id = pa.package_Id 
inner join consultant scon on s.consultant_id = scon.consultant_id
inner join (SELECT     dbo.Sale.Sales_ID, dbo.Sale.Total_Amount - COALESCE (dbo.Total_Payment.Payment_Amount, 0) 
                      - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0) AS Total_Receivable
           FROM         dbo.Sale LEFT OUTER JOIN
                      dbo.Total_Adjustment ON dbo.Sale.Sales_ID = dbo.Total_Adjustment.Sales_ID LEFT OUTER JOIN
                      dbo.Total_Payment ON dbo.Sale.Sales_ID = dbo.Total_Payment.Sales_ID
           WHERE      (dbo.Sale.Total_Amount - COALESCE (dbo.Total_Payment.Payment_Amount, 0) - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0) > 0.01) 
                      and dbo.sale.box_return_date IS NULL 
           ) p on s.sales_id = p.sales_id

and s.ar_status_id = 21
--and datediff(dd, coalesce(s.actual_ship_date,getdate()), getdate()) <= 90
and s.sales_date > '2008-1-1'
and s.actual_ship_date is not null
)  comm
where total_amount <> 0 

end



end
GO

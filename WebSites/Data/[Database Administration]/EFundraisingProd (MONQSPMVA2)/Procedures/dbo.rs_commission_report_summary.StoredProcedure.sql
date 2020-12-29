USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_commission_report_summary]    Script Date: 02/14/2014 13:08:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rs_commission_report_summary] @start_date datetime 
	, @end_date datetime 
	, @consultant_id int = null


AS
BEGIN 


if @consultant_id is null 
begin

select sale_consultant as lead_consultant
       ,product_class
       , min(conversion_rate) rate
       ,sum((payment_amount * (sales_amount / total_amount) * commission_rate_no_free) * conversion_rate) as converted_commission_on_item
       , sum((payment_amount * (sales_amount / total_amount) * commission_rate_no_free)) as commission_on_item_new
from 
(

select  s.sales_id
        , ca.country_code 
        , pc.description product_class
	, si.sales_item_no
	, p.payment_no
	, sb.scratch_book_id
	, sb.description
	, sb.is_active
           --if qsp referreal, give different rate
	   --if not sc, normal commission, If sc then check if upfront payment and adjust
        , [dbo].[fct_get_commission_rate](s.sales_id, ca.country_code,si.quantity_sold ) as commission_rate_no_free 
	, si.sales_amount
	, s.total_amount
        , s.sales_date
	, (case when si.quantity_free <> 0 then 1 else 0 end) as free_cases
	, p.payment_amount * (si.sales_amount / s.total_amount) as payment_on_item
	, p.payment_amount 
	, p.payment_entry_date
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
	, datediff(dd, s.actual_ship_date, p.payment_entry_date) as aging
	, sb.Product_Class_ID
        , (case when ca.country_code = 'CA' then 1.00
           else (select top 1 conversion_rate from conversion_rate_table where conversion_date <= p.payment_entry_date order by conversion_date desc)end) as conversion_rate
from lead l
inner join consultant lcon on l.consultant_id = lcon.consultant_id
inner join client c on l.lead_id = c.lead_id
inner join client_address ca on c.client_id = ca.client_id and c.client_sequence_code = ca.client_sequence_code and ca.address_type = 'bt'
inner join sale s on c.client_id = s.client_id and c.client_sequence_code = s.client_sequence_code
inner join sales_item si on si.sales_id = s.sales_id 
inner join scratch_book sb on si.scratch_book_id = sb.scratch_book_id
inner join product_class pc on sb.product_class_id = pc.product_class_id
left outer join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id
left outer join package pa on sb.package_id = pa.package_Id 
inner join consultant scon on s.consultant_id = scon.consultant_id
inner join payment p on s.sales_id = p.sales_id
left join Adjustment a on a.Sales_ID =  s.sales_id and a.Adjustment_No = 1 and Adjustment_Amount > 0
WHERE p.payment_entry_date IS NOT NULL
  AND s.actual_ship_date IS NOT NULL
  /** The following functions ensures that the max date between the shipment and the payment is used for the commission report **/
  AND dbo.LaterOf (p.payment_entry_date, s.actual_ship_date) between @start_date and @end_date
and   sb.product_class_id not in (25,26)
--and datediff(dd, coalesce(s.actual_ship_date,getdate()), p.payment_entry_date) <= 90

)  comm
where total_amount<> 0 
group by sale_consultant,
         product_class


end------------------------------------------------------------------------------------------------------------------------------------------------------------------------
else
begin

select sale_consultant as lead_consultant
       ,product_class
       , min(conversion_rate) rate
       ,sum((payment_amount * (sales_amount / total_amount) * commission_rate_no_free) * conversion_rate) as converted_commission_on_item
       , sum((payment_amount * (sales_amount / total_amount) * commission_rate_no_free )) as commission_on_item_new
from 
(

select  s.sales_id
       , ca.country_code 
        , pc.description product_class
	, si.sales_item_no
	, p.payment_no
	, sb.scratch_book_id
	, sb.description
	, sb.is_active
          --if qsp referreal, give different rate
	   --if not sc, normal commission, If sc then check if upfront payment and adjust
        , [dbo].[fct_get_commission_rate](s.sales_id, ca.country_code,si.quantity_sold ) as commission_rate_no_free 
	, si.sales_amount
	, s.total_amount
        , s.sales_date
	, (case when si.quantity_free <> 0 then 1 else 0 end) as free_cases
	, p.payment_amount * (si.sales_amount / s.total_amount) as payment_on_item
	, p.payment_amount
	, p.payment_entry_date
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
	, datediff(dd, s.actual_ship_date, p.payment_entry_date) as aging
	, sb.Product_Class_ID
        , (case when ca.country_code = 'CA' then 1.00
           else (select top 1 conversion_rate from conversion_rate_table where conversion_date <= p.payment_entry_date order by conversion_date desc)end) as conversion_rate
from lead l
inner join consultant lcon on l.consultant_id = lcon.consultant_id
inner join client c on l.lead_id = c.lead_id
inner join client_address ca on c.client_id = ca.client_id and c.client_sequence_code = ca.client_sequence_code and ca.address_type = 'bt'
inner join sale s on c.client_id = s.client_id and c.client_sequence_code = s.client_sequence_code
inner join sales_item si on si.sales_id = s.sales_id 
inner join scratch_book sb on si.scratch_book_id = sb.scratch_book_id
inner join product_class pc on sb.product_class_id = pc.product_class_id
left outer join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id
left outer join package pa on sb.package_id = pa.package_Id 
inner join consultant scon on s.consultant_id = scon.consultant_id
inner join payment p on s.sales_id = p.sales_id
left join Adjustment a on a.Sales_ID =  s.sales_id and a.Adjustment_No = 1 and Adjustment_Amount > 0
WHERE p.payment_entry_date IS NOT NULL
  AND s.actual_ship_date IS NOT NULL
  /** The following functions ensures that the max date between the shipment and the payment is used for the commission report **/
  AND dbo.LaterOf (p.payment_entry_date, s.actual_ship_date) between @start_date and @end_date
and   sb.product_class_id not in (25,26)
and ((lcon.consultant_id = @consultant_id and scon.consultant_id in (2258, 3450, 3518)) or scon.consultant_id = @consultant_id)
--and datediff(dd, coalesce(s.actual_ship_date,getdate()), p.payment_entry_date) <= 90

)  comm
where total_amount<> 0 
group by sale_consultant,
       product_class



end
END
GO

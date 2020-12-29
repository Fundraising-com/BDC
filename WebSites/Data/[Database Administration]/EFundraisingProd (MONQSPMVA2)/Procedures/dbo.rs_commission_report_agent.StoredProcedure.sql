USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_commission_report_agent]    Script Date: 02/14/2014 13:08:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--dbo.rs_commission_report_agent '2005-1-1', '2007-11-30',8

CREATE  PROCEDURE [dbo].[rs_commission_report_agent]
	  @start_date datetime 
	, @end_date datetime 
	, @consultant_id int = null


AS
BEGIN 


if @consultant_id is null 
begin

select * 
    ,(payment_amount * (sales_amount / total_amount) * commission_rate_no_free) * conversion_rate as converted_commission_on_item
    , (case when free_cases <> 0 and product_class_id in (4,5,14,15,17)  -- qty free to be deleted, not useful anymore
		then payment_amount * (sales_amount / total_amount) * (commission_rate_no_free * 0.17)
		else payment_amount * (sales_amount / total_amount) * commission_rate_no_free end) as commission_on_item_new
from 
(

select  s.sales_id
       , ca.country_code 
	, si.sales_item_no
	, p.payment_no
	, sb.scratch_book_id
	, sb.description
	, sb.is_active
	   --if sc, always 5% for agents
        , ( case when sb.product_class_id = 1 then 0.17
                 else sbc.commission_rate end) as commission_rate_no_free 

	--, (case when sb.Product_Class_ID in (1,10,11,21,23,24) then sbc.commission_rate  
	--    when sb.Package_ID = 0 and pa.profit_min is not null and sb.raising_potential is null or sb.raising_potential = 0 then sbc.commission_rate  
	--    when sb.Package_ID <> 0 and pa.profit_min is not null and pa.profit_max <= (1 - si.unit_price_sold / sb.raising_potential) then sbc.commission_rate  
	--    when sb.Package_ID <> 0 and pa.profit_min is not null and pa.profit_min >= (1 - si.unit_price_sold / sb.raising_potential) then sbc.commission_rate + convert(decimal(15,4), convert(real, (pa.profit_max - pa.profit_min) * 100 / 5) * 0.01)  
	--    when sb.Package_ID <> 0 and pa.profit_min is not null and sb.raising_potential is null or sb.raising_potential = 0	then sbc.commission_rate + convert(decimal(15,4), (convert(real, pa.profit_max - (1 - si.unit_price_sold / sb.raising_potential)) * 100 / 5)) * 0.01  
	--    else sbc.commission_rate end ) as commission_rate_no_free
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
        , scon.is_agent 
	, l.lead_id
        , l.organization
	, lcon.name as lead_consultant
        , acon.name as agent_consultant
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
left outer join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id
left outer join package pa on sb.package_id = pa.package_Id 
inner join consultant scon on s.consultant_id = scon.consultant_id
inner join payment p on s.sales_id = p.sales_id

inner join associate_mentor am on scon.consultant_id = am.associate_id
inner join consultant acon on am.mentor_id = acon.consultant_id

where (p.payment_entry_date between @start_date and @end_date)
and scon.is_agent <> 0
--and datediff(dd, coalesce(s.actual_ship_date,getdate()), p.payment_entry_date) <= 90
)  comm
where total_amount<> 0 
order by payment_entry_date asc

end
else
begin

select *
    ,(payment_amount * (sales_amount / total_amount) * commission_rate_no_free) * conversion_rate as converted_commission_on_item 
    , (case when free_cases <> 0 and product_class_id in (4,5,14,15,17)
		then payment_amount * (sales_amount / total_amount) * (commission_rate_no_free * 0.5)  -- qty free to be deleted, not useful anymore
		else payment_amount * (sales_amount / total_amount) * commission_rate_no_free end) as commission_on_item_new
from 
(

select  s.sales_id
       , ca.country_code 
	, si.sales_item_no
	, p.payment_no
	, sb.scratch_book_id
	, sb.description
	, sb.is_active
        , ( case when sb.product_class_id = 1 then 0.17
                 else sbc.commission_rate end) as commission_rate_no_free 
         
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

        , scon.is_agent
 	, l.lead_id
        , l.organization
	, lcon.name as lead_consultant
        , acon.name as agent_consultant
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
left outer join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id
left outer join package pa on sb.package_id = pa.package_Id 
inner join consultant scon on s.consultant_id = scon.consultant_id
inner join payment p on s.sales_id = p.sales_id

inner join associate_mentor am on scon.consultant_id = am.associate_id
inner join consultant acon on am.mentor_id = acon.consultant_id

where ( p.payment_entry_date between @start_date and @end_date)
and scon.is_agent  <> 0
and  (am.mentor_id = @consultant_id)
--and datediff(dd, coalesce(s.actual_ship_date,getdate()), p.payment_entry_date) <= 90

)  comm
where total_amount<> 0 
order by payment_entry_date asc


end
END
GO

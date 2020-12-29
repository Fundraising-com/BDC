USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_client_statement]    Script Date: 02/14/2014 13:08:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[rs_client_statement] @client_id int, @client_sequence_code varchar(2)  AS


select s.sales_id
,convert(varchar(20), s.actual_ship_date, 111) as invoice_date 
,convert(varchar(20), s.payment_due_date, 111) as due_date
, Isnull(s.total_amount, 0) as final_amount
,  coalesce(p.payment_amount, 0) as payment_received
,  Isnull(aa.adjustment_amount, 0) as adjustment_amount
, s.total_amount - (coalesce(p.payment_amount,0) + coalesce(aa.adjustment_amount,0)) as amount_due
, DATEDIFF(day, s.payment_due_date, getdate()) as daypastdue
, s.shipping_fees - s.shipping_fees_discount as shipping_fee
,convert(varchar(20), s.sales_date, 111) as sales_date 
, c.client_sequence_code + ' ' + convert(varchar(20), c.client_id) as customer
, l.lead_id as lead_id
, c.organization as school_name
, c.first_name as client_first_name
, c.last_name as client_last_name
, c.day_phone as client_phone
, c.client_id
, con.name as consultant
, con.email_address as consultant_email
, con.phone_extension as consultant_ext
, s.po_number as po_number
, bc.telephone_number as shipping_phone_number
, bc.Billing_Company_Name
, (bc.street_address + ', ' + bc.city_name + ', ' + bc.state_code + '  ' + bc.zip_code + ' | T.: ' 
	+ bc.telephone_number + '  | F: ' + bc.fax_number) as billing_compagny_address 
, '' +bc.web + '' as billing_compagny_website
, bc.logo_path as billing_compagny_logo
from lead l 
INNER JOIN client c ON l.lead_id = c.lead_id 
INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
left outer join
(
	select 
	pp.sales_id 
	,sum(Isnull(pp.payment_amount, 0)) as payment_amount
	from 
	payment pp
	group by pp.sales_id
) p on p.sales_id = s.sales_id
left outer join
(
	select 
	a.sales_id, 
	sum(Isnull(a.adjustment_amount, 0))  as adjustment_amount
	from 
	adjustment a
	group by a.sales_id
) aa on aa.sales_id = s.sales_id
left join billing_company bc on bc.billing_company_id = s.billing_company_id
inner join consultant con on s.consultant_id = con.consultant_id
where 
c.client_sequence_code = @client_sequence_code
and s.client_id = @client_id
and s.total_amount - (coalesce(p.payment_amount,0) + coalesce(aa.adjustment_amount,0)) > 0
and DATEDIFF(day, s.payment_due_date, getdate()) > 0
and 
 ((s.actual_ship_date is not null and s.box_return_date is null) 
	or (s.actual_ship_date is not null and  s.box_return_date is not null and s.reship_date is not null))
GO

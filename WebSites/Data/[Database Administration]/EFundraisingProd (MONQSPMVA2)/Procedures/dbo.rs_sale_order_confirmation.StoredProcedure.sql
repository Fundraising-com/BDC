USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_sale_order_confirmation]    Script Date: 02/14/2014 13:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[rs_sale_order_confirmation] @sale_id int AS


select
convert(varchar(20), s.sales_date, 111) as invoice_date 
--convert(varchar(4), year(s.actual_ship_date))  + '-' + left('0' + convert(varchar(2), month(s.actual_ship_date) ), 2)+ '-' + left('0' + convert(varchar(2), day(s.actual_ship_date)), 2)  as invoice_date-- (s.invoice_date) as invoice_date
	, c.client_sequence_code + ' ' + convert(varchar(20), c.client_id) as customer
	, l.lead_id as lead_id
	, pt.description as payment_term
	, s.sales_id as sale_id 
	, s.sales_id as ref_number
	, con.name as consultant
	, con.email_address as consultant_email
	, con.phone_extension as consultant_ext
	, s.po_number as po_number
--	, convert(varchar(4), year(s.payment_due_date))  + '-' + left('0' + convert(varchar(2), month(s.payment_due_date) ), 2)+ '-' + left('0' + convert(varchar(2), day(s.payment_due_date)), 2)  as due_date-- (s.invoice_date) as invoice_date
--	, day(s.payment_due_date) + '-' + month(s.payment_due_date) + '-' + year(s.payment_due_date)as due_date
	,convert(varchar(20), s.payment_due_date, 111) as due_date
	, c.organization as school_name
	, c.first_name as client_first_name
	, c.last_name as client_last_name
	, c.day_phone as client_phone
	, c.client_id
	, s.shipping_fees - s.shipping_fees_discount as shipping_fee
	, Isnull(s.fuelsurcharge, 0) as fuel_charge
	, Isnull(s.total_amount, 0)  as total_sale_amount
	, -1 as payment_received --Isnull(p.payment_amount, 0) as payment_received
	, -1  as amount_due --Isnull(s.total_amount, 0) - Isnull(p.payment_amount, 0) - Isnull(aa.adjustment_amount, 0) as amount_due
	, Isnull(s.total_amount, 0) as final_amount --+ Isnull(s.shipping_fees, 0) - Isnull(s.shipping_fees_discount, 0) + Isnull(s.fuelsurcharge, 0) as final_amount
	, -1 as adjustment_amount --Isnull(aa.adjustment_amount, 0) as adjustment_amount
	, bc.telephone_number as shipping_phone_number
	, bc.Billing_Company_Name
	, (bc.street_address + ', ' + bc.city_name + ', ' + bc.state_code + '  ' + bc.zip_code + ' | T.: ' 
		+ bc.telephone_number + '  | F: ' + bc.fax_number) as billing_compagny_address 
	, '' +bc.web + '' as billing_compagny_website
	, bc.logo_path as billing_compagny_logo
	,convert(varchar(20), s.sales_date, 111) as sales_date 
	,Isnull(aptGst.Tax_Amount, -1) as gst_TaxAmount
	,Isnull(aptQst.Tax_Amount, -1) as qst_TaxAmount
	,isnull(tt.Tax_Account_Number, -1) as gst_account
	, s.billing_company_id
from lead l 
INNER JOIN client c ON l.lead_id = c.lead_id 
INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
left outer join --payment p on p.sales_id = s.sales_id
(
select 
pp.sales_id, 
sum(Isnull(pp.payment_amount, 0))  as payment_amount
from 
payment pp
where pp.sales_id = @sale_id
group by pp.sales_id
) p on p.sales_id = s.sales_id
left outer join --payment p on p.sales_id = s.sales_id
(
select 
a.sales_id, 
sum(Isnull(a.adjustment_amount, 0))  as adjustment_amount
from 
adjustment a
where a.sales_id = @sale_id
group by a.sales_id
) aa on aa.sales_id = s.sales_id
inner join payment_term pt on s.payment_term_id = pt.payment_term_id
inner join consultant con on s.consultant_id = con.consultant_id
left join billing_company bc on bc.billing_company_id = s.billing_company_id
left join applicable_tax aptGst on (aptGst.Tax_Code ='GST' and aptGst.sales_id = @sale_id)
left join applicable_tax aptQst on (aptQst.Tax_Code ='QST' and aptQst.sales_id = @sale_id)
left join tax_table tt on tt.tax_code = aptGst.tax_code
where s.sales_id = @sale_id
GO

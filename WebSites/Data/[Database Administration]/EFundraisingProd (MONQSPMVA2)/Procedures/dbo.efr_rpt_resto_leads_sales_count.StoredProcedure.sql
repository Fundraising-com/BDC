USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_resto_leads_sales_count]    Script Date: 02/14/2014 13:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATE BY : Melissa Cote
	CREATE DATE : 2011-09-15
	
	DESCRIPTION : Created for resto.com
	exec [efr_rpt_resto_leads_sales_count] '2011-01-01', '2012-01-01', 821
*/
CREATE PROCEDURE [dbo].[efr_rpt_resto_leads_sales_count] (
    @start_date as datetime 
    , @end_date as datetime
    , @partner_id as int
)
AS
BEGIN 

    declare @end_date2 varchar(30)
    set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
    set @end_date = convert(datetime, @end_date2)

	select 
		'LEAD ID'
		, 'CONSULTANT NAME'
		, 'LEAD ENTRY DATE'
		, 'FIRST NAME'
		, 'LAST NAME'
		, 'ORGANIZATION NAME'
		, 'HAS BEEN CONTACTED'
		, 'RESTO TOTAL QTY'
		, 'RESTO TOTAL AMOUNT'
		, 'RESTO LAST PROCT DESC'
		, 'COUNT OD SALES'
		, 'TOTAL QTY'
		, 'TOTAL AMOUNT'
		, 'LAST SALE DATE'
		, 'LAST SHIP DATE'
		, 'LAST PRODUCT DESC'

	select 
		l.lead_id
		, con.name as consultant_name
		, l.lead_entry_date
		, l.first_name
		, l.last_name
		, l.organization as organization_name
		, (case when l.has_been_contacted = 1 then 'TRUE' ELSE 'FALSE' END) as has_been_contacted
		, SUM(case when sb.product_class_id = 10 then si.quantity_sold end) as resto_total_qty
		, SUM(case when sb.product_class_id = 10 then si.sales_amount end) as resto_total_amount
		, MAX(case when sb.product_class_id = 10 then sb.description else NULL end) as resto_last_product_desc
		, COUNT(distinct s.sales_id) as count_sales
		, SUM(si.quantity_sold) as total_qty
		, SUM(si.sales_amount) as total_amount
		, MAX(s.sales_date) as last_sales_date
		, MAX(s.actual_ship_date) as last_ship_date
		, MAX(sb.description) as last_product_desc

	from lead l 
	inner join consultant con on l.consultant_id = con.consultant_id 
	inner join promotion p on l.promotion_id = p.promotion_id and p.partner_id = @partner_id
	left join client c on l.lead_id = c.lead_id 
	left join sale s on c.client_id = s.client_id and c.client_sequence_code =  c.client_sequence_code and  s.actual_ship_date is not null
	left join sales_item si on s.sales_id = si.sales_id 
	left join scratch_book sb on sb.scratch_book_id = si.scratch_book_id
	--inner join product_class pc on pc.product_class_id = sb.product_class_id 
	where l.lead_entry_date between @start_date and @end_date
	group by 	l.lead_id
		, con.name 
		, l.lead_entry_date
		, l.first_name
		, l.last_name
		, l.organization 
		, (case when l.has_been_contacted = 1 then 'TRUE' ELSE 'FALSE' END) 
	order by l.lead_id

END 

/*
select * from partner where partner_name like 'res%'
select * from promotion where 1=2
select * from sales_item where 1=2
select * from scratch_book where 1=2
select * from product_class
*/
GO

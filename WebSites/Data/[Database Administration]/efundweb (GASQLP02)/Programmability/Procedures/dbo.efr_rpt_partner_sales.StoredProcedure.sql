USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_partner_sales]    Script Date: 02/14/2014 13:04:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
ALTER   BY : Melissa Cote
CREATION DATE : April 07, 2005
list of all sales shipped by partner for a specifique period
*/	
CREATE   PROCEDURE [dbo].[efr_rpt_partner_sales](
	@partner_id int
	, @date_from datetime 
	, @date_to datetime
)

AS
BEGIN 
	if @partner_id = -1
	begin
		set @partner_id = 0
		set @date_from = '2000-01-01'
		set @date_to = '2000-02-20'
	end

select 
	c.first_name
	, c.last_name
	, ca.street_address as address
	, ca.city
	, ca.state_code
	, ca.country_code
	, ca.zip_code
	, c.day_phone
	, c.evening_phone
	, c.email
	, ot.organization_type_desc as organization_type
	, gt.description as group_type
	, l.participant_count
	, s.sales_id
	, s.sales_date
	, s.confirmed_date
	, s.actual_ship_date as ship_date 
	, min(pc.description) as product_class_desc
	, s.total_amount 
	, count(si.sales_item_no) as total_product
from efundraisingprod.dbo.client c
	inner join efundraisingprod.dbo.client_address ca 
	on c.client_sequence_code = ca.client_sequence_code and c.client_id = ca.client_id
	inner join efundraisingprod.dbo.lead l
	on c.lead_id = l.lead_id
	inner join efundraisingprod.dbo.promotion p
	on c.promotion_id = p.promotion_id
	and p.partner_id = @partner_id 
	inner join efundraisingprod.dbo.sale s
	on c.client_sequence_code = s.client_sequence_code and c.client_id = s.client_id
	inner join efundraisingprod.dbo.sales_item si 
	on s.sales_id = si.sales_id
	inner join efundraisingprod.dbo.scratch_book sb
	on sb.scratch_book_id = si.scratch_book_id
	inner join efundraisingprod.dbo.product_class pc
	on pc.product_class_id = sb.product_class_id
	inner join efundraisingprod.dbo.group_type gt
	on c.group_type_id = gt.group_type_id
	inner join efundraisingprod.dbo.organization_type ot
	on l.organization_type_id = ot.organization_type_id 
where 
	s.sales_date between @date_from and @date_to
	and s.confirmed_date is not null --	and s.sales_status_id = 2
	and ca.address_type = 'st'
group by 
	c.first_name
	, c.last_name
	, ca.street_address
	, ca.city
	, ca.state_code
	, ca.country_code
	, ca.zip_code
	, c.day_phone
	, c.evening_phone
	, c.email
	, ot.organization_type_desc 
	, gt.description 
	, l.participant_count
	, s.sales_id
	, s.sales_date
	, s.confirmed_date
	, s.actual_ship_date 
--	, pc.description 
	, s.total_amount 
order by s.sales_id
END
GO

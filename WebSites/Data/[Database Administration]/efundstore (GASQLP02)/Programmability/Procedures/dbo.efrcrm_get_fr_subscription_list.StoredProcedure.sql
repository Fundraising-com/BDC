USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_fr_subscription_list]    Script Date: 02/14/2014 13:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------efrcrm_get_fr_subscription_list---------------------------------------------------------------------------

-- Generate get by id stored proc for Lead
CREATE PROCEDURE [dbo].[efrcrm_get_fr_subscription_list] 
as
begin

DELETE FROM [efundstore].[dbo].[newsletter_email_tmp]

INSERT INTO newsletter_email_tmp 

select  Distinct REPLACE(l.email, ',', '') as email
			, l.first_name
			, l.last_name
			, gt.description
			, REPLACE(l.organization, ',', '') as organization
			, ot.organization_type_desc
			, efundraisingprod.dbo.consultant.name
			, efundraisingprod.dbo.consultant.ext_consultant_id 
			, efundraisingprod.dbo.consultant.is_active
			, efundraisingprod.dbo.partner.partner_name
			, count(distinct s.sales_id) as countofsales
			, count(si.quantity_sold) as quantity_sold
			, sum(s.total_amount) as total_amount
			, sum(payment_amount) as payment_amount
			, max(pc.description) as product_class
			, max(sb.description) as product
FROM efundraisingprod.dbo.lead l
		INNER JOIN efundraisingprod.dbo.promotion ON l.promotion_id = efundraisingprod.dbo.promotion.promotion_id 
		INNER JOIN efundraisingprod.dbo.consultant ON l.consultant_id = efundraisingprod.dbo.consultant.consultant_id
		inner join efundraisingprod.dbo.organization_type ot on l.organization_type_id = ot.organization_type_id
		inner join efundraisingprod.dbo.group_type gt on l.group_type_id = gt.group_type_id
		inner join efundraisingprod.dbo.partner ON efundraisingprod.dbo.partner.partner_id = efundraisingprod.dbo.promotion.partner_id 
		left outer join efundraisingprod.dbo.crm_static_past3seasons_new p3s on p3s.qsp_account_matching_code = l.matching_code
			and fm_id not in(1234,1366,1386,1555,1556,1557,1558,1559,1560,1561,1562,1563,1564,1565,1566,1567,1568,1569,1570,
			1571,1572,1573,1574,1575,1576,1577,1578,1579,1580,1581,1582,1583,1683,1684,5728,5729)
			
		LEFT JOIN efundraisingprod.dbo.client c with(nolock) on c.lead_id = l.lead_id 
		LEFT JOIN efundraisingprod.dbo.Sale s with(nolock) 
				on c.client_id = s.client_id and c.client_sequence_code = s.client_sequence_code 
				and production_status_id in (4 , 8) 
		LEFT JOIN efundraisingprod.dbo.Sales_Item si with(nolock) on si.sales_id = s.sales_id
		LEFT JOIN efundraisingprod.dbo.payment pa with(nolock) on pa.sales_id = s.sales_id
		LEFT JOIN efundraisingprod.dbo.product_class pc on pc.product_class_id = si.product_class_id
		LEFT JOIN efundraisingprod.dbo.scratch_book sb on sb.scratch_book_id = si.scratch_book_id	
WHERE (l.email IS NOT NULL) 
	  AND (efundraisingprod.dbo.promotion.promotion_type_code <> 'AG') 
	  AND (efundraisingprod.dbo.promotion.promotion_type_code <> 'QC') 
	  AND (efundraisingprod.dbo.promotion.promotion_type_code <> 'FB') 
	  AND (efundraisingprod.dbo.promotion.promotion_type_code <> 'GF') 
	  AND (l.promotion_id <> 126) 
	  AND (l.promotion_id <> 172) 
	  AND (l.country_code = 'US') 
	  AND (l.onemaillist = 1) 
	  AND (efundraisingprod.dbo.promotion.partner_id  in (686))
	  AND (l.valid_email = 1) 
	  AND (efundraisingprod.dbo.consultant.is_fm = 0) 
	  AND (efundraisingprod.dbo.consultant.is_agent = 0) 
	  AND (l.ext_consultant_id is null) 
	  AND (l.transfered_date is null) 
	  and (efundraisingprod.dbo.promotion.promotion_type_code <> 'OUT') 
	  and efundraisingprod.dbo.promotion.promotion_type_code <> 'ON' 
	  AND CHARINDEX('@', l.email) > 1
	  AND l.lead_entry_date >='07/01/04'
	  group by REPLACE(l.email, ',', '')
			, l.first_name
			, l.last_name
			, gt.description
			, REPLACE(l.organization, ',', '')
			, ot.organization_type_desc
			, efundraisingprod.dbo.consultant.name
			, efundraisingprod.dbo.consultant.ext_consultant_id 
			, efundraisingprod.dbo.consultant.is_active
			, efundraisingprod.dbo.partner.partner_name

	
	INSERT INTO newsletter_email_tmp 
	SELECT n.email, null, null, null, null, null, null, null, null, efundraisingprod.dbo.partner.partner_name, null, null, null, null, null, null
	FROM efundstore..newsletter_subscription n
	inner join efundraisingprod.dbo.partner ON efundraisingprod.dbo.partner.partner_id = n.partner_id 
	WHERE( unsubscribed IS NULL OR unsubscribed = 0)
	  AND CHARINDEX('@', n.email) > 1


	DELETE FROM newsletter_email_tmp 
	WHERE email in (
		select n.email
		from efundstore..newsletter_subscription n
		where unsubscribed = 1)


	DELETE
	FROM newsletter_email_tmp 
	WHERE email in (
		SELECT efundraisingprod.dbo.lead.email
		FROM efundraisingprod.dbo.lead
		WHERE onemaillist = 0
	)

	select email, first_name, last_name, organization, organization_type_description, group_type_description, partner_name, consultant, consultant_ext, is_active, count_of_sales, quantity_sold, total_amount, payment_amount, product_class, product
	from newsletter_email_tmp 
	

end
GO

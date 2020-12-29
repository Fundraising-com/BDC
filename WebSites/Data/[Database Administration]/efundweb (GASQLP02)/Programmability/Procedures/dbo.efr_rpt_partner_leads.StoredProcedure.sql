USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_partner_leads]    Script Date: 02/14/2014 13:04:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
CREATE BY : Melissa Cote
CREATION DATE : April 07, 2005
list of all sales shipped by partner for a specific period
*/	
CREATE    PROCEDURE [dbo].[efr_rpt_partner_leads](
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
		set @date_to = '2000-01-15'
	end

select 
	l.lead_id
	,l.first_name
	, l.last_name
	, l.street_address as address
	, l.city
	, l.state_code
	, l.country_code
	, l.zip_code
	, l.day_phone
	, l.evening_phone
	, l.email
	, ot.organization_type_desc as organization_type
	, gt.description as group_type
	, l.participant_count
from efundraisingprod.dbo.lead l
	inner join efundraisingprod.dbo.promotion p
	on l.promotion_id = p.promotion_id
	and p.partner_id = @partner_id 
	inner join efundraisingprod.dbo.group_type gt
	on l.group_type_id = gt.group_type_id
	inner join efundraisingprod.dbo.organization_type ot
	on l.organization_type_id = ot.organization_type_id 
where 
	l.lead_entry_date between @date_from and @date_to
order by
	l.lead_id

END
GO

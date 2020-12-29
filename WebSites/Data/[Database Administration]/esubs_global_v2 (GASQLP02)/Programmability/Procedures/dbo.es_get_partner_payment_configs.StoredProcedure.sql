USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_payment_configs]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_payment_config
CREATE PROCEDURE [dbo].[es_get_partner_payment_configs] AS
begin

select ppc.Partner_id, p.Profit_percentage + coalesce(profit_range_percentage, 0) as Profit_percentage
	, ppc.Payment_to, ppc.Email_template_id, ppc.First_email_template_id, ppc.Is_default
	, p.profit_id
	, ppc.partner_payment_info_id 
	from Partner_payment_config ppc
	inner join efrcommon..partner_profit pp on ppc.partner_id = pp.partner_id and pp.end_date is null
	inner join efrcommon..profit p on pp.profit_group_id = p.profit_group_id and qsp_catalog_type_id is null
	left join (
		select sum(pr.profit_range_percentage) as profit_range_percentage, pr.profit_id
		from efrcommon..profit_range pr 
		group by pr.profit_id
	) pr on pr.profit_id = p.profit_id
		
end
GO

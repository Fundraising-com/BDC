USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_payment_config_by_payment_id]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_partner_payment_config_by_payment_id] @Payment_id int as
begin

select ppConfig.Partner_id, pr.Profit_percentage+ coalesce(profit_range_percentage, 0) as Profit_percentage, ppConfig.Payment_to, ppConfig.Email_template_id, ppConfig.First_email_template_id, ppConfig.Is_default, ppConfig.partner_payment_info_id, ppConfig.excluded
, pr.profit_id
from payment p
   inner join partner_payment_info ppinfo
      on (ppinfo.payment_info_id = p.payment_info_id and ppinfo.active = 1)
   inner join partner_payment_config ppConfig 
     on ppinfo.partner_payment_info_id = ppConfig.partner_payment_info_id
	inner join efrcommon..partner_profit pp on ppconfig.partner_id = pp.partner_id and pp.end_date is null
	inner join efrcommon..profit pr on pp.profit_group_id = pr.profit_group_id and qsp_catalog_type_id is null
left join (
		select sum(pr.profit_range_percentage) as profit_range_percentage, pr.profit_id
		from efrcommon..profit_range pr 
		group by pr.profit_id
	) prr on prr.profit_id = pr.profit_id
where
p.Payment_id = @Payment_id

end
GO

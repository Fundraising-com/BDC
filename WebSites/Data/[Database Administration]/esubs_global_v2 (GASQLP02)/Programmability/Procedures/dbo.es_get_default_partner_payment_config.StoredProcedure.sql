USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_default_partner_payment_config]    Script Date: 02/14/2014 13:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_payment_config

CREATE PROCEDURE [dbo].[es_get_default_partner_payment_config] AS

begin

SELECT ppc.Partner_id, 
            p.Profit_percentage, 
            ppc.Payment_to, 
            ppc.Email_template_id, 
            ppc.First_email_template_id, 
            ppc.Is_default, 
            ppc.partner_payment_info_id,
            p.profit_id
      FROM Partner_payment_config ppc
      inner join efrcommon..partner_profit pp on ppc.partner_id = pp.partner_id and pp.end_date is null
      inner join efrcommon..profit p on pp.profit_group_id = p.profit_group_id
      WHERE Is_default = 1 and ppc.Partner_id = -1

 

end
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotion_by_lead_id]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Promotion
CREATE PROCEDURE [dbo].[efrcrm_get_promotion_by_lead_id]
 @leadid int AS
begin

select p.Promotion_id, Promotion_type_code, Targeted_market_id, Advertising_support_id, Advertisement_id, Partner_id, Advertiser_id, Transfer_status_id, Advertisment_type_id, Destination_id, Advertiser_partner_id, Grabber_id, [Description], Script_name, Contact_name, Visibility, Tracking_serial, Nb_impression_bought, Is_active, p.Cookie_content, Is_predictive, Keyword, Is_displayable 
from Promotion p inner join Lead l on p.promotion_id = l.promotion_id
where l.lead_id = @leadid

end
GO

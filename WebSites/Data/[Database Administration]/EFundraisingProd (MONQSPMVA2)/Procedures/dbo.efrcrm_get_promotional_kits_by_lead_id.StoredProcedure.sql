USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotional_kits_by_lead_id]    Script Date: 02/14/2014 13:05:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Promotion_kit
CREATE PROCEDURE [dbo].[efrcrm_get_promotional_kits_by_lead_id]
 @leadid int AS
begin

select Promotional_kit_id, Lead_id, Lead_visit_id, Kit_type_id, Carrier_id, Carrier_tracking_id, Postal_address_id, Validated, Create_date, Sent_date, sample_id
from Promotional_kit

where Promotional_kit .lead_id = @leadid

end
GO

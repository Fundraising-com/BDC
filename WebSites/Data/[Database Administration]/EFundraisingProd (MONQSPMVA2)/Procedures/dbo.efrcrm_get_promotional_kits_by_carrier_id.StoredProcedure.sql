USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotional_kits_by_carrier_id]    Script Date: 02/14/2014 13:05:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for AR_Consultant
CREATE PROCEDURE [dbo].[efrcrm_get_promotional_kits_by_carrier_id] @carrier_id int AS
begin

select Promotional_kit_id, Lead_id, Lead_visit_id, Kit_type_id, Carrier_id, Carrier_tracking_id, Postal_address_id, Validated, Create_date, Sent_date, sample_id from promotional_kit 
where carrier_id = @carrier_id
and carrier_tracking_id is null
and validated = 1
and sent_date is null

end
GO

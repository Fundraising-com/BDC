USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotional_kit_by_id]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Promotional_kit
CREATE PROCEDURE [dbo].[efrcrm_get_promotional_kit_by_id] @Promotional_kit_id int AS
begin

select Promotional_kit_id, Lead_id, Lead_visit_id, Kit_type_id, Carrier_id, Carrier_tracking_id, Postal_address_id, Validated, Create_date, Sent_date, sample_id from Promotional_kit where Promotional_kit_id=@Promotional_kit_id

end
GO

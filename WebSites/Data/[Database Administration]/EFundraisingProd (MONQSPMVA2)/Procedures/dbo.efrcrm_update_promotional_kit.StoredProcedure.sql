USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_promotional_kit]    Script Date: 02/14/2014 13:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Promotional_kit
CREATE PROCEDURE [dbo].[efrcrm_update_promotional_kit] @Promotional_kit_id int, @Lead_id int, @Lead_visit_id int, @Kit_type_id int, @Carrier_id tinyint, @Carrier_tracking_id int, @Postal_address_id int, @Validated smallint, @Create_date datetime, @Sent_date datetime, @Sample_ID int AS
begin

update Promotional_kit set Lead_id=@Lead_id, Lead_visit_id=@Lead_visit_id, Kit_type_id=@Kit_type_id, Carrier_id=@Carrier_id, Carrier_tracking_id=@Carrier_tracking_id, Postal_address_id=@Postal_address_id, Validated=@Validated, Create_date=@Create_date, Sent_date=@Sent_date, Sample_ID =@Sample_ID where Promotional_kit_id=@Promotional_kit_id

end
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_promotional_kit]    Script Date: 02/14/2014 13:07:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Promotional_kit

CREATE PROCEDURE [dbo].[efrcrm_insert_promotional_kit] @Promotional_kit_id int OUTPUT, @Lead_id int, @Lead_visit_id int, @Kit_type_id int, @Carrier_id tinyint, @Carrier_tracking_id int, @Postal_address_id int, @Validated smallint, @Create_date datetime, @Sent_date datetime, @Sample_ID int = null AS

begin

insert into Promotional_kit(Lead_id, Lead_visit_id, Kit_type_id, Carrier_id, Carrier_tracking_id, Postal_address_id, Validated, Create_date, Sent_date, Sample_ID) values(@Lead_id, @Lead_visit_id, @Kit_type_id, @Carrier_id, @Carrier_tracking_id, @Postal_address_id, @Validated, @Create_date, @Sent_date, @Sample_ID)

select @Promotional_kit_id = SCOPE_IDENTITY()

end
GO

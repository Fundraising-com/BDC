USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotional_kits_ready_for_fedex]    Script Date: 02/14/2014 13:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_promotional_kits_ready_for_fedex]
AS
begin

select Promotional_kit_id, Lead_id, Lead_visit_id, Kit_type_id, Carrier_id, Carrier_tracking_id, Postal_address_id, Validated, Create_date, Sent_date, sample_id 
from Promotional_kit inner join fedex ON Promotional_kit.carrier_tracking_id = fedex.fedex_id
and fedex.inter_tracking_number is not null
and fedex.cancelled <> 1
and Promotional_kit.carrier_id = 2 -- make sure its a fedex tracking_id
and inter_update_sale_date is null

end
GO

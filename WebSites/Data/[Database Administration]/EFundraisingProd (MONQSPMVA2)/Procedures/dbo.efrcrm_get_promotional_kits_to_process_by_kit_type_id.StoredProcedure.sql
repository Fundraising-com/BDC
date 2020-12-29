USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotional_kits_to_process_by_kit_type_id]    Script Date: 02/14/2014 13:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_promotional_kits_to_process_by_kit_type_id] @kit_type_id int AS
begin

select top 100  Promotional_kit_id, 
	pk.Lead_id, 
	pk.Lead_visit_id, 
	pk.Kit_type_id, 
	pk.Carrier_id, 
	pk.Carrier_tracking_id, 
	pk.Postal_address_id, 
	pk.Validated, 
	pk.Create_date, 
	pk.Sent_date, 
	pk.sample_id,
	c.name
from promotional_kit pk inner join lead l on pk.lead_id = l.lead_id
inner join consultant c on c.consultant_id= l.consultant_id
where validated = 1
	and sent_date is null
	and postal_address_id is not null
	and kit_type_id = @kit_type_id
order by kit_type_id 

end
GO

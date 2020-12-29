USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotional_kits_to_process]    Script Date: 02/14/2014 13:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for AR_Consultant
CREATE PROCEDURE [dbo].[efrcrm_get_promotional_kits_to_process] AS
begin

select top 100    pk.Promotional_kit_id
			    , pk.Lead_id, Lead_visit_id
				, pk.Kit_type_id, Carrier_id
				, pk.Carrier_tracking_id
				, pk.Postal_address_id
				, pk.Validated
				, pk.Create_date
				, pk.Sent_date
				, pk.sample_id
				, c.name
from promotional_kit pk inner join lead l on pk.lead_id = l.lead_id
inner join consultant c on c.consultant_id= l.consultant_id
where validated = 1
	and sent_date is null
	and postal_address_id is not null
order by kit_type_id 

end
GO

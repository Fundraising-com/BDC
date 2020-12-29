USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_promotional_kits_address]    Script Date: 02/14/2014 13:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE           procedure [dbo].[efrcrm_promotional_kits_address]
		@promotional_kit_ids ntext
as



BEGIN

DECLARE @idoc int

IF (@promotional_kit_ids IS NULL) 
RETURN


EXEC sp_xml_preparedocument @idoc OUTPUT, @promotional_kit_ids

Select pk.promotional_kit_id, pk.lead_id, pa.postal_address_id,pa.address, pa.city, pa.zip_code,pa.country_code, pa.subdivision_code, 
s.subdivision_name_1 as subdivision_name, s.subdivision_category, l.organization as groupName, l.first_name + ' ' + l.last_name as leadName
, Isnull(kt.comments, 'D') as comments
From promotional_kit pk
INNER JOIN (SELECT   id
	FROM   OPENXML (@idoc, '/pids/p')
	WITH (id  varchar(20))) tids on tids.id = pk.promotional_kit_id
left join Kit_Type kt on pk.kit_type_id = kt.kit_type_id
inner join postal_address pa on pa.postal_address_id = pk.postal_address_id
inner join subdivision s on s.subdivision_code = pa.subdivision_code
left join lead l on l.lead_id = pk.lead_id
END
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_partners]    Script Date: 02/14/2014 13:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner
CREATE PROCEDURE [dbo].[efrcrm_get_partners] AS
begin

select Partner_id, Partner_group_type_id, Partner_subgroup_type_id, Partner_name, Partner_path, Esubs_url, Estore_url, Free_kit_url, Logo, Phone_number, Email_ext, Url, Guid, Prize_eligible, Has_collection_site from Partner

end
GO

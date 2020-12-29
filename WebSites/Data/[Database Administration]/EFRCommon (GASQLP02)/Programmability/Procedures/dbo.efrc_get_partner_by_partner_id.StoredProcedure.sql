USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_partner_by_partner_id]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by: Philippe Girard
	Date: 9 Novembre 2005
	
	2013 added proc to efrcommon, original proc on efundweb
*/
Create PROCEDURE [dbo].[efrc_get_partner_by_partner_id]
	@partnerID int
AS
BEGIN
SELECT     
	--partner_group_type_id,
	--country_id,
	partner_name,
	--partner_path,
	--eSubs_url, 
    --eStore_url,
	--free_kit_url,
	--phone_number,
	--url,
	guid,
	--prize_eligible,
	has_collection_site
    --partner_folder
FROM         
	partner
where 
	partner_id = @partnerID
END
GO

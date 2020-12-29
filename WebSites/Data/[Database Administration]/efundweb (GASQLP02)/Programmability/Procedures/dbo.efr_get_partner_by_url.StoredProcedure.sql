USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_partner_by_url]    Script Date: 02/14/2014 13:04:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by: Philippe Girard
	Date: 10 Novembre 2005
*/
CREATE PROCEDURE [dbo].[efr_get_partner_by_url]
	@url varchar(50)
AS
BEGIN
SELECT
	partner_id,      
	partner_group_type_id,
	country_id,
	partner_name,
	partner_path,
	eSubs_url, 
    	eStore_url,
	free_kit_url,
	phone_number,
	url,
	guid,
	prize_eligible,
	has_collection_site,
    	partner_folder
FROM         
	partner
where 
	url = @url
END
GO

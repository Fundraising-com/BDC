USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_partner_by_path]    Script Date: 02/14/2014 13:04:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:	Guillaume Carmel
-- Description	Get a partner with all his fields by his path
-- Create Date	2006-08-21

CREATE PROCEDURE [dbo].[efr_get_partner_by_path]
	@partner_path varchar(50)
AS
BEGIN
SELECT
	partner_id,
	partner_group_type_id,
	partner_subgroup_type_id,
	country_id,
	partner_name,
	partner_password,
	partner_path,
	eSubs_url, 
	eStore_url,
	free_kit_url,
	logo,
	phone_number,
	email_ext,
	url,
	guid,
	prize_eligible,
	has_collection_site,
	partner_folder
FROM
	partner
where
	partner_path = @partner_path
END
GO

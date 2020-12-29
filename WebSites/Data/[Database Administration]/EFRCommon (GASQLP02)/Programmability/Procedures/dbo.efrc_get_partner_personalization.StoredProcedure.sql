USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_partner_personalization]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrc_get_partner_personalization](
	@partner_id int, @culture_code nvarchar(5) = 'en-US'
	)
AS
BEGIN
	
	select dbo.efrc_get_partner_name(@partner_id, @culture_code) as partner_name
		, dbo.efrc_get_partner_url(@partner_id, @culture_code) as partner_url
		, dbo.efrc_get_partner_phone(@partner_id, @culture_code) as phone_number
		, dbo.efrc_get_partner_email(@partner_id, @culture_code) as email_address
		, dbo.efrc_get_partner_fax(@partner_id, @culture_code) as fax
		, dbo.efrc_get_partner_mailing_address(@partner_id, @culture_code) as mailing_address
		, dbo.[efrc_get_partner_path] (@partner_id, @culture_code) as partner_path
		, dbo.[efrc_get_partner_folder] (@partner_id, @culture_code) as partner_folder

END
GO

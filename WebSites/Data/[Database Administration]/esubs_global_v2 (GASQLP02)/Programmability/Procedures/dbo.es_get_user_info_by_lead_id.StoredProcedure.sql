USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_user_info_by_lead_id]    Script Date: 02/14/2014 13:06:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	es_get_user_info_lead
	
	Created by: Philippe Girard
	Date: 12 august 2005

	Description: get the info from the lead information table
	in efundraisingprod to create a user
*/
create PROCEDURE [dbo].[es_get_user_info_by_lead_id]
	@lead_id int
AS
SELECT 
	-- user info
	-- no culture_code
	country_code
	, 0 as opt_status_id
	, first_name
	, '' as middle_name
	, last_name
	, NULL as gender
	, email as email_address
	, NULL as password
	, 0 as bounced
	, NULL as comments
FROM eFundraisingProd..lead
WHERE lead_id = @lead_id
GO

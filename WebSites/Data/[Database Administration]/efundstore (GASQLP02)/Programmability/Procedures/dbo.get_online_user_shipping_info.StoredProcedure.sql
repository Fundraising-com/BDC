USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_online_user_shipping_info]    Script Date: 02/14/2014 13:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	jf lavigne
Created On:	june 1, 2004
Description:	This store procedure returns the shipping and billing info of a client
*/
CREATE PROCEDURE [dbo].[get_online_user_shipping_info]
	@intOnlineUserID INT
AS
SELECT
	c.first_name
	, c.last_name
	, c.title
	, c.organization
             	, ca.zip_code
	, ca.city
	, ca.state_code
	, s.state_name
	, ca.country_code
	, co.country_name
	, ca.street_address
	, ca.address_type
            
FROM
	eFundraisingProd..client c 
	INNER JOIN eFundraisingProd..client_address ca 
		ON c.client_sequence_code = ca.client_sequence_code
		 AND c.client_id = ca.client_id 
	INNER JOIN dbo.State s 
		ON ca.state_code = s.State_Code 
	INNER JOIN dbo.countries co 
		ON ca.country_code = co.country_code
	INNER JOIN dbo.online_users ou 
		ON c.client_sequence_code = ou.client_sequence_code
		 AND c.client_id = ou.client_id
WHERE
	ou.online_user_ID = @intOnlineUserID
GO

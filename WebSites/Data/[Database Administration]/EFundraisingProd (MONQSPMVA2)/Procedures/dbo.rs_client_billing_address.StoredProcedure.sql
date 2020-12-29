USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_client_billing_address]    Script Date: 02/14/2014 13:08:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rs_client_billing_address] @client_id int, @client_sequence_code varchar(2) AS

select 
c.client_sequence_code
,(case when ca.attention_of is not null 
		then ca.attention_of 
	else c.first_name + ' ' + c.last_name end) as attention_to
	, ca.street_address as bill_address
	, ca.city as bill_city
	, ca.state_code as bill_state
	, ca.zip_code as bill_zip
	, ca.state_code + ', ' + ca.zip_code as statezipecode
	, co.country_name as bill_country
	, c.organization
from client c
inner join client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code 
	and ca.address_type = 'BT'
inner join countries co on ca.country_code = co.country_code 
where c.client_id = @client_id
and c.client_sequence_code = @client_sequence_code
GO

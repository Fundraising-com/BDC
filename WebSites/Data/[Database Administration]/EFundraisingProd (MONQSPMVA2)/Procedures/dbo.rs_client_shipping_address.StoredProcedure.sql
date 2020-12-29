USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_client_shipping_address]    Script Date: 02/14/2014 13:08:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rs_client_shipping_address] @client_id int, @client_sequence_code varchar(2) AS


if(exists(
select (case when ca.attention_of is not null 
		then ca.attention_of 
		else c.first_name + ' ' + c.last_name end) as ship_attention_to
	, ca.street_address as ship_address
	, ca.city as ship_city
	, ca.state_code as ship_state
	, ca.zip_code as ship_zip	
	, co.country_name as ship_country
	, c.organization
from client c
inner join client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code 
	and ca.address_type = 'ST'
inner join countries co on ca.country_code = co.country_code 
where c.client_id = @client_id and c.client_sequence_code=@client_sequence_code))
begin

	select ca.attention_of as ship_attention_to
		, ca.street_address as ship_address
		, ca.city as ship_city
		, ca.state_code as ship_state
		, ca.zip_code as ship_zip, ca.state_code + ', ' + ca.zip_code as statezipecode
		, co.country_name as ship_country
		, ca.phone_1 + ' ' + ca.phone_2 as phone_number
	from client c
	inner join client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code 
		and ca.address_type = 'ST'
	inner join countries co on ca.country_code = co.country_code 
	where c.client_id = @client_id and c.client_sequence_code=@client_sequence_code

end
else
begin

	select (case when ca.attention_of is not null 
		then ca.attention_of 
		else c.first_name + ' ' + c.last_name end) as ship_attention_to
		, ca.street_address as ship_address
		, ca.city as ship_city
		, ca.state_code as ship_state
		, ca.zip_code as ship_zip, ca.state_code + ', ' + ca.zip_code as statezipecode
		, co.country_name as ship_country
		, ca.phone_1 + ' ' + ca.phone_2 as phone_number
	from client c
	inner join client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code 
		and ca.address_type = 'BT'
	inner join countries co on ca.country_code = co.country_code 
	where c.client_id = @client_id and c.client_sequence_code=@client_sequence_code

end
GO

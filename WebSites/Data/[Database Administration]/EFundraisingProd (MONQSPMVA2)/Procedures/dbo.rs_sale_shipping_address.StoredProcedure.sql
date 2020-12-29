USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_sale_shipping_address]    Script Date: 02/14/2014 13:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[rs_sale_shipping_address] @sale_id int AS

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
INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
inner join client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code 
	and ca.address_type = 'ST'
inner join countries co on ca.country_code = co.country_code 
where s.sales_id = @sale_id))
begin

	select ca.attention_of as ship_attention_to
		, ca.street_address as ship_address
		, ca.city as ship_city
		, ca.state_code as ship_state
		, ca.zip_code as ship_zip, ca.state_code + ', ' + ca.zip_code as statezipecode
		, co.country_name as ship_country
		, ca.phone_1 + ' ' + ca.phone_2 as phone_number
	from client c
	INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
	inner join client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code 
		and ca.address_type = 'ST'
	inner join countries co on ca.country_code = co.country_code 
	where s.sales_id = @sale_id

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
	INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
	inner join client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code 
		and ca.address_type = 'BT'
	inner join countries co on ca.country_code = co.country_code 
	where s.sales_id = @sale_id

end
GO

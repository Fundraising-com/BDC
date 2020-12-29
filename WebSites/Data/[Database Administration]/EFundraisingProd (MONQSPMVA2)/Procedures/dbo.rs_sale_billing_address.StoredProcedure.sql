USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_sale_billing_address]    Script Date: 02/14/2014 13:08:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[rs_sale_billing_address] @sale_id int AS

select (case when ca.attention_of is not null 
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
INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
inner join client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code 
	and ca.address_type = 'BT'
inner join countries co on ca.country_code = co.country_code 
where s.sales_id = @sale_id
GO

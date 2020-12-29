USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_call_es_insert_new_lead]    Script Date: 02/14/2014 13:05:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[es_call_es_insert_new_lead]
	@promotion_id as int	
	, @first_name as varchar(50)
	, @last_name as varchar(50)
	, @organization as varchar(100)
	, @street_address as varchar(100)
	, @city as varchar(50)
	, @state_code as varchar(10)
	, @country_code as varchar(10)
	, @zip_code as varchar(10)
	, @day_phone as varchar(20)
	, @email as varchar (50)
	, @participant_count int

as
declare @lead_id int
exec @lead_id = sqlerose_efundraisingprod.efundraisingprod.dbo.es_insert_new_lead
	@promotion_id 
	, @first_name 
	, @last_name 
	, @organization 
	, @street_address 
	, @city 
	, @state_code 
	, @country_code 
	, @zip_code 
	, @day_phone
	, @email 
	, @participant_count

select @lead_id
GO

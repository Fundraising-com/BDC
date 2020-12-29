USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_efr_insert_new_lead]    Script Date: 02/14/2014 13:04:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efr_call_efr_insert_new_lead] (
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
	-- new 
	, @title as varchar (100)
	, @evening_phone as varchar(20)
	, @evening_phone_ext as varchar(4)
	, @day_phone_ext as varchar(4)
	, @best_time_to_call as varchar(50)
	, @organization_type_id as int
	, @group_type_id as int
	, @fundraising_date as varchar(200)
	, @decision_maker as int
	, @products_interest_in as varchar(200)
	, @on_email_list as bit
	, @comments as varchar(200)
	, @lead_status_id int =1
	, @temp_lead_id as int = NULL
	, @consultant_id int = 0
	, @is_postal_address_validated int = 0
	, @fundraisers_per_year tinyint = 1
	, @address_zone_id int = 3
)
as 

declare @lead_id as int


	exec @lead_id = MONQSPMVA2_EFRPROD.efundraisingprod.dbo.efr_insert_new_lead 	
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
		-- new 
		, @title 
		, @evening_phone 
		, @evening_phone_ext 
		, @day_phone_ext 
		, @best_time_to_call 
		, @organization_type_id 
		, @group_type_id 
		, @fundraising_date 
		, @decision_maker
		, @products_interest_in 
		, @on_email_list 
		, @comments
		, @lead_status_id
		, @temp_lead_id
		, @consultant_id
		, @is_postal_address_validated
		, @fundraisers_per_year
		, @address_zone_id


IF @lead_id IS NULL
BEGIN
	RETURN 0
END
ELSE
BEGIN
	return @lead_id
END
GO

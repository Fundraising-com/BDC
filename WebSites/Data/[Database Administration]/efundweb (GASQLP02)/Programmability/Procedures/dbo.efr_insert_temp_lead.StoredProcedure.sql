USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_insert_temp_lead]    Script Date: 02/14/2014 13:04:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  procedure [dbo].[efr_insert_temp_lead]
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
	, @lead_status_id int = 1
as 
declare @intErrorCode INT


BEGIN TRANSACTION

 SET @intErrorCode = @@ERROR

 INSERT INTO temp_Lead
  (
    promotion_id
   , channel_code
   , first_name
   , last_name
   , organization
   , street_address
   , city
   , state_code
   , country_code
   , zip_code
   , day_phone
   , email
   , participant_count
   , evening_phone
   , evening_phone_ext
   , day_phone_ext
   , day_time_call
   , organization_type_id
   , group_type_id
   , decision_maker
   , onemaillist
   , comments
   , lead_status_id
  )
  VALUES
  (    
     @promotion_id 
    , 'INT'
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
   , @evening_phone
   , @evening_phone_ext
   , @day_phone_ext
   , @best_time_to_call
   , @organization_type_id
   , @group_type_id
   , @decision_maker
   , @on_email_list
   , @fundraising_date + ' / ' +  @products_interest_in + ' / ' + @comments
   , @lead_status_id
  )

 SET @intErrorCode = @@ERROR
 IF @intErrorCode = 0 
 begin
  	SET @intErrorCode = 0
	commit transaction
	return @intErrorCode	
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	return @intErrorCode
 end
GO

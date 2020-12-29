USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_insert_new_lead]    Script Date: 02/14/2014 13:03:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[efr_insert_new_lead]
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
	, @temp_lead_id as int = NULL
	, @consultant_id int = 0
	, @is_postal_address_validated int = 0
	, @fundraisers_per_year tinyint = 1
	, @address_zone_id int = 3
	, @evening_time_call as varchar(20) = NULL
	, @group_web_site as varchar(50) = NULL
	, @fk_kit_type_id as int = 42
as 
declare @intErrorCode INT
declare @intLeadID int
declare @comment_id int

BEGIN TRANSACTION

 SET @intErrorCode = @@ERROR

 EXEC @intLeadID = newid 'Lead_ID', 'ALL'
 SET @intErrorCode = @@ERROR

 IF @intErrorCode = 0 AND @intLeadID > 0 
 begin
  	SET @intErrorCode = 0
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	return @intErrorCode
 end

 EXEC @comment_id = newid 'Comments_ID', 'ALL'
 SET @intErrorCode = @@ERROR

 IF @intErrorCode = 0 AND @comment_id > 0 
 begin
  	SET @intErrorCode = 0
 end
 ELSE 
 begin
  	SET @intErrorCode = -2
	rollback transaction
	return @intErrorCode
 end


  INSERT INTO Lead
  (
   lead_id
   , lead_status_id
   , lead_priority_id
   , promotion_id
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
   , temp_lead_id
   , fund_raiser_start_date
   , consultant_id
   , is_postal_address_validated
   , fundraisers_per_year
   , address_zone_id
	, evening_time_call
	, group_web_site
	,fk_kit_type_id
  )
  VALUES
  (
    @intLeadID
   , @lead_status_id
    , 1
    , @promotion_id 
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
   , @comments
   , @temp_lead_id
   , @fundraising_date
   , @consultant_id
   , @is_postal_address_validated
   , @fundraisers_per_year
   , @address_zone_id
,@evening_time_call
	,@group_web_site
	,@fk_kit_type_id
  )

 SET @intErrorCode = @@ERROR
 IF @intErrorCode = 0 
 begin
  	SET @intErrorCode = 0
 end
 ELSE 
 begin
  	SET @intErrorCode = -3
	rollback transaction
	return @intErrorCode
 end

insert into comments (
	Comments_ID
	, Consultant_ID
	, Department_ID
	, Lead_ID
	, Entry_Date
	, Comments
	) 
	VALUES (
	@comment_id
	, 1523
	, 4
	, @intleadid
	, GetDate()
	, @fundraising_date + ' / '  + @comments 
	)


 SET @intErrorCode = @@ERROR
 IF @intErrorCode = 0 
 begin
	commit transaction
	return @intLeadID
 end
 ELSE 
 begin
  	SET @intErrorCode = -4
	rollback transaction
	return @intErrorCode
 end
GO

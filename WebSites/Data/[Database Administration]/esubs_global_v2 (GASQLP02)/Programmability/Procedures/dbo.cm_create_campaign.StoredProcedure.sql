USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cm_create_campaign]    Script Date: 02/14/2014 13:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
    cm_create_campaign
    
    Created by: Philippe Girard
    Created on: 07-09-2005
    
    ALTER  a campaign for the consultant module.
*/

CREATE PROCEDURE [dbo].[cm_create_campaign]
    -- generic parameter
   	@culture_code nvarchar(5) = 'en-US'

    -- member parameter
    ,@first_name varchar(50)
    ,@middle_name varchar(50) = NULL
    ,@last_name varchar(50)
    ,@gender char(1) = NULL
    ,@email_address varchar(100)
    ,@password varchar(100) = NULL
    
    -- group parameter
    ,@partner_id int
    ,@lead_id int
    ,@group_name varchar(255)
    ,@expected_membership int = 20
	,@group_url varchar(255) = NULL
	,@group_comments varchar(1024) = NULL
    
    -- event parameter
	,@event_name varchar(255)
	,@start_date datetime = NULL
	,@end_date datetime = NULL
	,@fundraising_goal money = 0.0
AS
BEGIN
    DECLARE @result_code int
    
    -- default value
    DECLARE @creation_channel_id int
    DECLARE @bounced int
    DECLARE @test_group bit
    DECLARE @image_url varchar(255)
    
    -- output values
    DECLARE @member_hierarchy_id int
    DECLARE @member_id int
    DECLARE @group_id int
    DECLARE @event_id int
    DECLARE @event_participation_id int
    DECLARE @personalization_id int
    
    SET @creation_channel_id = 6
    SET @bounced = 0
    SET @test_group = 0
       
    -- TODO: Generated password
    IF @password IS NULL
    BEGIN
        EXEC @result_code = cm_generate_random_password @password = @password OUTPUT
    END

    EXEC @result_code = es_create_member
        @first_name = @first_name
        ,@middle_name = @middle_name
        ,@last_name = @last_name
        ,@gender = @gender
        ,@email_address = @email_address
        ,@password = @password
        ,@creation_channel_id = @creation_channel_id
        ,@member_hierarchy_id = @member_hierarchy_id OUTPUT
        ,@member_id = @member_id OUTPUT

    IF @result_code <> 0
    BEGIN
        RETURN -1
    END    

    EXEC @result_code = es_create_group 
        @sponsor_id = @member_hierarchy_id
        ,@partner_id = @partner_id
        ,@lead_id = @lead_id
        ,@group_name = @group_name
        ,@expected_membership = @expected_membership
        ,@group_url = @group_url
        ,@comments = @group_comments
        ,@group_id = @group_id OUTPUT
        
    IF @result_code <> 0
    BEGIN
        RETURN -2
    END
    
    EXEC @result_code = es_create_event
        @group_id = @group_id
        ,@event_name = @event_name
        ,@start_date = @start_date
        ,@end_date = @end_date
        ,@event_id = @event_id OUTPUT
    
    IF @result_code <> 0
    BEGIN
        RETURN -3
    END
    
    EXEC @result_code = es_create_event_participation
        @event_id = @event_id
        ,@member_hierarchy_id = @member_hierarchy_id
        ,@participation_channel_id = 3
        ,@event_participation_id = @event_participation_id OUTPUT

    IF @result_code <> 0
    BEGIN
        RETURN -4
    END
    
    SELECT @image_url = pav.value
    FROM partner_attribute_value pav
    WHERE pav.partner_id = @partner_id
      AND pav.partner_attribute_id = 4
      AND pav.culture_code = @culture_code
    
    DECLARE @header_title1 varchar(255), @header_title2 varchar(255)
    
    SET @header_title1 = RTRIM(LTRIM(@group_name)) + ' Online Campaign'
    SET @header_title2 = RTRIM(LTRIM(@group_name)) + ' Fundraiser'
    
    -- create personalization
    EXEC @result_code = es_create_personalization
            @event_participation_id = @event_participation_id
	        ,@header_title1 = @header_title1
	        ,@header_title2 = @header_title2 
	        ,@body = 'Welcome to our Fundraising web site. With your help, we are sure to earn the funds we need this year! Thank you for your support!'
	        ,@fundraising_goal = @fundraising_goal
	        ,@site_bgcolor = 'A5A5A5'
	        ,@header_bgcolor = 'EB3E30'
	        ,@header_color = 'FFFFFF'
	        ,@group_url = @group_url
	        ,@image_url = @image_url
	        ,@personalization_id = @personalization_id OUTPUT
    
    IF @result_code <> 0
    BEGIN
        RETURN -5
    END

     declare @address1 varchar(100)
     declare @city varchar(100)
     declare @state_code varchar(15)
     declare @zip_code varchar(10)
     declare @country_code  varchar (50)
     declare @day_phone varchar(50)
     declare @name varchar(100)

    if @middle_name is not null
    begin
    	set @name = isnull(@first_name,'') + ' ' + isnull(@middle_name,'')  + ' ' + isnull(@last_name,'')
    end
    else
    begin
             set @name = isnull(@first_name,'') + ' ' + isnull(@last_name,'')

    end

    SELECT 
	
	 @address1 = street_address 
	, @city = city
	, @state_code = state_code 
	, @zip_code = zip_code
	, @country_code = country_code
	, @day_phone =  day_phone
	FROM eFundraisingProd..lead as lead
		inner join eFundraisingProd..promotion as promotion
			on lead.promotion_id = promotion.promotion_id
	WHERE lead_id = @lead_id


    if (@state_code like '%n/a%' or @state_code = 'BC')
            set @state_code = null
    else
            set @state_code = +'US-'+@state_code

    exec @result_code = es_create_payment_info @group_id 
    , @name
    , @group_name
    , @name
    , @day_phone
    , null  -- SSN
    , @address1
    , null -- address 2
    , @city
    , @zip_code
    , @country_code 
    , @state_code
    , null	
    , null	
    , null	

    IF @result_code <> 0
    BEGIN
       -- rollback transaction xxx
        RETURN -6
    END
    -- return event_id and group_id
    SELECT @event_id as event_id
        , @group_id as group_id

    RETURN 0
END
GO

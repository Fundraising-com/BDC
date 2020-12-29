USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_autocreate_group_by_lead_id]    Script Date: 02/14/2014 13:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:		 Jiro Hidaka
-- Create date:  July 18, 2010
-- Description:	 Auto Create group based on lead id
-- Version:      1.0
-- Error Codes:  0	Succesful
--				-1	Missing manadatory fields (Email, Group Name)
--				-2	Member creation failed
--				-3	Group creation failed
--				-4	Event creation failed
--				-5	Invalid subdivision code
--				-6	Payment Info creation failed
--				-7	Event Participation creation failed
--				-8	Personalization creation failed
--				-9	Personalization Image creation failed
-- ===============================================================
CREATE PROCEDURE [dbo].[es_autocreate_group_by_lead_id]
	@lead_id INT,
    @culture_code VARCHAR(5) = 'en-US',
    @sponsor_creation_channel_id INT = 43,
    @external_group_id varchar(128) = NULL,
	@partner_id INT = NULL

AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @err INT
    DECLARE @event_id INT
    DECLARE @sponsor_id INT
	DECLARE @event_participation_id INT
	DECLARE @orig_partner_id INT
	DECLARE @name VARCHAR(100)
	DECLARE @group_id INT
	DECLARE @group_name VARCHAR(100)
	DECLARE @address_1 VARCHAR(100)
	DECLARE @city VARCHAR(50)
	DECLARE @subdivision_code VARCHAR(10)
	DECLARE @country_code VARCHAR(10)
	DECLARE @zip_code VARCHAR(10)
	DECLARE @day_phone VARCHAR(20)
	DECLARE @email VARCHAR(50)
	DECLARE @expected_membership INT
	DECLARE @group_url VARCHAR(50)
	DECLARE @member_id INT
    DECLARE @profit_group_id INT
    DECLARE @profit_calculated FLOAT
	DECLARE	@postal_address_id INT
	DECLARE	@phone_number_id INT
	DECLARE	@payment_info_id INT
    DECLARE	@personalization_id INT
	DECLARE @image_id INT

    /* Get the group info from a lead_id:
		  If the group already exist, return 0,
          If the group doesn't exist, create group from the returned data */
    CREATE TABLE #tmp
	( event_participation_id INT
	 ,group_id INT
	 ,sponsor_id INT
	 ,orig_partner_id INT
	 ,lead_id INT
	 ,name VARCHAR(100)
	 ,group_name VARCHAR(100)
	 ,address_1 VARCHAR(100)
	 ,city VARCHAR(50)
	 ,subdivision_code VARCHAR(10)
	 ,country_code VARCHAR(10)
	 ,zip_code VARCHAR(10)
	 ,day_phone VARCHAR(20)
	 ,email VARCHAR(50)
	 ,expected_membership INT
	 ,group_url VARCHAR(50))
    INSERT #tmp
	EXEC es_get_group_information_by_lead_id @lead_id

	IF EXISTS(SELECT * FROM #tmp WHERE (orig_partner_id = @partner_id OR @partner_id IS NULL))
	BEGIN
		SELECT   @event_participation_id = event_participation_id 
				,@group_id = group_id
				,@sponsor_id = sponsor_id
				,@partner_id = ISNULL(@partner_id, orig_partner_id)
				,@name = name 
				,@group_name = group_name
				,@address_1 = address_1
				,@city = city
				,@subdivision_code = subdivision_code
				,@country_code = country_code
				,@zip_code = zip_code
				,@day_phone = day_phone
				,@email = email
				,@expected_membership = expected_membership
				,@group_url = group_url
		FROM    #tmp
		WHERE   (orig_partner_id = @partner_id OR @partner_id IS NULL)
	END
	ELSE
	BEGIN
		SELECT   @event_participation_id = event_participation_id 
				,@group_id = group_id
				,@sponsor_id = sponsor_id
				,@partner_id = ISNULL(@partner_id, orig_partner_id)
				,@name = name 
				,@group_name = group_name
				,@address_1 = address_1
				,@city = city
				,@subdivision_code = subdivision_code
				,@country_code = country_code
				,@zip_code = zip_code
				,@day_phone = day_phone
				,@email = email
				,@expected_membership = expected_membership
				,@group_url = group_url
		FROM    #tmp
	END

    IF @event_participation_id is not null -- <= Group already exists, return 0
	BEGIN
        RETURN 0
    END
	ELSE -- <= Group does not yet exist so create it with the returned info from lead table
    BEGIN

		IF EXISTS(select * from #tmp) AND @event_participation_id IS NULL AND @group_id IS NULL AND @sponsor_id IS NULL AND @name IS NULL 
		   AND @group_name IS NULL AND @address_1 IS NULL AND @city IS NULL AND @subdivision_code IS NULL AND @country_code IS NULL 
		   AND @zip_code IS NULL AND @day_phone IS NULL AND @email IS NULL AND @expected_membership IS NULL AND @group_url IS NULL
		BEGIN			
			SELECT TOP 1 @partner_id = ISNULL(@partner_id, #tmp.orig_partner_id)
						,@name = LTRIM(RTRIM(m.first_name)) + ' ' + LTRIM(RTRIM(m.last_name)) 
						,@group_name = g.group_name
						,@address_1 = pa.address_1
						,@city = pa.city
						,@subdivision_code = pa.subdivision_code
						,@country_code = pa.country_code
						,@zip_code = pa.zip_code
						,@day_phone = pn.phone_number
						,@email = m.email_address
						,@expected_membership = g.expected_membership
						,@group_url = g.group_url
			FROM	#tmp INNER JOIN [group] g WITH (NOLOCK) ON #tmp.group_id = g.group_id INNER JOIN
					member_hierarchy mh WITH (NOLOCK) ON g.sponsor_id = mh.member_hierarchy_id INNER JOIN 
					member m WITH (NOLOCK) ON mh.member_id = m.member_id LEFT JOIN 
					payment_info pi WITH (NOLOCK) ON pi.group_id = g.group_id LEFT JOIN
					postal_address pa WITH (NOLOCK) ON pi.postal_address_id = pa.postal_address_id LEFT JOIN
					phone_number pn WITH (NOLOCK) ON pi.phone_number_id = pn.phone_number_id
			WHERE	pi.active = 1
			ORDER BY #tmp.group_id DESC, pi.payment_info_id DESC
		END

        /* Validate mandatory fields */
        IF RTRIM(ISNULL(@email,''))='' OR RTRIM(ISNULL(@group_name,''))='' 
        BEGIN
			RETURN -1
        END

        /* Build first and last name */
        DECLARE @first_name varchar(50)
		DECLARE @last_name varchar(50)
		SELECT  @first_name = CASE WHEN charindex(' ', rtrim(ltrim(@name)))<>0 THEN
								 substring(@name,1,charindex(' ', @name)-1) 
							  ELSE
								 @name
							  END,
				@last_name = CASE WHEN charindex(' ', rtrim(ltrim(@name)))<>0 THEN
								 substring(@name,charindex(' ', @name)+1,len(@name))
							 ELSE
								 null
							 END       

        /* Generate random password length of 8 alphanumeric characters */
        DECLARE @password VARCHAR(8)
		SET @password=''
		SELECT @password=@password+char(n) from
		(
			SELECT TOP 8 number AS n FROM master..spt_values 
			WHERE TYPE='p' and (number between 48 and 57 OR 
								number between 65 and 90 OR
								number between 97 and 122) 
			ORDER BY newid()
		) AS t
    
        -- select @first_name, @last_name, @password
		
        /* Create sponsor member */
        BEGIN TRANSACTION
		EXEC @err = [dbo].[es_create_member]
					@culture_code = @culture_code,
					@opt_status_id = 1,
					@first_name = @first_name,
					@middle_name = NULL,
					@last_name = @last_name,
					@gender = NULL,
					@email_address = @email,
					@password = @password,
					@bounced = 0,
					@parent_first_name = NULL,
					@parent_last_name = NULL,
					@external_member_id = NULL,
					@comments = NULL,
					@parent_member_hierarchy_id = NULL, -- <= SPONSOR 
					@creation_channel_id = @sponsor_creation_channel_id,
					@partner_id = @partner_id,
					@lead_id = @lead_id,
					@facebook_id = NULL,
					@member_hierarchy_id = @sponsor_id OUTPUT,
					@member_id = @member_id OUTPUT

        IF (@err <> 0)
    	BEGIN
		ROLLBACK TRANSACTION
    		RETURN -2
    	END

        --SELECT @err, @member_hierarchy_id, @member_id

        /* Create Group */
		EXEC @err = [dbo].[es_create_group]
					@sponsor_id = @sponsor_id OUTPUT,
					@partner_id = @partner_id,
					@lead_id = @lead_id,
                    @external_group_id = @external_group_id,
					@group_name = @group_name,
                    @expected_membership = @expected_membership,
                    @group_url = @group_url,
					@group_id = @group_id OUTPUT

        IF (@err <> 0)
    	BEGIN
		ROLLBACK TRANSACTION
    		RETURN -3
    	END

        /* Get Profit ID and Profit Calculated for this partner */
        SELECT @profit_group_id = pp.profit_group_id, @profit_calculated = p.profit_percentage
		FROM   efrcommon..partner_profit pp INNER JOIN 
			   efrcommon..profit p on pp.profit_group_id = p.profit_group_id and p.qsp_catalog_type_id is null
		WHERE  pp.partner_id = @partner_id AND
               (pp.end_date is null or GETDATE() between pp.start_date and pp.end_date)

        /* Create Event */
        EXEC @err = [dbo].[es_create_event]
					@group_id = @group_id,
					@culture_code = @culture_code,
					@event_name = @group_name,
                    @redirect = NULL,
                    @profit_group_id = @profit_group_id,
					@profit_calculated = @profit_calculated,
					@event_id = @event_id OUTPUT

        IF (@err <> 0)
    	BEGIN
		ROLLBACK TRANSACTION
    		RETURN -4
    	END

        /* Validate Subdivision Code */
        IF RTRIM(ISNULL(@subdivision_code,''))<>'' AND RTRIM(ISNULL(@country_code,''))<>''
        BEGIN
			SET @subdivision_code = CASE WHEN @country_code LIKE 'US%' THEN
										  CASE WHEN CHARINDEX('US-', @subdivision_code)<>0 THEN
											  @subdivision_code ELSE 'US-' + ltrim(@subdivision_code) 
										  END
										  WHEN @country_code LIKE 'CA%' THEN
										  CASE WHEN CHARINDEX('CA-', @subdivision_code)<>0 THEN
											  @subdivision_code ELSE 'CA-' + ltrim(@subdivision_code) 
										  END
									END
	        
			IF NOT EXISTS(SELECT * FROM subdivision
						  WHERE country_code = @country_code AND 
								subdivision_code = @subdivision_code)
			BEGIN
			ROLLBACK TRANSACTION
			   RETURN -5
			END
        END

        /* Create Payment Info */
        EXEC @err = [dbo].[es_create_payment_info]
					@group_id = @group_id,
					@event_id = @event_id,
					@payment_name = @name,
                    @on_behalf_of_name = NULL,
					@ship_to_name = NULL,
					@phone_number = @day_phone,
					@ssn = NULL,	
					@address_1 = @address_1,
					@address_2 = NULL,
					@city = @city,
					@zip_code = @zip_code,
					@country_code = @country_code,
					@subdivision_code = @subdivision_code,
					@postal_address_id = @postal_address_id OUTPUT,
					@phone_number_id = @phone_number_id OUTPUT,
					@payment_info_id = @payment_info_id OUTPUT
  
        IF (@err <> 0)
    	BEGIN
		ROLLBACK TRANSACTION
    		RETURN -6
    	END

        /* Create Event Participation */
        EXEC @err = [dbo].[es_create_event_participation]
					@event_id = @event_id,
					@member_hierarchy_id = @sponsor_id,
					@participation_channel_id = 3,
					@salutation = @name,
					@event_participation_id = @event_participation_id OUTPUT
   
        IF (@err <> 0)
    	BEGIN
		ROLLBACK TRANSACTION
    		RETURN -7
    	END

        /* Create Default Personalization */
        EXEC @err = [dbo].[es_create_personalization]
                    @event_participation_id = @event_participation_id,
					@header_title1 = @group_name,
					@header_title2 = 'Online Fundraising Campaign',
					@body = 'We hope you will help support our organization by ordering or renewing your favorite magazine subscriptions online and sharing our site with your friends and relatives.  With your help, we are sure to earn the funds we need this year!  We appreciate your support.',
					@fundraising_goal = 0,
					@site_bgcolor = 'C6C6C6',
					@header_bgcolor = '5881C1',
					@header_color = 'FFFFFF',
                    @group_url = NULL,
					@image_url = '/Resources/Images/sponsor/groupPhotoPlaceholder.gif',
					@personalization_id = @personalization_id OUTPUT

        IF (@err <> 0)
    	BEGIN
		ROLLBACK TRANSACTION
    		RETURN -8
    	END

		/* Create Personalization Image */
        EXEC @err = [dbo].[es_create_personalization_image]
                    @personalization_id = @personalization_id,
					@image_url = '/Resources/Images/sponsor/groupPhotoPlaceholder.gif',
					@deleted = 0,
					@isCoverAlbum = 1,
					@image_approval_status_id = 1,
                    @image_id = @image_id OUTPUT

        IF (@err <> 0)
    	BEGIN
		ROLLBACK TRANSACTION
    		RETURN -9
    	END     

        COMMIT TRANSACTION
    END
    RETURN 0
END
GO

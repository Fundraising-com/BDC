USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_member]    Script Date: 02/14/2014 13:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[es_update_member]
  	@culture_code nvarchar(5),
	@opt_status_id int,
	@first_name varchar(50),
   	@middle_name varchar(50),
	@last_name varchar(50),
    @greeting varchar(50) = NULL,
   	@gender char(1),
	@email_address varchar(100),
	@password varchar(100),
	@parent_first_name varchar(100),
	@parent_last_name varchar(100),
   	@external_member_id varchar(128),
   	@comments varchar(1024), 
	@member_id int,
	@partner_id int = 0,
	@lead_id int,
	@facebook_id int = NULL,
	@deleted bit = 0,
    @bounced bit = 0,
    @user_id int = NULL
AS
BEGIN
	DECLARE @errorCode int
	DECLARE @validator int
       
/*		
	RETURN STATUS ARE
	0 = OK
	1 = FIRST NAME, LAST NAME, EMAIL ADDRESS AND PARTNER ID ALREADY EXISTS IN THE TABLE MEMBER
	2 = EXTERNAL MEMBER ID ALREADY EXISTS
*/ 

IF @member_id is null
RETURN -2

SELECT @validator = validate_state FROM dbo.es_validate_member(@member_id , @email_address , @partner_id , @external_member_id)

IF @validator > 0
begin
	RETURN @validator
end

if(@user_id is null)
begin
	set @user_id = dbo.es_get_user_id_by_member_id (@member_id);
	if(@user_id < 1)
	begin
		set @user_id = null;
	end
end

BEGIN TRANSACTION 
IF @external_member_id IS NULL
    BEGIN
       UPDATE member
    	SET 
    		first_name = @first_name
       		, middle_name = @middle_name
    		, last_name = @last_name
			, greeting =@greeting
       		, gender = @gender
    		, email_address = @email_address
       		, culture_code = @culture_code
    		, opt_status_id = @opt_status_id
    		, parent_first_name = @parent_first_name
    		, parent_last_name = @parent_last_name
       		, comments = @comments
			, lead_id = @lead_id
			, facebook_id = @facebook_id
			,deleted = @deleted
			, bounced = @bounced 
            , [user_id] = @user_id

    	WHERE member_id = @member_id 
    END
    ELSE
    BEGIN
    	UPDATE member
    	SET 
    		first_name = @first_name
	        , middle_name = @middle_name
    		, last_name = @last_name
			, greeting =@greeting
            , gender = @gender
    		, email_address = @email_address
            , culture_code = @culture_code
    		, opt_status_id = @opt_status_id
    		, parent_first_name = @parent_first_name
			, parent_last_name = @parent_last_name
			, external_member_id = @external_member_id
			, comments = @comments
			, lead_id = @lead_id
			, facebook_id = @facebook_id
			,deleted = @deleted
			, bounced = @bounced 
            , [user_id] = @user_id
    	WHERE member_id = @member_id
    END

	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -1
	END

	exec  es_update_member_password @member_id, @password 
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -2
	END
	
	
COMMIT TRANSACTION
RETURN 0

END
GO

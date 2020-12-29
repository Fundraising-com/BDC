USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_user]    Script Date: 02/14/2014 13:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[es_update_user]
  	@culture_code nvarchar(5),
	@opt_status_id int,
	@first_name varchar(50),
	@last_name varchar(50),
	@email_address varchar(100),
	@password varchar(100),
   	@external_member_id varchar(128),
	@member_id int,
	@partner_id int = 0,
    @coppa_month int = null,
	@coppa_year int = null,
	@agree_term_services bit = 0
AS
BEGIN
	DECLARE @errorCode int
	DECLARE @validator int
	DECLARE @userId int       

/*		
	RETURN STATUS ARE
	0 = OK
	1 = FIRST NAME, LAST NAME, EMAIL ADDRESS AND PARTNER ID ALREADY EXISTS IN THE TABLE MEMBER
	2 = EXTERNAL MEMBER ID ALREADY EXISTS
*/ 

IF @member_id is null
RETURN -2

-- BEGIN TRANSACTION

SELECT @validator = validate_state FROM dbo.es_validate_member(@member_id , @email_address , @partner_id , @external_member_id)

IF @validator > 0
begin
	RETURN @validator
end

SET @userId = dbo.es_get_user_id_by_member_id (@member_id)
IF(@userId > 0) 
BEGIN

   UPDATE users
	SET 
		first_name = @first_name
		, last_name = @last_name
		, email_address = @email_address
		, culture_code = @culture_code
		--, opt_status_id = @opt_status_id
		--, lead_id = @lead_id
        , coppa_month = @coppa_month
	    , coppa_year = @coppa_year
	    , agree_term_services = @agree_term_services
	WHERE [user_id] = @userId 

	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
		-- ROLLBACK TRANSACTION
		RETURN -1
	END

	exec  es_update_user_password @member_id, @password 
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
		-- ROLLBACK TRANSACTION
		RETURN -2
	END
END	
ELSE
BEGIN
	exec es_create_only_user @culture_code
		,@opt_status_id
		,@email_address
		,@first_name
		,@last_name
		,@email_address
		,@password
		,@member_id
		,@partner_id
        ,@coppa_month
	    ,@coppa_year
	    ,@agree_term_services
		,@userId 

	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
		-- ROLLBACK TRANSACTION
		RETURN -2
	END
END
	
-- COMMIT TRANSACTION
RETURN 0


END
GO

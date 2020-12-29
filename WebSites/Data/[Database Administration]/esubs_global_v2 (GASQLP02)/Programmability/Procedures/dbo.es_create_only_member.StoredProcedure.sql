USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_only_member]    Script Date: 02/14/2014 13:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	es_create_member
	
	Date: 26 Oct 2006
	
    
	
*/


CREATE   PROC [dbo].[es_create_only_member]
	@culture_code nvarchar(5) = 'en-US',
	@opt_status_id int = 2,
	@first_name varchar(50) = NULL,
	@middle_name varchar(50) = NULL,
	@last_name varchar(50) = NULL,
	@greeting varchar(50) = NULL,
	@gender char(1) = NULL,
	@email_address varchar(100),
	@password varchar(100) = NULL,
	@bounced bit = 0,
	@parent_first_name varchar(100) = NULL,
	@parent_last_name varchar(100) = NULL,
	@external_member_id varchar(128) = NULL,
	@comments varchar(1024) = NULL,
	@creation_channel_id int,
	@partner_id int = 0,
	@user_id int = NULL,
	@lead_id int = NULL,
	@facebook_id int = NULL,
	@member_id int OUTPUT
AS
BEGIN

	DECLARE @errorCode int
  	DECLARE @member_type_id int
	DECLARE @validator int
	
	-- check if the member can be inserted depending of the member rules definded in the validation function
	SELECT @validator = validate_state, @member_id = member_id
	FROM dbo.es_validate_member (@member_id ,  @email_address , @partner_id, @external_member_id)
	
	-- if the insertion of the member is ok
	if @validator != 0
	begin
		return @validator
	end
	else
	begin
		
		-- get the member type id from the creation channel passed to the stored procedure
		SELECT  @member_type_id = member_type_id from creation_channel where creation_channel_id = @creation_channel_id 
		set @member_id = null
		-- if the member does not exists, create it into member table
		BEGIN TRANSACTION
		if @member_id is null
		begin

		    	INSERT INTO member
		    	(
		    		culture_code
		    		, opt_status_id
		    		, first_name
		    		, middle_name
		    		, last_name
					, greeting
		    		, gender
		    		, email_address
		    		, password
		    		, bounced
					, parent_first_name
			    	, parent_last_name
		    		, comments
		    		, external_member_id
		    		, create_date
		            , partner_id
					, lead_id
					, facebook_id
					, [user_id]
		    	)
		    	VALUES
		    	(
		    		@culture_code
		    		, @opt_status_id
		    		, @first_name
		    		, @middle_name
		    		, @last_name
					,@greeting
		    		, @gender
		    		, @email_address
		    		, @password
		    		, @bounced
					, @parent_first_name
					, @parent_last_name
		    		, @comments
					, @external_member_id
		    		, GETDATE()
		           	, @partner_id
					, @lead_id
					, @facebook_id
					, @user_id
		    	)

			-- check if there has been an internal error		    	
		    	SET @errorCode = @@error
		    	IF (@errorCode <> 0)
		    	BEGIN
				ROLLBACK TRANSACTION
		    		RETURN -1
		    	END

			-- get the member id that has been inserted		    		
		    	SELECT @member_id = SCOPE_IDENTITY()

			-- check if there has been an internal error		    	
		    	SET @errorCode = @@error
		    	IF (@errorCode <> 0)
		    	BEGIN
				ROLLBACK TRANSACTION
		    		RETURN -2
		    	END
		END

		
		COMMIT TRANSACTION
	end
	RETURN 0
END
GO

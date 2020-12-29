USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_only_user]    Script Date: 02/14/2014 13:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	es_create_only_user
	
	Date: 2 Mar 2011
	
*/


CREATE PROC [dbo].[es_create_only_user]
	@culture_code nvarchar(5) = 'en-US',
	@opt_status_id int = 2,
	@username varchar(50),
	@first_name varchar(50) = NULL,
	@last_name varchar(50) = NULL,
	@email_address varchar(100),
	@password varchar(100) = NULL,
	@member_id int,
	@partner_id int = 0,
    @coppa_month int = null,
	@coppa_year int = null,
	@agree_term_services bit = 0,
	@user_id int OUTPUT
AS
BEGIN

	DECLARE @errorCode int
	DECLARE @userId int

	set @userId = dbo.es_get_user_id_by_member_id (@member_id)
	if(@userId > 0) 
	begin
		return -3
	end
	else
	begin
		begin transaction

    	INSERT INTO users
    	(
    		culture_code
    		, opt_status_id
			, username
    		, first_name
    		, last_name
    		, email_address
    		, password
            , partner_id
    		, create_date
            , coppa_month
	        , coppa_year
	        , agree_term_services
    	)
    	VALUES
    	(
    		@culture_code
    		, @opt_status_id
			, @username
    		, @first_name
    		, @last_name
    		, @email_address
    		, @password
            , @partner_id
    		, GETDATE()
            , @coppa_month
	        , @coppa_year
	        , @agree_term_services
    	)

	-- check if there has been an internal error		    	
    	SET @errorCode = @@error
    	IF (@errorCode <> 0)
    	BEGIN
			ROLLBACK TRANSACTION
    		RETURN -1
    	END

	-- get the user id that has been inserted		    		
    	SELECT @user_id = SCOPE_IDENTITY()

	-- check if there has been an internal error
    	SET @errorCode = @@error
    	IF (@errorCode <> 0)
    	BEGIN
			ROLLBACK TRANSACTION
    		RETURN -2
		END

		update member set [user_id] = @user_id where member_id = @member_id

		commit transaction
	end

	RETURN 0
END
GO

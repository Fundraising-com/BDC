USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_member]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	es_create_member
	
	Date: 13 July 2006
	
    
	
*/




CREATE   PROC [dbo].[es_create_member]
	@culture_code nvarchar(5) = 'en-US',
	@opt_status_id int = 2,
	@first_name varchar(50) = NULL,
	@middle_name varchar(50) = NULL,
	@last_name varchar(50) = NULL,
	@gender char(1) = NULL,
	@email_address varchar(100),
	@password varchar(100) = NULL,
	@bounced bit = 0,
	@parent_first_name varchar(100) = NULL,
	@parent_last_name varchar(100) = NULL,
	@external_member_id varchar(128) = NULL,
	@comments varchar(1024) = NULL,
	@parent_member_hierarchy_id int = NULL,
	@creation_channel_id int,
	@partner_id int = 0,
	@lead_id int = NULL,
	@facebook_id int = NULL,
	@member_hierarchy_id int OUTPUT,
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
		set @member_hierarchy_id = null
		select 
			@member_hierarchy_id = member_hierarchy_id 
		from
			 member_hierarchy 
		where
			 member_id = @member_id 
		and parent_member_hierarchy_id = @parent_member_hierarchy_id

		IF @member_hierarchy_id is null 
		BEGIN
			BEGIN TRANSACTION	
			    	INSERT INTO member_hierarchy
			    	(
			    		parent_member_hierarchy_id
			    		, member_id
			    		, creation_channel_id
			    		, create_date
			    	)
			    	VALUES
			    	(
			    		@parent_member_hierarchy_id
			    		, @member_id
			    		, @creation_channel_id
			    		, GETDATE()
			    	)
				-- check if errrors
			    	SET @errorCode = @@error
			    	IF (@errorCode <> 0)
			    	BEGIN
					ROLLBACK TRANSACTION
			    		RETURN -5
			    	END
	
				-- get the new id created		
			    	SELECT @member_hierarchy_id = SCOPE_IDENTITY()
	
				-- check if errrors
			    	SET @errorCode = @@error
			    	IF (@errorCode <> 0)
			    	BEGIN
					ROLLBACK TRANSACTION
			    		RETURN -6
			    	END
			COMMIT TRANSACTION
		end	
		RETURN 0
	end
	else
	begin
		----------------------------------------------------------------------------------------------------------------------------------------
		-- First step:  Insert or retreive the member id.
		-- Member is represented into two tables.  The first one is member, which contains the
		-- information about the user itself.  The second table is member_hierarchy, which 
		-- represents it's hiearchy position into the group (sponsor, participant, sponsor)
		----------------------------------------------------------------------------------------------------------------------------------------

		-- get the member type id from the creation channel passed to the stored procedure
		SELECT  @member_type_id = member_type_id from creation_channel where creation_channel_id = @creation_channel_id 

		set @member_id = null

		-- retreive the member id if available
		IF @external_member_id IS NULL
		BEGIN
			SELECT 
				@member_id = member_id 
			FROM
				 member 
			WHERE 
				email_address = @email_address 
		  		--AND first_name = @first_name 
		  		--AND last_name = @last_name		
				AND partner_id = @partner_id
		END
		ELSE
		BEGIN
			SELECT @member_id = member_id
				FROM member
			WHERE external_member_id = @external_member_id
		      		AND partner_id = @partner_id
		END

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
		    	)
		    	VALUES
		    	(
		    		@culture_code
		    		, @opt_status_id
		    		, @first_name
		    		, @middle_name
		    		, @last_name
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
		    	)

			-- check if there has been an internal error		    	
		    	SET @errorCode = @@error
		    	IF (@errorCode <> 0)
		    	BEGIN
				ROLLBACK TRANSACTION
		    		RETURN -3
		    	END

			-- get the member id that has been inserted		    		
		    	SELECT @member_id = SCOPE_IDENTITY()

			-- check if there has been an internal error		    	
		    	SET @errorCode = @@error
		    	IF (@errorCode <> 0)
		    	BEGIN
				ROLLBACK TRANSACTION
		    		RETURN -4
		    	END
		END

		----------------------------------------------------------------------------------------------------------------------------------------
		-- Second step:  Insert the member into the member hierarchy table.
		-- There is a condition that checks if the member is already a child of the 
		-- parent user.  If so, we do not create a new member hierarchy, we just
		-- get the existing one.
		----------------------------------------------------------------------------------------------------------------------------------------


		-- Detect if the member is already in the hierarchy, if so, retrieve it's member hierarchy id
		set @member_hierarchy_id = null
		IF EXISTS(select * 
	                from member_hierarchy 
	                where member_id = @member_id 
	                and parent_member_hierarchy_id = @parent_member_hierarchy_id)
		BEGIN
	
			select 
				@member_hierarchy_id = member_hierarchy_id 
			from
				 member_hierarchy 
			where
				 member_id = @member_id 
			and parent_member_hierarchy_id = @parent_member_hierarchy_id
	
		END
	
		-- if the member hierarchy id does not exists, just create it
		IF @member_hierarchy_id is null 
		BEGIN	
		    	INSERT INTO member_hierarchy
		    	(
		    		parent_member_hierarchy_id
		    		, member_id
		    		, creation_channel_id
		    		, create_date
		    	)
		    	VALUES
		    	(
		    		@parent_member_hierarchy_id
		    		, @member_id
		    		, @creation_channel_id
		    		, GETDATE()
		    	)
			-- check if errrors
		    	SET @errorCode = @@error
		    	IF (@errorCode <> 0)
		    	BEGIN
				ROLLBACK TRANSACTION
		    		RETURN -5
		    	END

			-- get the new id created		
		    	SELECT @member_hierarchy_id = SCOPE_IDENTITY()

			-- check if errrors
		    	SET @errorCode = @@error
		    	IF (@errorCode <> 0)
		    	BEGIN
				ROLLBACK TRANSACTION
		    		RETURN -6
		    	END
		end
		COMMIT TRANSACTION
	end
	RETURN 0
END
GO

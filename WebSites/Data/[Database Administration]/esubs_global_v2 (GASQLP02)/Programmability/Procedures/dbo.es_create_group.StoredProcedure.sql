USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_group]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[es_create_group] 
	@parent_group_id int = NULL,
	@sponsor_id int OUTPUT,
	@partner_id int,
	@lead_id int = NULL,
	@external_group_id varchar(128) = NULL,
	@group_name varchar(255),
	@test_group bit = 0,
	@expected_membership int = NULL,
	@group_url varchar(255) = NULL,
	@redirect varchar(255) = NULL,
	@comments varchar(1024) = NULL,
	@group_id int = NULL OUTPUT
AS
BEGIN
	DECLARE @errorCode int
	DECLARE @validationStatus int

	
	
	IF(LEN(@external_group_id) = 0)	-- ADDED BY JF BUIST -- This is a fix because the DAL was converting null value to string.Empty to the database (should be removed soon)
	BEGIN
		set @external_group_id = null
	END
	

	--SELECT @validationStatus= dbo.es_validate_group (@group_id, @partner_id, @external_group_id, @sponsor_id)  
	SELECT @validationStatus= validate_state, @group_id = group_id
	FROM dbo.es_validate_group(@group_id, @partner_id, @external_group_id, @sponsor_id)  

	IF @validationStatus = 1
		RETURN 1
	ELSE IF @validationStatus = 2
		RETURN 2
	ELSE IF @validationStatus = 0
	BEGIN	
		
		 	
		BEGIN TRANSACTION
	
		INSERT INTO [group]
		(
			parent_group_id
		    	,sponsor_id
		    	,partner_id
		    	,lead_id
		    	,external_group_id
		    	,group_name
		    	,test_group
		    	,expected_membership
		    	,group_url
		    	,redirect
		    	,comments
		    	,create_date
		)
		VALUES
		(
			@parent_group_id
			, @sponsor_id
			, @partner_id
			, @lead_id
			, @external_group_id
			, @group_name
			, @test_group
			, @expected_membership
			, @group_url
			, @redirect
			, @comments
			, GETDATE()
		)
		
		SET @errorCode = @@error
	
		IF (@errorCode <> 0)
		BEGIN
	  		ROLLBACK TRANSACTION
			RETURN -1
		END
		
		COMMIT TRANSACTION
	
		SELECT @group_id = SCOPE_IDENTITY()

		INSERT INTO group_group_status
		(
			group_id, group_status_id, create_date
		)
		VALUES
		(
			@group_id, 1, getdate()
		)
		RETURN 0
	END

	RETURN -1 -- Unknow error.
END
GO

USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_group]    Script Date: 02/14/2014 13:07:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[es_update_group]
	@parent_group_id int = NULL,
--	@group_type_id int,
	@sponsor_id int,
	@partner_id int,
	@lead_id int = NULL,
	@external_group_id varchar(128) = NULL,
	@group_name varchar(255),
	--@test_group bit = 0,
	@expected_membership int = NULL,
	@group_url varchar(255) = NULL,
	@redirect varchar(255) = NULL,
	@comments varchar(1024) = NULL,
	@group_id int = NULL OUTPUT
AS
BEGIN
    
	DECLARE @errorCode int
	DECLARE @sql varchar(1000)
	DECLARE @validator int
	
	
	IF LEN(@external_group_id ) = 0	-- Fix because the DAL was passing string.Empty instead of the Null value to the database
	BEGIN
		SET @external_group_id = NULL
	END
	
-- ES_VALIDATE_GROUP function :
-- RETURN 0: Validation Successfull
-- RETURN 1: Partner ID and ExternalGroupID already exists.
-- RETURN 2: SponsorID already exists
	--SELECT @validator = dbo.es_validate_group (@group_id, @partner_id, @external_group_id, @sponsor_id)

	SELECT @validator = validate_state, @group_id = group_id
	FROM dbo.es_validate_group(@group_id, @partner_id, @external_group_id, @sponsor_id)
	
	IF (@validator > 0)
		RETURN @validator 

	
	BEGIN TRANSACTION
    	-- Temp solution because redirect was added later

	-- ADDED BY JF BUIST, I removed the Group.Redirect manipulation since we do not use it anymore (this field (redirect) should be removed)
	UPDATE [group]
	SET parent_group_id = @parent_group_id
		, sponsor_id = @sponsor_id
		, partner_id = @partner_id
		, lead_id = @lead_id
		, external_group_id = @external_group_id
		, group_name = @group_name
		--, test_group = @test_group
		, expected_membership = @expected_membership
		, group_url = @group_url
		, comments = @comments
	WHERE group_id = @group_id

	
    	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -3
	END
    
	COMMIT TRANSACTION

    RETURN 0

END
GO

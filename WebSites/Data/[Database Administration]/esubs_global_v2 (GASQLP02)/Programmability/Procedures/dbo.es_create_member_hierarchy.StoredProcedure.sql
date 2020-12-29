USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_member_hierarchy]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROC [dbo].[es_create_member_hierarchy]
	@creation_channel_id int,
	@member_id int ,
	@parent_member_hierarchy_id int = NULL,
	@member_hierarchy_id int OUTPUT
AS
BEGIN

	
	DECLARE @errorCode int
	DECLARE @validator int


	SET @validator = -1
	SELECT @validator = validate_state, @member_hierarchy_id = member_hierarchy_id 
	FROM dbo.es_validate_member_hierarchy(@member_hierarchy_id, @member_id , @parent_member_hierarchy_id)

	IF (@validator != 0)
	RETURN @validator

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
    		RETURN -1
    	END

	-- get the new id created		
    	SELECT @member_hierarchy_id = SCOPE_IDENTITY()

	-- check if errrors
    	SET @errorCode = @@error
    	IF (@errorCode <> 0)
    	BEGIN
		ROLLBACK TRANSACTION
    		RETURN -1
    	END
	COMMIT TRANSACTION
	RETURN 0
END
GO

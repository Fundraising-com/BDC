USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_member_phone_number]    Script Date: 02/14/2014 13:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[es_create_member_phone_number]
	@member_id int
	,@phone_number_type_id int
    ,@phone_number varchar(50)
	,@member_phone_number_id int OUTPUT
	,@phone_number_id int OUTPUT
AS	
BEGIN	
	DECLARE @errorCode int
	
	BEGIN TRANSACTION
	
	-- if there is already a preceding active phone number of the same type, disable it
	IF EXISTS(SELECT phone_number_id 
                FROM member_phone_number 
               WHERE member_id = @member_id 
                 AND phone_number_type_id = @phone_number_type_id 
                 AND active = 1)
	BEGIN
		UPDATE member_phone_number
		SET active = 0
		WHERE member_id = @member_id 
		  AND phone_number_type_id = @phone_number_type_id 
		  AND active = 1
	END

	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -1
	END
	
	INSERT INTO phone_number
	(
		phone_number
		, create_date
	) VALUES (
		@phone_number
		, GETDATE()
	)

	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -2
	END	
	
	SET @phone_number_id = SCOPE_IDENTITY()
	
	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -3
	END	
	
	INSERT INTO member_phone_number
	(
		member_id
		, phone_number_type_id
		, phone_number_id
		, active
		, create_date
	) VALUES (
		@member_id
		, @phone_number_type_id
		, @phone_number_id
		, 1
		, GETDATE()
	)	
	
	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -4
	END
	
	COMMIT TRANSACTION
	RETURN 0
END
GO

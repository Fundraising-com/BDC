USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_member_postal_address]    Script Date: 02/14/2014 13:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[es_create_member_postal_address]
	@member_id int
	,@postal_address_type_id int
    ,@address_1 varchar(100)
	,@address_2 varchar(100)
	,@city varchar(100)
	,@zip_code varchar(50)
	,@country_code nvarchar(2)
	,@subdivision_code nvarchar(7)
   	,@postal_address_id int OUTPUT
	,@member_postal_address_id int OUTPUT
AS
BEGIN
	DECLARE @errorCode int

	BEGIN TRANSACTION

	IF EXISTS(SELECT member_postal_address_id 
                FROM member_postal_address 
               WHERE member_id = @member_id 
                 AND postal_address_type_id = @postal_address_type_id 
                 AND active = 1)
	BEGIN
		UPDATE member_postal_address
		SET active = 0
		WHERE member_id = @member_id
		  AND postal_address_type_id = @postal_address_type_id
		  AND active = 1
	END
	
	SET @errorCode = @@error	

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -1
	END
	
    INSERT INTO postal_address
	(
		address_1
		, address_2
		, city
		, zip_code
		, country_code
		, subdivision_code
		, create_date
	) VALUES (
		@address_1
		, @address_2
		, @city
		, @zip_code
		, @country_code
		, @subdivision_code
		, GETDATE()
	)
	
	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -2
	END

	SELECT @postal_address_id = SCOPE_IDENTITY()

	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -3
	END

	INSERT INTO member_postal_address (
		member_id
		, postal_address_id
		, postal_address_type_id
		, active
		, create_date
	) VALUES (
		@member_id
		, @postal_address_id
		, @postal_address_type_id
		, 1
		, GETDATE()
	)
	
	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -4
	END

	SELECT @member_postal_address_id = SCOPE_IDENTITY()
	
	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -5
	END

	COMMIT TRANSACTION
	RETURN 0
END
GO

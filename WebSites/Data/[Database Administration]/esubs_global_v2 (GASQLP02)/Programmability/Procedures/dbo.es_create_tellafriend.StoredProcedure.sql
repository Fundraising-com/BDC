USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_tellafriend]    Script Date: 02/14/2014 13:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	
*/
CREATE PROCEDURE [dbo].[es_create_tellafriend]
	@name varchar(100)
	, @email varchar(100)
	, @subject varchar(100)
	, @body_txt varchar(8000)
	, @body_html varchar(8000)
	, @recipient_name varchar(100)
	, @recipient_email varchar(100)
	, @tellafriend_id int = NULL OUTPUT
AS
BEGIN
	DECLARE @errorCode int

	BEGIN TRAN
	
	IF @tellafriend_id IS NULL
	BEGIN
		INSERT INTO tellafriend
		(
			[name]
			, email
			, subject
			, body_txt
			, body_html
			, create_date
		) VALUES (
			@name
			, @email
			, @subject
			, @body_txt
			, @body_html
			, GETDATE()
		)
		
		SET @errorCode = @@error
		
		IF (@errorCode <> 0)
		BEGIN
  			ROLLBACK TRAN
			RETURN -1
		END
		
		SELECT @tellafriend_id = SCOPE_IDENTITY()

		SET @errorCode = @@error

		IF (@errorCode <> 0)
		BEGIN
  			ROLLBACK TRAN
			RETURN -2
		END
	END

	INSERT INTO tellafriend_recipient
	(
		tellafriend_id
		, email
		, [name]
	) VALUES (
		@tellafriend_id
		, @recipient_email
		, @recipient_name
	)
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -3
	END

	COMMIT TRAN
	RETURN 0
END
GO

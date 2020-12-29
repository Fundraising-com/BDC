USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_personalization]    Script Date: 02/14/2014 13:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_create_personalization]
    @event_participation_id int
	, @header_title1 varchar(100)
	, @header_title2 varchar(100)
	, @body varchar(2000)
	, @fundraising_goal money
	, @site_bgcolor varchar(7)
	, @header_bgcolor varchar(7)
	, @header_color varchar(7)
	, @group_url varchar(255)
	, @image_url varchar(255)
    , @image_motivator tinyint = 0
	, @personalization_id int OUTPUT
	, @displayGroupMessage bit = 1
    , @redirect varchar(255) = NULL
	, @skip bit = 0
AS
BEGIN
	DECLARE @errorCode int
	
	BEGIN TRANSACTION
	
	INSERT INTO personalization
	(
        event_participation_id
		, header_title1
		, header_title2
		, body
		, fundraising_goal
		, site_bgcolor
		, header_bgcolor
		, header_color
		, group_url
		, image_url
        , image_motivator
		, displayGroupMessage
        , redirect
		, skip
	) VALUES (
        @event_participation_id
		, @header_title1
		, @header_title2
		, @body
		, @fundraising_goal
		, @site_bgcolor
		, @header_bgcolor
		, @header_color
		, @group_url
		, @image_url
		, @image_motivator
		, @displayGroupMessage
        , @redirect
		, @skip
	)
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -1
	END

 	SELECT @personalization_id = SCOPE_IDENTITY()
	
	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -2
	END
	
	COMMIT TRANSACTION
	RETURN 0
END
GO

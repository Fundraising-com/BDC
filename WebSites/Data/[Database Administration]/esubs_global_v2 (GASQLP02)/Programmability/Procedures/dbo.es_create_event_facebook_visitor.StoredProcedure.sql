USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_event_facebook_visitor]    Script Date: 02/14/2014 13:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
	-- Author:		Jason warren
	-- Create date: July 26, 2010
	-- Description:	Insert facebook event last visitor
	-- =============================================
	CREATE PROCEDURE [dbo].[es_create_event_facebook_visitor]
		 @personalization_id int
		,@facebook_id varchar(50)
		,@facebook_image_url varchar(500)
		,@facebook_firstname varchar(50)
		,@facebook_lastname varchar(50)
	    ,@event_facebook_visitor_id int OUTPUT
	AS
	BEGIN
		DECLARE @errorCode int

		BEGIN TRANSACTION

		INSERT INTO event_facebook_visitor
		(
		 personalization_id
			,facebook_id
		,facebook_image_url
			,facebook_firstname
			,facebook_lastname 

		) VALUES (
		  @personalization_id
			, @facebook_id
		, @facebook_image_url 
			, @facebook_firstname 
			, @facebook_lastname 
		)

		SET @errorCode = @@error

		IF (@errorCode <> 0)
		BEGIN
			ROLLBACK TRAN
			RETURN -1
		END

		SELECT @event_facebook_visitor_id = SCOPE_IDENTITY()

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

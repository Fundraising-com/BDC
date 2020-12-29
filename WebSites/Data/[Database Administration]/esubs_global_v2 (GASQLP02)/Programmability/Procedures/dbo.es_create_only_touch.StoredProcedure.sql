USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_only_touch]    Script Date: 02/14/2014 13:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ======================================================
-- Author:		Jiro Hidaka
-- Create date: March 14, 2011
-- Description:	When a user clicks on "Save For Later"
--              button on the kick off page and no contacts
--              is entered, an entry in touch is inserted
--              containing only the member hierarchy id
--              of the sender 
-- ======================================================
CREATE PROCEDURE [dbo].[es_create_only_touch]
	@member_hierarchy_id int,
	@touch_info_id int,
	@processed tinyint,
    @touch_id int OUTPUT
AS
BEGIN
    DECLARE @errorCode int    

	BEGIN TRANSACTION

    INSERT INTO touch (
        event_participation_id
        , member_hierarchy_id
        , touch_info_id
        , processed
        , create_date
    ) VALUES (
          NULL
        , @member_hierarchy_id
        , @touch_info_id
        , @processed
        , GETDATE()
    )

    SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -1
	END

	SELECT @touch_id = SCOPE_IDENTITY()

	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -2
	END

    COMMIT TRANSACTION
    RETURN 0    
END
GO

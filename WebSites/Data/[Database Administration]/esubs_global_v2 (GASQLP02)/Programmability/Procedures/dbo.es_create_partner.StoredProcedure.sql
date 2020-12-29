USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_partner]    Script Date: 02/14/2014 13:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	es_create_partner
	
	Created by: Philippe Girard
	Projects: Esubs v2
	Date: 1 August 2005
	
	Description: Add a partner
*/
CREATE PROC [dbo].[es_create_partner]
	@partner_type_id int,
	@partner_name varchar(50),
	@has_collection_site bit = 0
AS
BEGIN
	DECLARE @errorCode int
	
	/*
	-- Validate partner type
	IF NOT EXISTS(SELECT partner_type_id FROM partner_type WHERE partner_type_id = @partner_type_id)	
	BEGIN
		RAISERROR('Partner type not found.', 16, 1)
		RETURN
	END
	*/	

	BEGIN TRAN
	
	INSERT INTO partner
	(
		partner_type_id
		, partner_name
		, has_collection_site
		, create_date
	)
	VALUES
	(
		@partner_type_id
		, @partner_name
		, @has_collection_site
		, GETDATE()
	)
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -1
	END
	
	COMMIT TRAN
	RETURN 0
END
GO

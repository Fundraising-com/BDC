USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_partner]    Script Date: 02/14/2014 13:07:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

*/
CREATE PROC [dbo].[es_update_partner]
	@partner_id int,
	--@partner_type_id int,
	@partner_name varchar(50),
	@has_collection_site bit = 0
AS
BEGIN
	DECLARE @errorCode int
		
	-- Validate partner type

	BEGIN TRAN
	
	UPDATE partner
	SET partner_name = @partner_name
		, has_collection_site = @has_collection_site
	WHERE partner_id = @partner_id
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -1
	END
	
	COMMIT TRAN
	RETURN 0
END
GO

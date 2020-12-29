USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_user_unsubscribe]    Script Date: 02/14/2014 13:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

    Project: Esubs v2
    Date: Nov 2006

    Description: Uusubscribe a given member with member_hierarchy_id
    
*/

CREATE PROCEDURE [dbo].[es_update_user_unsubscribe]
 @member_id int,
 @unsubscribe bit

AS
BEGIN
 DECLARE @errorCode int
 DECLARE @unsubscribe_date DATETIME
 DECLARE @userId int

 IF @member_id IS NULL
 BEGIN
         RETURN -1
 END   

 IF @unsubscribe IS NULL
    SET @unsubscribe = 0 
 BEGIN TRANSACTION 

BEGIN

	set @userId = dbo.es_get_user_id_by_member_id (@member_id)
	if(@userId > 0) 
	begin


	IF (@unsubscribe IS NULL OR @unsubscribe = 0)
	   SET @unsubscribe_date = NULL
	ELSE
	   SET @unsubscribe_date = getdate()

	   UPDATE users
	   SET unsubscribe = @unsubscribe, unsubscribe_date = @unsubscribe_date
	   WHERE [user_id] = @userId

	END
	  
	 SET @errorCode = @@error
	 
	 IF (@errorCode <> 0)
	 BEGIN
		ROLLBACK TRANSACTION
	  RETURN -1
	 END
	end

 COMMIT TRANSACTION
 RETURN 0
END
GO

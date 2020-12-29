USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_member_hierarchy_unsubscribe]    Script Date: 02/14/2014 13:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
    es_update_member_hierarchy_unsubscribe

    Created by: Krystian Olszanski
    Project: Esubs v2
    Date: April 2006

    Description: Uusubscribe a given member with member_hierarchy_id
    
*/

CREATE PROCEDURE [dbo].[es_update_member_hierarchy_unsubscribe]
 @member_hierarchy_id int,
 @unsubscribe bit

AS
BEGIN
 DECLARE @errorCode int
 DECLARE @unsubscribe_date DATETIME
    
    -- If a member_hierarchy_id does not exist.
   IF NOT EXISTS(SELECT mh.member_hierarchy_id 
                FROM member_hierarchy mh 
                WHERE member_hierarchy_id = @member_hierarchy_id
        )
 BEGIN
         RETURN -1
 END

 BEGIN TRANSACTION 

IF @unsubscribe IS NULL OR @unsubscribe = 0
   SET @unsubscribe_date = null
ELSE
   SET @unsubscribe_date = getdate()

     UPDATE member_hierarchy
     SET unsubscribe = @unsubscribe, unsubscribe_date = @unsubscribe_date
     WHERE member_hierarchy_id = @member_hierarchy_id  

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

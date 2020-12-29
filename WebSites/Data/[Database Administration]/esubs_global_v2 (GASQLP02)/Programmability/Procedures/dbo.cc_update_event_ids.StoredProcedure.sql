USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_event_ids]    Script Date: 02/14/2014 13:05:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
--select * from event where event_id =
--dbo.cc_get_general_info 
/*dbo.cc_get_account_info 692221

Author	:	Jf Lavigne 2005-02-08
Description:	This stored procedure updates the information for a campaign.
*/
create  PROCEDURE [dbo].[cc_update_event_ids]
	 @intEventID INT
        , @intGroupID int = null
        , @intLeadID int = null
AS
DECLARE @intErrorCode INT
SET NOCOUNT ON
SET @intErrorCode = @@ERROR

BEGIN TRANSACTION
IF @intErrorCode = 0
BEGIN
       
     IF @intGroupID is not null
     BEGIN
	UPDATE
		[group]
	SET	external_group_id = @intGroupID,
                lead_id = @intLeadID 
	WHERE
		group_ID = @intEventID
	SET @intErrorCode = @@ERROR
     END 


END


IF @intErrorCode = 0
BEGIN
	COMMIT TRANSACTION
	RETURN( 0 )
END
ELSE
BEGIN
	ROLLBACK TRANSACTION
	RETURN( @intErrorCode )
END
GO

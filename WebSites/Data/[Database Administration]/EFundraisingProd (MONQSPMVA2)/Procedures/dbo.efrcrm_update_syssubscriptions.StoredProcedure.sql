USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_syssubscriptions]    Script Date: 02/14/2014 13:08:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Syssubscriptions
CREATE PROCEDURE [dbo].[efrcrm_update_syssubscriptions] @Artid int, @Srvid smallint, @Dest_db sysname, @Status tinyint, @Sync_type tinyint, @Login_name sysname, @Subscription_type int, @Distribution_jobid binary, @Timestamp timestamp, @Update_mode tinyint, @Loopback_detection bit, @Queued_reinit bit AS
begin

update Syssubscriptions set Srvid=@Srvid, Dest_db=@Dest_db, Status=@Status, Sync_type=@Sync_type, Login_name=@Login_name, Subscription_type=@Subscription_type, Distribution_jobid=@Distribution_jobid, Timestamp=@Timestamp, Update_mode=@Update_mode, Loopback_detection=@Loopback_detection, Queued_reinit=@Queued_reinit where Artid=@Artid

end
GO

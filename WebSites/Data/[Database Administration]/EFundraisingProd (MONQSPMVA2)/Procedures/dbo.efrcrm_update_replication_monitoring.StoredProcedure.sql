USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_replication_monitoring]    Script Date: 02/14/2014 13:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Replication_Monitoring
CREATE PROCEDURE [dbo].[efrcrm_update_replication_monitoring] @Replication_ID int, @Msg varchar(100) AS
begin

update Replication_Monitoring set Msg=@Msg where Replication_ID=@Replication_ID

end
GO

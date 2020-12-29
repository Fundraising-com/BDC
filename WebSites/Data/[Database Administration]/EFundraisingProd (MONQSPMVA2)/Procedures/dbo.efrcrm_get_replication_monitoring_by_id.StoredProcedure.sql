USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_replication_monitoring_by_id]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Replication_Monitoring
CREATE PROCEDURE [dbo].[efrcrm_get_replication_monitoring_by_id] @Replication_ID int AS
begin

select Replication_ID, Msg from Replication_Monitoring where Replication_ID=@Replication_ID

end
GO

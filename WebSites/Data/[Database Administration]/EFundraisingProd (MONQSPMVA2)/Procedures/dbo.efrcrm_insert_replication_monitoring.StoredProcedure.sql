USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_replication_monitoring]    Script Date: 02/14/2014 13:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Replication_Monitoring
CREATE PROCEDURE [dbo].[efrcrm_insert_replication_monitoring] @Replication_ID int OUTPUT, @Msg varchar(100) AS
begin

insert into Replication_Monitoring(Msg) values(@Msg)

select @Replication_ID = SCOPE_IDENTITY()

end
GO

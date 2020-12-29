USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_perfom_trace]    Script Date: 02/14/2014 13:07:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Perfom_trace
CREATE PROCEDURE [dbo].[efrcrm_insert_perfom_trace] @RowNumber int OUTPUT, @EventClass int, @TextData ntext, @NTUserName nvarchar(256), @HostName nvarchar(256), @ApplicationName nvarchar(256), @LoginName nvarchar(256), @SPID int, @Duration bigint, @StartTime datetime, @EndTime datetime, @CPU int, @EventSubClass int, @IntegerData int, @Error int, @ObjectName nvarchar(256), @DatabaseName nvarchar(256) AS
begin

insert into Perfom_trace(EventClass, TextData, NTUserName, HostName, ApplicationName, LoginName, SPID, Duration, StartTime, EndTime, CPU, EventSubClass, IntegerData, Error, ObjectName, DatabaseName) values(@EventClass, @TextData, @NTUserName, @HostName, @ApplicationName, @LoginName, @SPID, @Duration, @StartTime, @EndTime, @CPU, @EventSubClass, @IntegerData, @Error, @ObjectName, @DatabaseName)

select @RowNumber = SCOPE_IDENTITY()

end
GO

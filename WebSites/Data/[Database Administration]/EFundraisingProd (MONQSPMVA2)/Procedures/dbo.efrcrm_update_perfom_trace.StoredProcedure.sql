USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_perfom_trace]    Script Date: 02/14/2014 13:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Perfom_trace
CREATE PROCEDURE [dbo].[efrcrm_update_perfom_trace] @RowNumber int, @EventClass int, @TextData ntext, @NTUserName nvarchar(256), @HostName nvarchar(256), @ApplicationName nvarchar(256), @LoginName nvarchar(256), @SPID int, @Duration bigint, @StartTime datetime, @EndTime datetime, @CPU int, @EventSubClass int, @IntegerData int, @Error int, @ObjectName nvarchar(256), @DatabaseName nvarchar(256) AS
begin

update Perfom_trace set EventClass=@EventClass, TextData=@TextData, NTUserName=@NTUserName, HostName=@HostName, ApplicationName=@ApplicationName, LoginName=@LoginName, SPID=@SPID, Duration=@Duration, StartTime=@StartTime, EndTime=@EndTime, CPU=@CPU, EventSubClass=@EventSubClass, IntegerData=@IntegerData, Error=@Error, ObjectName=@ObjectName, DatabaseName=@DatabaseName where RowNumber=@RowNumber

end
GO

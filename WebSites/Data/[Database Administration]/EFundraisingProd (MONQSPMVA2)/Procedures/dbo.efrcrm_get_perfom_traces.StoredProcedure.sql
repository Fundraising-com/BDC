USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_perfom_traces]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Perfom_trace
CREATE PROCEDURE [dbo].[efrcrm_get_perfom_traces] AS
begin

select RowNumber, EventClass, TextData, NTUserName, HostName, ApplicationName, LoginName, SPID, Duration, StartTime, EndTime, CPU, EventSubClass, IntegerData, Error, ObjectName, DatabaseName from Perfom_trace

end
GO

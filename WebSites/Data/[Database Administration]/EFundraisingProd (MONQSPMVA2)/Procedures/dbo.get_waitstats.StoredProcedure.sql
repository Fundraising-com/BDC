USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[get_waitstats]    Script Date: 02/14/2014 13:08:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE proc [dbo].[get_waitstats]
AS
-- This stored procedure is provided "AS IS" with no warranties and confers no rights.
-- Use of included script samples are subject to the terms specified at 
-- http://www.microsoft.com/info/cpyright.htm.

-- This procedure creates a waitstats report that lists wait types by percentage.
-- You can run the procedure while track_waitstats is executing.
SET nocount ON

DECLARE @now datetime,@totalwait numeric(20,1)
   ,@endtime datetime,@begintime datetime
   ,@hr int,@min int,@sec int

SELECT  @now=max(now),@begintime=min(now),@endtime=max(now)
FROM waitstats WHERE [wait type] = 'Total'

-- Subtract waitfor, sleep, and resource_queue from total.
SELECT @totalwait = sum([wait time]) + 1 FROM waitstats
WHERE [wait type] NOT IN ('WAITFOR','SLEEP','RESOURCE_QUEUE', 'Total', '***total***') AND 
now = @now

-- Insert adjusted totals and rank by percentage in descending order.
DELETE waitstats WHERE [wait type] = '***total***' AND now = @now
INSERT INTO waitstats SELECT '***total***',0,@totalwait,@totalwait,@now

SELECT [wait type],[wait time],percentage=cast (100*[wait time]/@totalwait AS numeric(20,1))
FROM waitstats
WHERE [wait type] NOT IN ('WAITFOR','SLEEP','RESOURCE_QUEUE','Total')
AND now = @now
ORDER BY percentage desc
GO

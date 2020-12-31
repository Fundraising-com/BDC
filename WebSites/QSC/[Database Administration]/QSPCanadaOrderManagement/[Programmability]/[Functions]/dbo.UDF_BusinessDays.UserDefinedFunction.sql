USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_BusinessDays]    Script Date: 06/07/2017 09:21:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
create function [dbo].[UDF_BusinessDays] (@start datetime, @end datetime)
returns int 
as
begin
/*
Description:
   Function designed to calculate the number of business days 
between two dates.
*/
declare 
	@wks int
	,@days int 
	,@sdays int
	,@edays int

	-- Find the number of weeks between the dates. Subtract 1 
	-- since we do not want to count the current week.
	select @wks = datediff( week, @start, @end) - 1
	-- calculate the number of days in these full wks.
	select @days = @wks * 5
	-- Get the number of days in the week of the start date. This is the days
	-- between Saturday (datepart=7) and the startdate. We also remove the
	-- Sunday (datepart=1). If the first day is a Saturday, do not exclude
	-- this twice.
	if datepart( dw, @start) = 7
		select @sdays = 7 - datepart( dw, @start)
	else
		select @sdays = 7 - datepart( dw, @start) - 1
	-- Calculate the days in the last week. These are not included in the
	-- week calculation. Since we are starting with the end date, we only
	-- remove the Sunday (datepart=1) from the number of days. If the end
	-- date is Saturday, correct for this.
	if datepart( dw, @end) = 7
		select @edays = datepart( dw, @end) - 2
	else
		select @edays = datepart( dw, @end) - 1

	-- Sum everything together.
	select @days = @days + @sdays + @edays
	return( @days)
end
GO

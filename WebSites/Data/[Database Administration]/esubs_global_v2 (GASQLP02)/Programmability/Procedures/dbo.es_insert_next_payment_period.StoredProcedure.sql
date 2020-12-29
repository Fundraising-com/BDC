USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_next_payment_period]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment_period
CREATE PROCEDURE [dbo].[es_insert_next_payment_period] 
as
begin

declare @Payment_period_id int
declare @start_date datetime
declare @end_date datetime
declare @new_start_date datetime
declare @new_end_date datetime
declare @today datetime
set @today = getdate()

select @start_date = start_date, @end_date = End_date
   from Payment_period where 
end_date  = (select max(end_date) from payment_period)

select @new_start_date = dateadd(month,1,@start_date)

declare @month int
set @month = month(@new_start_date)


if @month in (1,3,5,7,8,10,12)
begin
   set @new_end_date = dateadd(day,30,@new_start_date)
end
else if @month in (4,6,9,11)
begin
   set @new_end_date = dateadd(day,29,@new_start_date)
end
else
begin
   set @new_end_date = dateadd(day,27,@new_start_date)
end
--select  DATEADD(hh,23,GETDATE())
--select DATEADD(mi,59,GETDATE())
--select DATEADD(ss,59,GETDATE())
--select DATEADD(ms,997,GETDATE())
set @new_end_date = DATEADD(hh,23,@new_end_date)
set @new_end_date = DATEADD(mi,59,@new_end_date)
set @new_end_date = DATEADD(ss,59,@new_end_date)
set @new_end_date = DATEADD(ms,997,@new_end_date)
--make sure period is not in the future
if @new_end_date < @today
begin
   insert into Payment_period(Start_date, End_date, Create_date) values(@new_Start_date, @new_End_date, @today)
end

select @Payment_period_id = SCOPE_IDENTITY()

end
GO

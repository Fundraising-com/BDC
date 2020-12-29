USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[report_efr_leads_daily_totals]    Script Date: 02/14/2014 13:08:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY : Melissa Cote
	CREATE DATE : 2011-06-01

	DESCRIPTION : Leads/Request Stats 

	declare @report varchar(8000)
	exec [report_efr_leads_daily_totals]  @report
	print @report

Shipped
Returned
Net Shipped (shipped not return) 
GP on shipped
Paid

2010 - 2011 et la variance

On choisit la période désiré

SC & Resto
FF
Subs
Beef
Lolipops
Other

*/
CREATE PROCEDURE [dbo].[report_efr_leads_daily_totals]
	@report varchar(8000) OUTPUT
AS
Begin 
	DECLARE @dteToday DATETIME
	DECLARE @now DATETIME
	declare @leadscurrent INT
	declare @leadslast INT 
	declare @leadslasttotal INT 
	declare @var decimal(4, 2)




	SET @dteToday = GETDATE()
	--    SET @dteToday = '2005-09-16'
	SET @dteToday = DATEADD(ms, -DATEPART(ms, @dteToday), @dteToday)
	SET @dteToday = DATEADD(s, -DATEPART(s, @dteToday), @dteToday)
	SET @dteToday = DATEADD(n, -DATEPART(n, @dteToday), @dteToday)
	SET @dteToday = DATEADD(hh, -DATEPART(hh, @dteToday), @dteToday)
	SET @now = GETDATE()

	SET @report = ''
    SET @report = @report + char(13) + char(10) + 'EFUNDRAISING Daily Report' 
    SET @report = @report + char(13) + char(10) + ''
    SET @report = @report + char(13) + char(10) + '================================'
    SET @report = @report + char(13) + char(10) + '  Leads'
    SET @report = @report + char(13) + char(10) + '================================'
    SET @report = @report + char(13) + char(10) + ''

	    
	-- today
	select @leadscurrent = count(lead_id) from lead where lead_entry_date between @dteToday and @now
	SET @report = @report + char(13) + char(10) + CONVERT( VARCHAR(10), @leadscurrent) + ' leads entered today'

	select @leadslast = count(lead_id) from lead 
	where lead_entry_date between dateadd(year, -1,@dteToday) and dateadd(year, -1,@now)

	select @leadslasttotal = count(lead_id) from lead 
	WHERE DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, @dteToday ))
		  AND DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD( year, -1, @dteToday))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD(year, -1, @dteToday))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' leads entered ('+CONVERT( VARCHAR(10), @leadslasttotal)+' total) last year same day'

	if @leadscurrent >= @leadslast
	begin
		set @var = CONVERT(decimal(8, 2),((convert(decimal(10, 6), @leadscurrent)-convert(decimal(10, 6),@leadslast))/ convert(decimal(10, 6),@leadscurrent) * 100))
		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @var) + ' %'
	end 


	-- yesterday
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead 
	WHERE DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD(day, -1, @dteToday ))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrent) + ' leads entered yesterday'

	select @leadslast = count(lead_id) from lead 
	WHERE DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
		  AND DATEPART(month, lead_entry_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' leads entered last year same day'

	set @var = CONVERT( decimal(4, 2),((convert(decimal(10, 6), @leadscurrent)-convert(decimal(10, 6),@leadslast))/ convert(decimal(10, 6),@leadscurrent) * 100))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @var) + ' %'


	-- this month
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD(day, -1, @dteToday ))AND
		   DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD(day, -1, @dteToday ))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrent) + ' leads entered this month'

	select @leadslast = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, -1, @dteToday )))AND
		   DATEPART(month, lead_entry_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' leads entered last year same month'

	set @var = CONVERT( decimal(4, 2),((convert(decimal(10, 6), @leadscurrent)-convert(decimal(10, 6),@leadslast))/ convert(decimal(10, 6),@leadscurrent) * 100))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @var) + ' %'


	SET @report = @report + char(13) + char(10) +  ''
	SET @report = @report + char(13) + char(10) +  '================================'
	SET @report = @report + char(13) + char(10) +  '  Requests'
	SET @report = @report + char(13) + char(10) +  '================================'
	SET @report = @report + char(13) + char(10) +  ''
	 
	-- today
	select @leadscurrent = count(lead_id) from lead_visit 
	where visit_date between @dteToday and @now
	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrent) + ' requests entered today'

	select @leadslast = count(lead_id) from lead_visit 
	where visit_date between dateadd(year, -1,@dteToday) and dateadd(year, -1,@now)

	select @leadslasttotal = count(lead_id) from lead_visit 
	WHERE DATEPART(day, visit_date) = DATEPART(day, DATEADD( year, -1, @dteToday ))
		  AND DATEPART(month, visit_date) = DATEPART(month, DATEADD( year, -1, @dteToday))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD(year, -1, @dteToday))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' requests entered ('+CONVERT( VARCHAR(10), @leadslasttotal)+' total) last year same day'

	if @leadscurrent >= @leadslast
	begin
		set @var = CONVERT(decimal(8, 2),((convert(decimal(10, 6), @leadscurrent)-convert(decimal(10, 6),@leadslast))/ convert(decimal(10, 6),@leadscurrent) * 100))
		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @var) + ' %'
	end 


	-- yesterday
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead_visit 
	WHERE DATEPART(day, visit_date) = DATEPART(day, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(month, visit_date) = DATEPART(month, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD(day, -1, @dteToday ))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrent) + ' requests entered yesterday'

	select @leadslast = count(lead_id) from lead_visit 
	WHERE DATEPART(day, visit_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
		  AND DATEPART(month, visit_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' requests entered last year same day'

	set @var = CONVERT( decimal(4, 2),((convert(decimal(10, 6), @leadscurrent)-convert(decimal(10, 6),@leadslast))/ convert(decimal(10, 6),@leadscurrent) * 100))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @var) + ' %'

	-- yesterday
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead_visit 
	WHERE --DATEPART(day, visit_date) = DATEPART(day, DATEADD(day, -1, @dteToday ))AND
		   DATEPART(month, visit_date) = DATEPART(month, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD(day, -1, @dteToday ))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrent) + ' requests entered this month'

	select @leadslast = count(lead_id) from lead_visit 
	WHERE --DATEPART(day, visit_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, -1, @dteToday )))AND
		   DATEPART(month, visit_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, -1, @dteToday )))
	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' requests entered last year same month'

	set @var = CONVERT( decimal(4, 2),((convert(decimal(10, 6), @leadscurrent)-convert(decimal(10, 6),@leadslast))/ convert(decimal(10, 6),@leadscurrent) * 100))

	SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @var) + ' %'


	--print @report

END
GO

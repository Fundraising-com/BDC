USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[report_efr_daily_totals]    Script Date: 07/15/2014 16:43:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/*
	CREATED BY : Melissa Cote
	CREATE DATE : 2011-06-01

	DESCRIPTION : Leads/Request Stats 

	declare @report varchar(8000)
	exec [report_efr_daily_totals]  @report
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
ALTER PROCEDURE [dbo].[report_efr_daily_totals]
	@report varchar(8000) OUTPUT
AS
Begin 
	DECLARE @dteToday DATETIME
	DECLARE @now DATETIME
	declare @leadscurrent INT
	declare @leadslast INT 
	declare @leadslasttotal INT 
	declare @leadscurrentCI INT
	declare @leadslastCI INT 
	declare @leadslasttotalCI INT 
	declare @var decimal(20, 6)

	declare @title1 char(10) 
	declare @title2 char(10) 
	declare @title3 char(10) 
	declare @title4 char(15) 
	declare @title5 char(15) 

	declare @column1 char(10) 
	declare @column2 char(10) 
	declare @column3 char(10) 
	declare @column4 char(10) 
	declare @column5 char(10) 

	SET @title1 = 'CY14'
	SET @title2 = 'CY13'
	SET @title3 = 'VAR%'
	SET @title4 = ''
	SET @title5 = ''
	--SET @report =  @report + char(13) + char(10) +  @column1 + @column2 + @column3 + @column4+ @column5


	SET @dteToday = GETDATE()
	--    SET @dteToday = '2005-09-16'
	SET @dteToday = DATEADD(ms, -DATEPART(ms, @dteToday), @dteToday)
	SET @dteToday = DATEADD(s, -DATEPART(s, @dteToday), @dteToday)
	SET @dteToday = DATEADD(n, -DATEPART(n, @dteToday), @dteToday)
	SET @dteToday = DATEADD(hh, -DATEPART(hh, @dteToday), @dteToday)
	SET @now = GETDATE()

	SET @report = ''
--    SET @report = @report + char(13) + char(10) + 'EFUNDRAISING Daily Report' 
    SET @report = @report + char(13) + char(10) + ''
    SET @report = @report + char(13) + char(10) + '================================'
    SET @report = @report + char(13) + char(10) + '  LEADS'
    SET @report = @report + char(13) + char(10) + '================================'
    SET @report = @report + char(13) + char(10) + ''

	    
	-- today
	select @leadscurrent = count(lead_id) 
	from lead 
	where lead_entry_date between @dteToday and @now
	AND promotion_id != 20241-- Convention

	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrent)

	select @leadslast = count(lead_id) 
	from lead 
	where lead_entry_date between dateadd(year, -1,dateadd(day, 1, @dteToday)) and dateadd(year, -1,dateadd(day, 1, @now)) AND promotion_id != 20241 -- Convention

	SET @column2 = CONVERT( NVARCHAR(10), @leadslast)

	select @leadslasttotal = count(lead_id) from lead 
	WHERE DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD( day, 1, @dteToday )))
		  AND DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD( year, -1, DATEADD( day, 1, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD( day, 1, @dteToday )))
	AND promotion_id != 20241 -- Convention

	SET @column4 = CONVERT( NVARCHAR(10), @leadslasttotal)


	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100

	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'


	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslasttotal)
	set @var = (@var / convert(decimal(20, 4),@leadslasttotal)) * 100
	
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'TODAY:'
	SET @title5 =  'This time'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3 --+ @column4+ @column5
--	SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'TODAY TOTAL:'
	SET @title5 =  'Total'
--	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 + @column1 + @column4 + @column5
	


	-------------------------------------------------
	-- yesterday
	-------------------------------------------------
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead 
	WHERE DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD(day, -1, @dteToday ))
	AND promotion_id != 20241 -- Convention

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrent) + ' leads entered yesterday'
	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrent)

	select @leadslast = count(lead_id) from lead 
	WHERE DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(month, lead_entry_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' leads entered last year same day'

	SET @column2 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'YESTERDAY:'
	SET @title5 =  'Total'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3 --+ @column4+ @column5



	-------------------------------------------------
	-- this month
	-------------------------------------------------
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD(day, 0, @dteToday ))AND
		   DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD(day, 0, @dteToday ))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD(day, 0, @dteToday ))
	AND promotion_id != 20241 -- Convention

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrent) + ' leads entered this month'
	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrent)

	select @leadslast = count(lead_id) from lead 
	WHERE DATEPART(day, lead_entry_date) <= DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(month, lead_entry_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	  AND promotion_id != 20241 -- Convention

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' leads entered last year month to date'
	SET @column2 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'


	select @leadslast = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		   DATEPART(month, lead_entry_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' leads entered last year same month'
	SET @column4 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'MONTH:'
	SET @title5 =  'To date'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3 
	SET @title5 =  'Total'
	--SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'MONTH TOTAL:'
--	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column4 + @column5 --+ @column4+ @column5


	-------------------------------------------------
	-- this year
	-------------------------------------------------
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD(day, 0, @dteToday ))AND
		  -- DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD(day, 0, @dteToday ))AND
		   DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD(day, 0, @dteToday ))
	AND promotion_id != 20241 -- Convention

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrent) + ' leads entered this year'
	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrent)

	select @leadslast = count(lead_id) from lead 
	WHERE lead_entry_date <= DATEADD(year, -1, @dteToday) 
			--DATEPART(day, lead_entry_date) <= DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  --AND DATEPART(month, lead_entry_date) <= DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		AND promotion_id != 20241 -- Convention

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' leads entered last year to date'
	SET @column2 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'YEAR:'
	--SET @report =  @report + char(13) + char(10) + 'YEAR:'
	SET @title5 =  'To date'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3 


	select @leadslast = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		   DATEPART(month, lead_entry_date) <= DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' leads entered last year same month'
	SET @column4 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	--SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'YEAR:'
	SET @title5 =  'Same month'
	--SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column4 + @column5



	select @leadslast = count(lead_id) from lead 

	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		  -- DATEPART(month, lead_entry_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		   DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, @dteToday ))
	AND promotion_id != 20241 -- Convention

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' leads entered last year total'
	SET @column4 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	--SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'YEAR:'
	SET @title5 =  'Total'
	--SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column4 + @column5 



--    SET @report = @report + char(13) + char(10) + 'EFUNDRAISING Daily Report' 
    SET @report = @report + char(13) + char(10) + ''
    SET @report = @report + char(13) + char(10) + '================================'
    SET @report = @report + char(13) + char(10) + '  LEADS CALL INS ONLY'
    SET @report = @report + char(13) + char(10) + '================================'
    SET @report = @report + char(13) + char(10) + ''


	-------------------------------------------------
	-- CALL INS ONLY
	-------------------------------------------------
	SET @report = @report + char(13) + char(10) +  ' '

	-- today
	select @leadscurrentCI = count(lead_id) 
	from lead 
	where lead_entry_date between @dteToday and @now
	AND promotion_id != 20241 -- Convention
	AND Channel_code = 'CI'

	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrentCI)

	select @leadslastCI = count(lead_id) 
	from lead 
	where lead_entry_date between dateadd(year, -1,dateadd(day, 1, @dteToday)) and dateadd(year, -1,dateadd(day, 1, @now)) 
	AND promotion_id != 20241 -- Convention
	AND Channel_code = 'CI'

	SET @column2 = CONVERT( NVARCHAR(10), @leadslastCI)

	select @leadslasttotalCI = count(lead_id) from lead 
	WHERE DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD( day, 1, @dteToday )))
		  AND DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD( year, -1, DATEADD( day, 1, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD( day, 1, @dteToday )))
	AND promotion_id != 20241 -- Convention
	AND Channel_code = 'CI'

	SET @column4 = CONVERT( NVARCHAR(10), @leadslasttotalCI)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrentCI-@leadslastCI)
	set @var = (@var / convert(decimal(20, 4),@leadslastCI)) * 100

	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'


	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrentCI-@leadslasttotalCI)
	set @var = (@var / convert(decimal(20, 4),@leadslasttotalCI)) * 100
	
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'TODAY:'
	SET @title5 =  'This Time'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3 --+ @column4+ @column5
--	SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'TODAY TOTAL CALL INS:'
	SET @title5 =  'Total CI'
--	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 + @column1 + @column4 + @column5

	-------------------------------------------------
	-- this month CALL INS
	-------------------------------------------------
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrentCI = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD(day, 0, @dteToday ))AND
		   DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD(day, 0, @dteToday ))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD(day, 0, @dteToday ))
	AND promotion_id != 20241 -- Convention
	AND Channel_code = 'CI'

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrentCI) + ' leads entered this month'
	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrentCI)

	select @leadslastCI = count(lead_id) from lead 
	WHERE DATEPART(day, lead_entry_date) <= DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(month, lead_entry_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	  AND promotion_id != 20241 -- Convention
	 AND Channel_code = 'CI'

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslastCI) + ' CI leads entered last year month to date'
	SET @column2 = CONVERT( NVARCHAR(10), @leadslastCI)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrentCI-@leadslastCI)
	set @var = (@var / convert(decimal(20, 4),@leadslastCI)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'


	select @leadslastCI = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		   DATEPART(month, lead_entry_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention
	AND Channel_code = 'CI'

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslastCI) + ' CI leads entered last year same month'
	SET @column4 = CONVERT( NVARCHAR(10), @leadslastCI)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrentCI-@leadslastCI)
	set @var = (@var / convert(decimal(20, 4),@leadslastCI)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'MONTH:'
	SET @title5 =  'To date'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3 
	SET @title5 =  'Total CI'
	--SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'MONTH TOTAL CI:'
--	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column4 + @column5 --+ @column4+ @column5


	-------------------------------------------------
	-- this year CALL INS
	-------------------------------------------------
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrentCI = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD(day, 0, @dteToday ))AND
		  -- DATEPART(month, lead_entry_date) = DATEPART(month, DATEADD(day, 0, @dteToday ))AND
		   DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD(day, 0, @dteToday ))
	AND promotion_id != 20241 -- Convention
	AND Channel_code = 'CI'
	
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadscurrentCI) + ' CI leads entered this year'
	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrentCI)

	select @leadslastCI = count(lead_id) from lead 
	WHERE lead_entry_date <= DATEADD(year, -1, @dteToday) 
			--DATEPART(day, lead_entry_date) <= DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  --AND DATEPART(month, lead_entry_date) <= DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		AND promotion_id != 20241 -- Convention
		AND Channel_code = 'CI'

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslastCI) + ' CI leads entered last year to date'
	SET @column2 = CONVERT( NVARCHAR(10), @leadslastCI)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrentCI-@leadslastCI)
	set @var = (@var / convert(decimal(20, 4),@leadslastCI)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'YEAR:'
	--SET @report =  @report + char(13) + char(10) + 'YEAR CI:'
	SET @title5 =  'To date'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3 


	select @leadslastCI = count(lead_id) from lead 
	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		   DATEPART(month, lead_entry_date) <= DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention
	AND Channel_code = 'CI'

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslastCI) + ' CI leads entered last year same month'
	SET @column4 = CONVERT( NVARCHAR(10), @leadslastCI)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrentCI-@leadslastCI)
	set @var = (@var / convert(decimal(20, 4),@leadslastCI)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	--SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'YEAR:'
	SET @title5 =  'Same month'
	--SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column4 + @column5



	select @leadslastCI = count(lead_id) from lead 

	WHERE --DATEPART(day, lead_entry_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		  -- DATEPART(month, lead_entry_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		   DATEPART(year, lead_entry_date) = DATEPART(year, DATEADD( year, -1, @dteToday ))
	AND promotion_id != 20241 -- Convention
	AND Channel_code = 'CI'

	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslastCI) + ' leads entered last year total'
	SET @column4 = CONVERT( NVARCHAR(10), @leadslastCI)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrentCI-@leadslastCI)
	set @var = (@var / convert(decimal(20, 4),@leadslastCI)) * 100
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10),convert(int, @var)) + ' %'
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	--SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'YEAR CI:'
	SET @title5 =  'Total CI'
	--SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column4 + @column5 
	

	
	SET @report = @report + char(13) + char(10) +  ''
	SET @report = @report + char(13) + char(10) +  '================================'
	SET @report = @report + char(13) + char(10) +  '  REQUESTS'
	SET @report = @report + char(13) + char(10) +  '================================'
	SET @report = @report + char(13) + char(10) +  ''
	 
	-- today
	select @leadscurrent = count(lead_id) from lead_visit 
	where visit_date between @dteToday and @now
	AND promotion_id != 20241 -- Convention

	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrent)

	select @leadslast = count(lead_id) from lead_visit 
	where visit_date between DATEADD( year, -1, DATEADD( day, 1, @dteToday )) and DATEADD( year, -1, DATEADD( day, 1, @now))
	AND promotion_id != 20241 -- Convention

	SET @column2 = CONVERT( NVARCHAR(10), @leadslast)

	select @leadslasttotal = count(lead_id) from lead_visit 
	WHERE DATEPART(day, visit_date) = DATEPART(day, DATEADD( year, -1, DATEADD( day, 1, @dteToday )))
		  AND DATEPART(month, visit_date) = DATEPART(month, DATEADD( year, -1, DATEADD( day, 1, @dteToday )))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD( year, -1, DATEADD( day, 1, @dteToday )))
	AND promotion_id != 20241 -- Convention

	SET @column4 = CONVERT( NVARCHAR(10), @leadslasttotal)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'TODAY:'
--	SET @report =  @report  + char(13) + char(10) + 'TODAY:'
	SET @title5 =  'This time'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3 

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslasttotal)
	set @var = (@var / convert(decimal(20, 4),@leadslasttotal)) * 100
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title5 =  'To date'
	--SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column4 + @column5


	-- yesterday
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead_visit 
	WHERE DATEPART(day, visit_date) = DATEPART(day, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(month, visit_date) = DATEPART(month, DATEADD(day, -1, @dteToday ))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD(day, -1, @dteToday ))
    AND promotion_id != 20241 -- Convention

	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrent)


	select @leadslast = count(lead_id) from lead_visit 
	WHERE DATEPART(day, visit_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(month, visit_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention

	SET @column2 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'YESTERDAY:'
	SET @title5 =  'Total'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3


	-- month
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead_visit 
	WHERE --DATEPART(day, visit_date) = DATEPART(day, DATEADD(day, -1, @dteToday ))AND
		   DATEPART(month, visit_date) = DATEPART(month, DATEADD(day, 0, @dteToday ))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD(day, 0, @dteToday ))
	AND promotion_id != 20241 -- Convention

	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrent)

	select @leadslast = count(lead_id) from lead_visit 
	WHERE DATEPART(day, visit_date) <= DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(month, visit_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention

	SET @column2 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'MONTH:'
	SET @title5 =  'To date'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3

	select @leadslast = count(lead_id) from lead_visit 
	WHERE --DATEPART(day, visit_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		   DATEPART(month, visit_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' requests entered last year same month'
	SET @column2 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	--SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'MONTH:'
	SET @title5 =  'Total'
	--SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3


	-- Year
	SET @report = @report + char(13) + char(10) +  ' '

	select @leadscurrent = count(lead_id) from lead_visit 
	WHERE --DATEPART(day, visit_date) = DATEPART(day, DATEADD(day, -1, @dteToday ))AND
		  -- DATEPART(month, visit_date) = DATEPART(month, DATEADD(day, 0, @dteToday ))AND
		   DATEPART(year, visit_date) = DATEPART(year, DATEADD(day, 0, @dteToday ))
	AND promotion_id != 20241 -- Convention
	SET @column1 = CONVERT( NVARCHAR(10), @leadscurrent)

	select @leadslast = count(lead_id) from lead_visit 
	WHERE	visit_date <= DATEADD(year, -1, @dteToday) 
			--DATEPART(day, visit_date) <= DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  --AND DATEPART(month, visit_date) <= DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		  AND DATEPART(year, visit_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention
	--SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(10), @leadslast) + ' requests entered last year to date'
	SET @column2 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	SET @column3 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title4 =  'YEAR:'
	SET @title5 =  'To date'
	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column2 + @column3

	select @leadslast = count(lead_id) from lead_visit 
	WHERE --DATEPART(day, visit_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		   DATEPART(month, visit_date) <= DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
		   AND DATEPART(year, visit_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention

	SET @column4 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	--SET @report =  @report + char(13) + char(10) + char(13) + char(10) + 'YEAR - SAME MONTH:'
	SET @title5 =  'Same month'
	--SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column4 + @column5


	select @leadslast = count(lead_id) from lead_visit 
	WHERE --DATEPART(day, visit_date) = DATEPART(day, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		  -- DATEPART(month, visit_date) = DATEPART(month,DATEADD( year, -1, DATEADD(day, 0, @dteToday )))AND
		   DATEPART(year, visit_date) = DATEPART(year, DATEADD( year, -1, DATEADD(day, 0, @dteToday )))
	AND promotion_id != 20241 -- Convention
	SET @column4 = CONVERT( NVARCHAR(10), @leadslast)

	set @var = 0 
	set @var = convert(decimal(20, 4), @leadscurrent-@leadslast)
	set @var = (@var / convert(decimal(20, 4),@leadslast)) * 100
	SET @column5 = CONVERT( VARCHAR(10),convert(int, @var)) + ' %'

	SET @title5 =  'Total'
--	SET @report =  @report + char(13) + char(10) + @title4 + @title1 + @title2+ @title3
	SET @report =  @report + char(13) + char(10) + @title5 +  @column1 + @column4 + @column5



END 





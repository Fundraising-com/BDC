USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_previous_business_day]    Script Date: 02/14/2014 13:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_previous_business_day] 
                  @date datetime,
                  @duration int  
AS
BEGIN

declare @sql nvarchar(1000)

set @sql = N'SELECT TOP ' + CAST(@duration + 1 AS varchar(100)) + ' business_Date'
			 + ' INTO #temp'
			 + ' FROM business_calendar '
			 + ' WHERE business_date <= @date AND holiday = 0 AND weekend = 0'
			 + ' ORDER BY business_date DESC;'
			 + ' SELECT TOP 1 business_date FROM #temp ORDER BY business_date ASC'

EXECUTE sp_executesql @sql, N'@date datetime', @date

END
GO

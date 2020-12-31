USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetCurrentFiscalStartAndEnd]    Script Date: 06/07/2017 09:33:08 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetCurrentFiscalStartAndEnd]
	@startDate datetime OUTPUT,
	@endDate datetime OUTPUT

 AS
	-- Systemoptions has the fiscal year
	declare  @fiscal  int
	select @fiscal = long1value from systemoptions where keyvalue='FiscalYear'
	
	select @startDate=isnull(min(startdate),'1/1/95'), 
		@endDate = isnull(max(enddate),'1/1/95') from season where fiscalyear=@fiscal and Season='Y'

print @startDate
print @endDate
GO

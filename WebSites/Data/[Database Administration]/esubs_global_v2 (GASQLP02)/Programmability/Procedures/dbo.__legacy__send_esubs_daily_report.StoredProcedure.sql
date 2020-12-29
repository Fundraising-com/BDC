USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[__legacy__send_esubs_daily_report]    Script Date: 02/14/2014 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =================================================
-- Author:		Philippe girard
-- Create date: 2006/05/09
-- Description:	Send report with xp_smtp_sendmail
--
-- =================================================
CREATE PROCEDURE [dbo].[__legacy__send_esubs_daily_report]
AS
BEGIN

	declare @report varchar(8000)
	declare @source_id int
	declare @project_id int
	declare @subject varchar(100)
	declare @to_name varchar(100)
	declare @to_email varchar(100)
	declare @rc int

	set @report = ''

	--exec esubs_global_v2.dbo.report_esubs_daily_totals_new @report OUTPUT
	exec esubs_global_v2.dbo.report_esubs_daily_totals_new2 @report OUTPUT

	exec @rc = master..xp_smtp_sendmail 
	@from = 'sqladmin@efundraising.com'
	,@from_name = 'sqladmin@efundraising.com'
	,@to = 'Melissa Cote- QSP <melissa_cote@qsp.com>'
	--,@to = 'Drew Pettit - QSP <drew_pettit@qsp.com>; Marc Alcindor- QSP <marc_alcindor@qsp.com>; Melissa Cote- QSP <melissa_cote@qsp.com>;  Xavier Desaunettes- QSP <xavier_desaunettes@qsp.com>'
	,@subject = '[SqlAdmin] eSubs Hourly Report'
	,@type = 'text/plain'
	,@message = @report
	,@server     = N'outgoingsmtp.qsp.timeinc.com'

	if (@@error <> 0 or @rc <> 0)
		raiserror(N'Sending message using xp_smtp_sendmail failed', 16, 1)

	return @rc

END
GO

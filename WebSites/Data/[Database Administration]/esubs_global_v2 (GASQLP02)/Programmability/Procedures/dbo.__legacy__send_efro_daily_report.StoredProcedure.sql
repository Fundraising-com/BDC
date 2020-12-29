USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[__legacy__send_efro_daily_report]    Script Date: 02/14/2014 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =================================================
-- Author:		Philippe girard
-- Create date: 2006/05/09
-- Description:	Send report with xp_smtp_sendmail
-- exec [send_esubs_daily_report]
-- =================================================
CREATE PROCEDURE [dbo].[__legacy__send_efro_daily_report]
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

	EXEC @rc = msdb.dbo.sp_send_dbmail  
      @profile_name = 'qspproddb3'
      , @recipients = 'Cote Melissa - QSP <melissa_cote@qsp.com>'
      --, @recipients = 'Pettit Drew - QSP <drew_pettit@qsp.com>; Alcindor Marc - QSP <marc_alcindor@qsp.com>; Cote Melissa - QSP <melissa_cote@qsp.com>; Desaunettes, Xavier - QSP <xavier_desaunettes@qsp.com>'
      --, @copy_recipients = 'melissa_cote@qsp.com'
      , @subject = '[EFUNDRAISING ONLINE] Hourly Report'
      , @body = @report
      , @from_address = 'mcote@qsp.com'
      , @reply_to = 'mcote@qsp.com'

	if (@@error <> 0 or @rc <> 0)
		raiserror(N'Sending message using xp_smtp_sendmail failed', 16, 1)

	return @rc

END
GO

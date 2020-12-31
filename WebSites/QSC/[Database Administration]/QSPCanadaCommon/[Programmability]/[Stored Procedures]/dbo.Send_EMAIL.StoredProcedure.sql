USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[Send_EMAIL]    Script Date: 06/07/2017 09:33:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------------------------------------------------------------------------------------
--     JLC   7/14/2004 - renamed from Send_CDONTS_MAIL to Send_EMAIL, based on underlying protocol change
--     	- John Pappas implemented the underlying master.dbo.xp_smtp_sendmail, 
--     	- see http://www.sqldev.net/xp/xpsmtp.htm for more details.
--     JLC   6/08/2004 - Added the @PRIORITY option
--     JLC   3/03/2004 - Added the @HTML option
--     MTC 4/12/2002 - Send Email using the CDONTS object
--     MS   11/21/06  Changed variable length to 1000 for email recipient
-----------------------------------------------------------------------------------------------------------------------------
CREATE   PROCEDURE [dbo].[Send_EMAIL]
	@From     varchar(1000),
	@To       varchar(1000),
	@Subject  varchar(100),
	@Body     varchar(8000),
	@CC 	  varchar(1000) = null,
	@BCC      varchar(1000) = null,
	@HTML     bit = 0,
	@PRIORITY tinyint = 1,
	@ReplyTo  varchar(100) = null
AS


--Set the e-mail importance level (0=low, 1=normal (default) , 2=high) 
DECLARE @vPriority varchar(8)
SELECT @vPriority = ''
SELECT @vPriority = 
	CASE @PRIORITY
		WHEN 2 THEN 'High'
		WHEN 1 THEN 'Normal'
		WHEN 0 THEN 'Low'
		ELSE 'Normal'
	END

DECLARE @vType varchar(10)
SELECT @vType = 
	CASE @HTML
		WHEN 1 THEN 'HTML'
		WHEN 0 THEN 'TEXT'
		ELSE 'TEXT'
	END


exec msdb.dbo.sp_send_dbmail  @profile_name =  'Email' 
      ,  @recipients = @To
      ,  @copy_recipients = @CC
      ,  @blind_copy_recipients = @BCC
      ,  @subject =  @Subject
      ,  @body = @Body
      ,  @body_format = @vType
      ,  @importance = @vPriority


/*
print 'about to exec the proc '
print 'rc: ' + cast (@rc as varchar) + ' '
*/
GO

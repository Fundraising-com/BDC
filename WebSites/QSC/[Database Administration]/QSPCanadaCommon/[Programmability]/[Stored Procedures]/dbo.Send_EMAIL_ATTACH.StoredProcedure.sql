USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[Send_EMAIL_ATTACH]    Script Date: 06/07/2017 09:33:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Send_EMAIL_ATTACH] 	@From varchar(100), 
							@To varchar(1000),
							@Subject  varchar(100), 
							@Body varchar(8000) ,
							@Fileattachment varchar(200)

AS

--Declare    @rc int

exec msdb.dbo.sp_send_dbmail  @profile_name =  'Email' 
      ,  @recipients = @To
      --,  @copy_recipients = @CC
      --,  @blind_copy_recipients = @BCC
      ,  @subject =  @Subject
      ,  @body = @Body
      --,  @body_format = @vType
      --,  @importance = @vPriority
      ,  @file_attachments = @Fileattachment


/*
exec @rc =  master.dbo.xp_smtp_sendmail
	    @FROM 	= @From,
	    @TO 	= @To,
	    @subject 	= @Subject,
                 @message 	= @Body,
	    @attachments = @Fileattachment,
	   @server 	= 'nasmtp.us.rdigest.com'
*/
GO

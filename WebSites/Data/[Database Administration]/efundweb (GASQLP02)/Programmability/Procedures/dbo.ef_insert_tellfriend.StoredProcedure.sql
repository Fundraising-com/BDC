USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[ef_insert_tellfriend]    Script Date: 02/14/2014 13:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ef_insert_tellfriend] 
	@cultureCode varchar(5),
	@fromName varchar(256),
	@fromEmail varchar(256),
	@toName varchar(256),
	@toEmail varchar(256),
	@subject varchar(256),
	@message varchar(8000),
	@dateSent datetime
AS

insert into tell_a_friend(culture_code, from_name, from_email, to_name, to_email, subject, message, datesent)
values(@cultureCode, @fromName, @fromEmail, @toName, @toEmail, @subject, @message, @dateSent)
GO

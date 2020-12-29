USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_email_template]    Script Date: 02/14/2014 13:07:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	fblais
Created On: 	2005-08-22
Description:	This stored proc update an email.
*/
CREATE PROCEDURE [dbo].[es_update_email_template]
	  @email_template_type_id tinyint =1
	, @email_template_name varchar(50)
	, @description varchar(255)
	, @param_procedure_call varchar(100)
	, @from_name varchar(50)
	, @from_email_address varchar(100)
	, @reply_to_name varchar(50)
	, @reply_to_email_address varchar(100)
	, @bounce_name varchar(50)
	, @bounce_email_address varchar(100)
	, @footer_text varchar(1000)
	, @footer_html varchar(1000)
	, @email_template_id int
	, @culture_code varchar(7)
	, @subject varchar(100)
	, @body_html text
	, @body_text text
AS
begin transaction


update email_template
	set	 email_template_type_id=@email_template_type_id
		, email_template_name=@email_template_name
		, description=@description
		, param_procedure_call=@param_procedure_call
		, from_name=@from_name
		, from_email_address=@from_email_address
		, reply_to_name=@reply_to_name
		, reply_to_email_address=@reply_to_email_address
		, bounce_name=@bounce_name
		, bounce_email_address=@bounce_email_address
		
where
	email_template_id = @email_template_id
	
IF @@ERROR <> 0 
begin 
	rollback transaction
	return -1
end


update email_template_culture
	set email_template_id=@email_template_id
	, culture_code=@culture_code
	, subject=@subject
	, body_html=@body_html
	, body_text=@body_text
	, footer_text=@footer_text
	, footer_html=@footer_html
where
	email_template_id = @email_template_id
and	culture_code = @culture_code

IF @@ERROR = 0 
begin
	commit transaction
	return @email_template_id
end
ELSE 
begin
	rollback transaction
	return -2
end
GO

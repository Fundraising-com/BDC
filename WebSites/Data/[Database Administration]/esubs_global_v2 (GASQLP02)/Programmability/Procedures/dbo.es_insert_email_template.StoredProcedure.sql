USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_email_template]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Fblais
Created On: 	Dec 9, 2004
Description:	This stored proc inserts a new email template into efo_email_type.
*/
CREATE PROCEDURE [dbo].[es_insert_email_template]
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
	, @email_template_id int =null
	, @culture_code varchar(7)
	, @subject varchar(100)
	, @body_html text
	, @body_text text

AS
begin transaction

if @email_template_id is null 
begin
	insert into email_template(
		  email_template_type_id
		, email_template_name
		, description
		, param_procedure_call
		, from_name
		, from_email_address
		, reply_to_name
		, reply_to_email_address
		, bounce_name
		, bounce_email_address
		
		, create_date
	)values(
		  @email_template_type_id
		, @email_template_name
		, @description
		, @param_procedure_call
		, @from_name
		, @from_email_address
		, @reply_to_name
		, @reply_to_email_address
		, @bounce_name
		, @bounce_email_address
		, getdate()
	)
	IF @@ERROR = 0 
	begin
		set @email_template_id = scope_identity()
	end
	ELSE 
	begin
		rollback transaction
		return -1
	end
end

insert into email_template_culture(
	email_template_id
	, culture_code
	, subject
	, body_html
	, body_text
	, footer_text
	, footer_html
	, create_date
)values(
	@email_template_id
	, @culture_code
	, @subject
	, @body_html
	, @body_text
	, @footer_text
	, @footer_html
	, getdate()
)

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

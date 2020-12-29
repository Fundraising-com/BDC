USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_email_template]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
Created By:	Fblais
date :		2005-08-22
*/
Create PROCEDURE [dbo].[es_get_email_template]
	@email_template_id int
	,@culture_code varchar(7)
AS

SELECT 
	subject
	,email_template_name
	,email_template_type_id
	,description
	,body_text
	,body_html
	,param_procedure_call
	,from_name
	,from_email_address
	,reply_to_name
	,reply_to_email_address
	,bounce_name
	,bounce_email_address
	,etc.footer_text
	,etc.footer_html
FROM 
	email_template et
	inner join email_template_culture etc
	on et.email_template_id = etc.email_template_id
	and etc.culture_code = @culture_code
WHERE 
	et.email_template_id = @email_template_id
GO

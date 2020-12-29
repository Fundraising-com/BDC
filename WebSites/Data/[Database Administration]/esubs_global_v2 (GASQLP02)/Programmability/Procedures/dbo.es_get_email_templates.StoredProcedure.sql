USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_email_templates]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
Created By:	Fblais
date :		2005-08-22
*/
CREATE PROCEDURE [dbo].[es_get_email_templates] AS

SELECT et.email_template_id,email_template_name, description,culture_code 
FROM 
	email_template et
	inner join email_template_culture etc
	on et.email_template_id = etc.email_template_id
ORDER BY et.email_template_id
GO

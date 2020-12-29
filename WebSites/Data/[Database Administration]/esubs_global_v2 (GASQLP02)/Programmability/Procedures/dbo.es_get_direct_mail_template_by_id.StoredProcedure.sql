USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_direct_mail_template_by_id]    Script Date: 02/14/2014 13:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_direct_mail_template_by_id] @direct_mail_template_id int
AS
BEGIN


select direct_mail_id
	, [message]
	, image_url
	, create_date
	, document_path
from direct_mail_template where direct_mail_id = @direct_mail_template_id;

 
END
GO

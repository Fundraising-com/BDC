USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_direct_mail_info_by_id]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_direct_mail_info_by_id] @direct_mail_info_id int
AS
BEGIN


select direct_mail_info_id
	, [message]
	, image_url
	, moderated
	, direct_mail_status
	, create_date
	, document_path
	, event_participation_id
	, member_hierarchy_id
from direct_mail_info 
where direct_mail_info_id = @direct_mail_info_id;

 
END
GO

USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_direct_mail_info]    Script Date: 02/14/2014 13:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_update_direct_mail_info] 
	@direct_mail_info_id int
	, @message text
	, @image_url varchar(256)
	, @moderated bit
	, @direct_mail_status smallint
	, @create_date datetime
	, @document_path varchar(256)
	, @event_participation_id int
	, @member_hierarchy_id int
AS
BEGIN

update direct_mail_info
	set [message] = @message
	, image_url = @image_url
	, moderated = @moderated
	, direct_mail_status = @direct_mail_status
	, create_date = @create_date
	, document_path = @document_path
	, event_participation_id = @event_participation_id
	, member_hierarchy_id = @member_hierarchy_id
where direct_mail_info_id = @direct_mail_info_id
 
END
GO

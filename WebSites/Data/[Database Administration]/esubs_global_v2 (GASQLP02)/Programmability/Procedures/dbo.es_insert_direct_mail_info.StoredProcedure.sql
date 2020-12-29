USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_direct_mail_info]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_insert_direct_mail_info] 
	@direct_mail_info_id int out
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

insert into direct_mail_info (
	[message]
	, image_url
	, moderated
	, direct_mail_status
	, create_date
	, document_path
	, event_participation_id
	, member_hierarchy_id)
values(
	@message
	,@image_url
	,@moderated
	,@direct_mail_status
	,@create_date
	,@document_path
	,@event_participation_id
	,@member_hierarchy_id
)

select @direct_mail_info_id = SCOPE_IDENTITY();
 
END
GO

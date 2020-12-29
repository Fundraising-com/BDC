USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_direct_mail]    Script Date: 02/14/2014 13:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_update_direct_mail] 
	@direct_mail_id int
	, @direct_mail_info_id int
	, @direct_mail_status smallint
	, @event_participation_id int
	, @member_hierarchy_id int
	, @postal_address_id int
	, @create_date datetime
AS
BEGIN

update direct_mail
	set direct_mail_info_id = @direct_mail_info_id
	, direct_mail_status = @direct_mail_status
	, create_date = @create_date
	, event_participation_id = @event_participation_id
	, member_hierarchy_id = @member_hierarchy_id
	, postal_address_id = @postal_address_id
where direct_mail_id = @direct_mail_id
 
END
GO

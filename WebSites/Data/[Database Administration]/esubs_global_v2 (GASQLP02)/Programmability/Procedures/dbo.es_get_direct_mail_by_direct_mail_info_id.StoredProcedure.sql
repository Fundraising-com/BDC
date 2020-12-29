USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_direct_mail_by_direct_mail_info_id]    Script Date: 02/14/2014 13:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_direct_mail_by_direct_mail_info_id] @direct_mail_info_id int
AS
BEGIN


select direct_mail_id
	, direct_mail_info_id
	, direct_mail_status
	, event_participation_id
	, member_hierarchy_id
	, postal_address_id
	, create_date
from direct_mail with (nolock)
where direct_mail_info_id = @direct_mail_info_id and direct_mail_status = 1 ;

 
END
GO

USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_direct_mail_info_ready_to_be_processed]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_direct_mail_info_ready_to_be_processed]
AS
BEGIN

select dmi.direct_mail_info_id
	, dmi.[message]
	, dmi.image_url
	, dmi.moderated
	, dmi.direct_mail_status
	, dmi.create_date
	, dmi.document_path
	, dmi.event_participation_id
	, dmi.member_hierarchy_id
from direct_mail_info dmi
inner join direct_mail dm
	on dmi.direct_mail_info_id = dm.direct_mail_info_id
inner join dm_personalization_image dpi
	on dmi.direct_mail_info_id = dpi.direct_mail_info_id
where dmi.[direct_mail_status] = 1 
	--and dpi.image_approval_status_id = 3
	and dm.direct_mail_status = 1
 
END
GO

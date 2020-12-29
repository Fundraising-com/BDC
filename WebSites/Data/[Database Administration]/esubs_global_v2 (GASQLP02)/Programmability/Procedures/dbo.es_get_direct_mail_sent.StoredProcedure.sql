USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_direct_mail_sent]    Script Date: 02/14/2014 13:05:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_direct_mail_sent]
	@event_participation_id int
AS
BEGIN

select dm.direct_mail_id
	, dm.direct_mail_info_id
	, dm.direct_mail_status
	, dm.event_participation_id
	, dm.member_hierarchy_id
	, dm.postal_address_id
	, dm.create_date
from direct_mail dm
where dm.direct_mail_info_id in (

	select direct_mail_info_id
	from direct_mail_info
	where event_participation_id = @event_participation_id

)
 
END
GO

USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_direct_mail]    Script Date: 02/14/2014 13:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_direct_mail]
AS
BEGIN

select direct_mail_id
	, direct_mail_info_id
	, direct_mail_status
	, event_participation_id
	, member_hierarchy_id
	, postal_address_id
	, create_date
from direct_mail;

 
END
GO

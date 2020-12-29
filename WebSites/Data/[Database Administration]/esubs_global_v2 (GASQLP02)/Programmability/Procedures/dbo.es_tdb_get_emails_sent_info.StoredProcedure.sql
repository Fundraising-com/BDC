USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_tdb_get_emails_sent_info]    Script Date: 02/14/2014 13:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_tdb_get_emails_sent_info] AS

SELECT   ep.*, cet.*, t.*, ti.* FROM custom_email_template cet
	INNER JOIN touch_info ti
		ON cet.touch_info_id = ti.touch_info_id
	INNER JOIN touch t
		ON t.touch_info_id = ti.touch_info_id
	INNER JOIN event_participation ep
		ON ep.event_participation_id = t.event_participation_id
WHERE cet.create_date >= '2006-01-01' AND cet.create_date <= '2006-01-31'
and ti.business_rule_id = 59
GO

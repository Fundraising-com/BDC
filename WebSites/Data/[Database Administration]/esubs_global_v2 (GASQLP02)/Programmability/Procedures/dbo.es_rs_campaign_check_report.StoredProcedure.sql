USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rs_campaign_check_report]    Script Date: 02/14/2014 13:07:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_rs_campaign_check_report]
	@event_id int
AS
BEGIN

	exec es_rpt_campaign_check_report @event_id

END
GO

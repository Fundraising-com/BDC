USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_efr_update_lead_activity]    Script Date: 02/14/2014 13:04:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efr_call_efr_update_lead_activity]
	@lead_activity_id int
	, @lead_id int
	, @lead_activity_type_id int
	, @lead_activity_date datetime
	, @completed_date datetime
	, @comments text
as

exec MONQSPMVA2_EFRPROD.efundraisingprod.dbo.efr_update_lead_activity
	@lead_activity_id
	, @lead_id
	, @lead_activity_type_id
	, @lead_activity_date
	, @completed_date
	, @comments
GO

USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_activitys]    Script Date: 02/14/2014 13:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_activity
CREATE PROCEDURE [dbo].[efrcrm_get_lead_activitys] AS
begin

select Lead_activity_id, Lead_id, Lead_activity_type_id, Lead_activity_date, Completed_date, Comments from Lead_activity

end
GO

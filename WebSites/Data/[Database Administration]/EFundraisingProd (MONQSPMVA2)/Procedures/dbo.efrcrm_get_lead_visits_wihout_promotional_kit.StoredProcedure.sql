USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_visits_wihout_promotional_kit]    Script Date: 02/14/2014 13:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_lead_visits_wihout_promotional_kit] 
AS
-- Generate get stored proc for Lead_Visit
begin

select Top 20  lv.Lead_Visit_ID
	, lv.Promotion_ID
	, lv.Lead_ID
	, lv.Temp_Lead_ID
	, lv.Visit_Date
	,lv. Channel_Code
from Lead_Visit as lv  with (nolock)
	Left Join promotional_kit as pk with (nolock)
		on lv.lead_visit_id = pk.lead_visit_id
where pk.lead_visit_id is null
and lv.visit_date > '2007-06-12 12:00:00'	-- deployment date 

--** Deployment date because before this, this process did not exits.  It was directly done using the lead table.  
--** Before this date, it's only hypothetical historical data.


end
GO

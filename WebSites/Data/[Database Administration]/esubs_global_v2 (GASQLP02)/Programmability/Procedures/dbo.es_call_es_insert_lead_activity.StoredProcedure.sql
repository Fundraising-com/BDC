USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_call_es_insert_lead_activity]    Script Date: 02/14/2014 13:05:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create procedure [dbo].[es_call_es_insert_lead_activity]
	@lead_id int
	, @lead_activity_type_id int
as

exec sqlerose_efundraisingprod.efundraisingprod.dbo.es_insert_lead_activity
	@lead_id 
	, @lead_activity_type_id
GO

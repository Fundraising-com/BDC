USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_es_insert_lead_activity]    Script Date: 02/14/2014 13:04:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   procedure [dbo].[efr_call_es_insert_lead_activity]
	@lead_id int
	, @lead_activity_type_id int
as

exec MONQSPMVA2_EFRPROD.efundraisingprod.dbo.es_insert_lead_activity
	@lead_id 
	, @lead_activity_type_id
GO

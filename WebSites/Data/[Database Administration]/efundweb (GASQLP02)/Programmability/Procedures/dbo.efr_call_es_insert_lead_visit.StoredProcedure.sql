USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_es_insert_lead_visit]    Script Date: 02/14/2014 13:04:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   procedure [dbo].[efr_call_es_insert_lead_visit]
	@lead_id int
	, @promotion_id int
	, @temp_lead_id int = NULL
as
declare @lead_visit_id int
exec @lead_visit_id = MONQSPMVA2_EFRPROD.efundraisingprod.dbo.es_insert_lead_visit
	@lead_id 
	, @promotion_id
	, @temp_lead_id
return @lead_visit_id
GO

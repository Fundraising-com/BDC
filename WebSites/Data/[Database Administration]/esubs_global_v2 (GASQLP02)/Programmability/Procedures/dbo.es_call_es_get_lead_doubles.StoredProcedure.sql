USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_call_es_get_lead_doubles]    Script Date: 02/14/2014 13:05:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[es_call_es_get_lead_doubles]
		@street as varchar(100)
		, @day_phone as varchar(20)
		, @evening_phone as varchar(20)
		, @email as varchar(50)
as

exec sqlerose_efundraisingprod.efundraisingprod.dbo.es_get_lead_doubles 
		@street 
		, @day_phone 
		, @evening_phone 
		, @email
GO

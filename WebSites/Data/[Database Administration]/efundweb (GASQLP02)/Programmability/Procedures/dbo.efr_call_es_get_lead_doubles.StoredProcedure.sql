USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_es_get_lead_doubles]    Script Date: 02/14/2014 13:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efr_call_es_get_lead_doubles]
		@first_name as varchar(50),
		@last_name as varchar(50),
		@street_address as varchar(100),
		@zip_code as varchar(10),
		@day_phone as varchar(20),
		@evening_phone as varchar(20),
		@email as varchar(50)
as

exec MONQSPMVA2_EFRPROD.efundraisingprod.dbo.es_get_lead_doubles 
		@first_name,
		@last_name,
		@street_address,
		@zip_code,
		@day_phone,
		@evening_phone,
		@email
GO

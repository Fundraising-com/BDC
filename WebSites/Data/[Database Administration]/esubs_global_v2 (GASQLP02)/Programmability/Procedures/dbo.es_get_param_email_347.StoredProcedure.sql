USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_347]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_347]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, 347 as email_template_id
		, @identification as identification
GO

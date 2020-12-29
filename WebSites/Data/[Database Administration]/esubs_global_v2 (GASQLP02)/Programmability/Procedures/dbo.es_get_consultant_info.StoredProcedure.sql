USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_consultant_info]    Script Date: 02/14/2014 13:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create procedure [dbo].[es_get_consultant_info]
		@consultant_id int
as
select 
	partner_id
	, ext_consultant_id
	, [name]
	, email_address
	, is_active
	, is_agent
from 
	efundraisingprod.dbo.consultant 
where 
	consultant_id = @consultant_id
GO

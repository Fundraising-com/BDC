USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_es_get_consultant]    Script Date: 02/14/2014 13:04:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[efr_call_es_get_consultant]
	@consultant_id int	
as

exec MONQSPMVA2_EFRPROD.efundraisingprod.dbo.es_get_consultant
		@consultant_id
GO

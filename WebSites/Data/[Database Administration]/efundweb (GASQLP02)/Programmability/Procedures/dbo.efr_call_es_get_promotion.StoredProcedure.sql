USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_es_get_promotion]    Script Date: 02/14/2014 13:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efr_call_es_get_promotion]
	@promotion_id int	
as

exec MONQSPMVA2_EFRPROD.efundraisingprod.dbo.es_get_promotion
		@promotion_id
GO

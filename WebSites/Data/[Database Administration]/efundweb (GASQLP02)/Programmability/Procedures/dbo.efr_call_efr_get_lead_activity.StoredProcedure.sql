USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_efr_get_lead_activity]    Script Date: 02/14/2014 13:04:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efr_call_efr_get_lead_activity]
	@lead_activity_id int	
as

exec MONQSPMVA2_EFRPROD.efundraisingprod.dbo.efr_get_lead_activity
		@lead_activity_id
GO

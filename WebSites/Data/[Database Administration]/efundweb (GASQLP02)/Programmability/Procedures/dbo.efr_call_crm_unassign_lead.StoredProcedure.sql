USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_crm_unassign_lead]    Script Date: 02/14/2014 13:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efr_call_crm_unassign_lead] (
	@lead_id as int,
	@consultant_id as int,
	@user_id as int	
)
as 

exec MONQSPMVA2_EFRPROD.efundraisingprod.dbo.crm_unassign_lead 	
	@lead_id, 
	@consultant_id,
	@user_id
GO

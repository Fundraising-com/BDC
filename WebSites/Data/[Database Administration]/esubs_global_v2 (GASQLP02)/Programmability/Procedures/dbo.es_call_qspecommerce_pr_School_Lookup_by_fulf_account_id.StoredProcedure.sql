USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_call_qspecommerce_pr_School_Lookup_by_fulf_account_id]    Script Date: 02/14/2014 13:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_call_qspecommerce_pr_School_Lookup_by_fulf_account_id]
	 @fulf_account_id int

AS

exec QSPEcommerce.dbo.pr_School_Lookup_by_fulf_account_id @fulf_account_id
GO

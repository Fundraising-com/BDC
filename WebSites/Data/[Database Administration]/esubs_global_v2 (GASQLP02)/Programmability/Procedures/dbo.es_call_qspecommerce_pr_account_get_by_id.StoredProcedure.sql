USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_call_qspecommerce_pr_account_get_by_id]    Script Date: 02/14/2014 13:05:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_call_qspecommerce_pr_account_get_by_id]
	 @intAccountID int

AS

exec QSPFulfillment.dbo.pr_account_get_by_id @intAccountID
GO

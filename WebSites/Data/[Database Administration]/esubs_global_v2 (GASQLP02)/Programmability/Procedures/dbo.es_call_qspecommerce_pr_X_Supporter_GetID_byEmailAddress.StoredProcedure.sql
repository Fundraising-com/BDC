USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_call_qspecommerce_pr_X_Supporter_GetID_byEmailAddress]    Script Date: 02/14/2014 13:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_call_qspecommerce_pr_X_Supporter_GetID_byEmailAddress]
	 @sEmail_Address varchar(75)

AS

exec QSPEcommerce.dbo.pr_X_Supporter_GetID_byEmailAddress @sEmail_Address
GO

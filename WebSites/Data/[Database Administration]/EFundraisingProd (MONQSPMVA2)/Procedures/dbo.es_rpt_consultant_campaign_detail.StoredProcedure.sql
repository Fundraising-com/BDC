USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_consultant_campaign_detail]    Script Date: 02/14/2014 13:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[es_rpt_consultant_campaign_detail]
 @start_date datetime,
 @end_date datetime, 
 @consultant_id int
AS
BEGIN
execute esubs_global_v2.dbo.es_rpt_consultant_campaign_detail @start_date, @end_date,@consultant_id
END
GO

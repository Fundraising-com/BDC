USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_auto_created_groups_by_partner_id]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 19-06-2013
-- Description:	get auto-created groups by partner and filter by create date
-- exec [dbo].[es_get_auto_created_groups_by_partner_id] 58, '08/06/2013'
-- =============================================
CREATE PROCEDURE [dbo].[es_get_auto_created_groups_by_partner_id]
	@partner_id int, 
	@start_date datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT group_id, group_name, create_date, partner_id from [group]
	WHERE partner_id = @partner_id and isnull(external_group_id,'')<>''
	AND create_date > DATEADD(d,DATEDIFF(d,0,@start_date),0)
	Order by 1 DESC	
END
GO

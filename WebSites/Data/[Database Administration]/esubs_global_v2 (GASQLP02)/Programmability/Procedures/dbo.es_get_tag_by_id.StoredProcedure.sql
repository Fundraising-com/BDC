USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_tag_by_id]    Script Date: 02/14/2014 13:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 9/13/2011
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_tag_by_id]
	@tag_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [tag_id]
		  ,[start_tag_name]
          ,[end_tag_name]
          ,[description]
	FROM   [esubs_global_v2].[dbo].[tag]
	WHERE  tag_id = @tag_id
END
GO

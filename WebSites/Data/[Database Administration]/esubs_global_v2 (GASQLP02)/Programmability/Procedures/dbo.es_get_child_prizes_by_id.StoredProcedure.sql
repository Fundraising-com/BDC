USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_child_prizes_by_id]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        Jiro Hidaka
-- Create date: 11/05/2010
-- Description:    Get child prizes by prize id
-- =============================================
CREATE PROCEDURE [dbo].[es_get_child_prizes_by_id]
    @Prize_id int
AS
BEGIN
    SELECT [prize_id]
      ,[parent_prize_id]
      ,[prize_type_id]
      ,[prize_name]
      ,[create_date]
    FROM [esubs_global_v2].[dbo].[prize]
    WHERE parent_prize_id = @Prize_id
END
GO

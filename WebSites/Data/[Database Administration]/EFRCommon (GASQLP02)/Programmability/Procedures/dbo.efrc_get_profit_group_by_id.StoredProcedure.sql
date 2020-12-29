USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_profit_group_by_id]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===================================================
-- Author:		Jason Warren
-- Create date: Today
-- Description:	Get the full partner profit info by id
-- ===================================================
CREATE PROCEDURE [dbo].[efrc_get_profit_group_by_id]
	@profit_group_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT description, disclaimer, alt_disclaimer
	FROM   profit_group
	WHERE  profit_group_id = @profit_group_id
END
GO

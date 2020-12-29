USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_profit_by_profit_group_id]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================
-- Author:		Jiro Hidaka
-- Create date: January 5, 2011
-- Description:	Get Profit data by profit group id
-- ===============================================
CREATE PROCEDURE [dbo].[efrc_get_profit_by_profit_group_id]
	@profit_group_id int
AS
BEGIN
    SET NOCOUNT ON;

	SELECT profit_id, profit_percentage, description, disclaimer, alt_disclaimer, profit_group_id, qsp_catalog_type_id FROM dbo.profit
	WHERE  profit_group_id = @profit_group_id
END
GO

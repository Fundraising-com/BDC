USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_profit_by_id]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: April 19, 2010
-- Description:	Get Profit data by profit id
-- =============================================
CREATE PROCEDURE [dbo].[efrc_get_profit_by_id]
	@profit_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT profit_id, profit_percentage, description, disclaimer, alt_disclaimer, profit_group_id, qsp_catalog_type_id FROM dbo.profit
	WHERE  profit_id = @profit_id
END
GO

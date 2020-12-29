USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_profits]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: April 19, 2010
-- Description:	Returns all Profit data
-- =============================================
CREATE PROCEDURE [dbo].[efrc_get_profits] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT profit_id, profit_percentage, description, disclaimer, alt_disclaimer, profit_group_id, qsp_catalog_type_id FROM dbo.profit
END
GO

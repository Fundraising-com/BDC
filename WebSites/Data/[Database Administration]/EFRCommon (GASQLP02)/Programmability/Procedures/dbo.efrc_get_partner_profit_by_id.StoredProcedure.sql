USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_partner_profit_by_id]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===================================================
-- Author:		Jiro Hidaka
-- Create date: April 19, 2010
-- Description:	Get the full partner profit info by id
-- ===================================================
CREATE PROCEDURE [dbo].[efrc_get_partner_profit_by_id]
	@partner_id int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT partner_profit_id, partner_id, start_date, end_date, profit_group_id
	FROM   partner_profit
	WHERE  partner_id = @partner_id
END
GO

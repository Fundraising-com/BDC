USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_current_partner_profit_by_id]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==================================================
-- Author:		Jiro Hidaka
-- Create date: April 20, 2010
-- Description:	Get current partner profit info by id
-- ==================================================
CREATE PROCEDURE [dbo].[efrc_get_current_partner_profit_by_id]
	@partner_id int
AS
BEGIN
    SET NOCOUNT ON;

	SELECT partner_profit_id, partner_id, start_date, end_date , profit_group_id
	FROM   partner_profit
	WHERE  partner_id = @partner_id
           and end_date is null
END
GO

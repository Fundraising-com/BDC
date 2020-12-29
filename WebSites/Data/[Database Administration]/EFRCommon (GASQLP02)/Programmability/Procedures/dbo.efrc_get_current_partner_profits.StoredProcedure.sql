USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_current_partner_profits]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: April 24, 2010
-- Description:	Get current partner profit infos
-- =============================================
CREATE PROCEDURE [dbo].[efrc_get_current_partner_profits]
AS
BEGIN
    SET NOCOUNT ON;

	SELECT partner_profit_id, partner_id, start_date, end_date, profit_group_id
	FROM   partner_profit
	WHERE  end_date is null
    ORDER BY partner_id
END
GO

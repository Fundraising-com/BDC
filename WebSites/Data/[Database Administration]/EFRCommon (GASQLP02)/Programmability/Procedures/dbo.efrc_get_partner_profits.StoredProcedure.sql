USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_partner_profits]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: April 19, 2010
-- Description:	Get the full partner profit info
-- =============================================
CREATE PROCEDURE [dbo].[efrc_get_partner_profits]
AS
BEGIN
    SET NOCOUNT ON;

	SELECT partner_profit_id, partner_id, start_date, end_date, profit_group_id
	FROM   partner_profit 
END
GO

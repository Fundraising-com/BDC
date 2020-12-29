USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_valid_order_ar_state]    Script Date: 02/14/2014 13:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Melissa Cote
-- Create date: 02/19/2013
-- Description:	Contains all the valid order states in GA.CustomerOrderARState table
--              used primarily for reproting
--
--				select * from ga.ar.CustomerOrderARState
-- =============================================
CREATE FUNCTION [dbo].[es_get_valid_order_ar_state] ()
RETURNS @ReportOrderStatus TABLE (order_status_id int)
AS
BEGIN
	INSERT INTO @ReportOrderStatus
	VALUES(2)
	INSERT INTO @ReportOrderStatus
	VALUES(3)

	RETURN
END
GO

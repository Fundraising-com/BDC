USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_valid_order_status]    Script Date: 02/14/2014 13:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 09/16/2009
-- Description:	Contains all the valid order status in QSPFulfilment.Order table
--              used primarily for reproting
-- =============================================
CREATE FUNCTION [dbo].[es_get_valid_order_status] ()
RETURNS @ReportOrderStatus TABLE (order_status_id int)
AS
BEGIN
	INSERT INTO @ReportOrderStatus
	VALUES(101)
	INSERT INTO @ReportOrderStatus
	VALUES(110)
	INSERT INTO @ReportOrderStatus
	VALUES(112)
	INSERT INTO @ReportOrderStatus
	VALUES(114)
	INSERT INTO @ReportOrderStatus
	VALUES(120)
	INSERT INTO @ReportOrderStatus
	VALUES(201)
	INSERT INTO @ReportOrderStatus
	VALUES(301)
	INSERT INTO @ReportOrderStatus
	VALUES(302)
	INSERT INTO @ReportOrderStatus
	VALUES(304)
	INSERT INTO @ReportOrderStatus
	VALUES(401)
	INSERT INTO @ReportOrderStatus
	VALUES(501)
	INSERT INTO @ReportOrderStatus
	VALUES(601)
	INSERT INTO @ReportOrderStatus
	VALUES(701) 

	RETURN
END
GO

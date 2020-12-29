USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_valid_order_state]    Script Date: 07/03/2014 12:19:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Melissa Cote
-- Create date: 02/19/2013
-- Description:	Contains all the valid order states in GA.CustomerOrderState table
--              used primarily for reproting
--
--				select * from ga.core.CustomerOrderState
-- =============================================
ALTER FUNCTION [dbo].[es_get_valid_order_state] ()
RETURNS @ReportOrderStatus TABLE (order_status_id int)
AS
BEGIN
	INSERT INTO @ReportOrderStatus
	VALUES(1)
	INSERT INTO @ReportOrderStatus
	VALUES(2)
	INSERT INTO @ReportOrderStatus
	VALUES(3)
	INSERT INTO @ReportOrderStatus
	VALUES(4)
	INSERT INTO @ReportOrderStatus
	VALUES(5)
	INSERT INTO @ReportOrderStatus
	VALUES(6)
	INSERT INTO @ReportOrderStatus
	VALUES(7)
	INSERT INTO @ReportOrderStatus
	VALUES(8)
	INSERT INTO @ReportOrderStatus
	VALUES(9)
	INSERT INTO @ReportOrderStatus
	VALUES(10)
	INSERT INTO @ReportOrderStatus
	VALUES(11)
	INSERT INTO @ReportOrderStatus
	VALUES(12)
	INSERT INTO @ReportOrderStatus
	VALUES(13) 
	INSERT INTO @ReportOrderStatus
	VALUES(14) 
	INSERT INTO @ReportOrderStatus
	VALUES(15) 
	INSERT INTO @ReportOrderStatus
	VALUES(16) 
	INSERT INTO @ReportOrderStatus
	VALUES(17) 
	INSERT INTO @ReportOrderStatus
	VALUES(18) 
	INSERT INTO @ReportOrderStatus
	VALUES(19) 
	INSERT INTO @ReportOrderStatus
	VALUES(20) 
	INSERT INTO @ReportOrderStatus
	VALUES(21) 
	INSERT INTO @ReportOrderStatus
	VALUES(24) 
	INSERT INTO @ReportOrderStatus
	VALUES(25) 
	INSERT INTO @ReportOrderStatus
	VALUES(26) 
	INSERT INTO @ReportOrderStatus
	VALUES(27) 
	INSERT INTO @ReportOrderStatus
	VALUES(28) 
	INSERT INTO @ReportOrderStatus
	VALUES(29) 
	INSERT INTO @ReportOrderStatus
	VALUES(30) 
	INSERT INTO @ReportOrderStatus
	VALUES(31) 
	INSERT INTO @ReportOrderStatus
	VALUES(34) 
	INSERT INTO @ReportOrderStatus
	VALUES(35) 
	INSERT INTO @ReportOrderStatus
	VALUES(99) 
	INSERT INTO @ReportOrderStatus
	VALUES(255)
	INSERT INTO @ReportOrderStatus
	VALUES(38)
	INSERT INTO @ReportOrderStatus
	VALUES(39)

	RETURN
END

